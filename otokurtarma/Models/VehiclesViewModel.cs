using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace otokurtarma.Models
{
    public class VehiclesViewModel
    {
        public IEnumerable<VehiclesModel> Vehicles { get; set; } = Enumerable.Empty<VehiclesModel>();

        private readonly AppDbContext _context;

        public VehiclesViewModel(AppDbContext context, string username)
        {
            _context = context;

            var user = _context.Users.FirstOrDefault(u => u.username == username);
            
            var vehicles = _context.Vehicles.Include(c => c.CompaniesModel).Where(s => s.CompaniesModelId == user.CompaniesModelId).AsEnumerable();

            Vehicles = vehicles;
        }
    }
}