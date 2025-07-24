using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VisitorManagementSystem.Business.Abstract;
using VisitorManagementSystem.Models.View;
using VisitorManagementSystem.Models.ViewModels;
using VisitorManagementSystem.Utilities;

namespace VisitorManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;

        private readonly ILocationManager _locationManager;

        public UserController(ILocationManager locationManager, IUserManager userManager)
        {
            _locationManager = locationManager;
            _userManager = userManager;
        }

        [Route("User/GirisYap")]
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Locations = _userManager.GetLocationSelectList();
            return View();
        }

        [Route("User/GirisYap")]
        [HttpPost]
        public  IActionResult Login(AdLoginVM adLoginVM)
        {
            if (string.IsNullOrWhiteSpace(adLoginVM.SelectedLocation))
            {
                ModelState.AddModelError("SelectedLocation", "Lütfen bir lokasyon seçiniz.");
            }

            if (ModelState.IsValid)
            {
                bool loginSuccess = _userManager.TryLogin(Response, adLoginVM.UserName, adLoginVM.Password, adLoginVM.SelectedLocation);

                if (loginSuccess)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                }
            }

            ViewBag.Locations = _userManager.GetLocationSelectList();
            return View();
        }

        [Route("User/CikisYap")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("GirisYap", "User");
        }
    }
}
