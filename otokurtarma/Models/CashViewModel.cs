using Entities.Models;

namespace otokurtarma.Models;

public class CashViewModel
{
    private readonly AppDbContext _context;
    public IEnumerable<StaffModel> Staff = Enumerable.Empty<StaffModel>();
    public IEnumerable<VehiclesModel> Vehicles = Enumerable.Empty<VehiclesModel>();

    public CashViewModel(AppDbContext context)
    {
        _context = context;

        Staff = _context.Staff.AsEnumerable();
        Vehicles = _context.Vehicles.AsEnumerable();
    }
}