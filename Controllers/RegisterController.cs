using Microsoft.AspNetCore.Mvc;
using OpenTable.Models.DataLayer;
using OpenTable.Models.DomainModels;
using OpenTable.Models.Validations;

namespace OpenTable.Controllers
{
    public class RegisterController : Controller
    {
        private readonly OpenTableContext _context;

        public RegisterController(OpenTableContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(AppUser user)
        {
            if (TempData["okEmail"] == null)
            {
                string msg = Check.EmailExists(_context, user.Email);
                if (!String.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(user.Email), msg);
                }
            }
            // DOB server-side validation
            if (user.DOB == null)
            {
                ModelState.AddModelError(nameof(user.DOB), "Date of birth is required.");
            }
            else
            {
                var dob = user.DOB.Value;

                if (dob > DateTime.Today)
                {
                    ModelState.AddModelError(nameof(user.DOB), "Date of birth cannot be in the future.");
                }
                else
                {
                    int age = DateTime.Today.Year - dob.Year;
                    if (dob.Date > DateTime.Today.AddYears(-age)) age--;

                    if (age > 100)
                    {
                        ModelState.AddModelError(nameof(user.DOB), "Date of birth cannot be more than 100 years old.");
                    }
                }
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please fix the error.");
                return View(user);
            }
            user.CreatedAt = DateTime.Now;
            user.UserType = UserType.Client;

            _context.AppUsers.Add(user);
            _context.SaveChanges();
            
            TempData["Message"] = "Your registration is completed. Welcome to Open Table!";

            return RedirectToAction("Index", controllerName:"Home");
            
        }

        public IActionResult Welcome() => View();
    }
}
