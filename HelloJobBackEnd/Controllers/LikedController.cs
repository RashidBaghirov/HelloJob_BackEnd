using Microsoft.AspNetCore.Mvc;

namespace HelloJobBackEnd.Controllers
{
	public class LikedController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
