using Microsoft.AspNetCore.Mvc;

namespace cqrs.Web.MVC.Controllers
{
    public class AuctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}