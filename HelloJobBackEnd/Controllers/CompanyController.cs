using Microsoft.AspNetCore.Mvc;

namespace HelloJobBackEnd.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult VacansDetail()
        {
            return View();
        }
    }
}
