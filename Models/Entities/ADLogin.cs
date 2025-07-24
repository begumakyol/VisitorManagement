using System.ComponentModel.DataAnnotations;

public class ADLogin
{
    [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
    public string? Password { get; set; }
}
