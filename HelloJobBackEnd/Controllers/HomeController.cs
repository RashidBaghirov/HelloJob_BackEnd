using Microsoft.AspNetCore.Mvc;

namespace HelloJobBackEnd.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
