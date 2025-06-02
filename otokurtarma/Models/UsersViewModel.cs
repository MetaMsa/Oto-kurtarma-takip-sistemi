using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace otokurtarma.Models
{
    public class UsersViewModel
    {
        public UsersModel? user { get; set; }

        public UsersViewModel()
        {
        }
    }
}