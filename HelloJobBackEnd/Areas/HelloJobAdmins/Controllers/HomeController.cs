using HelloJobBackEnd.Areas.HelloJobAdmins.ViewModel;
using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class HomeController : Controller
    {
        private readonly HelloJobDbContext _context;

        public HomeController(HelloJobDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            DashboardVM dashboardVM = new DashboardVM
            {
                AcceptedCv = _context.Cvs.Where(x => x.Status == OrderStatus.Accepted).Take(5).ToList(),
                PendingCV = _context.Cvs.Where(x => x.Status == OrderStatus.Pending).Take(5).ToList(),
                RejectedCV = _context.Cvs.Where(x => x.Status == OrderStatus.Rejected).Take(5).ToList(),
                AcceptedVacans = _context.Vacans.Where(x => x.Status == OrderStatus.Accepted).Take(5).ToList(),
                PendingVacans = _context.Vacans.Where(x => x.Status == OrderStatus.Pending).Take(5).ToList(),
                RejectedVacans = _context.Vacans.Where(x => x.Status == OrderStatus.Rejected).Take(5).ToList(),

                Maliyye = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Maliyyə").Count(),
                Marketinq = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Marketinq").Count(),
                Texnalogiya = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Texnologiya").Count(),
                Satish = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Satış").Count(),
                Xidmet = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Xidmət").Count(),
                Dizayn = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Dizayn").Count(),
                Muxtelif = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Müxtəlif").Count(),
                Sehiyye = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Səhiyyə").Count(),
                Huquq = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Hüquq").Count(),
                TehsilElm = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Təhsil və elm").Count(),
                Senaye = _context.BusinessTitle.Include(x => x.BusinessAreas).Where(x => x.Name == "Sənaye və k/t").Count(),
                SaturdayCvOrders = _context.Cvs.Where(x => x.CreatedAt.Day == 6 && x.Status == OrderStatus.Accepted).Count(),
                SaturdayVacansOrders = _context.Vacans.Where(x => x.CreatedAt.Day == 6 && x.Status == OrderStatus.Accepted).Count(),
                SundayCvOrders = _context.Cvs.Where(x => x.CreatedAt.Day == 7 && x.Status == OrderStatus.Accepted).Count(),
                SundayVacansOrders = _context.Vacans.Where(x => x.CreatedAt.Day == 7 && x.Status == OrderStatus.Accepted).Count(),
                MondayCVOrders = _context.Cvs.Where(x => x.CreatedAt.Day == 1 && x.Status == OrderStatus.Accepted).Count(),
                MondayVacansOrders = _context.Vacans.Where(x => x.CreatedAt.Day == 1 && x.Status == OrderStatus.Accepted).Count(),
                TuesdayCVOrders = _context.Cvs.Where(x => x.CreatedAt.Day == 4 && x.Status == OrderStatus.Accepted).Count(),
                TuesdayVacansOrders = _context.Vacans.Where(x => x.CreatedAt.Day == 4 && x.Status == OrderStatus.Accepted).Count(),
                WednesCVOrders = _context.Cvs.Where(x => x.CreatedAt.Day == 3 && x.Status == OrderStatus.Accepted).Count(),
                WednesdayVacansOrders = _context.Vacans.Where(x => x.CreatedAt.Day == 3 && x.Status == OrderStatus.Accepted).Count(),
                ThursdayCVOrders = _context.Cvs.Where(x => x.CreatedAt.Day == 2 && x.Status == OrderStatus.Accepted).Count(),
                ThursdayVacansOrders = _context.Vacans.Where(x => x.CreatedAt.Day == 2 && x.Status == OrderStatus.Accepted).Count(),
                FridayCVOrders = _context.Cvs.Where(x => x.CreatedAt.Day == 5 && x.Status == OrderStatus.Accepted).Count(),
                FridayVacansOrders = _context.Vacans.Where(x => x.CreatedAt.Day == 5 && x.Status == OrderStatus.Accepted).Count(),
            };
            //var businessTitleCounts = new Dictionary<string, int>();

            ////foreach (var cv in dashboardVM.AcceptedCv)
            ////{
            ////    var businessAreaName = cv.BusinessArea.Name;
            ////    if (businessTitleCounts.ContainsKey(businessAreaName))
            ////        businessTitleCounts[businessAreaName]++;
            ////    else
            ////        businessTitleCounts[businessAreaName] = 1;
            ////}

            ////foreach (var vacans in dashboardVM.AcceptedVacans)
            ////{
            ////    var businessAreaName = vacans.BusinessArea.Name;
            ////    if (businessTitleCounts.ContainsKey(businessAreaName))
            ////        businessTitleCounts[businessAreaName]++;
            ////    else
            ////        businessTitleCounts[businessAreaName] = 1;
            ////}

            //var mostPopularBusinessArea = businessTitleCounts.OrderByDescending(kv => kv.Value).FirstOrDefault();

            return View(dashboardVM);
        }
    }
}
