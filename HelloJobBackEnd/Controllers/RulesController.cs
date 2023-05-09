using Microsoft.AspNetCore.Mvc;

namespace HelloJobBackEnd.Controllers
{
    public class RulesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
