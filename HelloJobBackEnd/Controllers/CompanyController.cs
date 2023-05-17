using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using HelloJobBackEnd.Utilities.Extension;
using HelloJobBackEnd.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System;

namespace HelloJobBackEnd.Controllers
{
    public class CompanyController : Controller
    {
        private readonly HelloJobDbContext _context;
        private readonly UserManager<User> _usermanager;

        public CompanyController(HelloJobDbContext context, UserManager<User> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
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

        public async Task<IActionResult> VacansDetailAsync(int id)
        {
            IQueryable<Vacans> vacanss = _context.Vacans.AsNoTracking().AsQueryable();
            if (User.Identity.IsAuthenticated)
            {
                User user = await _usermanager.FindByNameAsync(User.Identity.Name);
                if (user is null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Cvs = _context.Cvs.Where(x => x.UserId == user.Id).ToList();

            }

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

        [HttpPost]
        public async Task<IActionResult> VacansDetailAsync(int id, int cvsID)
        {
            IQueryable<Vacans> vacanss = _context.Vacans.AsNoTracking().AsQueryable();
            if (User.Identity.IsAuthenticated)
            {
                User user = await _usermanager.FindByNameAsync(User.Identity.Name);
                if (user is null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Cvs = _context.Cvs.Where(x => x.UserId == user.Id).ToList();

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

            Vacans? vacans = _context.Vacans.Include(v => v.BusinessArea)
                .Include(e => e.Education)
                .Include(e => e.Experience)
                .Include(c => c.City)
                .Include(c => c.Company)
                .Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle)
                .Include(i => i.infoEmployeers)
                .Include(i => i.InfoWorks)
                .Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);

            vacans.Count++;
            await _context.SaveChangesAsync();

            ViewBag.Related = ExtensionMethods.RelatedByBusinessArea(vacanss, vacans, id);
            return View(vacans);
        }


    }
}
