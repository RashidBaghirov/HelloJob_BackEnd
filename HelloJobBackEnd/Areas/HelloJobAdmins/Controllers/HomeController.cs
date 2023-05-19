using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Principal;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Area("HelloJobAdmins")]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HelloJobDbContext _context;

        public HomeController(UserManager<User> usermanager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, HelloJobDbContext context)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _usermanager = usermanager;
            _context = context;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            TempData["Login"] = false;
            if (!ModelState.IsValid) return RedirectToAction("login", "home");

            User user = await _usermanager.FindByNameAsync(login.UserName);
            if (user is null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return RedirectToAction("login", "home");
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Due to your efforts, our account was blocked for 5 minutes");
                }
                ModelState.AddModelError("", "Username or password is incorrect");
                return RedirectToAction("login", "home");
            }
            TempData["Login"] = true;
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "superadmin, admin, moderator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
