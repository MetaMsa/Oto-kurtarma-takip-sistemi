using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class VehiclesModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Plaka Zorunludur.")]
        [MaxLength(8, ErrorMessage = "Plaka maksimum 8 karakter uzunluğunda olmalıdır.")]
        [MinLength(7, ErrorMessage = "Plaka 7 karakterden uzun olmalıdır.")]
        public string plate { get; set; }
        [Required(ErrorMessage = "Tip Zorunludur.")]
        public string type { get; set; }
        [Required(ErrorMessage = "Ücret Zorunludur.")]
        public int price { get; set; }
        [Required(ErrorMessage = "Koordinat Zorunludur.")]
        public float lat { get; set; }
        [Required(ErrorMessage = "Koordinat Zorunludur.")]
        public float lng { get; set; }
        public int CompaniesModelId { get; set; }
        public CompaniesModel? CompaniesModel { get; set; }
    }
}