using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models.DataLayer;
using OpenTable.Models.DomainModels;
using OpenTable.Models.ExtensionMethods;

namespace OpenTableMVC.Areas.Main.Controllers
{
    public class ReservationController : Controller
    {
        private readonly OpenTableContext _context;

        public ReservationController(OpenTableContext context)
        {
            _context = context;
        }

        public IActionResult List()
        {
            var reservations = _context.Reservations
                .Include(r => r.Restaurant)
                .Where(r => r.Status == ReservationType.Final)
                .OrderByDescending(r => r.Date)
                .ThenBy(r => r.TimeSlot)
                .ToList();

            return View(reservations);        
        }

        public IActionResult Add()
        {
            return Content("Area: Main, Controller: Reservation, Action: Add");
        }

        public IActionResult Update()
        {
            return Content("Area: Main, Controller: Reservation, Action: Update");
        }

        public IActionResult Delete()
        {
            return Content("Area: Main, Controller: Reservation, Action: Delete");
        }
        
        
        [HttpGet]
        public IActionResult Confirm()
        {
            var session = new ReservationSession(HttpContext.Session);
            var reservationIds = session.GetCartIds(); // new method

            if (reservationIds == null || reservationIds.Count == 0)
            {
                TempData["ErrorMessage"] = "Your reservation cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var reservations = _context.Reservations
                .Include(r => r.Restaurant)
                .Where(r => reservationIds.Contains(r.Id))
                .ToList();

            return View(reservations);
        }

        [HttpPost]
        public IActionResult Reserve()
        {
            var session = new ReservationSession(HttpContext.Session);
            var ids = session.GetCartIds();

            if (ids == null || ids.Count == 0)
            {
                TempData["ErrorMessage"] = "Your reservation cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var reservations = _context.Reservations
                .Where(r => ids.Contains(r.Id) && r.Status == ReservationType.Hold)
                .ToList();

            foreach (var r in reservations)
            {
                r.Status = ReservationType.Final;
                _context.Reservations.Update(r);
            }

            _context.SaveChanges();

            session.ClearCart();
            new ReservationCookies(Response.Cookies).RemoveReservationKeys();

            TempData["Message"] = "All reservations have been confirmed!";
            return RedirectToAction("Index", "Home");
        }

    }
}
