using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Services.Helper;

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
    public IActionResult Ayarlar()
    {
        return View();
    }

    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}