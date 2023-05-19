using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using HelloJobBackEnd.Utilities.Extension;
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
        public IActionResult Index(string? search)
        {
            IQueryable<Cv> allcvs = _context.Cvs.Include(v => v.BusinessArea)
                   .Include(e => e.Education)
                   .Include(e => e.Experience)
                   .Include(c => c.City)
                   .Include(c => c.BusinessArea)
                   .Include(o => o.OperatingMode)
                   .Include(x => x.User).
                   Include(x => x.WishListItems).ThenInclude(x => x.WishList)
                   .Where(c => c.Status == OrderStatus.Accepted);

            if (!string.IsNullOrEmpty(search))
            {
                allcvs = allcvs.Where(c => c.Position.Contains(search));
            }

            List<Cv> filteredCvs = allcvs.ToList();

            ViewBag.Education = _context.Educations.ToList();
            ViewBag.Experince = _context.Experiences.ToList();
            ViewBag.Mode = _context.OperatingModes.ToList();
            ViewBag.Business = _context.BusinessArea.Include(b => b.BusinessTitle).Include(b => b.Cvs).ToList();

            return View(filteredCvs);
        }


        [HttpPost]
        public IActionResult Sorts(string? sort)
        {
            IQueryable<Cv> allcvs = _context.Cvs.Include(v => v.BusinessArea)
                    .Include(e => e.Education)
                    .Include(e => e.Experience)
                    .Include(c => c.City)
                    .Include(c => c.BusinessArea)
                    .Include(o => o.OperatingMode)
                    .Include(x => x.User).
                    Include(x => x.WishListItems).ThenInclude(x => x.WishList)
                    .Where(c => c.Status == OrderStatus.Accepted);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "new":
                        allcvs = allcvs.OrderByDescending(c => c.Id);
                        break;
                    case "old":
                        allcvs = allcvs.OrderBy(c => c.Id);
                        break;
                    case "salary_desc":
                        allcvs = allcvs.OrderByDescending(c => c.Salary);
                        break;
                    case "salary_asc":
                        allcvs = allcvs.OrderBy(c => c.Salary);
                        break;
                }
            }
            List<Cv> filteredCvs = allcvs.ToList();

            ViewBag.Education = _context.Educations.ToList();
            ViewBag.Experince = _context.Experiences.ToList();
            ViewBag.Mode = _context.OperatingModes.ToList();
            ViewBag.Business = _context.BusinessArea.Include(b => b.BusinessTitle).Include(b => b.Cvs).ToList();

            return PartialView("_UserblocksPartial", filteredCvs);
        }

        [HttpPost]
        public IActionResult FilterData(int[] businessIds, int[] modeIds, int[] educationIds, int[] experienceIds, bool? hasDriverLicense)
        {
            IQueryable<Cv> allcvs = _context.Cvs.Include(v => v.BusinessArea)
                    .Include(e => e.Education)
                    .Include(e => e.Experience)
                    .Include(c => c.City)
                    .Include(c => c.BusinessArea)
                    .Include(o => o.OperatingMode)
                    .Include(x => x.User).
                    Include(x => x.WishListItems).ThenInclude(x => x.WishList)
                    .Where(c => c.Status == OrderStatus.Accepted);

            List<Cv> filteredCvs = ApplyFilters(allcvs, businessIds, modeIds, educationIds, experienceIds, hasDriverLicense);
            return PartialView("_UserblocksPartial", filteredCvs);
        }

        private List<Cv> ApplyFilters(IQueryable<Cv> cvs, int[] businessIds, int[] modeIds, int[] educationIds, int[] experienceIds, bool? hasDriverLicense)
        {
            if (businessIds != null && businessIds.Length > 0)
            {
                cvs = cvs.Where(c => businessIds.Contains(c.BusinessArea.Id));
            }

            if (modeIds != null && modeIds.Length > 0)
            {
                cvs = cvs.Where(c => modeIds.Contains(c.OperatingMode.Id));
            }

            if (educationIds != null && educationIds.Length > 0)
            {
                cvs = cvs.Where(c => educationIds.Contains(c.Education.Id));
            }

            if (experienceIds != null && experienceIds.Length > 0)
            {
                cvs = cvs.Where(c => experienceIds.Contains(c.Experience.Id));
            }

            if (hasDriverLicense.HasValue)
            {
                cvs = cvs.Where(c => c.DrivingLicense == hasDriverLicense.Value);
            }



            return cvs.ToList();
        }
        public IActionResult Detail(int id)
        {
            if (id == 0) return NotFound();
            IQueryable<Cv> cvs = _context.Cvs.AsNoTracking().AsQueryable();

            Cv? cv = _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
              Include(o => o.OperatingMode).
                 Include(x => x.WishListItems).ThenInclude(x => x.WishList)
             .FirstOrDefault(x => x.Id == id);
            if (cv is null) return NotFound();
            cv.Count++;
            _context.SaveChanges();
            ViewBag.Related = ExtensionMethods.RelatedByBusinessArea(cvs, cv, id);
            return View(cv);
        }

        public IActionResult Search(string search)
        {
            IQueryable<Cv> query = _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).
              Include(o => o.OperatingMode).
                 Include(x => x.WishListItems).ThenInclude(x => x.WishList).
              Include(x => x.User).AsQueryable().Where(x => x.Position.Contains(search));
            List<Cv> cvs = query.OrderByDescending(x => x.Id).Take(3).Where(c => c.Status == OrderStatus.Accepted).ToList();
            return PartialView("_SerachcvPartial", cvs);
        }



    }
}
