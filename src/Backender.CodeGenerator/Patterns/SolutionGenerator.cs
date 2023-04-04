using Backender.CodeEditor.CSharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeGenerator.Patterns
{
    public abstract class SolutionGenerator
    {
        public abstract Solution Run();
        public abstract void OnConfigure();
    }
}
