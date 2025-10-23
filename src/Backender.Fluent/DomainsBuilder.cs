using Backender.Translator;

namespace Backender.Fluent
{
    public class DomainsBuilder
    {
        private readonly Domains _domains;
        internal DomainsBuilder(Domains domains) => _domains = domains;

        /// <summary>
        /// Defines a new entity (domain model).
        /// </summary>
        /// <param name="name">The name of the entity class.</param>
        /// <param name="category">(Optional) A sub-folder/namespace for organization.</param>
        /// <returns>An EntityBuilder to configure the new entity.</returns>
        public EntityBuilder Entity(string name, string category = null)
        {
            var entity = new Entity { EntityName = name, EntityCategory = category };
            _domains.Entities.Add(entity);
            return new EntityBuilder(entity, _domains);
        }

        /// <summary>
        /// Defines a new enumeration.
        /// </summary>
        /// <param name="name">The name of the enum.</param>
        /// <returns>An EnumBuilder to configure the new enum.</returns>
        public EnumBuilder Enum(string name)
        {
            var enum_ = new Enum_ { EnumName = name };
            _domains.Enums.Add(enum_);
            return new EnumBuilder(enum_);
        }
    }
}