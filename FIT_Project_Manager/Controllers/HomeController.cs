using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FIT_Project_Manager.Models;
using FIT_Project_Manager.Sessionlib;
using Microsoft.AspNetCore.Http;
using FIT_Project_Manager.SQLlib;

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
        HomeViewModel? view_model = new HomeViewModel();
        DataBaseHandler db_handler = new DataBaseHandler();
        if (SessionHandler.IsLogin(HttpContext.Session))
        {
            Console.WriteLine("Get Record Data.");
            var response = await db_handler.GetTodayRecordDataAsync(
                Int32.Parse(
                    SessionHandler.Get(HttpContext.Session, SessionHandler.IdAccessKey)
                )
            );
            view_model.RecordData = response.RecordData;
            if (view_model.RecordData.Count == 0)
            {
                Console.WriteLine("Have not today record.");
            }
            Console.WriteLine(response.Message);
        }
        else
        {
            view_model.RecordData = null;
        }
        Console.WriteLine($"view model: {view_model.RecordData}");
       
        return View("./Views/Home/Index.cshtml", view_model);
    }

    [HttpGet]
    public async Task<IActionResult> SetSession()
    {
        string userIdKey = SessionHandler.IdAccessKey;
        string usernameKey = SessionHandler.NameAccessKey;
        if (string.IsNullOrEmpty(SessionHandler.Get(HttpContext.Session, userIdKey)))
        {
            SessionHandler.Set(HttpContext.Session, userIdKey, "2");
            SessionHandler.Set(HttpContext.Session, usernameKey, "井上優也");
        }
        Console.WriteLine($"UserID: {SessionHandler.Get(HttpContext.Session, userIdKey)}");
        Console.WriteLine($"UserName: {SessionHandler.Get(HttpContext.Session, usernameKey)}");
        return Redirect("/");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
