using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenTable.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]

    public class HomeController : Controller
    {
       
        public ActionResult About()
        {
            return Content("Area: Admin, Controller: Home, Action: About");
        }

        public ActionResult Contact()
        {
            return Content("Area: Admin, Controller: Home, Action: Contact");
        }

        public ActionResult Privacy()
        {
            return Content("Area: Admin, Controller: Home, Action: Privacy");
        }

        public ActionResult TermsOfUse()
        {
            return Content("Area: Admin, Controller: Home, Action: TermsOfUse");
        }

        public ActionResult CookiePolicy()
        {
            return Content("Area: Admin, Controller: Home, Action: CookiePolicy");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}