using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models.DataLayer;
using OpenTable.Models.DomainModels;
using OpenTable.Models.ViewModels;
using OpenTable.Models.ExtensionMethods;
using OpenTable.Models.Grid;
using OpenTable.Models.DataLayer.Repositories;


namespace OpenTable.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly OpenTableContext _context;
        private Repository<Restaurant> restaurantss { get; set; }
        public RestaurantController(OpenTableContext context)
        {
            restaurantss = new Repository<Restaurant>(context);
            _context = context;
        }


        public IActionResult Index()
        {
            var session = new ReservationSession(HttpContext.Session);
            return RedirectToAction("List", 
                new
                {
                    ActiveMetropolis = session.GetActiveMetropolis(),
                    ActiveCuisine = session.GetActiveCuisine(),
                    ActivePriceRange = session.GetActivePrice(),
                });
        }

        //[Route("[controller]s/{id?}")]
        //public IActionResult List([FromQuery] RestaurantsViewModel model)
        //{
        //    var session = new ReservationSession(HttpContext.Session);
        //    session.SetActiveMetropolis(model.ActiveMetropolis);
        //    session.SetActivePrice(model.ActivePriceRange);
        //    session.SetActiveCuisine(model.ActiveCuisine);

        //    model.Metropolises = _context.Metropolises.ToList();
        //    model.PriceRanges = _context.Prices.ToList();
        //    model.Cuisines = _context.Cuisines.ToList();
            
        //    // get teams from database - filter by Metropolis, price range and cuisine
        //    IQueryable<Restaurant> query = _context.Restaurants.OrderBy(t => t.Id);
        //    if (model.ActiveMetropolis != "all")
        //        query = query.Where(
        //            t => t.Metropolis.Name.ToLower() == model.ActiveMetropolis.ToLower());
        //    if (model.ActivePriceRange != "all")
        //        query = query.Where(
        //            t => t.Price.Name.ToLower() == model.ActivePriceRange.ToLower());
        //    if (model.ActiveCuisine != "all")
        //        query = query.Where(
        //            t => t.Cuisine.Name.ToLower() == model.ActiveCuisine.ToLower());
        //    model.Restaurants = query.ToList();

        //    return View(model);
        //}
        //[Route("[controller]s/{id?}")]
        public ViewResult List(RestaurantGridData values)
        {
            // create options for querying books
            var options = new QueryOptions<Restaurant>
            {
                Includes = "Metropolis, Price",
                OrderByDirection = values.SortDirection,
                PageNumber = values.PageNumber,
                PageSize = values.PageSize
            };
            if (values.SortByMetropolis)
                options.OrderBy = b => b.Metropolis.Name;
            else if (values.SortByPriceRange)
                options.OrderBy = b => b.Price.Name;
            else
                options.OrderBy = b => b.Name;

            // create view model
            var vm = new RestaurantsViewModel
            {
                Restaurantss = restaurantss.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(restaurantss.Count)
            };

            return View(vm);
        }
        public IActionResult Details(int id, DateTime? date)
        {
            var session = new ReservationSession(HttpContext.Session);
            var restaurant = _context.Restaurants
                .Include(r => r.Metropolis)
                .Include(r => r.Price)
                .Include(r => r.Cuisine)
                .FirstOrDefault(r => r.Id == id);

            var model = new RestaurantsViewModel
            {
                Restaurant = restaurant,
                ActiveCuisine = session.GetActiveCuisine(),
                ActiveMetropolis = session.GetActiveMetropolis(),
                ActivePriceRange = session.GetActivePrice()
            };

            DateTime selectedDate = date ?? DateTime.Today;

            var reservedSlots = _context.Reservations
                .Where(r => r.RestaurantId == id && r.Date == selectedDate && r.Status == ReservationType.Final)
                .Select(r => r.TimeSlot)
                .ToList();

            var availableSlots = ParseAvailableTimeSlots(restaurant.OpenHours, reservedSlots);

            ViewBag.AvailableSlots = availableSlots;
            ViewBag.SelectedDate = selectedDate;

            return View(model);
        }

        private List<string> ParseAvailableTimeSlots(string openHours, List<string> reservedSlots)
        {
            List<string> slots = new();

            var ranges = openHours.Split(';');
            foreach (var range in ranges)
            {
                var times = range.Split('-');
                if (times.Length != 2) continue;
                if (TimeSpan.TryParse(times[0], out var start) &&
                    TimeSpan.TryParse(times[1], out var end))
                {
                    for (var time = start; time < end; time = time.Add(TimeSpan.FromHours(1)))
                    {
                        string formatted = time.ToString(@"hh\:mm");
                        if (!reservedSlots.Contains(formatted))
                        {
                            slots.Add(formatted);
                        }
                    }
                }
            }
            return slots;
        }
    }
}