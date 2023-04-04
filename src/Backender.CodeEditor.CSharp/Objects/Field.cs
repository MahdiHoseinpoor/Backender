using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeEditor.CSharp.Objects
{
    public class Field:InnerCsFileItem
    {
        
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
        public bool AllowAutoImplement { get; set; }
    }
}
