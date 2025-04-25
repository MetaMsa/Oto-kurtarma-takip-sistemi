using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using otokurtarma.Models;

namespace otokurtarma.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public string username;
    public string password;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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

    public void OnPost(string _username, string _password)
    {
        username = _username;
        password = _password;
    }
}
