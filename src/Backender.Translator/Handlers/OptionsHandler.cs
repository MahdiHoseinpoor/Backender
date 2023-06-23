using Backender.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backender.Translator.Models;
namespace Backender.Translator.Handlers
{


    public static class OptionsHandler
    {
        public static void Configure(Blueprint Blueprint)
        {
            Blueprint.TranslateAllMinimalOptions();
            Blueprint.ParseGlobalOptions();
        }
        public static List<Option> ParseOptions(this string optionsString)
        {
            string[] options = optionsString.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            List<Option> optionList = new List<Option>();

            foreach (string option in options)
            {
                string trimmedOption = option.Trim();
                string[] parts = trimmedOption.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                Option newOption = new Option
                {
                    Name = parts[0].Trim()
                };
                if (parts.Length == 2)
                {
                    var parameters = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
                    newOption.Values.AddRange(parameters);
                }
                optionList.Add(newOption);
            }
            return optionList;
        }
        public static string AddOption(this string optionsString, string name, params string[] parameters)
        {
            optionsString = optionsString + " " + name;
            if (parameters.Any())
            {
                optionsString = optionsString + " (" + string.Join(',', parameters) + ")";
            }
            return optionsString;
        }
        public static bool HasOption(this string optionsString, string optionName)
        {
            return optionsString.ParseOptions().Any(p => p.Name == optionName);
        }
        public static void ParseGlobalOptions(this Blueprint Blueprint)
        {
            var GlobalOptions = Blueprint.Domains.GlobalOptions;
            foreach (var GlobalOption in GlobalOptions)
            {
                var EntityCols = GlobalOption.EntityCols.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var EntityCol in EntityCols)
                {
                    var EntityColParts = EntityCol.Split('.', StringSplitOptions.RemoveEmptyEntries);
                    var tableName = EntityColParts[0].Trim();
                    if (tableName == "root")
                    {
                        var splitedPart = EntityColParts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        Blueprint.Domains.Entities
                            .Select(p => p.Cols)
                            .ToList()
                            .ForEach(x =>
                            {
                                x.Where(q => splitedPart.Contains(q.ColName))
                                .ToList().ForEach(y => y.Options += " " + GlobalOption.Options);
                            });
                    }
                    else 
                    {
                        if (EntityColParts[1].Trim() != "All()")
                        {
                            var colNames = EntityColParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            Blueprint.Domains.Entities
                                .FirstOrDefault(p => p.EntityName == tableName).Cols
                                .Where(q => colNames.Contains(q.ColName))
                                .ToList()
                                .ForEach(x => x.Options += " " + GlobalOption.Options);
                        }
                        else
                        {
                            Blueprint.Domains.Entities
                                .FirstOrDefault(p => p.EntityName == tableName).Cols
                                .ToList()
                                .ForEach(x => x.Options += " " + GlobalOption.Options);
                        }
                    }
                    
                }
            }
        }
        public static void TranslateAllMinimalOptions(this Blueprint Blueprint)
        {
            var GlobalOptions = Blueprint.Domains.GlobalOptions;
            var Entities = Blueprint.Domains.Entities;
            foreach (var GlobalOption in GlobalOptions)
            {
                GlobalOption.Options= GlobalOption.Options.TranslateMinimalOptions();
            }
            foreach (var Entity in Entities)
            {
                Entity.Options= Entity.Options.TranslateMinimalOptions();
                foreach (var Col in Entity.Cols)
                {
                    Col.Options = Col.Options.TranslateMinimalOptions();
                }
            }
        }
        public static string TranslateMinimalOptions(this string options)
        {
            // Define the replacements using a dictionary
            Dictionary<string, string> replacements = new Dictionary<string, string>
        {
            { "-k", "key" },
            { "-r", "required" },
            { "-e", "email" },
            {"-l","length" },
            {"-dn","displayname" }
        };

            // Split the input string into individual items
            // Iterate over each item and replace if necessary
            foreach (var replacement in replacements)
            {
                options = options.Replace(replacement.Key, replacement.Value);
            }

            // Join the modified items back into a single string
            return options;
       
        }
}
}
