using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeEditor.CSharp.Objects
{
    public class Attribute:CodeObject
    {
		public string Value { get; set; }
		public List<AttributeParameter> Parameters { get; set; } = new List<AttributeParameter>();
    }
}
