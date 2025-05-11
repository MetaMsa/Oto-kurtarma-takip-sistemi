using System.ComponentModel.DataAnnotations;
using System.Data;

namespace otokurtarma.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
    public string username {get; set;}
    [Required(ErrorMessage = "Şifre gereklidir")]
    public string password {get; set;}
}