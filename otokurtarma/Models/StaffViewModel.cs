using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace otokurtarma.Models
{
    public class StaffViewModel
    {
        public IEnumerable<StaffModel> Staff { get; set; } = Enumerable.Empty<StaffModel>();

        private readonly AppDbContext _context;

        public StaffViewModel(AppDbContext context, string username)
        {
            _context = context;

            var user = _context.Users.FirstOrDefault(u => u.username == username);

            var staff = _context.Staff.Include(c => c.CompaniesModel).Where(s => s.CompaniesModelId == user.CompaniesModelId).AsEnumerable();

            Staff = staff;
        }
    }
}