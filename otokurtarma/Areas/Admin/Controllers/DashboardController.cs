using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using otokurtarma.Models;

[Area("Admin")]
[Authorize]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly AppDbContext _context;
    public DashboardController(ILogger<DashboardController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View(new AdminListViewModel(_context));
    }

    public async Task<IActionResult> DeleteCompany([FromRoute] int id)
    {
        var usr = await _context.Users.Include(u => u.CompaniesModel).FirstOrDefaultAsync(u => u.username == User.Identity.Name);

        if (usr.CompaniesModel.ID != id)
        {
            var company = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        else
        {
            TempData["Hata"] = "Ana şirketi silemezsiniz.";
            return RedirectToAction("Index");
        }
    }

    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var usr = await _context.Users.FirstOrDefaultAsync(u => u.username == User.Identity.Name);

        if (usr.ID != id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        else
        {
            TempData["Hata"] = "Admini silemezsiniz.";
            return RedirectToAction("Index");
        }
    }

    public async Task<IActionResult> DeleteStaff([FromRoute] int id)
    {
        var usr = await _context.Staff.FirstOrDefaultAsync(s => s.ID == id);

        var staff = await _context.Staff.FindAsync(id);
        _context.Staff.Remove(staff);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}