using System.ComponentModel.DataAnnotations;

namespace otokurtarma.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
    public string ?username {get; set;}
    [Required(ErrorMessage = "Şifre gereklidir")]
    [MinLength(6, ErrorMessage = "Şifre 6 karakterden uzun olmalıdır.")]
    public string ?password {get; set;}
}