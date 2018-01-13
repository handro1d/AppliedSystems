using System.Web.Mvc;

namespace AppliedSystems.Web.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("")]
        [HttpGet, AllowAnonymous]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}