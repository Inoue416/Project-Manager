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

    public class UserData
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
    }
    public async Task<List<UserData>> GetAsyncUserData()
    {
        string connectionString = GetDBConnectInfo();
        await using var dataSource = NpgsqlDataSource.Create(connectionString);
        await using var connection = await dataSource.OpenConnectionAsync();
        await using var command = new NpgsqlCommand("SELECT id, name FROM users WHERE id = ($1)", connection)
        {
            Parameters = 
            {
                new() { Value = 1 }
            }
        };

        List<UserData> user_data = new List<UserData>();

        await using (var reader = await command.ExecuteReaderAsync())
        {
            while(await reader.ReadAsync())
            {
                string uname = reader.GetString("name");
                int uid = int.Parse(reader.GetValue("id").ToString());
                user_data.Add(new UserData() {UserName=uname, UserId=uid});
            }
        }
        // for (int i=0; i < reader.FieldCount; i++)
        // {
        //     Console.WriteLine($"{reader[i]}");
        // }
        Console.WriteLine("Success...");
        return user_data;
    }

    public async Task<IActionResult> Index()
    {
        List<UserData> user_data = await GetAsyncUserData();
        var viewModel = new HomeViewModel()
        {
            Id=user_data[0].UserId,
            Name=user_data[0].UserName
        };
        return View("./Views/Home/Index.cshtml", viewModel);
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
