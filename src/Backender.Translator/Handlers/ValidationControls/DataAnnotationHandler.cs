using Backender.Translator.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator.Handlers.ValidationControls
{
    public static class DataAnnotationHandler
    {
        #region Attributes
        public const string KeyAttribute = "Key";
        public const string RequiredAttribute = "Required";
        public const string DisplayAttribute = "Display";
        public const string LengthAttribute = "Length";
        public const string MaxLengthAttribute = "MaxLength";
        public const string MinLengthAttribute = "MinLength";
        public const string EmailAddressAttribute = "EmailAddress";
        #endregion

        public static string GetAttributeByOption(Option option)
        {
            switch (option.Name)
            {
                case "key":
                    return CreateKeyAttribute();
                case "required":
                    return CreateRequiredAttribute();
                case "email":
                    return CreateEmailAddressAttribute();
                case "displayname":
                    return CreateDisplayAttribute(option.Values[0]);
                case "length":
                    return CreateLengthAttribute(option);
                case "minlength":
                    return CreateMinLengthAttribute(option.Values[0]);
                case "maxlength":
                    return CreateMaxLengthAttribute(option.Values[0]);
                default:
                    return string.Empty;
            }
        }

        private static string CreateAttribute(string name,bool HasParameter = false)
        {
            string attribute = $"[{name}{(HasParameter ? "({0})" : string.Empty)}]";
            return attribute;
        }
        private static string CreateRequiredAttribute()
        {
            return CreateAttribute(RequiredAttribute);
        }
        private static string CreateKeyAttribute()
        {
            return CreateAttribute(KeyAttribute);
        }
        private static string CreateEmailAddressAttribute()
        {
            return CreateAttribute(EmailAddressAttribute);
        }
        private static string CreateDisplayAttribute(string name)
        {
            var attribute = CreateAttribute(DisplayAttribute,true);
            var nameParameter = $"Name = \"{name}\"";
            attribute = string.Format(attribute, nameParameter);
            return attribute;
        }
        private static string CreateLengthAttribute( Option option)
        {
            if (option.Values.Count == 1)
            {
                var MaxLengthAttribute = CreateMaxLengthAttribute(option.Values[0]);
                return MaxLengthAttribute;
            }
            else if (option.Values.Count == 2)
            {
                var MinLengthAttribute = CreateMinLengthAttribute(option.Values[0]);
                var MaxLengthAttribute = CreateMaxLengthAttribute(option.Values[1]);
                return MinLengthAttribute + " " + MaxLengthAttribute;
            }
            else
            {
                return string.Empty;
            }
        }
        private static string CreateMaxLengthAttribute(string Value)
        {
            return string.Format(CreateAttribute(MaxLengthAttribute, true), Value);
        }
        private static string CreateMinLengthAttribute(string Value)
        {
            return string.Format(CreateAttribute(MinLengthAttribute, true), Value);
        }
    }
}
