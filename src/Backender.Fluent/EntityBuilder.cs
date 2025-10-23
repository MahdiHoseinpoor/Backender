using Backender.Translator;

namespace Backender.Fluent
{
    public class EntityBuilder
    {
        private readonly Entity _entity;
        private readonly Domains _domainsContext;

        internal EntityBuilder(Entity entity, Domains domainsContext)
        {
            _entity = entity;
            _domainsContext = domainsContext;
        }

        /// <summary>
        /// Defines a property on the entity using a generic type.
        /// </summary>
        public PropertyBuilder Property<T>(string name)
        {
            return AddProperty(name, typeof(T).Name);
        }

        /// <summary>
        /// Defines a property on the entity using a string type name (ideal for enums).
        /// </summary>
        public PropertyBuilder Property(string name, string typeName)
        {
            return AddProperty(name, typeName);
        }

        private PropertyBuilder AddProperty(string name, string typeName)
        {
            var col = new Col { ColName = name, ColType = typeName };
            _entity.Cols.Add(col);
            return new PropertyBuilder(col, this);
        }

        /// <summary>
        /// Defines a one-to-many relationship from this entity to another.
        /// </summary>
        public EntityBuilder HasMany(string relatedEntityName)
        {
            _domainsContext.RelationShips.Add(new RelationShip
            {
                Entity1 = _entity.EntityName,
                Entity2 = relatedEntityName,
                RelationShipType = "O2M"
            });
            return this; // Return this for chaining
        }

        /// <summary>
        /// Defines a one-to-one or many-to-one relationship from this entity to another.
        /// </summary>
        public EntityBuilder HasOne(string relatedEntityName)
        {
            _domainsContext.RelationShips.Add(new RelationShip
            {
                Entity1 = relatedEntityName,
                Entity2 = _entity.EntityName,
                RelationShipType = "O2M"
            });
            return this; // Return this for chaining
        }
    }
}