using cqrs.Domain.Common;

namespace cqrs.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; protected set; }

        public User(string name)
        {
            Name = name;
        }
    }
}
