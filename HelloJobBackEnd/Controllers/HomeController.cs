using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloJobBackEnd.Controllers
{
	public class HomeController : Controller
	{
        private readonly HelloJobDbContext _context;

        public HomeController(HelloJobDbContext context )
        {
            _context = context;
        }
        public IActionResult Index(string search)
		{
            List<Vacans>  vacans= _context.Vacans.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.Company).
              Include(c => c.Company).
                ThenInclude(x => x.User).
              Include(c => c.BusinessArea).
                Include(c => c.BusinessArea).ThenInclude(b=>b.BusinessTitle).
              Include(o => o.OperatingMode).Where(c => c.Status == OrderStatus.Accepted).ToList();

            ViewBag.Company= _context.Companies
                    .Include(v => v.Vacans)
                    .OrderByDescending(c => c.Vacans.Count)
                        .Take(4)
                        .ToList();

         
            if (search is not null )
            {
                List<Vacans> findedVacans = _context.Vacans.Include(v => v.BusinessArea).
             Include(e => e.Education).
             Include(e => e.Experience).
             Include(c => c.City).
             Include(c => c.Company).
             Include(c => c.Company).
               ThenInclude(x => x.User).
             Include(c => c.BusinessArea).
               Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
             Include(o => o.OperatingMode).Where(c => c.Position.Contains(search) && c.Status == OrderStatus.Accepted).ToList();
                return View(findedVacans);
            }


            return View(vacans);
		}

        public IActionResult Search(string search)
        {
            var query = _context.Vacans.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.Company).
              Include(c => c.Company).
                ThenInclude(x => x.User).
              Include(c => c.BusinessArea).
                Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
              Include(o => o.OperatingMode).AsQueryable().Where(x => x.Position.Contains(search));
            List<Vacans> vacans = query.OrderByDescending(x => x.Id).Take(3).Where(c=>c.Status==OrderStatus.Accepted).ToList();
            return PartialView("_SearchvacansPartial", vacans);
        }
    }
}
