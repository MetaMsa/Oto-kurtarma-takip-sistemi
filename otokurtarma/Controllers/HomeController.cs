using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using otokurtarma.Models;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Runtime.Intrinsics.Arm;
using Services.Helper;
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
            if (await _context.Users.FirstOrDefaultAsync(d => d.username == model.username) == null)
            {
                ModelState.AddModelError("db", "Böyle bir kullanıcı yok.");
                return View(model);
            }
            else
            {
                var user = await _context.Users.FirstOrDefaultAsync(d => d.username == model.username);
                if (AesEncryptionHelper.Decrypt(user.password, "her-sabit-dusunce-sahibi-icin-zindandır") != model.password)
                {
                    ModelState.AddModelError("password", "Şifre yanlış");
                    return View(model);
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.username)
                    };
                    var userid = new ClaimsIdentity(claims, "AuthId");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userid);
                    await HttpContext.SignInAsync(principal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(3)
                    });
                    return RedirectToAction("Index", "User");
                }
            }
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
            if (await _context.Users.FirstOrDefaultAsync(d => d.username == model.username) != null || await _context.Users.FirstOrDefaultAsync(d => d.Email == model.Email) != null)
            {
                ModelState.AddModelError("db", "Bu kullanıcı zaten var.");
            }
            else
            {
                string encryptpsw = AesEncryptionHelper.Encrypt(model.password, "her-sabit-dusunce-sahibi-icin-zindandır");

                UsersModel user = new()
                {
                    username = model.username,
                    fullname = model.fullname,
                    Email = model.Email,
                    password = encryptpsw
                };
                await _context.Users.AddAsync(user);
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