using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin")]
    [Area("HelloJobAdmins")]
    public class AdminRegisterController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HelloJobDbContext _context;

        public AdminRegisterController(UserManager<User> usermanager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, HelloJobDbContext context)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM account)
        {
            TempData["Register"] = false;
            if (!ModelState.IsValid) return View();
            User user = new()
            {
                FullName = string.Concat(account.Firstname, " ", account.Lastname),
                Email = account.Email,
                UserName = account.Username
            };
            IdentityResult result = await _usermanager.CreateAsync(user, account.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError message in result.Errors)
                {
                    ModelState.AddModelError("", message.Description);
                }
                return View();
            }
            TempData["Register"] = true;
            await _usermanager.AddToRoleAsync(user, account.adminRoles.ToString());

            return RedirectToAction("login", "adminLogin");

        }




        public async Task<IActionResult> Security()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);
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
            return RedirectToAction("Login", "adminlogin");
        }
    }
}
