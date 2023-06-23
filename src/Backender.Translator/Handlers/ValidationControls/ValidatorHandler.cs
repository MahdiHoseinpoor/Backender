using Backender.Translator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Handlers.ValidationControls
{
    public static class ValidatorHandler
    {
        public static ValidationControl ValidationControl = ValidationControl.DataAnnotation;
        public static void Configure(Blueprint Blueprint)
        {
            switch (Blueprint.ValidationControl.ToLower())
            {
                case "dataannotation":
                    ValidationControl = ValidationControl.DataAnnotation;
                    break;
                case "fluentvalidation":
                    ValidationControl = ValidationControl.FluentValidation;
                    break;
                default:
                    break;
            }
        }
        public static List<Option> GetValidationOptions(this List<Option> options)
        {
            var ValidationOptions = new List<Option>();
            foreach (var option in options.DistinctBy(p=>p.Name))
            {
                if (ValidationOptionsName.Any(p => p.Key == option.Name))
                {
                    option.DisplayOrder = ValidationOptionsName[option.Name];
                    ValidationOptions.Add(option);
                }
            }
            return ValidationOptions.OrderBy(p=>p.DisplayOrder).ToList();
        }
        private readonly static Dictionary<string, int> ValidationOptionsName = new() {
            { "required",0 },
            { "key",-1 },
            { "length", 1 },
            { "displayname", 3 },
            { "email", 2 }
        };
    }
    public enum ValidationControl
    {
        DataAnnotation,
        FluentValidation
    }


}
