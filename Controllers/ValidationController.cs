using Microsoft.AspNetCore.Mvc;
using OpenTable.Models.DataLayer;
using OpenTable.Models.Validations;

namespace Registration.Controllers
{
    public class ValidationController : Controller
    {
        private OpenTableContext context;
        public ValidationController(OpenTableContext ctx) => context = ctx;

        public JsonResult CheckEmail(string email)
        {
            string msg = Check.EmailExists(context, email);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okEmail"] = true;
                return Json(true);
            }
            else return Json(msg);
        }
    }
}
