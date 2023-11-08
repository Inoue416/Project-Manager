using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FIT_Project_Manager.Models;
using System;
// TODO: 後にSQLの処理を行うクラスを別で作りたい
using System.Linq;
using Npgsql;
using Microsoft.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.Transactions;
using System.ComponentModel;

namespace FIT_Project_Manager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        return View("./Views/Home/Index.cshtml");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
