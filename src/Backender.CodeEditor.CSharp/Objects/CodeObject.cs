using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeEditor.CSharp.Objects
{
    public abstract class CodeObject
    {
        public string Name { get; set; }
		public List<string> RequiredNameSpaces { get; set; } = new List<string>();
        public AccessModifier AccessModifier { get; set; }
    }
    public enum AccessModifier
{
    Public,
    Protected,
    Private,
    Internal,
    None
}
}
