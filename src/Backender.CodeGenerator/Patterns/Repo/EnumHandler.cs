using Backender.CodeEditor.CSharp;
using Backender.CodeEditor.CSharp.Objects;
using Backender.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeGenerator.Patterns.Repo
{
	/// <summary>
	/// This class Generate the enums
	/// </summary>
	public static class EnumHandler
	{
		/// <summary>
		/// Create Enum and add their values based on enums section of config informations.
		/// </summary>
		/// <param name="_enum">The enum section of config informations.</param>
		/// <param name="proj">The project in which the entity class is built.</param>
		/// <param name="Options">Some Options for the Created Class.</param>
		public static void EnumGenerate(this Enum_ _enum, ref Project proj, List<string> Options = null)
		{
			var enumValues = new List<EnumValue>();
			foreach (var enumValue in _enum.EnumValues)
			{
				enumValues.Add(new EnumValue
				{
					Name = enumValue.Name,
					Value = enumValue.Value
				});
			}
			var _Enum = proj.AddEnum(_enum.EnumName, enumValues, AppendNameSpace: "Enums");
		}

		/// <summary>
		/// Create All enums based on enums section of config informations.
		/// </summary>
		/// <param name="_enums">List of enum section of config informations.</param>
		/// <param name="proj">The project in which the entity class is built.</param>
		public static void AddEnums(this Project proj, ref List<Enum_> _enums)
		{
			foreach (var _enum in _enums)
			{
				_enum.EnumGenerate(ref proj);
			}
		}
	}
}
