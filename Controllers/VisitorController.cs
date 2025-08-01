using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Business;
using VisitorManagementSystem.Business.Abstract;
using VisitorManagementSystem.Models.Entities;
using VisitorManagementSystem.Utilities;

namespace VisitorManagementSystem.Controllers
{
    [Authorize]
    public class VisitorController : Controller
    {
        private readonly IVisitorManager _visitorManager;

        public VisitorController(IVisitorManager visitorManager)
        {
            _visitorManager = visitorManager;
        }

        [Route("Visitor/ZiyaretciEkle")]
        [HttpGet]
        public IActionResult AddVisitor()
        {
            return View();
        }

        [Route("Visitor/ZiyaretciEkle")]
        [HttpPost]
        public IActionResult AddVisitor(Visitor visitor)
        {

            string? locationIdStr = Request.Cookies["LocationId"];
            string? recordedBy = Request.Cookies["UserName"];

            _visitorManager.AddVisitor(visitor, locationIdStr, recordedBy);

            return RedirectToAction("ListVisitor");
        }

        [Route("Visitor/ZiyaretciListele")]
        [HttpGet]
        public IActionResult ListVisitor(DateTime? startDate, DateTime? endDate)
        {
            var locationIdClaim = User.Claims.FirstOrDefault(c => c.Type == "LocationId");

            if (locationIdClaim == null || !int.TryParse(locationIdClaim.Value, out int locationId))
            {
                return BadRequest("Geçerli bir LocationId claim değeri bulunamadı.");
            }

            var visitors = _visitorManager.ListVisitorViewModel(startDate, endDate, locationId);


            return View(visitors);
        }

        [Route("Visitor/ZiyaretciGuncelle")]
        [HttpPost]
        public IActionResult UpdateVisitor(Visitor visitor)
        {
            if (!_visitorManager.VisitorExists(visitor.VisitorId))
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(new { success = false, message = "Ziyaretçi bulunamadı." });

                TempData["Error"] = "Ziyaretçi bulunamadı!";
                return RedirectToAction("ListVisitor");
            }

            _visitorManager.UpdateVisitor(visitor);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { success = true, message = "Ziyaretçi bilgileri güncellendi." });

            TempData["Success"] = "Ziyaretçi başarıyla güncellendi!";
            return RedirectToAction("ListVisitor");

        }

        [HttpPost]
        public IActionResult ExitVisitor(int visitorId)
        {
            var userNameClaim = User.FindFirst("UserName");

            if (userNameClaim == null || string.IsNullOrEmpty(userNameClaim.Value))
            {
                return BadRequest("Geçerli bir UserName claim değeri bulunamadı.");
            }

            string stopRecordedBy = userNameClaim.Value;
            _visitorManager.MarkVisitorAsExited(visitorId, stopRecordedBy);

            return RedirectToAction("ListVisitor");
        }
    }
}
