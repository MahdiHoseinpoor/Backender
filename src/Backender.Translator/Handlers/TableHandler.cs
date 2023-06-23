using Backender.Translator;
using Backender.Translator.Handlers;
using Backender.Translator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Handlers
{
    public static class TableHandler
    {
        public static string GetTableIdByName(List<Table> tables, string Name)
        {
            string Id = tables.FirstOrDefault(p => p.Name == Name).Id;
            return Id;
        }
        public static List<Table> CreateTables(Blueprint Blueprint)
        {
            List<Table> tables = new List<Table>();
            List<Relation> Relations = new List<Relation>();
            foreach (var entity in Blueprint.Domains.Entities)
            {
                Table table = CreateTable(entity,Blueprint.Domains.Enums);
                
                tables.Add(table);
            }
            var middleEntityTables = tables.Where(p => p.Options.HasOption("MiddleEntity"));
            foreach (var table in tables)
            {
                foreach (var Relation in Blueprint.Domains.RelationShips.Where(p => p.Entity1 == table.Name || p.Entity2 == table.Name))
                {
                    var _Relation = Relations.FirstOrDefault(p => p.FromTableId == table.Id && p.ToTableId == table.Id);
                    if (_Relation == null)
                    {
                        string ToTableName = string.Empty;
                        RelationType RelationType = 0;
                        if (Relation.Entity1 == table.Name)
                        {
                            ToTableName = Relation.Entity2;
                            switch (Relation.RelationShipType)
                            {
                                case "O2M":
                                    RelationType = RelationType.ToMany;
                                    break;
                                case "O2O":
                                    RelationType = RelationType.ToOne;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if(Relation.Entity2 == table.Name)
                        {
                            ToTableName = Relation.Entity1;
                            switch (Relation.RelationShipType)
                            {
                                case "M2M":
                                    break;
                                case "O2M":
                                    RelationType = RelationType.ToOne;
                                    break;
                                case "O2O":
                                    RelationType = RelationType.ToOne;
                                    break;
                                default:
                                    break;
                            }
                        }
                        _Relation = new Relation() { FromTableId=table.Id,ToTableId = GetTableIdByName(tables, ToTableName),TableName=ToTableName, RelationType= RelationType};
                        Relations.Add(_Relation);
                    }
                    table.Relations.Add(_Relation);
                }
                foreach (var middleEntityTable in middleEntityTables.Where(p => p.DataBag["Entity1"] == table.Name))
                {
                    table.MiddleEntities.Add(middleEntityTable);
                }
            }

           
            return tables;
        }
        
        private static Table CreateTable(Entity entity,List<Enum_> enums)
        {
            var table = new Table { Name = entity.EntityName, Category = entity.EntityCategory ,Options = entity.Options,DataBag = entity.DataBag};
            foreach (var column in entity.Cols)
            {
                var _column = new Column()
                {
                    DataType = column.ColType,
                    Name = column.ColName,
                    Options = column.Options,
                    TableId = table.Id,
                    IsNullable = true
                };
                if (column.Options.HasOption("required"))
                {
                    _column.IsNullable = false;
                }
                _column.IsEnum = enums.Any(p=>p.EnumName==_column.DataType);
                table.Columns.Add(_column);
            }
            return table;
        }
    }
}
