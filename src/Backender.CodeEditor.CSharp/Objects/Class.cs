using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Backender.CodeEditor.CSharp.Objects
{
    public class Class: CsFile
    {

        public string BaseClassName { get; set; }
        public bool IsStatic { get; set; }
        public List<Attribute> Attributes { get; set; } = new List<Attribute>();

    }
}
