using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models.DataLayer;
using OpenTable.Models.DomainModels;
using OpenTable.Models.Utils;

namespace OpenTable.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class RestaurantController : Controller
    {
        private readonly OpenTableContext _context;

        public RestaurantController(OpenTableContext context)
        {
            _context = context;
        }
        
        public ViewResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Metropolises = _context.Metropolises.ToList();
            ViewBag.Prices = _context.Prices.ToList();
            ViewBag.Cuisines = _context.Cuisines.ToList();

            // get open tasks from database based on current filters
            IQueryable<Restaurant> query = _context.Restaurants
                .Include(t => t.Metropolis)
                .Include(t => t.Price)
                .Include(t=>t.Cuisine);

            if (filters.HasMetropolis) {
                query = query.Where(t => t.MetropolisId == filters.MetropolisId);
            }
            if (filters.HasPrice) {
                query = query.Where(t => t.PriceId == filters.PriceId);
            }
            if (filters.HasCuisine) {
                query = query.Where(t => t.CuisineId == filters.CuisineId);
            }
            var restaurants = query.ToList();
            return View(restaurants);
        }
        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        public IActionResult Delete(int id)
        {
            var restaurant = _context.Restaurants
                .Include(r => r.Metropolis)
                .FirstOrDefault(r => r.Id == id);
            if (restaurant == null) return NotFound();
            return View(restaurant);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var restaurant =  _context.Restaurants.Find(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                 _context.SaveChanges();
                TempData["Message"] = "Restaurant deleted successfully!";
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Metropolises = _context.Metropolises.ToList();
            ViewBag.Prices = _context.Prices.ToList();
            ViewBag.Cuisines = _context.Cuisines.ToList();
            ViewBag.Operation = "Add";
            return View("AddEdit", new Restaurant());
        }
        
        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Metropolises = _context.Metropolises.ToList();
            ViewBag.Prices = _context.Prices.ToList();
            ViewBag.Cuisines = _context.Cuisines.ToList();
            ViewBag.Operation = "Edit";
            var restaurant = _context.Restaurants.Find(id);
            return View("AddEdit", restaurant);
        }

[HttpPost]
public IActionResult AddEdit(Restaurant model, IFormFile? LogoFile)
{
    if (!ModelState.IsValid)
    {
        ViewBag.Metropolises = _context.Metropolises.ToList();
        ViewBag.Cuisines = _context.Cuisines.ToList();
        ViewBag.Prices = _context.Prices.ToList();
        ViewBag.Operation = model.Id == 0 ? "Add" : "Edit";
        return View(model);
    }
    var imgDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

    if (!Directory.Exists(imgDirectory))
    {
        Directory.CreateDirectory(imgDirectory);
    }

    var existingRestaurant =  _context.Restaurants.AsNoTracking()
                                .FirstOrDefault(r => r.Id == model.Id);

    string oldFilePath = existingRestaurant?.LogoPath;

    if (LogoFile != null && LogoFile.Length > 0)
    {
        if (model.Id == 0)
        {
            _context.Restaurants.Add(model);
             _context.SaveChanges(); // Save first to generate ID
        }

        var fileName = $"{model.Id}.jpg";
        var filePath = Path.Combine(imgDirectory, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
             LogoFile.CopyToAsync(stream);
        }

        model.LogoPath = "images/" + fileName;
    }
    else if (existingRestaurant != null)
    {
        model.LogoPath = oldFilePath;  
    }
    else
    {
        model.LogoPath = "images/default.jpg"; 
    }

    if (model.Id == 0)
    {
        _context.Restaurants.Add(model);
        TempData["Message"] = "Restaurant added successfully!";
    }
    else
    {
        _context.Restaurants.Update(model);
        TempData["Message"] = "Restaurant updated successfully!";
    }

_context.SaveChanges();
return RedirectToAction("Index");

}
    }
}
