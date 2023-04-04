using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeEditor.CSharp.Objects
{
    public class Project
    {
        public string Name { get; set; }
        public string DefaultNameSpace { get; set; }
        public string SolutionName { get; set; }
        public List<Project> ProjectReference { get; set; } = new List<Project>();
        public List<CsFile> CsFiles { get; set; } = new List<CsFile>();
    }
}
