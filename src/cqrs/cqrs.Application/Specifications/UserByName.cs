using System;
using System.Linq.Expressions;
using cqrs.Domain.Entities;
using cqrs.Domain.Interfaces;

namespace cqrs.Application.Specifications
{
    public class UserByName : ISpecification<User>
    {
        private readonly string _name;

        public UserByName(string name)
        {
            _name = name;
        }

        public Expression<Func<User, bool>> GetExpression()
        {
            return x => x.Name == _name;
        }
    }
}
