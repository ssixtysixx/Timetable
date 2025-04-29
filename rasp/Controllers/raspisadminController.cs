using Microsoft.AspNetCore.Mvc;

namespace rasp.Controllers
{
    public class raspisadminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
