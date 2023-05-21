using HelloJobBackEnd.Entities;
using HelloJobBackEnd.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using HelloJobBackEnd.Services.Interface;

namespace HelloJobBackEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HelloJobDbContext _context;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<User> usermanager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, HelloJobDbContext context, IEmailService emailService)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _usermanager = usermanager;
            _emailService = emailService;
            _context = context;
        }



        public async Task<IActionResult> Register(RegisterVM account)
        {
            TempData["Register"] = false;
            if (!ModelState.IsValid) return RedirectToAction("index", "home");

            User user = new User
            {
                FullName = string.Concat(account.Firstname, " ", account.Lastname),
                Email = account.Email,
                UserName = account.Username
            };

            if (_usermanager.Users.Any(x => x.NormalizedEmail == account.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "Bu e-poçtda istifadəçi mövcuddur");
                return RedirectToAction("index", "home");
            }

            IdentityResult result = await _usermanager.CreateAsync(user, account.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError message in result.Errors)
                {
                    ModelState.AddModelError("", message.Description);
                }
                return RedirectToAction("index", "home");
            }

            string token = await _usermanager.GenerateEmailConfirmationTokenAsync(user);
            string link = Url.Action(nameof(VerifyEmail), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());
            string body = GetVerifyEmailBody(user.FullName, link);

            _emailService.SendEmail(user.Email, "Verify Email", body);

            await _usermanager.AddToRoleAsync(user, account.userRole.ToString());

            TempData["Register"] = true;
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            User user = await _usermanager.FindByEmailAsync(email);
            if (user == null) return BadRequest();

            await _usermanager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home");
        }

        private string GetVerifyEmailBody(string userFullName, string link)
        {
            string body;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/verifyemail.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{userFullName}}", userFullName);
            body = body.Replace("{{link}}", link);
            return body;
        }

        public async Task<IActionResult> Login(LoginVM account)
        {
            TempData["Login"] = false;
            if (!ModelState.IsValid) return RedirectToAction("index", "home");

            User user = await _usermanager.FindByNameAsync(account.UserName);
            if (user is null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return RedirectToAction("index", "home");
            }

            var userRoles = await _usermanager.GetRolesAsync(user);

            if (userRoles.Contains(UserRole.business.ToString()) || userRoles.Contains(UserRole.employeer.ToString()))
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, account.Password, account.RememberMe, true);

                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Due to your efforts, our account was blocked for 5 minutes");
                    }
                    ModelState.AddModelError("", "Username or password is incorrect");
                    return RedirectToAction("index", "home");
                }
                TempData["Login"] = true;
            }
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        public async Task<IActionResult> ForgotPassword(AccountVM account)
        {
            TempData["ForgotPassword"] = false;
            if (account.User.Email is null) return RedirectToAction("index", "home");
            User user = await _usermanager.FindByEmailAsync(account.User.Email);

            if (user is null) return RedirectToAction("index", "home");

            string token = await _usermanager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hellojob440@gmail.com", "HelloJOB");
            mail.To.Add(new MailAddress(user.Email));

            mail.Subject = "Reset Password";
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/ResetPassword.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{userFullName}}", user.FullName);
            mail.Body = body.Replace("{{link}}", link);
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;


            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("hellojob440@gmail.com", "eomddhluuxosvnoy");

            smtp.Send(mail);
            TempData["ForgotPassword"] = true;
            return RedirectToAction("Index", "Home");


        }


        public async Task<IActionResult> ResetPassword(string email, string token)
        {

            User user = await _usermanager.FindByEmailAsync(email);
            if (user == null) BadRequest();

            AccountVM model = new()
            {
                User = user,
                Token = token
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(AccountVM account)
        {
            User user = await _usermanager.FindByEmailAsync(account.User.Email);
            AccountVM model = new()
            {
                User = user,
                Token = account.Token
            };
            if (!ModelState.IsValid) return View(model);
            await _usermanager.ResetPasswordAsync(user, account.Token, account.Password);
            return RedirectToAction("Index", "Home");
        }


        //public async Task CreateRoles()
        //{
        //await _roleManager.CreateAsync(new IdentityRole(UserRole.business.ToString()));
        //await _roleManager.CreateAsync(new IdentityRole(UserRole.employeer.ToString()));

        //await _roleManager.CreateAsync(new IdentityRole(AdminRoles.superadmin.ToString()));
        //await _roleManager.CreateAsync(new IdentityRole(AdminRoles.admin.ToString()));
        //await _roleManager.CreateAsync(new IdentityRole(AdminRoles.moderator.ToString()));

        //}



    }
}
