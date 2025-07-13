using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models;
using OpenTable.Models.DataLayer;
using OpenTable.Models.ExtensionMethods;
using OpenTable.Models.ViewModels;

namespace OpenTable.Controllers;

public class HomeController : Controller
{

    public ActionResult About()
    {
        return Content("Area: Main, Controller: Home, Action: About");
    }

    public ActionResult Contact()
    {
        return Content("Area: Main, Controller: Home, Action: Contact");
    }

    public ActionResult Privacy()
    {
        return Content("Area: Main, Controller: Home, Action: Privacy");
    }

    public ActionResult TermsOfUse()
    {
        return Content("Area: Main, Controller: Home, Action: TermsOfUse");
    }

    public ActionResult CookiePolicy()
    {
        return Content("Area: Main, Controller: Home, Action: CookiePolicy");
    }
    
    public ActionResult Mobile()
    {
        return Content("Area: Main, Controller: Home, Action: Mobile");
    }
    public ActionResult FAQs()
    {
        return Content("Area: Main, Controller: Home, Action: FAQs");
    }
    
    private readonly OpenTableContext _context;

    public HomeController(OpenTableContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var session = new ReservationSession(HttpContext.Session);
        int? count = session.GetCartCount();

        // If session cart is empty, restore it using cookie
        if (!count.HasValue || count == 0)
        {
            var cookies = new ReservationCookies(Request.Cookies);
            var reservationIds = cookies.GetReservationIds(); 

            if (reservationIds.Any())
            {
                session.SetCartIds(reservationIds); 
            }
        }

        // Load full reservation details to display
        var ids = session.GetCartIds();
        var reservations = _context.Reservations
            .Include(r => r.Restaurant)
            .Where(r => ids.Contains(r.Id))
            .ToList();

        var model = new RestaurantsViewModel
        {
            Reservations = reservations
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}