using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using HelloJobBackEnd.Utilities.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloJobBackEnd.Controllers
{
    public class CompanyController : Controller
    {
        private readonly HelloJobDbContext _context;

        public CompanyController(HelloJobDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Company> companies = _context.Companies.Include(v => v.Vacans).ToList();
            return View(companies);
        }





        public IActionResult Detail(int id)
        {
            Company? company = _context.Companies.Include(v => v.Vacans).Include(b => b.Vacans).ThenInclude(b => b.BusinessArea).FirstOrDefault(x => x.Id == id);

            return View(company);
        }

        public IActionResult VacansDetail(int id)
        {
            IQueryable<Vacans> vacanss = _context.Vacans.AsNoTracking().AsQueryable();

            Vacans? vacans = _context.Vacans.Include(v => v.BusinessArea).
              Include(e => e.Education).
            Include(e => e.Experience).
            Include(c => c.City).
            Include(c => c.Company).
            Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
            Include(i => i.infoEmployeers).
             Include(i => i.InfoWorks).
            Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);
            vacans.Count++;
            _context.SaveChanges();
            ViewBag.Related = ExtensionMethods.RelatedByBusinessArea(vacanss, vacans, id);

            return View(vacans);
        }

    }
}
