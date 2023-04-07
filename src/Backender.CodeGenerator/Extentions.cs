using Backender.CodeEditor.CSharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.CodeGenerator
{
    public static class Extentions
    {
       public static string ToPlural(this string word)
        {
            if (word.EndsWith("s"))
            {
                word += "es";
            }
            else
            {
                word += "s";
            }
            return word;
        }
		public static string ToSingular(this string word)
		{
			if (word.EndsWith("ses") && word.Length>3)
			{
				word = word.Substring(0, word.Length-3);
			}
			else if(word.EndsWith("s") && !word.EndsWith("es"))
			{
				word = word.Substring(0, word.Length - 1);
			}
			return word;
		}

		public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input[0].ToString().ToUpper() + input.Substring(1)
        };
        public static string GetEntityFieldByEntityName(this Class entityService,string entityName)
        {
            var field = entityService.InnerItems.OfType<Field>().FirstOrDefault(p => p.Name.CaseInsensitiveContains(entityName));
            if (field != null)
            {
                return field.Name;

            }
            return "";
        }
        public static bool CaseInsensitiveContains(this string text, string value,
        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }
    }
}
