using Backender.Translator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Backender.Translator.Handlers.ValidationControls
{
    public static class FluentValidationHandler
    {
        #region Validators
        public const string NotNullValidator = ".NotNull()";
        public const string WithNameValidator = ".WithName(\"{0}\")";
        public const string LengthValidator = ".Length({0},{1})";
        public const string MaximumLengthValidator = ".MaximumLength({0})";
        public const string MinimumLengthValidator = ".MinimumLength({1})";
        public const string EmailAddressValidator = ".EmailAddress()";
        #endregion

        public static string GetValidatorByOption(Option option)
        {
            switch (option.Name)
            {
                case "required":
                    return CreateRequiredValidator();
                case "email":
                    return CreateEmailAddressValidator();
                case "displayname":
                    return CreateDisplayValidator(option.Values[0]);
                case "length":
                    return option.CreateLengthValidator();
                case "minlength":
                    return CreateMinLengthValidator(option.Values[0]);
                case "maxlength":
                    return CreateMaxLengthValidator(option.Values[0]);
                default:
                    return string.Empty;
            }
        }

        private static string CreateRequiredValidator()
        {
            return NotNullValidator;
        }
        private static string CreateEmailAddressValidator()
        {
            return EmailAddressValidator;
        }
        private static string CreateDisplayValidator(string name)
        {
            return string.Format(WithNameValidator, name);
        }
        private static string CreateLengthValidator(this Option option)
        {
            if (option.Values.Count == 1)
            {
                var MaxLengthValidator = CreateMaxLengthValidator(option.Values[0]);
                return MaxLengthValidator;
            }
            else if (option.Values.Count == 2)
            {
                return string.Format(LengthValidator, option.Values[0], option.Values[1]);
            }
            else
            {
                return string.Empty;
            }
        }
        private static string CreateMaxLengthValidator(string Value)
        {
            return string.Format(MaximumLengthValidator, Value);
        }
        private static string CreateMinLengthValidator(string Value)
        {
            return string.Format(MinimumLengthValidator, Value);
        }
    }
}
