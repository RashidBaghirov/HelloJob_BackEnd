using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Services.Interface;
using HelloJobBackEnd.Utilities.Enum;
using HelloJobBackEnd.Utilities.Extension;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloJobBackEnd.Controllers
{
    public class CompanyController : Controller
    {
        private readonly HelloJobDbContext _context;
        private readonly UserManager<User> _usermanager;
        private readonly IVacansService _vacansService;
        private readonly ICompanyService _companyService;

        public CompanyController(HelloJobDbContext context, UserManager<User> usermanager, IVacansService vacansService, ICompanyService companyService)
        {
            _context = context;
            _usermanager = usermanager;
            _vacansService = vacansService;
            _companyService = companyService;
        }
        public IActionResult Index()
        {
            List<Company> companies = _companyService.GetTopAcceptedCompaniesWithVacans().Where(x => x.Status == OrderStatus.Accepted).ToList();
            return View(companies);
        }

        public IActionResult Detail(int id)
        {
            Company? company = _companyService.GetCompanyWithVacansById(id);

            return View(company);
        }

        public async Task<IActionResult> VacansDetail(int id)
        {
            IQueryable<Vacans> vacanss = _vacansService.GetAcceptedVacansWithRelatedData();
            if (User.Identity.IsAuthenticated)
            {
                User user = await _usermanager.FindByNameAsync(User.Identity.Name);
                if (user is null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Cvs = _context.Cvs.Where(x => x.UserId == user.Id && x.Status == OrderStatus.Accepted).ToList();
                ViewBag.Setting = _context.Settings.ToDictionary(s => s.Key, s => s.Value);

            }
            ViewBag.Setting = _context.Settings.ToDictionary(s => s.Key, s => s.Value);
            Vacans? vacans = vacanss.FirstOrDefault(x => x.Id == id);
            vacans.Count++;
            _context.SaveChanges();
            ViewBag.Related = ExtensionMethods.RelatedByBusinessArea(vacanss, vacans, id);
            return View(vacans);
        }

        [HttpPost]
        public async Task<IActionResult> VacansDetail(int id, int cvsID)
        {
            TempData["Request"] = false;
            IQueryable<Vacans> vacanss = _vacansService.GetAcceptedVacansWithRelatedData();
            if (User.Identity.IsAuthenticated)
            {
                User user = await _usermanager.FindByNameAsync(User.Identity.Name);
                if (user is null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Cvs = _context.Cvs.Where(x => x.UserId == user.Id && x.Status == OrderStatus.Accepted).ToList();
                ViewBag.Setting = _context.Settings.ToDictionary(s => s.Key, s => s.Value);
                Request userRequest = _context.Requests.Include(r => r.RequestItems).FirstOrDefault(r => r.UserId == user.Id);
                if (userRequest != null)
                {
                    bool cvRequestExists = userRequest.RequestItems.Any(ri => ri.CvId == cvsID && ri.VacansId == id);
                    if (!cvRequestExists)
                    {
                        RequestItem newRequestItem = new RequestItem
                        {
                            CvId = cvsID,
                            VacansId = id,
                            Status = OrderStatus.Pending
                        };
                        userRequest.RequestItems.Add(newRequestItem);
                    }
                }
                else
                {
                    RequestItem newRequestItem = new RequestItem
                    {
                        CvId = cvsID,
                        VacansId = id,
                        Status = OrderStatus.Pending
                    };
                    userRequest = new Request
                    {
                        UserId = user.Id,
                        RequestItems = new List<RequestItem> { newRequestItem }
                    };
                    _context.Requests.Add(userRequest);
                }
                await _context.SaveChangesAsync();
            }
            ViewBag.Setting = _context.Settings.ToDictionary(s => s.Key, s => s.Value);
            Vacans? vacans = vacanss.FirstOrDefault(x => x.Id == id);
            ViewBag.Related = ExtensionMethods.RelatedByBusinessArea(vacanss, vacans, id);
            TempData["Request"] = true;

            return View(vacans);
        }


    }
}
