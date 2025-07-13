using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenTable.Models.DataLayer;
using OpenTable.Models.DomainModels;

namespace OpenTable.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class MetropolisController : Controller
    {
        private readonly OpenTableContext _context;

        public MetropolisController(OpenTableContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Metropolises.ToList());
        }
    
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var metro = _context.Metropolises.Find(id);
            return View(metro);
        }

       
        //[HttpPost]
        //public  IActionResult DeleteConfirmed(Metropolis metro)
        //{
        //    _context.Metropolises.Remove(metro);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public IActionResult DeleteConfirmed(Metropolis metropolis)
        {
            var relatedRestaurants = _context.Restaurants
                .FirstOrDefault(r => r.MetropolisId == metropolis.Id);

            if (relatedRestaurants != null)
            {
                TempData["Message"] = $"Can't delete {metropolis.Name} " +
                                           "because it has related Restaurants.";
                return RedirectToAction("Index");
            }
            else
            {
                _context.Metropolises.Remove(metropolis);
                _context.SaveChanges();
                TempData["Message"] = $"{metropolis.Name} Deleted Successfully";
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Operation = "Add";
            return View("AddEdit", new Metropolis());
        }
        
        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Operation = "Edit";
            var metropolis = _context.Metropolises.Find(id);
            return View("AddEdit", metropolis);
        }
        [HttpPost]
        public IActionResult Add(Metropolis model)
        {
            bool isAdd = model.Id == 0;

            if (ModelState.IsValid) {
                if (isAdd)
                    _context.Metropolises.Add(model);
                else
                    _context.Metropolises.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            string operation = (isAdd) ? "Add" : "Edit";
            ViewBag.Operation = operation;
            return View("AddEdit", model);
        }
    }
}
