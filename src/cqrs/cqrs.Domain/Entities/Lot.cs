using cqrs.Domain.Common;

namespace cqrs.Domain.Entities
{
    public class Lot : Entity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected Lot() { }

        public Lot(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
