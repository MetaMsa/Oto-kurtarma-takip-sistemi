using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using otokurtarma.Models;
using Services.Helper;
using Entities.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace otokurtarma.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _context;
    public UserController(ILogger<UserController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public IActionResult SetConsent()
    {
        // 10 yıl geçerli cookie ayarlanıyor
        var cookieOptions = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(1),
            Path = "/",
            SameSite = SameSiteMode.Lax,
            IsEssential = true,
            HttpOnly = false
        };

        Response.Cookies.Append(".AspNet.Consent", "yes", cookieOptions);

        return Ok();
    }

    [Authorize]
    public IActionResult Islemler()
    {
        return View();
    }

    [Authorize]
    public IActionResult Raporlar()
    {
        return View();
    }

    [Authorize]
    public IActionResult Tanimlar()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Ayarlar()
    {
        var usrname = User.Identity?.Name;
        var usr = await _context.Users.FirstOrDefaultAsync(u => u.username == usrname);

        var model = new UsersViewModel { user = usr };

        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Ayarlar([FromForm] UsersViewModel model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(d => d.username == User.Identity.Name);
        if (user != null)
        {
            string encryptpsw = AesEncryptionHelper.Encrypt(model.user.password, "her-sabit-dusunce-sahibi-icin-zindandır");
            user.password = encryptpsw;

            await _context.SaveChangesAsync();

            return RedirectToAction("Ayarlar");
        }
        else
        {
            return View(model);
        }
    }

    public async Task<IActionResult> signOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}