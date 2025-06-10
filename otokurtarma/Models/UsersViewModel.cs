using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace otokurtarma.Models
{
    public class UsersViewModel
    {
        [Required(ErrorMessage = "Lütfen Adınızı ve Soyadınızı girin")]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adınızı girin")]
        public string username { get; set; }

        [Required(ErrorMessage = "Lütfen E-Posta adresinizi girin")]
        [EmailAddress(ErrorMessage = "Geçersiz E-Posta adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi girin")]
        [MinLength(6, ErrorMessage = "Şifre 6 karakterden uzun olmalıdır.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Lütfen şirket adınızı girin")]
        public int CompaniesModelId { get; set; }
        public CompaniesModel? CompaniesModel { get; set; }

        public IFormFile pp { get; set; } = new FormFile(Stream.Null, 0, 0, "pp", "?.png");

        public UsersViewModel()
        {
        }
    }
}