using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FIT_Project_Manager.Models;
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
        string db_user = Environment.GetEnvironmentVariable("POSTGERS_USER");
        string db_password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        string db_port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
        string connect_query = $"Server={db_contname};Database={db_name};Port={db_port};User ID={db_user};Password={db_password};Enlist=true";
        return connect_query;
    }

    public IActionResult Index()
    {
        string user_name;
        int user_id;
        using (TransactionScope ts = new TransactionScope())
        {
            using (NpgsqlConnection connect = new NpgsqlConnection(GetDBConnectInfo()))
            {
                connect.Open();
                string cmd = $@"
                    SELECT 
                        id
                        ,name
                    FROM
                        users
                    WHERE
                        id == 1;
                ";
                DataTable dt = new DataTable();
                var cmd_obj = new NpgsqlCommand(cmd, connect);
                var data = new NpgsqlDataAdapter(cmd_obj);
                data.Fill(dt);
                user_name = $"{dt.Rows[0][1]}";
                user_id = (int)dt.Rows[0][0];
            }
            ts.Complete();
        }

        var viewModel = new HomeViewModel()
        {
            Id=user_id,
            Name=user_name
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
