﻿using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;
using System.Net;
using HelloJobBackEnd.Utilities.Extension;
using HelloJobBackEnd.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class CompanyOrderController : Controller
    {
        private readonly HelloJobDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CompanyOrderController(HelloJobDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Company> companies = _context.Companies.Include(u => u.User).Include(v => v.Vacans).ToList();
            return View(companies);
        }

        public IActionResult Accept(int id)
        {
            TempData["CompanyAccepted"] = false;
            Company? company = _context.Companies.Include(u => u.User).Include(v => v.Vacans).FirstOrDefault(x => x.Id == id);

            if (company is null) return NotFound();
            company.Status = OrderStatus.Accepted;
            _context.SaveChanges();
            TempData["CompanyAccepted"] = true;
            string recipientEmail = company.Email;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/StickyacceptedMail.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{userFullName}}", company.User.FullName);
            body = body.Replace("{{companyName}}", company.Name);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hellojob440@gmail.com", "HelloJOB");
            mail.To.Add(new MailAddress(recipientEmail));
            mail.Subject = "Elanla Bağlı Məlumat";
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("hellojob440@gmail.com", "eomddhluuxosvnoy");

            smtp.Send(mail);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Reject(int id)
        {
            TempData["CompanyReject"] = false;
            Company? company = _context.Companies.Include(u => u.User).Include(v => v.Vacans).FirstOrDefault(x => x.Id == id);

            if (company is null) return NotFound();
            company.Status = OrderStatus.Rejected;
            _context.SaveChanges();
            TempData["CompanyReject"] = true;

            string recipientEmail = company.Email;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/StickyrejectedMail.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{userFullName}}", company.User.FullName);
            body = body.Replace("{{companyName}}", company.Name);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hellojob440@gmail.com", "HelloJOB");
            mail.To.Add(new MailAddress(recipientEmail));
            mail.Subject = "Elanla Bağlı Məlumat";
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("hellojob440@gmail.com", "eomddhluuxosvnoy");

            smtp.Send(mail);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            TempData["Delete"] = false;
            Company company = _context.Companies.FirstOrDefault(x => x.Id == id);
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            string filepath = Path.Combine(imagefolderPath, "Company", company.Image);
            ExtensionMethods.DeleteImage(filepath);

            _context.Companies.Remove(company);
            _context.SaveChanges();
            TempData["Delete"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
