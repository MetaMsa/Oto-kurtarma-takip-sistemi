using Entities.Models;

namespace otokurtarma.Models
{
    public class AdminListViewModel
    {
        public IEnumerable<CompaniesModel> Companies { get; set; } = Enumerable.Empty<CompaniesModel>();
        public IEnumerable<UsersModel> Users { get; set; } = Enumerable.Empty<UsersModel>();
        public IEnumerable<StaffModel> Staff { get; set; } = Enumerable.Empty<StaffModel>();

        private readonly AppDbContext _context;

        public AdminListViewModel(AppDbContext context)
        {
            _context = context;

            var companies = _context.Companies.AsEnumerable();
            var users = _context.Users.AsEnumerable();
            var staff = _context.Staff.AsEnumerable();

            Companies = companies;
            Users = users;
            Staff = staff;
        }
    }
}