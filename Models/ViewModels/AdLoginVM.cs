using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models.ViewModels
{
    public class AdLoginVM
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        public string? Password { get; set; }
        public string? SelectedLocation { get; set; }
    }
}
