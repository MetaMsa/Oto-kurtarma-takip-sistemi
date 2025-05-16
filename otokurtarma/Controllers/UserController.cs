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

    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}