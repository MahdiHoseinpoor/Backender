using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeEditor.CSharp.Objects
{
    

    public class Property:InnerCsFileItem
    {
		public List<Attribute> Attributes { get; set; } = new List<Attribute>();
        public bool IsVirtual { get; set; }
        public string DataType { get; set; }
		public string GetInnerCode { get; set; } = string.Empty;  
        public string SetInnerCode { get; set; } = string.Empty;
	}
}
