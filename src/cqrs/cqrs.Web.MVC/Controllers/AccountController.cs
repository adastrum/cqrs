using System.Threading.Tasks;
using cqrs.CommandStack.Commands;
using cqrs.Messaging.Interfaces;
using cqrs.Web.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cqrs.Web.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICommandDispatcher _commandDispatcher;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ICommandDispatcher commandDispatcher
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser { Email = model.Email, UserName = model.Email };

                var identityResult = await _userManager.CreateAsync(identityUser, model.Password);

                if (identityResult.Succeeded)
                {
                    await _commandDispatcher.PublishAsync(new CreateUserCommand(model.Email));

                    return RedirectToAction("Index", "Auction");
                }

                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new SignInViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Auction");
                }

                ModelState.AddModelError(string.Empty, "Your password is incorrect");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Auction");
        }
    }
}