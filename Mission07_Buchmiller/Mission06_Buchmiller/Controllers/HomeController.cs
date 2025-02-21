using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission06_Buchmiller.Models;

namespace Mission06_Buchmiller.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
}
