using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VisitorManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var username = User.Identity.Name;
            ViewData["UserName"] = username;
            return View();
        }
    }
}