using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloJobBackEnd.Controllers
{
	public class CvPageController : Controller
	{
        private readonly HelloJobDbContext _context;

        public CvPageController(HelloJobDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
            List<Cv> cvs = _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).
              Include(o => o.OperatingMode).
              Include(x => x.User).Where(c => c.Status == OrderStatus.Accepted).ToList();

            ViewBag.Education = _context.Educations.ToList();
            ViewBag.Experince = _context.Experiences.ToList();
            ViewBag.Mode = _context.OperatingModes.ToList();
            ViewBag.Business = _context.BusinessArea.Include(b => b.BusinessTitle).Include(b => b.Cvs).ToList();

            return View(cvs);
		}

        public IActionResult Detail()
        {
            return View();
        }
    }
}
