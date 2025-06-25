using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace otokurtarma.Models;

public class CashViewModel
{
    private readonly AppDbContext _context;
    public IEnumerable<StaffModel> Staff = Enumerable.Empty<StaffModel>();
    public IEnumerable<VehiclesModel> Vehicles = Enumerable.Empty<VehiclesModel>();

    public CashViewModel(AppDbContext context, string username)
    {
        _context = context;

        var user = _context.Users.FirstOrDefault(u => u.username == username);

        Staff = _context.Staff.Include(c => c.CompaniesModel).Where(s => s.CompaniesModelId == user.CompaniesModelId).AsEnumerable();
        Vehicles = _context.Vehicles.Include(c => c.CompaniesModel).Where(v => v.CompaniesModelId == user.CompaniesModelId).AsEnumerable();
    }
}