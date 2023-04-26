using Backender.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeGenerator.Patterns
{
    public static class ConfigHandler
    {
        public static IEnumerable<RelationShip> GetRealationShipsByEntity(this IEnumerable<RelationShip> realationShips,Entity entity)
        {
            return realationShips.Where(p=>p.Entity1==entity.EntityName || p.Entity2 == entity.EntityName);
        }
        public static IEnumerable<RelationShip> GetRealationShipsByEntity(this IEnumerable<RelationShip> realationShips, string entityName)
        {
            return realationShips.Where(p => p.Entity1 == entityName || p.Entity2 == entityName);
        }
    }
}
