using Backender.CodeEditor.CSharp;
using Backender.CodeEditor.CSharp.Objects;
using Backender.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeGenerator.Patterns.Repo
{
	public static class DtoHandler
	{
		public static Class DtoGenerate(this Entity entity, ref Project proj, List<string> Options = null)
		{
			var AppendNameSpace = "Dtos";

			if (!string.IsNullOrEmpty(entity.EntityCategory))
			{
				AppendNameSpace += "." + entity.EntityCategory;
			}

			var options = new List<string>();
			options.Add("DtoClass");
			if (Options != null)
			{
				options.AddRange(Options);
			}
			var entityDto = proj.AddClass(entity.EntityName + "Dto", baseClassName: "BaseDto", Options: options, AppendNameSpace: AppendNameSpace);
			foreach (var Col in entity.Cols)
			{
				entityDto.AddProperty(Col.ColType, Col.ColName, AccessModifier.Public);
			}
			entityDto.UsingNameSpaces.Add(proj.DefaultNameSpace+".Enums");
			//Add relations
			return entityDto;
		}
		public static void AddDtorelations(this Project proj, ref List<RelationShip> relationShips)
		{
			var options = new List<string>();
			foreach (var relation in relationShips)
			{
				var class1 = proj.GetClassByName(relation.Entity1 + "Dto");
				var class2 = proj.GetClassByName(relation.Entity2 + "Dto");
				if (class1 == null || class2 == null) continue;
				switch (relation.RelationShipType)
				{
					case "M2M":
						break;
					case "O2M":
						class2.AddProperty(relation.Entity1 + "Dto", relation.Entity1 + "Dto", AccessModifier.Public);
						break;
					case "O2O":
						class2.AddProperty(relation.Entity1 + "Dto", relation.Entity1 + "Dto", AccessModifier.Public);
						break;
					default:
						break;
				}
			}
		}
		public static List<Class> FactoriesGenerate(this List<Entity> entities, ref Project proj, Project coreProj)
		{
			//entities = entities.OrderBy(p => p.EntityCategory).ToList();
			var FactoryClasses = new List<Class>();
			foreach (var entity in entities)
			{
				if (entity.EntityCategory == null)
				{
					entity.EntityCategory = string.Empty;
				}
				var entityFactoryClassName = entity.EntityCategory + "DtosFactory";
				var entityFactory = FactoryClasses.FirstOrDefault(p => p.Name == entityFactoryClassName);

				if (!FactoryClasses.Any(p => p.Name == entityFactoryClassName))
				{
					entityFactory = proj.AddClass(entityFactoryClassName, AppendNameSpace: entity.EntityCategory);
					entityFactory.UsingNameSpaces.Add(proj.SolutionName + ".Core.Dtos");
					entityFactory.UsingNameSpaces.Add(proj.SolutionName + ".Core.Domains");
					if (!string.IsNullOrEmpty(entity.EntityCategory))
					{
						entityFactory.UsingNameSpaces.Add(proj.SolutionName + ".Core.Dtos." + entity.EntityCategory);
						entityFactory.UsingNameSpaces.Add(proj.SolutionName + ".Core.Domains." + entity.EntityCategory);
					}
				}
				else
				{
					FactoryClasses.Remove(entityFactory);
				}
				AddPrepareMethod(entityFactory, entity, coreProj, entities);
				AddPrepareMethodOverLoad(entityFactory, entity, coreProj);
				FactoryClasses.Add(entityFactory);
			}
			foreach (var FactoryClass in FactoryClasses)
			{
				AutoImplementFields(FactoryClass);
			}
			foreach (var FactoryClass in FactoryClasses)
			{
				proj.AddClass(FactoryClass);
			}

			return FactoryClasses;
		}
		private static void AddPrepareMethod(Class entityFactory, Entity entity, Project coreProj, List<Entity> entities)
		{
			var Parameter = new MethodParameter()
			{
				DataType = entity.EntityName,
				Name = entity.EntityName.ToLower()
			};
			var DtoClass = coreProj.GetClassByName(entity.EntityName + "Dto");

			var DtoClassValues = new List<string>();
			var DtoDtosProperties = new List<Property>();
			foreach (var dtoProperty in DtoClass.InnerItems.OfType<Property>())
			{
				if (coreProj.IsClassExist(dtoProperty.DataType)) {
					DtoDtosProperties.Add(dtoProperty);
					continue; 
				}
				DtoClassValues.Add($"{dtoProperty.Name} = {Parameter.Name}.{dtoProperty.Name}");
			}
			var PrepareCode = $"var {entity.EntityName.ToLower()}Dto = new {DtoClass.Name}()\n" +
			"{\n" + string.Join(",\n", DtoClassValues) + $"\n}};\n";
			foreach (var DtoDtosProperty in DtoDtosProperties)
			{
				var entityName = DtoDtosProperty.Name.Substring(0, DtoDtosProperty.Name.Length-3);
				var dtoEntity = entities.FirstOrDefault(p => p.EntityName == entityName);
				var idParameter = "";
				if (entityName == Parameter.DataType)
				{
					idParameter = $"{Parameter.Name}.Id";
				}
				else
				{
					idParameter = $"{Parameter.Name}.{entityName}Id";
				}
				PrepareCode += $"\n{entity.EntityName.ToLower()}Dto.{entityName}Dto = ";
				if (dtoEntity.EntityCategory!=entity.EntityCategory)
				{
					var usageFactory = entityFactory.ImplementFactory(dtoEntity.EntityCategory);
					entityFactory.UsingNameSpaces.Add(coreProj.SolutionName + ".Services" + (string.IsNullOrEmpty(dtoEntity.EntityCategory) ? "" : $".{dtoEntity.EntityCategory}"));
					PrepareCode += $"{usageFactory.Name}.";
				}
				var usageService= entityFactory.ImplementService(entityName);
				PrepareCode += $"Prepare{entityName}Dto(_{entityName.ToLower()}Service.Get{entityName}ById({idParameter}));";
			}
			PrepareCode += $"\nreturn {entity.EntityName.ToLower()}Dto;";
			entityFactory.AddMethod(entity.EntityName + "Dto",
				$"Prepare{entity.EntityName}Dto",
				PrepareCode,
				Parameter);
		}

		private static void AddPrepareMethodOverLoad(Class entityFactory, Entity entity, Project coreProj)
		{
			var Parameter = new MethodParameter()
			{
				DataType = $"List<{entity.EntityName}>",
				Name = entity.EntityName.ToLower().ToPlural()
			};

			var PrepareCode = $"var {entity.EntityName.ToLower()}Dtos = new List<{entity.EntityName}Dto>();\n" +
				$"foreach (var {entity.EntityName.ToLower()} in {Parameter.Name})\n" +
				$"{{\n" +
				$"{entity.EntityName.ToLower()}Dtos.Add(Prepare{entity.EntityName}Dto({entity.EntityName.ToLower()}));\n" +
				$"}}\n" +
				$"return {entity.EntityName.ToLower()}Dtos;";
			entityFactory.AddMethod($"List<{entity.EntityName}Dto>",
				$"Prepare{entity.EntityName}Dto",
				PrepareCode,
				Parameter);
		}
		public static Class FactoriyGenerate(this Entity entity, ref Project proj)
		{
			throw new NotImplementedException();
		}
		private static Class AutoImplementFields(this Class entityFactory)
		{
			var parameters = new List<MethodParameter>();
			var innerCode = "";
			var fieldsObject = entityFactory.InnerItems.OfType<Field>().Where(p => p.AllowAutoImplement).ToList();
			foreach (var fieldObject in fieldsObject)
			{
				var field = fieldObject;
				var parameter = new MethodParameter()
				{
					DataType = field.DataType,
					Name = field.Name.Replace("_", "").FirstCharToUpper(),
				};

				innerCode += $"{field.Name} = {parameter.Name};\n";
				parameters.Add(parameter);
			}
			entityFactory.AddConstructor(innerCode, parameters);
			return entityFactory;
		}

		private static Field ImplementService(this Class entityFactory, string entityName)
		{
			var serviceField = entityFactory.AddField(entityName + "Service", $"_{entityName.ToLower()}Service");
			return serviceField;
		}
		private static Field ImplementFactory(this Class entityFactory, string entityCategory)
		{
			if(!entityFactory.InnerItems.OfType<Field>().Any(p=>p.Name  == $"{entityCategory}DtosFactory")){
				var factoryField = entityFactory.AddField(entityCategory + "DtosFactory", $"_{(!string.IsNullOrEmpty(entityCategory) ? entityCategory.ToLower()+ "Dtos" : "dtos")}Factory");
				return factoryField;
			}
			else
			{
				return entityFactory.InnerItems.OfType<Field>().FirstOrDefault();
			}
			
		}
	}
}
