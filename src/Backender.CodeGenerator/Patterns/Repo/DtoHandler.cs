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
	public static class DtoHandler
	{
		public static Class DtoGenerate(this Entity entity, ref Project proj, List<string> Options = null)
		{
			var options = new List<string>();
			options.Add("DtoClass");
			if (Options != null)
			{
				options.AddRange(Options);
			}
			var entityClass = proj.AddClass(entity.EntityName+"Dto", baseClassName: "BaseDto", Options: options, AppendNameSpace: "Dto");
			foreach (var Col in entity.Cols)
			{
				entityClass.AddProperty(Col.ColType, Col.ColName, AccessModifier.Public);
			}

			//Add Realations
			return entityClass;
		}
		public static void AddDtoRealations(this Project proj, ref List<RealationShip> realationShips)
		{
			var options = new List<string>();
			foreach (var Realation in realationShips)
			{
				var class1 = proj.GetClassByName(Realation.Entity1 + "Dto");
				var class2 = proj.GetClassByName(Realation.Entity2 + "Dto");
				if (class1 == null || class2 == null) continue;
				switch (Realation.RealationShipType)
				{
					case "M2M":
					
						break;
					case "O2M":
						class2.AddProperty(Realation.Entity1 + "Dto", Realation.Entity1 + "Dto", AccessModifier.Public);
						break;
					case "O2O":

						class1.AddProperty(Realation.Entity2 + "Dto", Realation.Entity2 + "Dto", AccessModifier.Public);
						class2.AddProperty(Realation.Entity1 + "Dto", Realation.Entity1 + "Dto", AccessModifier.Public);

						break;
					default:
						break;
				}
			}
		}


	}
}
