using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Models
{
    public class Constructor: InnerFileItem
    {
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}
