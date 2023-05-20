﻿using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class OperatingModeController : Controller
    {
        private readonly HelloJobDbContext _context;

        public OperatingModeController(HelloJobDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<OperatingMode> mode = _context.OperatingModes.OrderBy(x => x.Name).ToList();
            return View(mode);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OperatingMode newmode)
        {
            TempData["Create"] = false;
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }
                return View();
            }
            bool Isdublicate = _context.OperatingModes.Any(c => c.Name == newmode.Name);

            if (Isdublicate)
            {
                ModelState.AddModelError("", "You cannot enter the same data again");
                return View();
            }
            _context.OperatingModes.Add(newmode);
            _context.SaveChanges();
            TempData["Create"] = true;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            OperatingMode? mode = _context.OperatingModes.FirstOrDefault(c => c.Id == id);
            if (mode is null) return NotFound();
            return View(mode);
        }

        [HttpPost]
        public IActionResult Edit(int id, OperatingMode editmode)
        {
            TempData["Edit"] = false;

            if (id != editmode.Id) return NotFound();
            OperatingMode? mode = _context.OperatingModes.FirstOrDefault(c => c.Id == id);
            if (mode is null) return NotFound();
            bool duplicate = _context.Experiences.Any(c => c.Name == editmode.Name && mode.Name != editmode.Name);
            if (duplicate)
            {
                ModelState.AddModelError("Name", "This Operating Mode  is now available");
                return View();
            }
            mode.Name = editmode.Name;
            _context.SaveChanges();
            TempData["Edit"] = true;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();
            OperatingMode? mode = _context.OperatingModes.FirstOrDefault(c => c.Id == id);
            return mode is null ? BadRequest() : View(mode);
        }



        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            OperatingMode? mode = _context.OperatingModes.FirstOrDefault(c => c.Id == id);
            if (mode is null) return NotFound();
            return View(mode);
        }

        [HttpPost]
        public IActionResult Delete(int id, OperatingMode deleteMode)
        {
            TempData["Delete"] = false;

            if (id != deleteMode.Id) return NotFound();
            OperatingMode? mode = _context.OperatingModes.FirstOrDefault(c => c.Id == id);
            if (mode is null) return NotFound();
            _context.OperatingModes.Remove(mode);
            _context.SaveChanges();
            TempData["Delete"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
