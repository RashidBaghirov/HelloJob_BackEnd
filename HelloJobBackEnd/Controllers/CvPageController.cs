﻿using HelloJobBackEnd.DAL;
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
        public IActionResult Index(string search)
        {
            List<Cv> cvs = _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).
              Include(o => o.OperatingMode).
              Include(x => x.User).Where(c => c.Status == OrderStatus.Accepted).ToList();

            ViewBag.Education = _context.Educations.ToList();
            ViewBag.Experince = _context.Experiences.ToList();
            ViewBag.Mode = _context.OperatingModes.ToList();
            ViewBag.Business = _context.BusinessArea.Include(b => b.BusinessTitle).Include(b => b.Cvs).ToList();

            if (search is not null)
            {
                List<Cv> findedcv = _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).
              Include(o => o.OperatingMode).
              Include(x => x.User).Where(c => c.Position.Contains(search) && c.Status == OrderStatus.Accepted).ToList();
                return View(findedcv);
            }

            return View(cvs);
        }

        public IActionResult Detail(int id)
        {
            if (id == 0) return NotFound();
            IQueryable<Cv> cvs = _context.Cvs.AsNoTracking().AsQueryable();

            Cv? cv = cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
              Include(o => o.OperatingMode)
             .FirstOrDefault(x => x.Id == id);
            if (cv is null) return NotFound();
            cv.Count++;
            _context.SaveChanges();
            ViewBag.Related = ExtensionMethods.RelatedByBusinessArea(cvs, cv, id);
            return View(cv);
        }

        public IActionResult Search(string search)
        {
            var query = _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).
              Include(o => o.OperatingMode).
              Include(x => x.User).AsQueryable().Where(x => x.Position.Contains(search));
            List<Cv> cvs = query.OrderByDescending(x => x.Id).Take(3).Where(c => c.Status == OrderStatus.Accepted).ToList();
            return PartialView("_SerachcvPartial", cvs);
        }



    }
}
