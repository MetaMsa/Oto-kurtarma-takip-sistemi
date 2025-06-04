using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class RolesModel
    {
        [Key]
        public int ID { get; set; }
        public string Role { get; set; }
        
        public List<UsersModel> Users { get; set; }
    }
}