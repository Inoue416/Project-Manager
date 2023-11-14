using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FIT_Project_Manager.Models;
using Microsoft.AspNetCore.Components.Web;
using FIT_Project_Manager.SQLlib;
using FIT_Project_Manager.Sessionlib;

namespace FIT_Project_Manager.Controllers;

public class RecordController : Controller
{
    private readonly ILogger<RecordController> _logger;

    public RecordController(ILogger<RecordController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!SessionHandler.IsLogin(HttpContext.Session))
        {
            return Redirect("/");
        }
        RecordViewModel view_model = new RecordViewModel()
        { 
            Title = "",
            Content = ""
        };
        return View("./Views/Record/Index.cshtml", view_model);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterRecord(RecordViewModel view_model)
    {
        if (!SessionHandler.IsLogin(HttpContext.Session))
        {
            return Redirect("/");
        }
        // TODO: サニタイズを行う, トランザクションを入れたい
        List<string> values = new List<string>();
        values.Add(view_model.Title);
        values.Add(view_model.Content);
        values.Add("0");
        DataBaseHandler db_handler = new DataBaseHandler();
        var response = await db_handler.InsertRecord(values, Int32.Parse(SessionHandler.Get(HttpContext.Session, SessionHandler.IdAccessKey)));
        Console.WriteLine($"Response Message: {response.Message}");
        if (response.Flag)
        {
            TempData["ResponseMessage"] = "記録しました。";
            TempData["ResponseClass"] = "alert alert-success";
            return Redirect("/");
        }
        TempData["ResponseMessage"] = "エラーが発生しました。";
        TempData["ResponseClass"] = "alert alert-danger";
        return View("./Views/Record/Index.cshtml", view_model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}