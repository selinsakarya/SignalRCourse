using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SignalRExample.Constants;
using SignalRExample.Models;

namespace SignalRExample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult UsersCount()
    {
        return View();
    }
    
    public IActionResult DeathlyHallowRace()
    {
        return View();
    }
    
    public async Task<IActionResult> DeathlyHallows(string type)
    {
        if (StaticData.DeathlyHallowRace.ContainsKey(type))
        {
            StaticData.DeathlyHallowRace[type] += 1;
        }
        
        return Accepted();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}