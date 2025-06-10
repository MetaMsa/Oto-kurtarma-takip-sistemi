using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class StaffModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        
        public int RolesModelId { get; set; }
        public RolesModel? RolesModel { get; set; }

        public int CompaniesModelId { get; set; }
        public CompaniesModel? CompaniesModel { get; set; }
    }
}