using Microsoft.AspNetCore.Mvc.Rendering;

namespace VisitorManagementSystem.Business.Abstract
{
    public interface IUserManager
    {
        bool ValidateUser(string username, string password);
        void SetLocationCookie(HttpResponse response, string locationId);
        void SetUserNameCookie(HttpResponse response, string userName);
        List<SelectListItem> GetLocationSelectList();
        public bool TryLogin(HttpResponse response, string username, string password, string locationId);
    }
}
