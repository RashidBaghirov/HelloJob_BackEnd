using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class CityController : Controller
    {
        private readonly HelloJobDbContext _context;

        public CityController(HelloJobDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<City> cities = _context.Cities.OrderBy(x => x.Name).ToList();
            return View(cities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(City newcity)
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
            bool Isdublicate = _context.Cities.Any(c => c.Name == newcity.Name);

            if (Isdublicate)
            {
                ModelState.AddModelError("", "You cannot enter the same data again");
                return View();
            }
            _context.Cities.Add(newcity);
            _context.SaveChanges();
            TempData["Create"] = true;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            City? city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city is null) return NotFound();
            return View(city);
        }

        [HttpPost]
        public IActionResult Edit(int id, City editCity)
        {
            TempData["Edit"] = false;

            if (id != editCity.Id) return NotFound();
            City? city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city is null) return NotFound();
            bool duplicate = _context.Cities.Any(c => c.Name == editCity.Name && city.Name != editCity.Name);
            if (duplicate)
            {
                ModelState.AddModelError("Name", "This  city name is now available");
                return View();
            }
            city.Name = editCity.Name;
            _context.SaveChanges();
            TempData["Edit"] = true;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();
            City? city = _context.Cities.FirstOrDefault(c => c.Id == id);
            return city is null ? BadRequest() : View(city);
        }



        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            City? city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city is null) return NotFound();
            return View(city);
        }

        [HttpPost]
        public IActionResult Delete(int id, City deleteCity)
        {
            TempData["Delete"] = false;

            if (id != deleteCity.Id) return NotFound();
            City? city = _context.Cities.FirstOrDefault(c => c.Id == id);
            if (city is null) return NotFound();
            _context.Cities.Remove(city);
            _context.SaveChanges();
            TempData["Delete"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
