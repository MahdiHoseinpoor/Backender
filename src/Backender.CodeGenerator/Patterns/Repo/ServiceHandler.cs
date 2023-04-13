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
	/// <summary>
	/// This class Create Service class and interfaces.
	/// </summary>
	public static class ServiceHandler
    {
		/// <summary>
		/// Create Service class and interface and add properties and methods based on entity and Realation sections of config informations.
		/// </summary>
		/// <param name="entity">The entity section of config informations.</param>
		/// <param name="proj">The project in which the entity class is built.</param>
		/// <param name="realationShips">The Realation section of config informations with MiddleClasses Realations.</param>
		/// <returns>
		/// The class that was created.
		/// </returns>
		public static Class ServiceGenerate(this Entity entity, ref Project proj, IEnumerable<RealationShip> realationShips)
        {
			var AppendNameSpace = "";

			if (!string.IsNullOrEmpty(entity.EntityCategory))
			{
				AppendNameSpace = entity.EntityCategory;
			}
			var entityService = proj.AddClass(entity.EntityName + "Service",AppendNameSpace:AppendNameSpace);
            var entityRepo = entityService.ImplementRepo(entity.EntityName);
            entityService.AddRepoWithCommonMethods(entity.EntityName);
            var realations = realationShips.GetRealationShipsByEntity(entity);
            foreach (var EntityRealation in realations)
            {
                entityService.AddRepoBasedOnRealationtype(EntityRealation, entity.EntityName);                
            }
			foreach (var col in entity.Cols.Where(p => !string.IsNullOrEmpty(p.Options)))
			{
				entityService.AddRepoByOptions(col, entity.EntityName);
			}
			entityService.AutoImplementFields();
            foreach (var item in proj.ProjectReference)
            {
                entityService.UsingNameSpaces.Add(item.DefaultNameSpace);
            }
            entityService.UsingNameSpaces.Add(proj.SolutionName + ".Core.Domains");
			if (!string.IsNullOrEmpty(entity.EntityCategory))
			{
				entityService.UsingNameSpaces.Add(proj.SolutionName + ".Core.Domains."+ AppendNameSpace);
			}
			entityService.UsingNameSpaces.Add("Microsoft.EntityFrameworkCore");

            proj.AddInterface(entityService.ToInterface());
            return entityService;
        }

		/// <summary>
		/// Automaticly implemented the requird classes in Service class in ctor
		/// </summary>
		/// <param name="entityService">the service class to implemented requirds</param>

		/// <returns>
		/// The Service class with implemented requirds
		/// </returns>
		private static Class AutoImplementFields(this Class entityService)
        {
            var parameters = new List<MethodParameter>();
            var innerCode = "";
            var fieldsObject = entityService.InnerItems.OfType<Field>().Where(p=>p.AllowAutoImplement);
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
            entityService.AddConstructor(innerCode, parameters);
            return entityService;
        }

		/// <summary>
		/// Add some methods based on Realationtype to service
		/// </summary>
		/// <param name="entityService">the service class</param>
		/// <param name="EntityName">the entity class name, witch service class is for it</param>
		/// <param name="Realation">The Realations.</param>
		private static void AddRepoBasedOnRealationtype(this Class entityService, RealationShip Realation, string EntityName)
        {
            switch (Realation.RealationShipType)
            {
                case "O2M":
                    AddRepoO2M(entityService, Realation, EntityName);
                    break;
                case "M2M":

                    entityService.ImplementService(Realation.Entity1 + Realation.Entity2);
                    AddRepoM2M(entityService, Realation, EntityName);
                    break;
                case "O2O":
                    AddRepoO2O(entityService, Realation, EntityName);
                    break;
                default:
                    break;
            }

        }


		/// <summary>
		/// Add some methods based on O2O Realationtype to service
		/// </summary>
		/// <param name="entityService">the service class</param>
		/// <param name="EntityName">the entity class name, witch service class is for it</param>
		/// <param name="Realation">The Realations.</param>
		private static void AddRepoO2O(Class entityService, RealationShip Realation, string EntityName)
        {
            var entityRealationName = Realation.Entity1;

            if (Realation.Entity1 == EntityName)
            {
                entityRealationName = Realation.Entity2;
            }

            var idParameter = new MethodParameter()
            {
                DataType = "string",
                Name = entityRealationName.ToLower() + "Id"
            };
            var GetEntityByRelatedEntityIdCode = $"return {entityService.GetEntityFieldByEntityName(EntityName)}.GetAll(where: p => p.{entityRealationName}Id == {idParameter.Name}).FirstOrDefault();";

            entityService.AddMethod(EntityName,
                $"Get{EntityName}By{entityRealationName}",
                GetEntityByRelatedEntityIdCode,
                idParameter);

        }
		/// <summary>
		/// Add some methods based on M2M Realationtype to service
		/// </summary>
		/// <param name="entityService">the service class</param>
		/// <param name="EntityName">the entity class name, witch service class is for it</param>
		/// <param name="Realation">The Realations.</param>
		private static void AddRepoM2M(Class entityService, RealationShip Realation, string EntityName)
        {
            var entityRealationName = Realation.Entity1;

            if (Realation.Entity1 == EntityName)
            {
                entityRealationName = Realation.Entity2;
            }
            var idParameter = new MethodParameter()
            {
                DataType = "string",
                Name = entityRealationName.ToLower() + "Id"
            };
            var MiddleEntityName = Realation.Entity1 + Realation.Entity2;
            var GetEntitiesByRelatedEntityIdCode = $@"
var {MiddleEntityName.ToPlural()} = {entityService.GetEntityFieldByEntityName(MiddleEntityName)}.Get{MiddleEntityName.ToPlural()}By{entityRealationName}({idParameter.Name});
            var {EntityName.ToPlural()} = new List<{EntityName}>();
            foreach (var {MiddleEntityName} in {MiddleEntityName.ToPlural()})
            {{
                var {EntityName.ToLower()} = Get{EntityName}ById({idParameter.Name});
                {EntityName.ToPlural()}.Add({EntityName.ToLower()});
            }}
            return {EntityName.ToPlural()};
";
            entityService.AddMethod($"IList<{EntityName}>",
                $"Get{EntityName.ToPlural()}By{entityRealationName}",
                GetEntitiesByRelatedEntityIdCode,
                idParameter);
        }
		/// <summary>
		/// Add some methods based on O2M Realationtype to service
		/// </summary>
		/// <param name="entityService">the service class</param>
		/// <param name="EntityName">the entity class name, witch service class is for it</param>
		/// <param name="Realation">The Realations.</param>
		private static void AddRepoO2M(Class entityService, RealationShip Realation, string EntityName)
        {
            if (EntityName != Realation.Entity1)
            {
                var idParameter = new MethodParameter()
                {
                    DataType = "string",
                    Name = Realation.Entity1.ToLower() + "Id"
                };
                var GetEntityByRelatedEntityIdCodeO2M = $"return {entityService.GetEntityFieldByEntityName(EntityName)}.GetAll(where: p => p.{Realation.Entity1}Id == {idParameter.Name}).ToList();";

                entityService.AddMethod("List<" + EntityName + ">",
                    $"Get{EntityName.ToPlural()}By{Realation.Entity1}",
                    GetEntityByRelatedEntityIdCodeO2M,
                    idParameter);
            }
        }

		/// <summary>
		/// Adding common methods to the service, methods that all services should have
		/// </summary>
		/// <param name="entityService">the entity class to implemented requirds</param>
		/// <param name="EntityName">the entity class name, witch service class is for it</param>
		private static void AddRepoWithCommonMethods(this Class entityService, string EntityName)
        {

            var entityRepoName = entityService.GetEntityFieldByEntityName(EntityName);
            var GetAllCode = $"return {entityRepoName}.GetAll().ToList();";
            entityService.AddMethod($"IList<{EntityName}>", "GetAll" + EntityName.ToPlural(), GetAllCode);

            var GetByIdCode = $"return {entityRepoName}.GetById(id);";
            var idParameter = new MethodParameter()
            {
                DataType = "string",
                Name = "id"
            };
            var entityParameter = new MethodParameter()
            {
                DataType = EntityName,
                Name = EntityName.ToLower()
            };
            entityService.AddMethod(EntityName, $"Get{EntityName}ById", GetByIdCode, idParameter);


            var InsertCode = $" {entityRepoName}.Insert({entityParameter.Name});\nreturn {entityRepoName}.Save();";

            entityService.AddMethod("bool", $"Insert{EntityName}", InsertCode, entityParameter);

            var UpdateCode = $" {entityRepoName}.Update({entityParameter.Name});\nreturn {entityRepoName}.Save();";
            entityService.AddMethod("bool", $"Update{EntityName}", UpdateCode, entityParameter);

            var DeleteWithEntityCode = $" {entityRepoName}.Delete({entityParameter.Name});\nreturn {entityRepoName}.Save();";
            entityService.AddMethod("bool", $"Delete{EntityName}", DeleteWithEntityCode, entityParameter);

            var DeleteWithIdCode = $" {entityRepoName}.Delete({idParameter.Name});\nreturn {entityRepoName}.Save();";
            entityService.AddMethod("bool", $"Delete{EntityName}", DeleteWithIdCode, idParameter);

        }

		/// <summary>
		/// Adding methods based on Entity col options
		/// </summary>
		/// <param name="entityService">the service class</param>
		/// <param name="EntityName">the entity class name, witch service class is for it</param>
		/// <param name="col">the entity col</param>
		private static void AddRepoByOptions(this Class entityService, Col col, string EntityName)
        {
            foreach (var option in col.Options.Split(' '))
            {
                switch (option)
                {
                    case "-g":
                        var Parameter = new MethodParameter()
                        {
                            DataType = "string",
                            Name = col.ColName.ToLower()
                        };
                        var GetEntityByRelatedEntityIdCodeO2M = $"return {entityService.GetEntityFieldByEntityName(EntityName)}.GetAll(where: p => p.{col.ColName} == {Parameter.Name}).FirstOrDefault();";

                        entityService.AddMethod(EntityName,
                            $"Get{EntityName}By{col.ColName}",
                            GetEntityByRelatedEntityIdCodeO2M,
                            Parameter);
                        break;
                    default:
                        break;
                }
            }
        }

        private static Field ImplementRepo(this Class entityService, string EntityName)
        {
            var serviceField = entityService.AddField($"IRepo<{EntityName.FirstCharToUpper()}>", $"_{EntityName.ToLower()}Repo");
            return serviceField;
        }
        private static Field ImplementService(this Class entityService, string ServiceName)
        {
            var serviceField = entityService.AddField(ServiceName + "Service", $"_{ServiceName.ToLower()}Service");
            return serviceField;
        }
    }
}
