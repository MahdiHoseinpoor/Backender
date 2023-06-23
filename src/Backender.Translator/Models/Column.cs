using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Models
{
    public class Column
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TableId { get; set; }
        public string Name { get; set; }        
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
        public bool IsKey { get; set; }
        public bool IsEnum { get; set; }
        public string Options { get; set; }
    }
}
