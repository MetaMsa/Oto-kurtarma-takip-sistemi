using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using System.Data;

namespace otokurtarma.Models
{
    public class UsersModel
    {
        public int ID {get; set;}

        [Required(ErrorMessage = "Lütfen kullanıcı adınızı girin")]
        public string username {get; set;}

        [Required(ErrorMessage = "Lütfen E-Posta adresinizi girin")]
        [EmailAddress(ErrorMessage = "Geçersiz E-Posta adresi")]
        public string Email {get; set;}

        [Required(ErrorMessage = "Lütfen şifrenizi girin")]
        [StringLength(7, ErrorMessage = "Şifre 8 karakterden uzun olamaz")]
        public string password {get; set;}
    }
}