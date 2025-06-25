using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace otokurtarma.Models
{
    public class VehicleQueryViewModel
    {
        public IEnumerable<VehiclesModel> Vehicles { get; set; } = Enumerable.Empty<VehiclesModel>();

        private readonly AppDbContext _context;

        public VehicleQueryViewModel(AppDbContext context, string username, string type)
        {
            _context = context;

            var user = _context.Users.FirstOrDefault(u => u.username == username);
            
            var vehicles = _context.Vehicles.Include(c => c.CompaniesModel).Where(s => s.CompaniesModelId == user.CompaniesModelId).Where(v => v.type == type).AsEnumerable();

            Vehicles = vehicles;
        }
    }
}