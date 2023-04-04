using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeEditor.CSharp.Objects
{
    public class Constructor:InnerCsFileItem
    {
        public IEnumerable<MethodParameter> Parameters { get; set; }
        public string InnerCode { get; set; }

    }
}
