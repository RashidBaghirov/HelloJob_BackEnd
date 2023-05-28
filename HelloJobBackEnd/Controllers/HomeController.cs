using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Services.Interface;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        public IActionResult Index(int page = 1)
        {
            IQueryable<Vacans> allVacans = _vacansService.GetAcceptedVacansWithRelatedData();

            ViewBag.Company = _companyService.GetTopAcceptedCompaniesWithVacans(4).Where(x => x.Status == OrderStatus.Accepted).ToList();

            ViewBag.TotalPage = Math.Ceiling((double)_context.Vacans.Count() / 9);
            ViewBag.CurrentPage = page;

            List<Vacans> vacans = allVacans.AsNoTracking().Skip((page - 1) * 9).Where(x => x.Status == OrderStatus.Accepted).Take(9).ToList();
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

        public IActionResult Sorted(int titleid)
        {
            IQueryable<Vacans> allVacans = _vacansService.GetAcceptedVacansWithRelatedData();

            List<Vacans> sortvacans = allVacans.Where(c => c.BusinessArea.BusinessTitleId == titleid).ToList();

            return PartialView("_HomePartial", sortvacans);
        }

        public IActionResult SearchResult(string search)
        {
            IQueryable<Vacans> allVacans = _vacansService.GetAcceptedVacansWithRelatedData();

            if (search is not null)
            {
                allVacans = allVacans.Where(c => c.Position.Contains(search));
            }
            else
            {
                allVacans = allVacans;
            }
            List<Vacans> searching = allVacans.ToList();

            return PartialView("_HomePartial", searching);

        }

        public ActionResult Subscribe(string email)
        {
            TempData["Subscribe"] = false;
            bool Isdublicate = _context.Subscribe.Any(c => c.Email == email);

            if (Isdublicate)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            Subscribe subscribe = new()
            {
                Email = email
            };
            _context.Subscribe.Add(subscribe);
            _context.SaveChanges();
            TempData["Subscribe"] = true;
            return Redirect(Request.Headers["Referer"].ToString());
        }


    }



}
