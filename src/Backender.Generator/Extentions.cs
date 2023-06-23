using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Generator
{
    public static class Extensions
    {
        public static string ToPlural(this string word)
        {
            if (word.EndsWith("s"))
            {
                word += "es";
            }
            else if (word.EndsWith("y"))
            {
                word = word.Substring(0, word.Length - 1) + "ies";
            }
            else
            {
                word += "s";
            }
            return word;
        }
        public static string ToFieldName(this string name)
        {
            name = "_" + name.Substring(0, 1).ToLower() + name.Substring(1, name.Length - 1);
            return name;
        }
    }
}
