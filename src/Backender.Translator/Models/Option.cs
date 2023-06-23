using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Models
{
    public class Option
    {
        public string Name { get; set; }
        public List<string> Values { get; set; } = new List<string>();
        public int DisplayOrder { get; set; } = 1;
    }
}
