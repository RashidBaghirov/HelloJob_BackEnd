using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Services.Interface;
using HelloJobBackEnd.Utilities.Enum;
using HelloJobBackEnd.Utilities.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloJobBackEnd.Controllers
{
    public class CvPageController : Controller
    {
        private readonly HelloJobDbContext _context;
        private readonly ICvPageService _cvPageService;

        public CvPageController(HelloJobDbContext context, ICvPageService cvPageService)
        {
            _context = context;
            _cvPageService = cvPageService;
        }
        public IActionResult Index(string? search)
        {
            IQueryable<Cv> allcvs = _cvPageService.GetAllCvs();

            if (!string.IsNullOrEmpty(search))
            {
                allcvs = allcvs.Where(c => c.Position.Contains(search));
            }

            List<Cv> filteredCvs = allcvs.ToList();

            ViewBag.Education = _context.Educations.ToList();
            ViewBag.Experince = _context.Experiences.ToList();
            ViewBag.Mode = _context.OperatingModes.ToList();
            ViewBag.Business = _context.BusinessArea.Include(b => b.BusinessTitle).Include(b => b.Cvs).ToList();
            ViewBag.Setting = _context.Settings.ToDictionary(s => s.Key, s => s.Value);


            return View(filteredCvs);
        }


        [HttpPost]
        public async Task<IActionResult> Sorts(string? sort)
        {
            List<Cv> filteredCvs = await _cvPageService.GetSortedCvs(sort);

            ViewBag.Education = _context.Educations.ToList();
            ViewBag.Experince = _context.Experiences.ToList();
            ViewBag.Mode = _context.OperatingModes.ToList();
            ViewBag.Setting = _context.Settings.ToDictionary(s => s.Key, s => s.Value);

            ViewBag.Business = _context.BusinessArea.Include(b => b.BusinessTitle).Include(b => b.Cvs).ToList();

            return PartialView("_UserblocksPartial", filteredCvs);
        }

        [HttpPost]
        public async Task<IActionResult> FilterData(int[] businessIds, int[] modeIds, int[] educationIds, int[] experienceIds, bool? hasDriverLicense)
        {
            ViewBag.Setting = _context.Settings.ToDictionary(s => s.Key, s => s.Value);

            List<Cv> filteredCvs = await _cvPageService.GetFilteredData(businessIds, modeIds, educationIds, experienceIds, hasDriverLicense);
            return PartialView("_UserblocksPartial", filteredCvs);
        }

        public IActionResult Detail(int id)
        {
            if (id == 0) return NotFound();
            IQueryable<Cv> allcvs = _cvPageService.GetAllCvs();


            Cv? cv = _cvPageService.Details(id);
            if (cv is null) return NotFound();
            cv.Count++;
            _context.SaveChanges();
            ViewBag.Related = ExtensionMethods.RelatedByBusinessArea(allcvs, cv, id);
            ViewBag.Setting = _context.Settings.ToDictionary(s => s.Key, s => s.Value);

            return View(cv);
        }

        public async Task<IActionResult> Search(string search)
        {
            List<Cv> cvs = await _cvPageService.SearchCvs(search);
            return PartialView("_SerachcvPartial", cvs);
        }



    }
}
