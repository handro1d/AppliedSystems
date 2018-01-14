using System.Web.Mvc;

namespace AppliedSystems.Web.Controllers
{
    [RoutePrefix("Error")]
    public class ErrorController : Controller
    {
        [Route("Internal")]
        [HttpGet, AllowAnonymous]
        public ActionResult Internal()
        {
            return View("Internal");
        }
    }
}