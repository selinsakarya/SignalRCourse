using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRExample.Constants;
using SignalRExample.Data;
using SignalRExample.Hubs;

namespace SignalRExample.Controllers;

public class HomeController : Controller
{
    private readonly IHubContext<DeathlyHallowsHub> _deathlyHallowsHub;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IHubContext<OrderHub> _orderHub;

    public HomeController(
        IHubContext<DeathlyHallowsHub> deathlyHallowsHub,
        ApplicationDbContext applicationDbContext,
        IHubContext<OrderHub> orderHub)
    {
        _deathlyHallowsHub = deathlyHallowsHub;
        _applicationDbContext = applicationDbContext;
        _orderHub = orderHub;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Privacy()
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

    public IActionResult Notification()
    {
        return View();
    }

    public IActionResult BasicChat()
    {
        return View();
    }
    
    [ActionName("Order")]
    public IActionResult GetOrder()
    {
        string[] names = { "Bhrugen", "Ben", "Jess", "Laura", "Ron" };
        string[] itemNames = { "Food1", "Food2", "Food3", "Food4", "Food5" };

        Random rand = new Random();

        int index = rand.Next(names.Length);

        Order order = new Order()
        {
            Name = names[index],
            ItemName = itemNames[index],
            Count = index
        };

        return View(order);
    }

    [ActionName("Order")]
    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order order)
    {
        _applicationDbContext.Orders.Add(order);

        await _applicationDbContext.SaveChangesAsync();

        await _orderHub.Clients.All.SendAsync("newOrderReceived");

        return RedirectToAction(nameof(Order));
    }
        
    [ActionName("OrderManagement")]
    public IActionResult OrderManagement()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Orders()
    {
        List<Order> productList = await _applicationDbContext.Orders.ToListAsync();
        
        return Json(new { Data = productList });
    }
}