using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Business.Abstract;

namespace VisitorManagement.Controllers
{
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
