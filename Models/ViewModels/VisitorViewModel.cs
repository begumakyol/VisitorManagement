using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models.ViewModels
{
    public class VisitorViewModel
    {
        public long VisitorId { get; set; }

        [Required(ErrorMessage = "Ad ve Soyad gereklidir.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "TC numarası gereklidir.")]
        [StringLength(11, ErrorMessage = "TC numarası 11 karakter olmalıdır.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC numarası sadece rakamlardan oluşmalıdır.")]
        public string CitizenshipNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şirket adı gereklidir.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ziyaret edilen kişi gereklidir.")]
        public string MeetingWith { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cinsiyet gereklidir.")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giriş tarihi gereklidir.")]
        public DateTime EntryDate { get; set; }

        [Required(ErrorMessage = "Çıkış tarihi gereklidir.")]
        public DateTime ExitDate { get; set; }
        public bool IsInside { get; set; } = true;
    }
}


