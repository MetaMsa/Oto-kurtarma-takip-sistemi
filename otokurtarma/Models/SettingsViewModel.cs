using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace otokurtarma.Models
{
    public class SettingsViewModel
    {
        [Required(ErrorMessage = "Lütfen şifrenizi girin")]
        [MinLength(6, ErrorMessage = "Şifre 6 karakterden uzun olmalıdır.")]
        public string password { get; set; } 
    }
}