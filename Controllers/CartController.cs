using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models.DataLayer;
using OpenTable.Models.DomainModels;
using OpenTable.Models.ExtensionMethods;
using OpenTable.Models.ViewModels;

namespace OpenTable.Controllers
{
    public class CartController : Controller
    {
        private readonly OpenTableContext _context;

        public CartController(OpenTableContext context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            var session = new ReservationSession(HttpContext.Session);
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
        
        [HttpPost]
        public IActionResult Add(int restaurantId, DateTime date, string timeSlot, int partySize)
        {
            var reservation = new Reservation
            {
                RestaurantId = restaurantId,
                Date = date.Date,
                TimeSlot = timeSlot,
                PartySize = partySize
            };
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            
            var session = new ReservationSession(HttpContext.Session);
            var cookies = new ReservationCookies(Response.Cookies);
            var cart = session.GetCartIds();
            cart.Add(reservation.Id);
            session.SetCartIds(cart);
            cookies.SetReservationIds(cart);

            TempData["Message"] = "Reservation added to cart.";
            // redirect to Home page
            return RedirectToAction("Index", "Restaurant", 
                new
                {
                    ActiveMetropolis = session.GetActiveMetropolis(),
                    ActiveCuisine = session.GetActiveCuisine(),
                    ActivePriceRange = session.GetActivePrice(),
                });
            
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var reservation = _context.Reservations
                .Include(r => r.Restaurant)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null || reservation.Status != ReservationType.Hold)
            {
                return RedirectToAction("Index");
            }

            return View(reservation);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var session = new ReservationSession(HttpContext.Session);
            var ids = session.GetCartIds();

            if (ids.Contains(id))
            {
                var reservation = _context.Reservations.Find(id);

                if (reservation != null && reservation.Status == ReservationType.Hold)
                {
                    _context.Reservations.Remove(reservation);
                    _context.SaveChanges();
                }

                ids.Remove(id);
                session.SetCartIds(ids);
                new ReservationCookies(Response.Cookies).SetReservationIds(ids);

                TempData["Message"] = "Reservation removed from cart.";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Clear()
        {
            var session = new ReservationSession(HttpContext.Session);
            session.ClearCart();
            
            var cookies = new ReservationCookies(Response.Cookies);
            cookies.RemoveReservationKeys();

            TempData["Message"] = "Reservation cart cleared.";
            return RedirectToAction("Index", "Home");
        }
    }
}