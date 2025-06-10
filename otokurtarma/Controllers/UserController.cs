using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using otokurtarma.Models;
using Services.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace otokurtarma.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _context;
    public UserController(ILogger<UserController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SetConsent()
    {
        // 10 yıl geçerli cookie ayarlanıyor
        var cookieOptions = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddMonths(1),
            Path = "/",
            SameSite = SameSiteMode.Lax,
            IsEssential = true,
            HttpOnly = false
        };

        Response.Cookies.Append(".AspNet.Consent", "yes", cookieOptions);

        return Ok();
    }

    public IActionResult Islemler()
    {
        return View();
    }

    public IActionResult Raporlar()
    {
        return View();
    }

    public IActionResult Tanimlar()
    {
        return View();
    }

    public async Task<IActionResult> Ayarlar()
    {
        var usrname = User.Identity?.Name;
        var usr = await _context.Users.Include(u => u.RolesModel).FirstOrDefaultAsync(u => u.username == usrname);

        ViewBag.Role = usr.RolesModel.Role;

        var model = new UsersViewModel
        {
            fullname = usr.fullname,
            username = usr.username,
            Email = usr.Email,
            password = usr.password
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ayarlar([FromForm] UsersViewModel model)
    {
        if (model.password != null)
        {
            var user = await _context.Users.FirstOrDefaultAsync(d => d.username == User.Identity.Name);
            if (user != null)
            {
                string encryptpsw = AesEncryptionHelper.Encrypt(model.password, "her-sabit-dusunce-sahibi-icin-zindandır");
                user.password = encryptpsw;

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Ayarlar");
        }

        if (model.pp != null)
        {
            if (model.pp.ContentType == "image/jpeg" || model.pp.ContentType == "image/jpg" || model.pp.ContentType == "image/png")
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\profilePictures", User.Identity.Name + ".png");
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.pp.CopyToAsync(stream);
                }
            }
            return RedirectToAction("Ayarlar");
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}