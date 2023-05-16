using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using HelloJobBackEnd.Utilities.Extension;
using HelloJobBackEnd.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;

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

        //-------------------------------------------------------------------------------------------------------------------------------------------------------


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
            TempData["Create"] = false;
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
                CityId = newVacans.CityId,
                OperatingModeId = newVacans.OperatingModeId,
                ExperienceId = newVacans.ExperienceId,
                Salary = newVacans.Salary,
                BusinessAreaId = newVacans.BusinessAreaId,
                EducationId = newVacans.EducationId,
                DrivingLicense = newVacans.DrivingLicense,
                CreatedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddMonths(1),
                Position = newVacans.Position,
                CompanyId = newVacans.CompanyId,
                Status = OrderStatus.Pending,
                Count = 0
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
                        Vacans = vacans,
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
            TempData["Create"] = true;
            return RedirectToAction(nameof(MyOrder));
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------





        public async Task<IActionResult> EditVacans(int id)
        {
            if (id == 0) return BadRequest();
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            VacansVM? vacanVM = EditedModelVC(id);
            if (vacanVM is null) return BadRequest();
            return View(vacanVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditVacans(int id, VacansVM editedvacans)
        {
            if (id == 0) return BadRequest();
            TempData["Edited"] = false;
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);
            VacansVM? vacanVM = EditedModelVC(id);
            if (vacanVM is null) return BadRequest();
            Vacans? vacans = _context.Vacans.Include(v => v.BusinessArea).
                  Include(e => e.Education).
                Include(e => e.Experience).
                Include(c => c.City).
                Include(c => c.Company).
                Include(c => c.BusinessArea).
                Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
                Include(i => i.infoEmployeers).
                 Include(i => i.InfoWorks).
                Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);



            vacans.InfoWorks.RemoveAll(p => !editedvacans.DeleteWork.Contains(p.Id));
            if (editedvacans.InfoWorks is not null)
            {
                string[] work_info = editedvacans.InfoWorks.Split('/');
                foreach (string info in work_info)
                {
                    InfoWork infos = new()
                    {
                        Vacans = vacans,
                        Info = info,
                    };

                    _context.InfoWorks.Add(infos);
                }
            }
            vacans.infoEmployeers.RemoveAll(p => !editedvacans.DeleteEmployeers.Contains(p.Id));
            if (editedvacans.infoEmployeers is not null)
            {
                string[] employee_info = editedvacans.infoEmployeers.Split('/');
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


            vacans.CityId = editedvacans.CityId;
            vacans.OperatingModeId = editedvacans.OperatingModeId;
            vacans.ExperienceId = editedvacans.ExperienceId;
            vacans.Salary = editedvacans.Salary;
            vacans.BusinessAreaId = editedvacans.BusinessAreaId;
            vacans.EducationId = editedvacans.EducationId;
            vacans.DrivingLicense = editedvacans.DrivingLicense;
            vacans.CreatedAt = DateTime.Now;
            vacans.EndedAt = DateTime.Now.AddMonths(1);
            vacans.Position = editedvacans.Position;
            vacans.Status = OrderStatus.Pending;
            _context.SaveChanges();
            TempData["Edited"] = true;
            return RedirectToAction(nameof(MyOrder));
        }



        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        private VacansVM? EditedModelVC(int id)
        {
            VacansVM? vacanVM = _context.Vacans.Include(v => v.BusinessArea).
               Include(e => e.Education).
               Include(e => e.Experience).
               Include(c => c.City).
               Include(c => c.Company).
               Include(c => c.BusinessArea).
               Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
               Include(i => i.infoEmployeers).
                Include(i => i.InfoWorks).
               Include(o => o.OperatingMode).Select(p =>
                                        new VacansVM
                                        {
                                            Id = p.Id,
                                            BusinessAreaId = p.BusinessAreaId,
                                            CityId = p.CityId,
                                            EducationId = p.EducationId,
                                            ExperienceId = p.ExperienceId,
                                            OperatingModeId = p.OperatingModeId,
                                            CompanyId = p.CompanyId,
                                            Salary = p.Salary,
                                            Position = p.Position,
                                            AllEmployeerInfos = p.infoEmployeers,
                                            AllWorkInfos = p.InfoWorks,
                                            Status = p.Status
                                        }).FirstOrDefault(x => x.Id == id);
            return vacanVM;
        }



        //-----------------------------------------------------------------------------------------------------------------------------------------------------





        public async Task<IActionResult> DeleteVacans(int id)
        {


            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            VacansVM? vacanVM = _context.Vacans.Include(v => v.BusinessArea).
                Include(e => e.Education).
                Include(e => e.Experience).
                Include(c => c.City).
                Include(c => c.BusinessArea).
                Include(i => i.infoEmployeers).
                 Include(i => i.InfoWorks).
                Include(o => o.OperatingMode).Select(p =>
                                         new VacansVM
                                         {
                                             Id = p.Id,
                                             BusinessAreaId = p.BusinessAreaId,
                                             CityId = p.CityId,
                                             EducationId = p.EducationId,
                                             ExperienceId = p.ExperienceId,
                                             OperatingModeId = p.OperatingModeId,
                                             Salary = p.Salary,
                                             Position = p.Position,
                                             AllEmployeerInfos = p.infoEmployeers,
                                             AllWorkInfos = p.InfoWorks,
                                             Status = p.Status
                                         }).FirstOrDefault(x => x.Id == id);

            return View(vacanVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteVacans(int id, VacansVM deleteVacans)
        {
            TempData["Deleted"] = false;
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);


            VacansVM? vacanVM = _context.Vacans.Include(v => v.BusinessArea).
                Include(e => e.Education).
                Include(e => e.Experience).
                Include(c => c.City).
                Include(c => c.BusinessArea).
                Include(i => i.infoEmployeers)
                 .Include(i => i.InfoWorks).
                Include(o => o.OperatingMode).Select(p =>
                                         new VacansVM
                                         {
                                             Id = p.Id,
                                             BusinessAreaId = p.BusinessAreaId,
                                             CityId = p.CityId,
                                             EducationId = p.EducationId,
                                             ExperienceId = p.ExperienceId,
                                             OperatingModeId = p.OperatingModeId,
                                             Salary = p.Salary,
                                             Position = p.Position,
                                             AllEmployeerInfos = p.infoEmployeers,
                                             AllWorkInfos = p.InfoWorks,
                                             Status = p.Status
                                         }).FirstOrDefault(x => x.Id == id);
            Vacans? vacans = _context.Vacans.Include(v => v.BusinessArea).
                  Include(e => e.Education).
                Include(e => e.Experience).
                Include(c => c.City).
                Include(c => c.BusinessArea).
                Include(i => i.infoEmployeers).
                 Include(i => i.InfoWorks).
                Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);




            _context.Vacans.Remove(vacans);
            _context.SaveChanges();
            TempData["Deleted"] = true;
            return RedirectToAction(nameof(MySticker));
        }



        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------------------


        public async Task<IActionResult> MySticker()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            if (User.IsInRole(UserRole.employeer.ToString()))
            {
                ViewBag.Allcv = _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).
              Include(o => o.OperatingMode).
              Include(x => x.User).
              Where(x => x.UserId == user.Id && x.Status == OrderStatus.Accepted).ToList();
                return View();
            }
            else
            {
                ViewBag.Allvacans = _context.Vacans.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.Company).
              Include(c => c.Company).
                ThenInclude(x => x.User).
              Include(c => c.BusinessArea).
              Include(o => o.OperatingMode).
              Where(x => x.Company.UserId == user.Id && x.Status == OrderStatus.Accepted).ToList();
                return View();

            }

        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------
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
            TempData["Create"] = false;
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
                ModelState.AddModelError(string.Empty, "Şəkil file seçin");
                return View();
            }
            if (!newCv.Image.IsValidLength(1))
            {
                ModelState.AddModelError(string.Empty, "Maximum ölçü 1 mb ola bilər");
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
                Position = newCv.Position,
                Status = OrderStatus.Pending,
                Count = 0
            };
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            var pdffolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "User");
            cv.Image = await newCv.Image.CreateImage(imagefolderPath, "User");
            cv.CvPDF = await newCv.CvPDF.CreateImage(pdffolderPath, "CVs");

            _context.Cvs.Add(cv);
            _context.SaveChanges();
            TempData["Create"] = true;
            return RedirectToAction(nameof(MyOrder));
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------------------------------------


        public async Task<IActionResult> MyOrder()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            if (User.IsInRole(UserRole.employeer.ToString()))
            {
                ViewBag.Allcv = _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).
              Include(o => o.OperatingMode).
              Include(x => x.User).
              Where(x => x.UserId == user.Id && x.Status == OrderStatus.Pending).ToList();
                return View();
            }
            else
            {
                ViewBag.Allvacans = _context.Vacans.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.Company).
              Include(c => c.Company).
                ThenInclude(x => x.User).
              Include(c => c.BusinessArea).
              Include(o => o.OperatingMode).
              Where(x => x.Company.UserId == user.Id && x.Status == OrderStatus.Pending).ToList();
                return View();

            }

        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------





        public async Task<IActionResult> EditCV(int id)
        {

            if (id == 0) return BadRequest();
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            CvVM? cvVm = EditedModelCv(id);
            ViewBags(user);
            if (cvVm is null) return BadRequest();
            return View(cvVm);
        }


        [HttpPost]
        public async Task<IActionResult> EditCV(int id, CvVM editedCv)
        {
            if (id == 0) return BadRequest();
            TempData["Edited"] = false;
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            CvVM? cvVm = EditedModelCv(id);

            Cv? cv = await _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
              Include(o => o.OperatingMode)
             .FirstOrDefaultAsync(x => x.Id == id);

            if (cv is null) return BadRequest();

            if (editedCv.Image is not null)
            {
                if (!editedCv.Image.IsValidFile("image/"))
                {
                    ModelState.AddModelError(string.Empty, "Şəkil file seçin");
                    return View();
                }
                if (!editedCv.Image.IsValidLength(2))
                {
                    ModelState.AddModelError(string.Empty, "Maximum ölçü 1 mb ola bilər");
                    return View();
                }
                var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
                string filepath = Path.Combine(imagefolderPath, "User", cv.Image);
                ExtensionMethods.DeleteImage(filepath);
                cv.Image = await editedCv.Image.CreateImage(imagefolderPath, "User");
            }

            if (editedCv.CvPDF is not null)
            {
                var cvfolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "User");
                string filepath = Path.Combine(cvfolderPath, "CVs", cv.CvPDF);
                ExtensionMethods.DeleteImage(filepath);
                cv.CvPDF = await editedCv.CvPDF.CreateImage(cvfolderPath, "CVs");
            }

            cv.Name = editedCv.Name;
            cv.Email = editedCv.Email;
            cv.BusinessAreaId = editedCv.BusinessAreaId;
            cv.CityId = editedCv.CityId;
            cv.EducationId = editedCv.EducationId;
            cv.ExperienceId = editedCv.ExperienceId;
            cv.OperatingModeId = editedCv.OperatingModeId;
            cv.Salary = editedCv.Salary;
            cv.Position = editedCv.Position;
            cv.Number = editedCv.Number;
            cv.Status = OrderStatus.Pending;
            _context.SaveChanges();
            TempData["Edited"] = true;
            return RedirectToAction(nameof(MyOrder));
        }



        //-------------------------------------------------------------------------------------------------------------------------------------------------------


        private CvVM? EditedModelCv(int id)
        {
            CvVM? cvVm = _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
              Include(o => o.OperatingMode).Select(p =>
                                         new CvVM
                                         {
                                             Id = p.Id,
                                             Name = p.Name,
                                             Surname = p.Surname,
                                             Email = p.Email,
                                             BusinessAreaId = p.BusinessAreaId,
                                             CityId = p.CityId,
                                             EducationId = p.EducationId,
                                             ExperienceId = p.ExperienceId,
                                             OperatingModeId = p.OperatingModeId,
                                             Salary = p.Salary,
                                             Position = p.Position,
                                             Number = p.Number,
                                             BornDate = p.BornDate,
                                             Images = p.Image,
                                             CvPDFs = p.CvPDF,
                                             Status = p.Status,
                                             Count = p.Count
                                         }).FirstOrDefault(x => x.Id == id);
            return cvVm;
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------





        public async Task<IActionResult> DeleteCV(int id)
        {


            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);
            CvVM? cv = _context.Cvs.Include(v => v.BusinessArea).
                Include(e => e.Education).
                Include(e => e.Experience).
                Include(c => c.City).
                Include(c => c.BusinessArea).
                Include(o => o.OperatingMode).Select(p =>
                                         new CvVM
                                         {
                                             Id = p.Id,
                                             Name = p.Name,
                                             Surname = p.Surname,
                                             Email = p.Email,
                                             BusinessAreaId = p.BusinessAreaId,
                                             CityId = p.CityId,
                                             EducationId = p.EducationId,
                                             ExperienceId = p.ExperienceId,
                                             OperatingModeId = p.OperatingModeId,
                                             Salary = p.Salary,
                                             Position = p.Position,
                                             Number = p.Number,
                                             BornDate = p.BornDate,
                                             Images = p.Image,
                                             CvPDFs = p.CvPDF
                                         }).FirstOrDefault(x => x.Id == id);

            return View(cv);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCV(int id, CvVM deleteCv)
        {
            TempData["Deleted"] = false;
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            CvVM? cvvm = _context.Cvs.Include(v => v.BusinessArea).
                  Include(e => e.Education).
                  Include(e => e.Experience).
                  Include(c => c.City).
                  Include(c => c.BusinessArea).
                  Include(o => o.OperatingMode).Select(p =>
                                           new CvVM
                                           {
                                               Id = p.Id,
                                               Name = p.Name,
                                               Surname = p.Surname,
                                               Email = p.Email,
                                               BusinessAreaId = p.BusinessAreaId,
                                               CityId = p.CityId,
                                               EducationId = p.EducationId,
                                               ExperienceId = p.ExperienceId,
                                               OperatingModeId = p.OperatingModeId,
                                               Salary = p.Salary,
                                               Position = p.Position,
                                               Number = p.Number,
                                               BornDate = p.BornDate,
                                               Images = p.Image,
                                               CvPDFs = p.CvPDF
                                           }).FirstOrDefault(x => x.Id == id);

            Cv? cv = _context.Cvs.Include(v => v.BusinessArea).
                Include(e => e.Education).
                Include(e => e.Experience).
                Include(c => c.City).
                Include(c => c.BusinessArea).
                Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);


            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            string filepath = Path.Combine(imagefolderPath, "User", cv.Image);
            ExtensionMethods.DeleteImage(filepath);
            string pdfpath = Path.Combine(imagefolderPath, "User", "CVs", cv.CvPDF);
            ExtensionMethods.DeleteImage(pdfpath);

            _context.Cvs.Remove(cv);
            _context.SaveChanges();
            TempData["Deleted"] = true;
            return RedirectToAction(nameof(MySticker));
        }



        //-------------------------------------------------------------------------------------------------------------------------------------------------------


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


        //-------------------------------------------------------------------------------------------------------------------------------------------------------


        public async Task<IActionResult> Security()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);
            ProfileVM profileVM = new()
            {
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName
            };
            return View(profileVM);
        }

        [HttpPost]
        public async Task<IActionResult> Security(ProfileVM profileVM)
        {
            TempData["Security"] = false;
            if (!ModelState.IsValid) return View();

            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            ViewBags(user);

            if (!string.IsNullOrWhiteSpace(profileVM.ConfirmNewPassword) && !string.IsNullOrWhiteSpace(profileVM.NewPassword))
            {
                var passwordChangeResult = await _usermanager.ChangePasswordAsync(user, profileVM.CurrentPassword, profileVM.NewPassword);

                if (!passwordChangeResult.Succeeded)
                {
                    foreach (var item in passwordChangeResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    return View();
                }

            }

            user.UserName = profileVM.UserName;
            var result = await _usermanager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View();
            }
            await _signInManager.SignOutAsync();
            TempData["Security"] = true;
            return RedirectToAction("Index", "Home");
        }




        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> CreatingCompany()
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
        public async Task<IActionResult> CreatingCompany(CompanyVM newCompany)
        {
            TempData["Company"] = false;
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
            if (!newCompany.Image.IsValidFile("image/"))
            {
                ModelState.AddModelError(string.Empty, "Şəkil file seçin");
                return View();
            }
            if (!newCompany.Image.IsValidLength(1))
            {
                ModelState.AddModelError(string.Empty, "Maximum ölçü 1 mb ola bilər");
                return View();
            }
            Company company = new()
            {
                User = user,
                Name = newCompany.Name,
                Email = newCompany.Email,
                Status = OrderStatus.Pending,
            };

            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            company.Image = await newCompany.Image.CreateImage(imagefolderPath, "Company");

            _context.Companies.Add(company);
            _context.SaveChanges();
            TempData["Company"] = true;
            return RedirectToAction(nameof(MyCompany));
        }



        //-----------------------------------------------------------------------------------------------------------------------------------------------------





        public async Task<IActionResult> EditCompany(int id)
        {

            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);
            CompanyVM? company = _context.Companies.Include(v => v.Vacans).Select(p =>
                                                new CompanyVM
                                                {
                                                    Id = p.Id,
                                                    Name = p.Name,
                                                    Email = p.Email,
                                                    Images = p.Image,
                                                    Status = p.Status

                                                }).FirstOrDefault(x => x.Id == id);

            return View(company);
        }
        [HttpPost]
        public async Task<IActionResult> EditCompany(int id, CompanyVM editedCompany)
        {
            TempData["Edited"] = false;
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            CompanyVM? companyVM = _context.Companies.Include(v => v.Vacans).Select(p =>
                                         new CompanyVM
                                         {
                                             Id = p.Id,
                                             Name = p.Name,
                                             Email = p.Email,
                                             Images = p.Image,
                                             Status = p.Status
                                         }).FirstOrDefault(x => x.Id == id);

            Company company = _context.Companies.FirstOrDefault(x => x.Id == id);

            if (editedCompany.Image is not null)
            {
                if (!editedCompany.Image.IsValidFile("image/"))
                {
                    ModelState.AddModelError(string.Empty, "Şəkil file seçin");
                    return View();
                }
                if (!editedCompany.Image.IsValidLength(2))
                {
                    ModelState.AddModelError(string.Empty, "Maximum ölçü 1 mb ola bilər");
                    return View();
                }
                await AdjustPlantPhoto(editedCompany.Image, company);
            }

            company.Name = editedCompany.Name;
            company.Email = editedCompany.Email;
            company.Status = OrderStatus.Pending;
            _context.SaveChanges();
            TempData["Edited"] = true;
            return RedirectToAction(nameof(MyCompany));
        }



        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------------------------------------





        public async Task<IActionResult> DeleteCompany(int id)
        {

            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);
            CompanyVM? company = _context.Companies.Include(v => v.Vacans).Select(p =>
                                                new CompanyVM
                                                {
                                                    Id = p.Id,
                                                    Name = p.Name,
                                                    Email = p.Email,
                                                    Images = p.Image
                                                }).FirstOrDefault(x => x.Id == id);

            return View(company);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCompany(int id, CompanyVM deleteCompany)
        {
            TempData["Deleted"] = false;
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);

            CompanyVM? companyVM = _context.Companies.Include(v => v.Vacans).Select(p =>
                                         new CompanyVM
                                         {
                                             Id = p.Id,
                                             Name = p.Name,
                                             Email = p.Email,
                                             Images = p.Image
                                         }).FirstOrDefault(x => x.Id == id);

            Company company = _context.Companies.FirstOrDefault(x => x.Id == id);

            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            string filepath = Path.Combine(imagefolderPath, "Company", company.Image);
            ExtensionMethods.DeleteImage(filepath);

            _context.Companies.Remove(company);
            _context.SaveChanges();
            TempData["Deleted"] = true;
            return RedirectToAction(nameof(MyCompany));
        }



        //-------------------------------------------------------------------------------------------------------------------------------------------------------


        public async Task<IActionResult> MyCompany()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBags(user);
            List<Company> companies = _context.Companies.Include(v => v.Vacans).Where(u => u.UserId == user.Id).ToList();
            return View(companies);
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
            ViewBag.Company = _context.Companies.Where(x => x.UserId == user.Id).ToList();

        }


        private async Task AdjustPlantPhoto(IFormFile image, Company company)
        {
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            string filepath = Path.Combine(imagefolderPath, "Company", company.Image);
            ExtensionMethods.DeleteImage(filepath);
            company.Image = await image.CreateImage(imagefolderPath, "Company");
        }



    }
}
