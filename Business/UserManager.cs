using Microsoft.AspNetCore.Mvc.Rendering;
using VisitorManagementSystem.Business.Abstract;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Utilities;

namespace VisitorManagementSystem.Business
{
    public class UserManager : IUserManager
    {
        private readonly ILocationManager _locationManager;
        private readonly VisitorDbContext _context;
        public UserManager(VisitorDbContext context, ILocationManager locationManager)
        {
            _context = context;
            _locationManager = locationManager;
        }

        public List<SelectListItem> GetLocationSelectList()
        {
            return _locationManager.GetLocations()
                .Select(loc => new SelectListItem
                {
                    Text = loc.LocationName,
                    Value = loc.LocationId.ToString()
                }).ToList();
        }

        public bool ValidateUser(string username, string password)
        {
            return LdapHelper.IsValidDirectoryServiceUser(username, password);
        }

        public bool TryLogin(HttpResponse response, string username, string password, string locationId)
        {
            if (!ValidateUser(username, password))
                return false;

            if (string.IsNullOrWhiteSpace(locationId))
                return false;

            return true;
        }
        public void AddADLogin(ADLogin aDLogin)
        {
            _context.ADLogins.Add(aDLogin);
            _context.SaveChanges();
        }

        public void UpdateADLogin(ADLogin aDLogin)
        {
            var existing = _context.ADLogins.FirstOrDefault(ad => ad.Id == aDLogin.Id);
            if (existing is null) return;

            existing.LogoutDate = DateTime.Now;

            _context.ADLogins.Update(aDLogin);
            _context.SaveChanges();
        }
    }
}
