using Backender.Translator;

namespace Backender.Fluent
{
    public class EnumBuilder
    {
        private readonly Enum_ _enum;
        internal EnumBuilder(Enum_ enum_) => _enum = enum_;

        /// <summary>
        /// Adds a name-value pair to the enum.
        /// </summary>
        public EnumBuilder HasValue(string name, int value)
        {
            _enum.EnumValues.Add(new EnumValue_ { Name = name, Value = value });
            return this;
        }
    }
}