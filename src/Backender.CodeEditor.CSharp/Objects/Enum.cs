using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeEditor.CSharp.Objects
{
	public class Enum:CsFile
	{
		public List<EnumValue> EnumValues { get; set; }
	}
}
