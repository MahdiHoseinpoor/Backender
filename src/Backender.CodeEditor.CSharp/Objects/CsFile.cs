using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeEditor.CSharp.Objects
{
    public class CsFile:CodeObject
    {
        public string NameSpace { get; set; }
        public string ProjectName { get; set; }

        public List<InnerCsFileItem> InnerItems { get; set; } = new List<InnerCsFileItem>();
        public List<string> Options { get; set; }

        public List<string> UsingNameSpaces { get; set; } = new List<string>();
    }
}
