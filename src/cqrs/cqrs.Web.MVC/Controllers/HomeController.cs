using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cqrs.Web.MVC.Models;

namespace cqrs.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
