using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeEditor.CSharp.Objects
{
    public class Method: InnerCsFileItem
    {
        public List<MethodParameter> Parameters { get; set; } = new List<MethodParameter>();
        public string DataType { get; set; }
        public string InnerCode { get; set; }
    }
}
