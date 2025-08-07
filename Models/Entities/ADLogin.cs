using System.ComponentModel.DataAnnotations;
using VisitorManagementSystem.Models.Entities;
public class ADLogin
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
    public string? UserName { get; set; }

    public DateTime LoginDate { get; set; }
    public DateTime? LogoutDate { get; set; }
    public int LocationId { get; set; }
    public Location? Location { get; set; }
}
