using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FIT_Project_Manager.Models;

namespace FIT_Project_Manager.Controllers;

public class RecordController : Controller
{
    private readonly ILogger<RecordController> _logger;

    public RecordController(ILogger<RecordController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        return View("./Views/Record/Index.cshtml");
    }

}