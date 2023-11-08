using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FIT_Project_Manager.Sessionlib;

namespace FIT_Project_Manager.Controllers;

public class AuthController : Controller
{
    public AuthController()
    {}

    public async Task<IActionResult> LogOut()
    {
        Console.WriteLine("Hello LogOut");
        SessionHandler.ClearSession(HttpContext.Session);
        return Redirect("/");
    }
}