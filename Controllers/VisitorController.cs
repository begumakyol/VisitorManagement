using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Business;
using VisitorManagementSystem.Business.Abstract;
using VisitorManagementSystem.Models.Entities;
using VisitorManagementSystem.Utilities;

namespace VisitorManagementSystem.Controllers
{
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
			if (!Request.Cookies.TryGetValue("LocationId", out string locationIdStr) || !int.TryParse(locationIdStr, out int locationId))
			{
				return BadRequest("Geçerli bir LocationId cookie değeri bulunamadı.");
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
            string? stopRecordedBy = Request.Cookies["UserName"];
            _visitorManager.MarkVisitorAsExited(visitorId, stopRecordedBy);

            // Güncelleme başarılıysa anasayfaya veya ilgili sayfaya yönlendir
            return RedirectToAction("ListVisitor");
        }
    }
}
