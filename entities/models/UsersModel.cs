using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class UsersModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Lütfen Adınızı ve Soyadınızı girin")]
        public string? fullname { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adınızı girin")]
        public string? username { get; set; }

        [Required(ErrorMessage = "Lütfen E-Posta adresinizi girin")]
        [EmailAddress(ErrorMessage = "Geçersiz E-Posta adresi")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi girin")]
        [MinLength(6, ErrorMessage = "Şifre 6 karakterden uzun olmalıdır.")]
        public string? password { get; set; }

        public int RolesModelId { get; set; }
        public RolesModel? RolesModel { get; set; }

        [Required(ErrorMessage = "Lütfen şirket adınızı girin")]
        public int CompaniesModelId { get; set; }
        public CompaniesModel? CompaniesModel { get; set; }
    }
}