using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRExample.Constants;
using SignalRExample.Hubs;
using SignalRExample.Models;

namespace SignalRExample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHubContext<DeathlyHallowsHub> _deathlyHallowsHub;

    public HomeController(
        ILogger<HomeController> logger,
        IHubContext<DeathlyHallowsHub> deathlyHallowsHub)
    {
        _logger = logger;
        _deathlyHallowsHub = deathlyHallowsHub;
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

            int cloak = StaticData.DeathlyHallowRace[StaticData.Cloak];
            
            int stone = StaticData.DeathlyHallowRace[StaticData.Stone];
            
            int wand = StaticData.DeathlyHallowRace[StaticData.Wand];

            await _deathlyHallowsHub.Clients.All.SendAsync("updateDeathlyHallowsScores", cloak, stone, wand);
        }

        return Accepted();
    }
    
    public IActionResult HarryPotterHouse()
    {
        return View();
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