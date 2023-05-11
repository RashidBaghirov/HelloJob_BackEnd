using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Extension;
using HelloJobBackEnd.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HelloJobBackEnd.Controllers
{
    [Authorize(Roles = "business,employeer")]
    public class MyAccountController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HelloJobDbContext _context;
        private readonly IWebHostEnvironment _env;

        public MyAccountController(UserManager<User> usermanager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, HelloJobDbContext context, IWebHostEnvironment env)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _usermanager = usermanager;
            _context = context;
            _env = env;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            return View();
        }
        public async Task<IActionResult> CreateVacans()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateVacans(VacansVM newVacans)
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);
            if (!ModelState.IsValid)
            {
                return View();
            }

            Vacans vacans = new()
            {

                User = user,
                Name = newVacans.Name,
                CityId = newVacans.CityId,
                OperatingModeId = newVacans.OperatingModeId,
                ExperienceId = newVacans.ExperienceId,
                Salary = newVacans.Salary,
                BusinessAreaId = newVacans.BusinessAreaId,
                EducationId = newVacans.EducationId,
                DrivingLicense = newVacans.DrivingLicense,
                CreatedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddMonths(1),
                Email = newVacans.Email,
                Position = newVacans.Position
            };

            if (newVacans.InfoWorks is null)
            {
                ModelState.AddModelError("", "Please write work's info");
                return View();
            }
            else
            {
                string[] work_info = newVacans.InfoWorks.Split('/');
                foreach (string info in work_info)
                {
                    InfoWork infos = new()
                    {
                        Vacans=vacans,
                        Info = info,
                    };

                    _context.InfoWorks.Add(infos);
                }
            }

            if (newVacans.infoEmployeers is null)
            {
                ModelState.AddModelError("", "Please write work's info");
                return View();
            }
            else
            {
                string[] employee_info = newVacans.infoEmployeers.Split('/');
                foreach (string info in employee_info)
                {
                    InfoEmployeer infos = new()
                    {
                        Vacans = vacans,
                        Info = info,
                    };

                    _context.InfoEmployeers.Add(infos);
                }
            }
            _context.Vacans.Add(vacans);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MySticker()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);


            return View();
        }
        public async Task<IActionResult> CreateCv()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCv(CvVM newCv)
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!newCv.Image.IsValidFile("image/"))
            {
                ModelState.AddModelError(string.Empty, "Please choose image file");
                return View();
            }
            if (!newCv.Image.IsValidLength(1))
            {
                ModelState.AddModelError(string.Empty, "Please choose image which size is maximum 1MB");
                return View();
            }
            Cv cv = new()
            {

                User = user,
                Name = newCv.Name,
                Surname = newCv.Surname,
                CityId = newCv.CityId,
                BornDate = newCv.BornDate,
                OperatingModeId = newCv.OperatingModeId,
                ExperienceId = newCv.ExperienceId,
                Salary = newCv.Salary,
                BusinessAreaId = newCv.BusinessAreaId,
                EducationId = newCv.EducationId,
                DrivingLicense = newCv.DrivingLicense,
                CreatedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddMonths(1),
                Email = newCv.Email,
                Number = newCv.Number,
                Position = newCv.Position
            };
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            var pdffolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "User");

            cv.Image = await newCv.Image.CreateImage(imagefolderPath, "User");
            cv.CvPDF = await newCv.CvPDF.CreateImage(pdffolderPath, "CVs");

            _context.Cvs.Add(cv);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> WishListPage()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            return View();
        }


        public async Task<IActionResult> Security()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            return View();
        }













        public void ViewBags(User user)
        {
            ProfileVM profileVM = new()
            {
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName
            };
            ViewBag.User = profileVM;
            ViewBag.Cities = _context.Cities.ToList();
            ViewBag.Education = _context.Educations.ToList();
            ViewBag.Experince = _context.Experiences.ToList();
            ViewBag.Mode = _context.OperatingModes.ToList();
            ViewBag.Business = _context.BusinessTitle.Include(b => b.BusinessAreas).ToList();
        }
    }
}
