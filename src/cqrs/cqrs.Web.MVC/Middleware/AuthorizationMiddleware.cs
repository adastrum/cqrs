using System.Linq;
using System.Threading.Tasks;
using cqrs.Application.Specifications;
using cqrs.Domain.Entities;
using cqrs.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace cqrs.Web.MVC.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<IdentityUser> userManager, IRepository<User> userRepository)
        {
            var identityUser = await userManager.GetUserAsync(context.User);

            var users = await userRepository.FindAllAsync(new UserByName(identityUser.UserName));

            context.Items["CurrentUser"] = users.SingleOrDefault();

            await _next(context);
        }
    }
}
