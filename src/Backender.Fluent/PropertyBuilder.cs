using Backender.Translator;
using Backender.Translator.Handlers;

namespace Backender.Fluent
{
    public class PropertyBuilder
    {
        private readonly Col _col;
        private readonly EntityBuilder _parentEntityBuilder;

        internal PropertyBuilder(Col col, EntityBuilder parentEntityBuilder)
        {
            _col = col;
            _parentEntityBuilder = parentEntityBuilder;
        }

        #region Property-Level Methods
        /// <summary>
        /// Marks the property as required (non-nullable).
        /// </summary>
        public PropertyBuilder IsRequired()
        {
            _col.Options = _col.Options.AddOption("required");
            return this;
        }

        /// <summary>
        /// Marks the property as the primary key.
        /// </summary>
        public PropertyBuilder IsKey()
        {
            _col.Options = _col.Options.AddOption("key");
            return this;
        }

        /// <summary>
        /// Validates that the property is a valid email address format.
        /// </summary>
        public PropertyBuilder IsEmailAddress()
        {
            _col.Options = _col.Options.AddOption("email");
            return this;
        }

        /// <summary>
        /// Sets the maximum allowed length for a string property.
        /// </summary>
        public PropertyBuilder HasMaxLength(int length)
        {
            _col.Options = _col.Options.AddOption("length", length.ToString());
            return this;
        }

        /// <summary>
        /// Sets both the minimum and maximum allowed length for a string property.
        /// </summary>
        public PropertyBuilder HasLength(int min, int max)
        {
            _col.Options = _col.Options.AddOption("length", min.ToString(), max.ToString());
            return this;
        }
        #endregion

        #region Entity-Level Continuation Methods (The Fix)

        /// <summary>
        /// Defines another property on the same entity, allowing the chain to continue.
        /// </summary>
        public PropertyBuilder Property<T>(string name)
        {
            return _parentEntityBuilder.Property<T>(name);
        }

        /// <summary>
        /// Defines another property on the same entity using a string type name.
        /// </summary>
        public PropertyBuilder Property(string name, string typeName)
        {
            return _parentEntityBuilder.Property(name, typeName);
        }

        /// <summary>
        /// Defines a one-to-many relationship from this entity to another.
        /// </summary>
        public EntityBuilder HasMany(string relatedEntityName)
        {
            // Delegate the call to the parent and return the result.
            return _parentEntityBuilder.HasMany(relatedEntityName);
        }

        /// <summary>
        /// Defines a one-to-one or many-to-one relationship from this entity to another.
        /// </summary>
        public EntityBuilder HasOne(string relatedEntityName)
        {
            return _parentEntityBuilder.HasOne(relatedEntityName);
        }

        #endregion
    }
}