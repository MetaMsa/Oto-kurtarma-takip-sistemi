using Entities.Models;

namespace otokurtarma.Models
{
    public class StaffViewModel
    {
        public IEnumerable<StaffModel> Staff { get; set; } = Enumerable.Empty<StaffModel>();

        private readonly AppDbContext _context;

        public StaffViewModel(AppDbContext context)
        {
            _context = context;

            var staff = _context.Staff.AsEnumerable();

            Staff = staff;
        }
    }
}