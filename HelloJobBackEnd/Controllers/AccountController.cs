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

namespace HelloJobBackEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HelloJobDbContext _context;

        public AccountController(UserManager<User> usermanager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, HelloJobDbContext context)
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



        public async Task<IActionResult> Register(RegisterVM account)
        {
            TempData["Register"] = false;
            if (!ModelState.IsValid) return RedirectToAction("index", "home");
            User user = new()
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
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hellojob440@gmail.com", "HelloJOB");
            mail.To.Add(new MailAddress(user.Email));

            mail.Subject = "Verify Email";


            mail.Body = string.Empty;
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/verifyemail.html"))
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
        //	await _roleManager.CreateAsync(new IdentityRole(UserRole.business.ToString()));
        //	await _roleManager.CreateAsync(new IdentityRole(UserRole.employeer.ToString()));
        //}



    }
}
