using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Models
{
    public class Table
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Category { get; set; }
        public string Options { get; set; }
        public List<Table> MiddleEntities { get; set; } = new List<Table>();
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Relation> Relations { get; set; } = new List<Relation>();
        public Dictionary<string, string> DataBag { get; set; } = new Dictionary<string, string>();
    }
}
