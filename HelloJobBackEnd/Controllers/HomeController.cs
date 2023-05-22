using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Services.Interface;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly HelloJobDbContext _context;
        private readonly IVacansService _vacansService;
        private readonly ICompanyService _companyService;
        private readonly IBusinessTitleService _businessTitleService;


        public HomeController(HelloJobDbContext context, IVacansService vacansService, ICompanyService companyService, IBusinessTitleService businessTitleService)
        {
            _vacansService = vacansService;
            _companyService = companyService;
            _businessTitleService = businessTitleService;
            _context = context;
        }
        public IActionResult Index(string search, int titleid)
        {
            IQueryable<Vacans> allVacans = _vacansService.GetAcceptedVacansWithRelatedData();

            ViewBag.Company = _companyService.GetTopAcceptedCompaniesWithVacans(4).Where(x => x.Status == OrderStatus.Accepted).ToList();

            if (search is not null)
            {
                allVacans = allVacans.Where(c => c.Position.Contains(search));
            }
            else if (titleid != 0)
            {
                allVacans = allVacans.Where(c => c.BusinessArea.BusinessTitleId == titleid);
            }
            List<Vacans> vacans = allVacans.ToList();
            ViewBag.Titles = _businessTitleService.GetAllBusinessTitlesWithAreas();

            return View(vacans);
        }

        public IActionResult Search(string search)
        {
            IQueryable<Vacans> query = _vacansService.GetAcceptedVacansWithRelatedData()
                .Where(x => x.Position.Contains(search));

            List<Vacans> vacans = query.OrderByDescending(x => x.Id)
                .Take(3)
                .Where(c => c.Status == OrderStatus.Accepted)
                .ToList();

            return PartialView("_SearchvacansPartial", vacans);
        }


    }
}
