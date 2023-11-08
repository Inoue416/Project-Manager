using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FIT_Project_Manager.Models;
using FIT_Project_Manager.Sessionlib;
using Microsoft.AspNetCore.Http;

namespace FIT_Project_Manager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        string userIdKey = SessionHandler.IdAccessKey;
        string usernameKey = SessionHandler.NameAccessKey;
        if (string.IsNullOrEmpty(SessionHandler.Get(HttpContext.Session, userIdKey)))
        {
            SessionHandler.Set(HttpContext.Session, userIdKey, "0");
        }
        Console.WriteLine($"Before: {SessionHandler.Get(HttpContext.Session, userIdKey)}");
        SessionHandler.ClearSession(HttpContext.Session);
        Console.WriteLine($"After: {SessionHandler.Get(HttpContext.Session, userIdKey)}");
        return View("./Views/Home/Index.cshtml");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
