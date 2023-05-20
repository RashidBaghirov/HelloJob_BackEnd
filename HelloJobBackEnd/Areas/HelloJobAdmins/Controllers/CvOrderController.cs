﻿using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class CvOrderController : Controller
    {
        private readonly HelloJobDbContext _context;

        public CvOrderController(HelloJobDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Cv>? cvs = _context.Cvs.Include(v => v.BusinessArea).
                Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.User).
              Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
              Include(o => o.OperatingMode).ToList();
            return View(cvs);
        }

        public IActionResult Accept(int id)
        {
            TempData["CVaccepted"] = false;
            Cv? cv = _context.Cvs.Include(v => v.BusinessArea).
                 Include(e => e.Education).
               Include(e => e.Experience).
               Include(c => c.City).
               Include(c => c.User).
               Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
               Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);

            if (cv is null) return NotFound();
            cv.Status = OrderStatus.Accepted;
            _context.SaveChanges();
            TempData["CVaccepted"] = true;
            string recipientEmail = cv.User.Email;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/StickyacceptedMail.html"))
            {
                body = reader.ReadToEnd();
            }


            body = body.Replace("{{userFullName}}", cv.User.FullName);
            body = body.Replace("{{position}}", cv.Position);
            body = body.Replace("{{companyName}}", string.Concat(cv.Name, " ", cv.Surname));

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
            TempData["CVrejected"] = false;
            Cv? cv = _context.Cvs.Include(v => v.BusinessArea).
                 Include(e => e.Education).
               Include(e => e.Experience).
               Include(c => c.City).
               Include(c => c.User).
               Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
               Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);

            if (cv is null) return NotFound();
            cv.Status = OrderStatus.Rejected;
            _context.SaveChanges();
            TempData["CVrejected"] = true;
            string recipientEmail = cv.User.Email;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/StickyrejectedMail.html"))
            {
                body = reader.ReadToEnd();
            }


            body = body.Replace("{{userFullName}}", cv.User.FullName);
            body = body.Replace("{{position}}", cv.Position);
            body = body.Replace("{{companyName}}", string.Concat(cv.Name, " ", cv.Surname));

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


        public IActionResult Details(int id)
        {
            Cv? cv = _context.Cvs.Include(v => v.BusinessArea).
              Include(e => e.Education).
            Include(e => e.Experience).
            Include(c => c.City).
            Include(c => c.User).
            Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
            Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);

            return View(cv);
        }

    }
}
