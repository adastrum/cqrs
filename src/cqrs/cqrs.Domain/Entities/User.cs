using cqrs.Domain.Common;
using cqrs.Domain.Interfaces;

namespace cqrs.Domain.Entities
{
    public class User : Entity, IAggreagateRoot
    {
        public string Name { get; protected set; }

        protected User() { }

        public User(string name)
        {
            Name = name;
        }
    }
}
