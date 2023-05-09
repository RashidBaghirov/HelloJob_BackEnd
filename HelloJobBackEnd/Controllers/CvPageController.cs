using Microsoft.AspNetCore.Mvc;

namespace HelloJobBackEnd.Controllers
{
	public class CvPageController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

        public IActionResult Detail()
        {
            return View();
        }
    }
}
