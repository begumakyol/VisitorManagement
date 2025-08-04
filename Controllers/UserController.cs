using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using VisitorManagementSystem.Business.Abstract;
using VisitorManagementSystem.Models.ViewModels;

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
        public async Task<IActionResult> LoginAsync(AdLoginVM adLoginVM)
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
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, adLoginVM.UserName),
                        new Claim("LocationId", adLoginVM.SelectedLocation),
                        new Claim("CustomClaimType", "CustomValue") // Ekstra claimler ekleyebilirsin
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "cookie");

                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1)
                    };

                    await HttpContext.SignInAsync(
                        "cookie",
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
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
            await HttpContext.SignOutAsync("cookie");

            return RedirectToAction("GirisYap", "User");
        }
    }
}
