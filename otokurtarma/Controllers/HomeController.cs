using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using otokurtarma.Models;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace otokurtarma.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View(new LoginModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index([FromForm] LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        else
        {
            return View();
        }
    }

    public IActionResult Register()
    {
        return View(new UsersModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] UsersModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        else
        {
            if (_context.Users.FirstOrDefault(d => d.username == model.username) != null || _context.Users.FirstOrDefault(d => d.Email == model.Email) != null)
            {
                ModelState.AddModelError("a", "a");
            }
            else
            {
                UsersModel user = new()
                {
                    username = model.username,
                    fullname = model.fullname,
                    Email = model.Email,
                    password = model.password
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new LoginModel());
            }
        }
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
