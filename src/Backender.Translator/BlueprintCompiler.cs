using Backender.Translator.Handlers;
using Backender.Translator.Handlers.ValidationControls;
using Backender.Translator.Handlers;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Backender.Translator
{
	public static class BlueprintCompiler
	{

        private static HashSet<string> visitedFiles = new HashSet<string>();
        public static List<Message> Messages { get; private set; } = new List<Message>();
        public static void Configure()
        {
            visitedFiles = new HashSet<string>();
        }
        public static void Validate(this Blueprint Blueprint)
		{
			SolutionValidation(Blueprint);
			EntityValidation(Blueprint);
			EnumValidation(Blueprint);
			RelationShipsValidation(Blueprint);
			OptionsValidation(Blueprint);
		}

        //public static XmlDocument Compile(string BlueprintPath)
        //{


        //    visitedFiles.Add(BlueprintPath);


        //    XmlDocument xmlDocument = XmlDeserializer.GetXmlDocument(BlueprintPath);
        //    XmlNodeList referenceNodes = xmlDocument.SelectNodes("//Reference");
        //    foreach (XmlNode referenceNode in referenceNodes)
        //    {
        //        string referenceType = referenceNode.Attributes["type"].Value;
        //        switch (referenceType.ToLower())
        //        {
        //            case "partial/blueprint":
        //                string referencedFilePath = referenceNode.Attributes["href"].Value;

        //                Uri uriResult;
        //                bool IsUrl = Uri.TryCreate(referencedFilePath, UriKind.Absolute, out uriResult)
        //                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        //                bool IsFileExist = false;
        //                if (!Path.IsPathRooted(referencedFilePath) && !IsUrl)
        //                {
        //                    referencedFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(BlueprintPath), referencedFilePath));
        //                }
        //                if (visitedFiles.Contains(referencedFilePath))
        //                {
        //                    Messages.Add(new Message("BA008", MessageType.Error, $"the referenced Blueprint can't reference to this file."));
        //                    continue;
        //                }
        //                if (IsUrl)
        //                {
        //                    IsFileExist = WebHelper.Exists(uriResult);
        //                }
        //                else
        //                {
        //                    IsFileExist = File.Exists(referencedFilePath);
        //                }
        //                if (IsFileExist)
        //                {
        //                    var refXmlDocument = Compile(referencedFilePath);
        //                    XmlNodeList entityNodes = refXmlDocument.SelectNodes("//Domains/Entity");
        //                    XmlNodeList enumNodes = refXmlDocument.SelectNodes("//Domains/Enums");
        //                    XmlNodeList globalOptionNodes = refXmlDocument.SelectNodes("//Domains/GlobalOption");

        //                    foreach (XmlNode entityNode in entityNodes)
        //                    {
        //                        // Create a new Entity tag
        //                        XmlNode newEntityNode = xmlDocument.CreateElement("Entity");

        //                        // Copy attributes from the existing Entity node to the new node
        //                        foreach (XmlAttribute attribute in entityNode.Attributes)
        //                        {
        //                            XmlNode newAttribute = xmlDocument.CreateAttribute(attribute.Name);
        //                            newAttribute.Value = attribute.Value;
        //                            newEntityNode.Attributes.Append((XmlAttribute)newAttribute);
        //                        }

        //                        // Add the new Entity tag to the main file
        //                        xmlDocument.DocumentElement.AppendChild(newEntityNode);
        //                    }
        //                    XmlNode domainsNode = xmlDocument.SelectSingleNode("//Domains");

        //                    foreach (XmlNode enumNode in enumNodes)
        //                    {
        //                        // Create a new Enums tag
        //                        XmlNode newEnumsNode = xmlDocument.CreateElement("Enums");

        //                        // Copy attributes from the existing Enums node to the new node
        //                        foreach (XmlAttribute attribute in enumNode.Attributes)
        //                        {
        //                            XmlNode newAttribute = xmlDocument.CreateAttribute(attribute.Name);
        //                            newAttribute.Value = attribute.Value;
        //                            newEnumsNode.Attributes.Append((XmlAttribute)newAttribute);
        //                        }

        //                        // Add the new Enums tag to the Domains node
        //                        domainsNode.AppendChild(newEnumsNode);
        //                    }

        //                    foreach (XmlNode globalOptionNode in globalOptionNodes)
        //                    {
        //                        // Create a new GlobalOption tag
        //                        XmlNode newGlobalOptionNode = xmlDocument.CreateElement("GlobalOption");

        //                        // Copy attributes from the existing GlobalOption node to the new node
        //                        foreach (XmlAttribute attribute in globalOptionNode.Attributes)
        //                        {
        //                            XmlNode newAttribute = xmlDocument.CreateAttribute(attribute.Name);
        //                            newAttribute.Value = attribute.Value;
        //                            newGlobalOptionNode.Attributes.Append((XmlAttribute)newAttribute);
        //                        }

        //                        // Add the new GlobalOption tag to the Domains node
        //                        domainsNode.AppendChild(newGlobalOptionNode);
        //                    }
        //                }
        //                break;
        //            case "template/Blueprint":
        //                break;
        //            default:
        //                break;
        //        }
        //    }




        //    xmlDocument.Save(BlueprintPath);
        //    return xmlDocument;
        //}


        public static void Compile(this Blueprint Blueprint, string BlueprintLocation)
        {
            visitedFiles.Add(BlueprintLocation);
            foreach (var reference in Blueprint.References)
            {
                Uri uriResult;
                bool IsUrl = Uri.TryCreate(reference.Href, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                bool IsFileExist = false;
                if (!Path.IsPathRooted(reference.Href) && !IsUrl)
                {
                    reference.Href = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(BlueprintLocation), reference.Href));
                }
                if (visitedFiles.Contains(reference.Href))
                {
                    Messages.Add(new Message("BA008", MessageType.Error, $"the referenced Blueprint can't reference to this file."));
                    continue;
                }
                if (IsUrl)
                {
                    IsFileExist = WebHelper.Exists(uriResult);
                }
                else
                {
                    IsFileExist = File.Exists(reference.Href);
                }
                if (IsFileExist)
                {
                    switch (reference.Type.ToLower())
                    {
                        case "partial/blueprint":
                            var additionBlueprint = BlueprintHandler.BlueprintBuilder(reference.Href);
                            additionBlueprint.Compile(reference.Href);
                            if (!Messages.Any(p => p.MessageType == MessageType.Error))
                            {
                                Blueprint.CombineEntities(additionBlueprint);
                                Blueprint.CombineRelationShips(additionBlueprint);
                                Blueprint.CombineEnums(additionBlueprint);
                                Blueprint.CombineGlobalOptions(additionBlueprint);
                            }
                            break;
                        case "template/blueprint":
                            var ComponentBlueprint = BlueprintHandler.BlueprintBuilder(reference.Href);
                            ComponentBlueprint.Compile(reference.Href);
                            if (!Messages.Any(p => p.MessageType == MessageType.Error))
                            {
                                Blueprint.AddEntityComponents(ComponentBlueprint);
                                Blueprint.AddEnumComponents(ComponentBlueprint);
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Messages.Add(new Message("BA007", MessageType.Error, $"the reference is not found in this path: '{reference.Href}' "));
                    continue;
                }

            }
        }

        private static void CombineEntities(this Blueprint Blueprint, Blueprint additionBlueprint)
        {
            foreach (var additionEntity in additionBlueprint.Domains.Entities)
            {
                if (!Blueprint.Domains.Entities.Any(p => p.EntityName == additionEntity.EntityName && p.EntityCategory == additionEntity.EntityCategory))
                {
                    Blueprint.Domains.Entities.Add(additionEntity);
                }
                else
                {
                    var entity = Blueprint.Domains.Entities.FirstOrDefault(p => p.EntityName == additionEntity.EntityName);
                    var entityIndex = Blueprint.Domains.Entities.FindIndex(p => p == entity);
                    foreach (var additionEntityCol in additionEntity.Cols)
                    {
                        if (!entity.Cols.Any(p => p.ColName == additionEntityCol.ColName))
                        {
                            entity.Cols.Add(additionEntityCol);
                        }
                    }
                    Blueprint.Domains.Entities[entityIndex] = entity;
                }
            }
        }
        private static void CombineEnums(this Blueprint Blueprint, Blueprint additionBlueprint)
        {
            foreach (var additionEnum in additionBlueprint.Domains.Enums)
            {
                if (!Blueprint.Domains.Enums.Any(p => p.EnumName == additionEnum.EnumName))
                {
                    Blueprint.Domains.Enums.Add(additionEnum);
                }
            }
        }
        private static void CombineRelationShips(this Blueprint Blueprint, Blueprint additionBlueprint)
        {
            Blueprint.CombineRelationShips(additionBlueprint.Domains.RelationShips);
        }
        private static void CombineRelationShips(this Blueprint Blueprint, List<RelationShip> additionRelationShips)
        {
            foreach (var additionRelationShip in additionRelationShips)
            {
                Blueprint.CombineRelationShip(additionRelationShip);
            }
        }
        private static void CombineRelationShip(this Blueprint Blueprint, RelationShip additionRelationShip)
        {
                if (!Blueprint.Domains.RelationShips.Any(p => (p.Entity1 == additionRelationShip.Entity1 && p.Entity2 == additionRelationShip.Entity2) || (p.Entity2 == additionRelationShip.Entity1 && p.Entity1 == additionRelationShip.Entity2)))
                {
                    Blueprint.Domains.RelationShips.Add(additionRelationShip);
                }
        }
        private static void CombineGlobalOptions(this Blueprint Blueprint, Blueprint additionBlueprint)
        {
            foreach (var additionGlobalOptions in additionBlueprint.Domains.GlobalOptions)
            {
                if (!Blueprint.Domains.GlobalOptions.Any(p => p.EntityCols == additionGlobalOptions.EntityCols))
                {
                    Blueprint.Domains.GlobalOptions.Add(additionGlobalOptions);
				}
            }
        }


        private static void AddEntityComponents(this Blueprint Blueprint, Blueprint ComponentBlueprint)
        {
			var ChangedEntities = new Dictionary<int, Entity>();
            foreach (var BlueprintEntity in Blueprint.Domains.Entities.Where(p=>p.EntityName.StartsWith('#')))
            {
                string ComponentQuery = BlueprintEntity.EntityName.Replace("#","");
                string[] ComponentQueryParts = ComponentQuery.Split(".", StringSplitOptions.RemoveEmptyEntries);
                string ComponentEntityName = string.Empty;
                if (ComponentQueryParts.Count() > 1)
                {
                    if (ComponentBlueprint.Solution.SolutionName != ComponentQueryParts[0])
                    {
                        continue;
                    }
                    ComponentEntityName = ComponentQueryParts[1];

                }
                else
                {
                    ComponentEntityName = ComponentQueryParts[0];
                }
                if (!ComponentBlueprint.Domains.Entities.Any(p=>p.EntityName == ComponentEntityName))
				{
                    Messages.Add(new Message("BA009", MessageType.Error, $"the #{ComponentEntityName} is not found in your Components"));
                    continue;
                }
                BlueprintEntity.EntityName = ComponentEntityName;
                var ComponentBlueprintEntity = ComponentBlueprint.Domains.Entities.FirstOrDefault(p => p.EntityName == ComponentEntityName);
                var BlueprintEntityIndex = Blueprint.Domains.Entities.FindIndex(p => p == BlueprintEntity);
				ChangedEntities.Add(BlueprintEntityIndex, ComponentBlueprintEntity);

            }
			foreach (var Relation in ComponentBlueprint.Domains.RelationShips)
			{
				if (ChangedEntities.Any(p=>p.Value.EntityName == Relation.Entity1) && ChangedEntities.Any(p => p.Value.EntityName == Relation.Entity2))
				{
                    Blueprint.CombineRelationShip(Relation);
                }
			}
			foreach (var ChangedEntity in ChangedEntities)
            {
                Blueprint.Domains.Entities[ChangedEntity.Key] = ChangedEntity.Value;
            }
        }
        private static void AddEnumComponents(this Blueprint Blueprint, Blueprint ComponentBlueprint)
        {
            var BlueprintEnums = new Dictionary<int, Enum_>();
            foreach (var BlueprintEnum in Blueprint.Domains.Enums.Where(p => p.EnumName.StartsWith('#')))
            {
                BlueprintEnum.EnumName = BlueprintEnum.EnumName.Replace("#", "");

                if (!ComponentBlueprint.Domains.Entities.Any(p => p.EntityName == BlueprintEnum.EnumName))
                {
                    Messages.Add(new Message("BA009", MessageType.Error, $"the #{BlueprintEnum.EnumName} is not found in your Components"));
                    continue;
                }
                var ComponentBlueprintEnum = ComponentBlueprint.Domains.Enums.FirstOrDefault(p => p.EnumName == BlueprintEnum.EnumName);
                var BlueprintEnumIndex = Blueprint.Domains.Enums.FindIndex(p => p == BlueprintEnum);
                BlueprintEnums.Add(BlueprintEnumIndex, ComponentBlueprintEnum);

            }
            foreach (var ChangedEntity in BlueprintEnums)
            {
                Blueprint.Domains.Enums[ChangedEntity.Key] = ChangedEntity.Value;
            }
        }


        private static void RelationShipsValidation(Blueprint Blueprint)
		{
			foreach (var RelationShip in Blueprint.Domains.RelationShips)
			{	
				if (string.IsNullOrEmpty(RelationShip.RelationShipType))
				{
					Messages.Add(new Message("BA001", MessageType.Error, $"RelationShipType in RelationShips can't be null"));
				}
				if (string.IsNullOrEmpty(RelationShip.Entity1))
				{
					Messages.Add(new Message("BA001", MessageType.Error, $"Entity1 in RelationShips can't be null"));
				}
				if (string.IsNullOrEmpty(RelationShip.Entity2))
				{
					Messages.Add(new Message("BA001", MessageType.Error, $"Entity2 in RelationShips can't be null"));
				}
				if (RelationShip.RelationShipType != "O2M" && RelationShip.RelationShipType != "M2M" && RelationShip.RelationShipType != "O2O")
				{
					Messages.Add(new Message("BA005", MessageType.Error, $"'{RelationShip.RelationShipType}' is not a RelationType"));
				}
				if (!Blueprint.Domains.Entities.Any(p => p.EntityName == RelationShip.Entity1) && !string.IsNullOrEmpty(RelationShip.Entity1))
				{
					Messages.Add(new Message("BA006", MessageType.Error, $"'{RelationShip.Entity1}' which you defined in the Entity1 of the relation, does not exist"));
				}
				if (!Blueprint.Domains.Entities.Any(p => p.EntityName == RelationShip.Entity2) && !string.IsNullOrEmpty(RelationShip.Entity2))
				{
					Messages.Add(new Message("BA006", MessageType.Error, $"'{RelationShip.Entity2}' which you defined in the Entity2 of the relation, does not exist"));
				}
			}
		}
		private static void EntityValidation(Blueprint Blueprint)
		{
			using (CSharpCodeProvider codeProvider = new CSharpCodeProvider())
			{
				foreach (var Entity in Blueprint.Domains.Entities)
				{
					if (string.IsNullOrEmpty(Entity.EntityName))
					{
						Messages.Add(new Message("BA001", MessageType.Error, $"EntityName in Entities can't be null"));
					}
					if (!codeProvider.IsValidIdentifier(Entity.EntityName))
					{
						Messages.Add(new Message("BA002", MessageType.Error, $"EntityName:{Entity.EntityName} is an identifier"));
					}
					foreach (var item in Entity.Cols.GroupBy(p => p.ColName).Where(p => p.Count() > 1))
					{
						Messages.Add(new Message("BA003", MessageType.Error, $"A ColName in '{Entity.EntityName}' entity named '{item.FirstOrDefault().ColName}' is already defined in this scope"));
					}
					foreach (var EntityCols in Entity.Cols)
					{
						if (string.IsNullOrEmpty(EntityCols.ColName))
						{
							Messages.Add(new Message("BA001", MessageType.Error, $"ColName in Cols can't be null"));
						}
						if (string.IsNullOrEmpty(EntityCols.ColType))
						{
							Messages.Add(new Message("BA001", MessageType.Error, $"ColType in Cols can't be null"));
						}
						if (!codeProvider.IsValidIdentifier(EntityCols.ColName))
						{
							Messages.Add(new Message("BA002", MessageType.Error, $"ColName:{EntityCols.ColName} in {Entity.EntityName} entity is an identifier"));
						}
					}
				}
			}
			foreach (var item in Blueprint.Domains.Entities.GroupBy(p => p.EntityName).Where(p => p.Count() > 1))
			{
                foreach (var item2 in item.GroupBy(p=>p.EntityCategory).Where(p => p.Count() > 1))
                {
                    Messages.Add(new Message("BA003", MessageType.Error, $"A EntityName named '{item.FirstOrDefault().EntityName}' is already defined in this scope"));
                }
            }
		}
		private static void SolutionValidation(Blueprint Blueprint)
		{
			if (string.IsNullOrEmpty(Blueprint.Solution.SolutionName))
			{
				Messages.Add(new Message("BA001", MessageType.Error, $"SolutionName Can't Be null"));
			}
			if (string.IsNullOrEmpty(Blueprint.SavePath))
			{
				Messages.Add(new Message("BA901", MessageType.Warning, $"The SavePath is null, your Solution will Create in 'Documents/Backender 2023/Sources'"));
			}
		}
		private static void EnumValidation(Blueprint Blueprint)
		{
			using (CSharpCodeProvider codeProvider = new CSharpCodeProvider())
			{
				foreach (var Enum in Blueprint.Domains.Enums)
				{
					if (string.IsNullOrEmpty(Enum.EnumName))
					{
						Messages.Add(new Message("BA001", MessageType.Error, $"EnumName in Enums can't be null"));
					}
					foreach (var item in Enum.EnumValues.GroupBy(p => p.Name).Where(p => p.Count() > 1))
					{
						Messages.Add(new Message("BA003", MessageType.Error, $"A EnumName in '{Enum.EnumName}' enum named '{item.FirstOrDefault().Name}' is already defined in this scope"));
					}
					foreach (var item in Enum.EnumValues.GroupBy(p => p.Value).Where(p => p.Count() > 1))
					{
						Messages.Add(new Message("BA004", MessageType.Error, $"'{item.FirstOrDefault().Value}' in '{Enum.EnumName}' enum used for '{item.Count()}' items"));
					}
					if (!codeProvider.IsValidIdentifier(Enum.EnumName))
					{
						Messages.Add(new Message("BA002", MessageType.Error, $"EnumName:{Enum.EnumName} is an identifier"));
					}
				}
			}
		}
        private static void OptionsValidation(Blueprint Blueprint)
		{
            foreach (var entity in Blueprint.Domains.Entities)
            {
                foreach (var col in entity.Cols)
                {
                    var options = col.Options.ParseOptions();
                    if (options.GroupBy(p=>p.Name).Count() > 1)
                    {
                        Messages.Add(new Message("BA301", MessageType.Warning, $"the options of {col.ColName} col in {entity.EntityName} has duplicate options"));
                    }
                    if (options.Any(p=>p.Name == "length") && options.Any(p => p.Name == "maxlength" || p.Name == "minlength"))
                    {
                        Messages.Add(new Message("BA302", MessageType.Error, $"{col.ColName} col in {entity.EntityName}: you cannot have Length and MaxLength, MinLength together in one Options"));
                    }
                }
            }
		}
	}
}