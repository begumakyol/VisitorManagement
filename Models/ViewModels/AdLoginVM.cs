using System.ComponentModel.DataAnnotations;
using VisitorManagementSystem.Models.Entities;

namespace VisitorManagementSystem.Models.ViewModels
{
    public class AdLoginVM : ADLogin
    {
        public string? SelectedLocation { get; set; } 
    }
}
