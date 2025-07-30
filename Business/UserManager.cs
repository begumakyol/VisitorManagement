
using Microsoft.AspNetCore.Mvc.Rendering;
using VisitorManagementSystem.Business.Abstract;
using VisitorManagementSystem.Utilities;

namespace VisitorManagementSystem.Business
{
    public class UserManager : IUserManager
    {
        private readonly ILocationManager _locationManager;
        public UserManager(ILocationManager locationManager)
        {
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

        public void SetLocationCookie(HttpResponse response, string locationId)
        {
            response.Cookies.Append("LocationId", locationId);
        }

        public void SetUserNameCookie(HttpResponse response, string userName)
        {
            response.Cookies.Append("UserName", userName);
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
    }
}
