using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using otokurtarma.Models;
using Services.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Entities.Models;
using System.Text.RegularExpressions;

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

    public async Task<IActionResult> Index()
    {
        var usr = await _context.Users.Include(u => u.RolesModel).Include(u => u.CompaniesModel).FirstOrDefaultAsync(u => u.username == User.Identity.Name);

        var model = new UsersModel
        {
            fullname = usr.fullname,
            username = usr.username,
            Email = usr.Email,
            password = usr.password,
            CompaniesModel = usr.CompaniesModel,
            RolesModel = usr.RolesModel
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult SetConsent()
    {
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



    public IActionResult Kasa()
    {
        return View(new CashViewModel(_context));
    }

    public IActionResult KurtarmaListe()
    {
        return View(new VehiclesViewModel(_context));
    }

    public IActionResult PersonelListe()
    {
        return View(new StaffViewModel(_context));
    }

    public IActionResult Kurtarma()
    {
        return View(new VehiclesModel());
    }

    [HttpPost]
    public async Task<IActionResult> Kurtarma([FromForm] VehiclesModel model)
    {
        var user = await _context.Users.Include(c => c.CompaniesModel).FirstOrDefaultAsync(u => u.username == User.Identity.Name);
        VehiclesModel vehicle = new VehiclesModel()
        {
            plate = model.plate,
            type = model.type,
            price = model.price,
            lng = model.lng,
            lat = model.lat,
            CompaniesModelId = user.CompaniesModelId
        };

        await _context.Vehicles.AddAsync(vehicle);
        await _context.SaveChangesAsync();

        return RedirectToAction("Kurtarma");
    }

    public IActionResult Personel()
    {
        return View(new StaffModel());
    }

    [HttpPost]
    public async Task<IActionResult> Personel([FromForm] StaffModel model)
    {
        var user = await _context.Users.Include(s => s.RolesModel).Include(c => c.CompaniesModel).FirstOrDefaultAsync(u => u.username == User.Identity.Name);
        StaffModel staff = new StaffModel()
        {
            Name = model.Name,
            RolesModelId = 3,
            CompaniesModelId = user.CompaniesModel.ID 
        };

        await _context.Staff.AddAsync(staff);
        await _context.SaveChangesAsync();

        return RedirectToAction("Personel");
    }

    public async Task<IActionResult> Ayarlar()
    {
        var usrname = User.Identity?.Name;
        var usr = await _context.Users.Include(u => u.RolesModel).FirstOrDefaultAsync(u => u.username == usrname);

        var model = new SettingsViewModel
        {
            password = usr.password
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ayarlar([FromForm] SettingsViewModel model)
    {
        var usrname = User.Identity?.Name;
        var usr = await _context.Users.Include(u => u.RolesModel).FirstOrDefaultAsync(u => u.username == usrname);

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        else
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
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Pp([FromForm] IFormFile file)
    {
        if (file != null)
        {
            if (file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/png")
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\profilePictures", User.Identity.Name + ".png");
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            else
            {
                TempData["imgError"] = "Geçerli bir resim yükleyiniz";
            }
            return RedirectToAction("Ayarlar");
        }
        return RedirectToAction("Ayarlar");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Pp2([FromForm] string file)
    {
        if (file.StartsWith("data:image"))
        {
            var base64 = Regex.Match(file, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var bytes = Convert.FromBase64String(base64);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\profilePictures", User.Identity.Name + ".png");

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            System.IO.File.WriteAllBytes(path, bytes);
        }
        else
        {
            TempData["imgError"] = "Geçerli bir resim yükleyiniz";
        }
        return RedirectToAction("Ayarlar");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}