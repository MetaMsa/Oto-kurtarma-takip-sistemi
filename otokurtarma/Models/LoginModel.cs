using System.ComponentModel.DataAnnotations;
using System.Data;

namespace otokurtarma.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
    public string ?username {get; set;}
    [Required(ErrorMessage = "Şifre gereklidir")]
        [StringLength(8, ErrorMessage = "Şifre 8 karakterden uzun, 6 karakterden kısa olamaz",MinimumLength=6)]
    public string ?password {get; set;}
}