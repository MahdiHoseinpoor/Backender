using Backender.Translator.Handlers.ValidationControls;
using Backender.Translator.Handlers;
using Backender.Translator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Backender.Translator.Handlers
{

    public static class BlueprintHandler
    {
        private const string MiddleEntityOption = "MiddleEntity";
        private const string BaseEntityName = "BaseEntity";
        private const string Entity1Name = "Entity1";
        private const string Entity2Name = "Entity2";
        private const string BaseEntityOption = "BaseEntity";
        private const string IdColName = "Id";
        private const string IdColType = "string";
        private const string CreatedAtColName = "CreatedAt_";
        private const string ModifiedAtColName = "ModifiedAt_";
        private const string O2MRelationShipType = "O2M";
        private const string M2MRelationShipType = "M2M";
        public async static Task<Blueprint> BlueprintBuilderAsync(string FileName)
        {
            XmlDocument xmlDocument = await XmlDeserializer.GetXmlDocumentAsync(FileName);
            Blueprint blueprint = XmlDeserializer.ConvertXmlToBlueprint(xmlDocument);
            return blueprint;
        }
        public static Blueprint BlueprintBuilder(string FileName)
        {
            XmlDocument xmlDocument = XmlDeserializer.GetXmlDocument(FileName);
            Blueprint blueprint = XmlDeserializer.ConvertXmlToBlueprint(xmlDocument);
            return blueprint;
        }
        public static Blueprint Configuration(this Blueprint Blueprint)
        {
            OptionsHandler.Configure(Blueprint);
            ValidatorHandler.Configure(Blueprint);
            var newEntities = new List<Entity>();
            var newRelationships = new List<RelationShip>();
         
            foreach (var relationship in Blueprint.Domains.RelationShips.Where(p=>p.RelationShipType== M2MRelationShipType))
            {
                var entity1 = Blueprint.Domains.Entities.FirstOrDefault(p => p.EntityName == relationship.Entity1);
                var entity2 = Blueprint.Domains.Entities.FirstOrDefault(p => p.EntityName == relationship.Entity2);

                var middleEntity = CreateMiddleEntity(entity1, entity2);
                var entity1ToMiddleEntityRelation = CreateEntity1ToMiddleEntityRelation(entity1, middleEntity);
                var entity2ToMiddleEntityRelation = CreateEntity2ToMiddleEntityRelation(entity2, middleEntity);

                newEntities.Add(middleEntity);
                newRelationships.Add(entity1ToMiddleEntityRelation);
                newRelationships.Add(entity2ToMiddleEntityRelation);
            }
           
      
            Blueprint.Domains.RelationShips = Blueprint.Domains.RelationShips
                .Where(p => p.RelationShipType != M2MRelationShipType).ToList();

            if (!Blueprint.Domains.Entities.Any(p => p.EntityName == BaseEntityName))
            {
                var baseEntity = CreateBaseEntity();
                newEntities.Add(baseEntity);
            }

            Blueprint.Domains.RelationShips.AddRange(newRelationships);
            Blueprint.Domains.Entities.AddRange(newEntities);

            return Blueprint;
        }

        private static Entity CreateMiddleEntity(Entity entity1, Entity entity2)
        {
            var middleEntity = new Entity()
            {
                EntityName = entity1.EntityName + entity2.EntityName,
                EntityCategory = entity1.EntityCategory,
                Options = MiddleEntityOption
            };
            middleEntity.DataBag[Entity1Name] = entity1.EntityName;
            middleEntity.DataBag[Entity2Name] = entity2.EntityName;
            return middleEntity;
        }

        private static RelationShip CreateEntity1ToMiddleEntityRelation(Entity entity1, Entity middleEntity)
        {
            return new RelationShip()
            {
                Entity1 = entity1.EntityName,
                Entity2 = middleEntity.EntityName,
                RelationShipType = O2MRelationShipType
            };
        }

        private static RelationShip CreateEntity2ToMiddleEntityRelation(Entity entity2, Entity middleEntity)
        {
            return new RelationShip()
            {
                Entity1 = entity2.EntityName,
                Entity2 = middleEntity.EntityName,
                RelationShipType = O2MRelationShipType
            };
        }

        private static Entity CreateBaseEntity()
        {
            return new Entity()
            {
                EntityName = BaseEntityName,
                Options = BaseEntityOption,
                Cols = new List<Col>()
        {
            new Col()
            {
                ColName = IdColName,
                ColType = IdColType,
                Options = "key"
            },
            new Col()
            {
                ColName = CreatedAtColName,
                ColType = nameof(DateTime)
            },
            new Col()
            {
                ColName = ModifiedAtColName,
                ColType = nameof(DateTime)
            }
        }
            };
        }
    }
}