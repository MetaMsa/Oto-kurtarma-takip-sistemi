using Entities.Models;

namespace otokurtarma.Models
{
    public class VehiclesViewModel
    {
        public IEnumerable<VehiclesModel> Vehicles { get; set; } = Enumerable.Empty<VehiclesModel>();

        private readonly AppDbContext _context;

        public VehiclesViewModel(AppDbContext context)
        {
            _context = context;

            var vehicles = _context.Vehicles.AsEnumerable();

            Vehicles = vehicles;
        }
    }
}