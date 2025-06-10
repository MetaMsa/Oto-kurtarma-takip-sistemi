using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class CompaniesModel
    {
        [Key]
        public int ID { get; set; }
        public string Company { get; set; }

        public List<UsersModel> Users { get; set; } = new();
        public List<StaffModel> Staffs { get; set; } = new();
    }
}