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

namespace FIT_Project_Manager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public static string GetDBConnectInfo()
    {
        string db_contname = Environment.GetEnvironmentVariable("DB_CONTNAME");
        string db_name = Environment.GetEnvironmentVariable("POSTGRES_DB");
        string db_user = Environment.GetEnvironmentVariable("POSTGRES_USER");
        string db_password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        string db_port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
        var connectionString = $"Host={db_contname};Port={db_port};Username={db_user};Password={db_password};Database={db_name}";
        // var connectionString = "Host=db;Port=5432;Username=fit_staff;Password=password;Database=fit_db";
        // string connect_query = $"Server={db_contname}; Database={db_name}; Port={db_port}; User ID={db_user}; Password={db_password};";
        // Console.WriteLine(connectionString2);
        // Console.WriteLine(connectionString);
        return connectionString;
    }

    public async void GetAsyncUserData()
    {
        string connectionString = GetDBConnectInfo();
        await using var dataSource = NpgsqlDataSource.Create(connectionString);
        await using var connection = await dataSource.OpenConnectionAsync();
        Console.WriteLine("Success...");
    }

    public async Task<IActionResult> Index()
    {
        GetAsyncUserData();   
        var viewModel = new HomeViewModel()
        {
            Id=0,
            Name="sample"
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
