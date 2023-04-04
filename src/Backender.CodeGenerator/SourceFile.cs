using Backender.CodeEditor.CSharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeGenerator
{
    public class SourceFile
    {
        public string Name { get; set; }
        public string SourceCode { get; set; }
        public string ProjectName { get; set; }
        public string Path { get; set; }
    }

}
