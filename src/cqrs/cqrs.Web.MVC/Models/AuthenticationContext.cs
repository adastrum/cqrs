using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cqrs.Web.MVC.Models
{
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext() { }

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }
    }
}
