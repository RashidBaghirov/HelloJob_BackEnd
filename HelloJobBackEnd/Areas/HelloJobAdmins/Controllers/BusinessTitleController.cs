using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using HelloJobBackEnd.Utilities.Extension;
using HelloJobBackEnd.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class BusinessTitleController : Controller
    {
        private readonly HelloJobDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BusinessTitleController(HelloJobDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<BusinessTitle> businessTitles = _context.BusinessTitle.Include(b => b.BusinessAreas).ToList();
            return View(businessTitles);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BusinessTitleVM newtitle)
        {
            TempData["Create"] = false;
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!newtitle.Image.IsValidFile("image/"))
            {
                ModelState.AddModelError(string.Empty, "Şəkil file seçin");
                return View();
            }
            if (!newtitle.Image.IsValidLength(1))
            {
                ModelState.AddModelError(string.Empty, "Maximum ölçü 1 mb ola bilər");
                return View();
            }
            BusinessTitle title = new()
            {
                Name = newtitle.Name,
            };

            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            title.Image = await newtitle.Image.CreateImage(imagefolderPath, "BusinessTitle");

            _context.BusinessTitle.Add(title);
            _context.SaveChanges();
            TempData["Create"] = true;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {

            BusinessTitleVM? titleVM = _context.BusinessTitle.Include(v => v.BusinessAreas).Select(p =>
                                                new BusinessTitleVM
                                                {
                                                    Id = p.Id,
                                                    Name = p.Name,
                                                    Images = p.Image

                                                }).FirstOrDefault(x => x.Id == id);

            return View(titleVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BusinessTitleVM edited)
        {
            TempData["Edit"] = false;
            BusinessTitleVM? titleVM = _context.BusinessTitle.Include(v => v.BusinessAreas).Select(p =>
                                               new BusinessTitleVM
                                               {
                                                   Id = p.Id,
                                                   Name = p.Name,
                                                   Images = p.Image

                                               }).FirstOrDefault(x => x.Id == id);

            BusinessTitle title = _context.BusinessTitle.Include(v => v.BusinessAreas).FirstOrDefault(x => x.Id == id);

            if (edited.Image is not null)
            {
                if (!edited.Image.IsValidFile("image/"))
                {
                    ModelState.AddModelError(string.Empty, "Şəkil file seçin");
                    return View();
                }
                if (!edited.Image.IsValidLength(2))
                {
                    ModelState.AddModelError(string.Empty, "Maximum ölçü 1 mb ola bilər");
                    return View();
                }
                var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
                string filepath = Path.Combine(imagefolderPath, "BusinessTitle", title.Image);
                ExtensionMethods.DeleteImage(filepath);
                title.Image = await edited.Image.CreateImage(imagefolderPath, "BusinessTitle");
            }

            title.Name = edited.Name;
            _context.SaveChanges();
            TempData["Edit"] = true;
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();
            BusinessTitle? title = _context.BusinessTitle.FirstOrDefault(s => s.Id == id);
            return title is null ? BadRequest() : View(title);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            BusinessTitle? title = _context.BusinessTitle
                .Include(x => x.BusinessAreas)
                .FirstOrDefault(s => s.Id == id);
            if (title is null) return NotFound();
            return View(title);
        }
        [HttpPost]
        public IActionResult Delete(int id, BusinessTitle deleted)
        {
            TempData["Delete"] = false;

            if (id != deleted.Id)
                return NotFound();

            BusinessTitle? title = _context.BusinessTitle
                .Include(x => x.BusinessAreas)
                .FirstOrDefault(s => s.Id == id);

            if (title is null)
                return NotFound();

            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            string filePath = Path.Combine(imageFolderPath, "BusinessTitle", title.Image);

            ExtensionMethods.DeleteImage(filePath);

            _context.BusinessArea.RemoveRange(title.BusinessAreas);
            _context.BusinessTitle.Remove(title);
            _context.SaveChanges();

            TempData["Delete"] = true;
            return RedirectToAction(nameof(Index));
        }


    }
}
