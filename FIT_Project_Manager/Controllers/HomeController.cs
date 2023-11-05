using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FIT_Project_Manager.Models;
// TODO: 後にSQLの処理を行うクラスを別で作りたい
using System.Linq;
using System.Data.SqlClient;

namespace FIT_Project_Manager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string db_contname = Environment.GetEnvironmentVariable("POSTGRES_DB");
        string db_user = Environment.GetEnvironmentVariable("POSTGERS_USER");
        string db_password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        // string constr = @"Data Source=192.168.0.1;Initial Catalog=iPentecSandBox;Connect Timeout=60;Persist Security Info=True;User ID=sa;Password=saPassword";
        string sql = $@"
            SELECT 
                id
                ,name
            FROM
                users;
        ";
        var viewModel = new HomeViewModel()
        {
            Id=0,
            Name=db_contname
        };
        return View("~/Views/Home/Index.cshtml", viewModel);
    }

    public IActionResult Privacy()
    {
        return View("~/Views/Home/Privacy.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
