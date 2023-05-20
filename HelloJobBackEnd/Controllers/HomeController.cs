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

        public HomeController(HelloJobDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string search)
        {

            IQueryable<Vacans> allVacans = _context.Vacans.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.Company).
              Include(c => c.Company).
                ThenInclude(x => x.User).
              Include(c => c.BusinessArea).
                Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
                     Include(x => x.WishListItems).ThenInclude(wt => wt.WishList).
               Include(x => x.WishListItems).ThenInclude(wt => wt.WishList.User).
              Where(c => c.Status == OrderStatus.Accepted && c.Company.Status == OrderStatus.Accepted);

            ViewBag.Company = _context.Companies
                    .Include(v => v.Vacans)
                    .OrderByDescending(c => c.Vacans.Count)
                        .Take(4).Where(x => x.Status == OrderStatus.Accepted)
                        .ToList();
            if (!string.IsNullOrEmpty(search))
            {
                allVacans = allVacans.Where(c => c.Position.Contains(search));

            }
            List<Vacans> vacans = allVacans.ToList();
            return View(vacans);
        }

        public IActionResult Search(string search)
        {
            IQueryable<Vacans> query = _context.Vacans.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.Company).
              Include(c => c.Company).
                ThenInclude(x => x.User).
              Include(c => c.BusinessArea).
                Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
              Include(o => o.OperatingMode).AsQueryable().Where(x => x.Position.Contains(search));
            List<Vacans> vacans = query.OrderByDescending(x => x.Id).Take(3).Where(c => c.Status == OrderStatus.Accepted).ToList();
            return PartialView("_SearchvacansPartial", vacans);
        }
    }
}
