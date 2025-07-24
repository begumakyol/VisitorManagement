using Microsoft.AspNetCore.Mvc.Rendering;
using VisitorManagementSystem.Models.Entities;

namespace VisitorManagementSystem.Models.View
{
    public class DropdownViewModel
    {
        public List<SelectListItem> Locations { get; set; }
    }
}
