using Microsoft.AspNetCore.Mvc;
using VisitorManagementSystem.Business.Abstract;
using VisitorManagementSystem.Models.Entities;

namespace VisitorManagementSystem.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationManager _locationManager;

        public LocationController(ILocationManager locationManager)
        {
            _locationManager = locationManager;
        }

        [HttpPost]
        public IActionResult AddLocation(string newLocationName)
        {
            if (!string.IsNullOrWhiteSpace(newLocationName))
            {
                var newLocation = new Location
                {
                    LocationName = newLocationName
                };

                _locationManager.AddLocation(newLocation);
            }

            return RedirectToAction("Login", "User");
        }
    }
}
