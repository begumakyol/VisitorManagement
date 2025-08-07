using Microsoft.AspNetCore.Mvc.Rendering;

namespace VisitorManagementSystem.Business.Abstract
{
    public interface IUserManager
    {
        bool ValidateUser(string username, string password);
        List<SelectListItem> GetLocationSelectList();
        public bool TryLogin(HttpResponse response, string username, string password, string locationId);
        public void AddADLogin(ADLogin aDLogin);
        public void UpdateADLogin(string userName);
    }
}