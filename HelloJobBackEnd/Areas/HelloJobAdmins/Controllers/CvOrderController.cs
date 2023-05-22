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
using HelloJobBackEnd.Services.Interface;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class CvOrderController : Controller
    {
        private readonly HelloJobDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;
        private readonly ICvPageService _cvPageService;

        public CvOrderController(HelloJobDbContext context, IWebHostEnvironment env, IEmailService emailService, ICvPageService cvPageService)
        {
            _context = context;
            _env = env;
            _emailService = emailService;
            _cvPageService = cvPageService;
        }

        public IActionResult Index()
        {
            List<Cv>? cvs = _cvPageService.GetAllCvs().ToList();
            return View(cvs);
        }

        public IActionResult Accept(int id)
        {
            TempData["CVaccepted"] = false;
            Cv? cv = _cvPageService.Details(id);

            if (cv is null) return NotFound();
            cv.Status = OrderStatus.Accepted;
            _context.SaveChanges();
            TempData["CVaccepted"] = true;
            string recipientEmail = cv.User.Email;
            string subject = "Elanla Bağlı Məlumat";
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/StickyacceptedMail.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{userFullName}}", cv.User.FullName);
            body = body.Replace("{{position}}", cv.Position);
            body = body.Replace("{{companyName}}", string.Concat(cv.Name, " ", cv.Surname));
            _emailService.SendEmail(recipientEmail, subject, body);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Reject(int id)
        {
            TempData["CVrejected"] = false;
            Cv? cv = _cvPageService.Details(id);

            if (cv is null) return NotFound();
            cv.Status = OrderStatus.Rejected;
            _context.SaveChanges();
            TempData["CVrejected"] = true;
            string recipientEmail = cv.User.Email;
            string subject = "Elanla Bağlı Məlumat";
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/StickyrejectedMail.html"))
            {
                body = reader.ReadToEnd();
            }


            body = body.Replace("{{userFullName}}", cv.User.FullName);
            body = body.Replace("{{position}}", cv.Position);
            body = body.Replace("{{companyName}}", string.Concat(cv.Name, " ", cv.Surname));
            _emailService.SendEmail(recipientEmail, subject, body);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int id)
        {
            Cv? cv = _cvPageService.Details(id);
            return View(cv);
        }

        public async Task<IActionResult> Delete(int id)
        {
            TempData["Delete"] = false;
            Cv? cv = _cvPageService.Details(id);
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            string filepath = Path.Combine(imagefolderPath, "User", cv.Image);
            ExtensionMethods.DeleteImage(filepath);
            string pdfpath = Path.Combine(imagefolderPath, "User", "CVs", cv.CvPDF);
            ExtensionMethods.DeleteImage(pdfpath);
            List<WishListItem> wishlistItems = _context.WishListItems.Where(w => w.CvId == cv.Id).ToList();
            _context.WishListItems.RemoveRange(wishlistItems);
            _context.Cvs.Remove(cv);
            _context.SaveChanges();
            TempData["Delete"] = true;
            return RedirectToAction(nameof(Index));
        }


    }
}
