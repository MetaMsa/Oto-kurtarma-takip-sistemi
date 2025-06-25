using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace otokurtarma.Models;

public class CompanyViewModel
{
    private readonly AppDbContext _context;
    public IEnumerable<UsersModel> Users = Enumerable.Empty<UsersModel>();
    public IEnumerable<StaffModel> Staff = Enumerable.Empty<StaffModel>();

    public CompanyViewModel(AppDbContext context, string username)
    {
        _context = context;

        var user = _context.Users.FirstOrDefault(u => u.username == username);

        Users = _context.Users.Include(r => r.RolesModel).Include(c => c.CompaniesModel).Where(u => u.CompaniesModelId == user.CompaniesModelId).AsEnumerable();
        Staff = _context.Staff.Include(r => r.RolesModel).Include(c => c.CompaniesModel).Where(u => u.CompaniesModelId == user.CompaniesModelId).AsEnumerable();
    }
}