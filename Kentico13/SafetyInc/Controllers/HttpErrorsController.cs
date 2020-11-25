using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SafetyInc.Controllers
{
    public class HttpErrorsController : Controller
    {
        [AllowAnonymous]
        public IActionResult Error(int code)
        {
            if (code == 404)
            {
                return View("NotFound");
            }

            return StatusCode(code);
        }
    }
}