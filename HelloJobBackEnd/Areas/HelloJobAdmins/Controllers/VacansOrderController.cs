using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;
using System.Net;
using HelloJobBackEnd.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class VacansOrderController : Controller
    {

        private readonly HelloJobDbContext _context;

        public VacansOrderController(HelloJobDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Vacans>? vacans = _context.Vacans.Include(v => v.BusinessArea).
                Include(e => e.Education).
              Include(e => e.Experience).
              Include(c => c.City).
              Include(c => c.Company).
              Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
              Include(i => i.infoEmployeers).
               Include(i => i.InfoWorks).
              Include(o => o.OperatingMode).ToList();
            return View(vacans);
        }

        public IActionResult Accept(int id)
        {
            TempData["CompanyAccepted"] = false;
            Vacans? vacans = _context.Vacans.Include(v => v.BusinessArea).
               Include(e => e.Education).
             Include(e => e.Experience).
             Include(c => c.City).
             Include(c => c.Company).
                  ThenInclude(c => c.User).
             Include(c => c.BusinessArea).
             Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
             Include(i => i.infoEmployeers).
              Include(i => i.InfoWorks).
             Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);

            if (vacans is null) return NotFound();
            vacans.Status = OrderStatus.Accepted;
            _context.SaveChanges();
            TempData["CompanyAccepted"] = true;
            string recipientEmail = vacans.Company.Email;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/StickyacceptedMail.html"))
            {
                body = reader.ReadToEnd();
            }


            body = body.Replace("{{userFullName}}", vacans.Company.User.FullName);
            body = body.Replace("{{position}}", vacans.Position);
            body = body.Replace("{{companyName}}", vacans.Company.Name);

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
            Vacans? vacans = _context.Vacans.Include(v => v.BusinessArea).
               Include(e => e.Education).
             Include(e => e.Experience).
             Include(c => c.City).
             Include(c => c.Company).
                  ThenInclude(c => c.User).
             Include(c => c.BusinessArea).
             Include(c => c.BusinessArea).ThenInclude(b => b.BusinessTitle).
             Include(i => i.infoEmployeers).
              Include(i => i.InfoWorks).
             Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);


            if (vacans is null) return NotFound();
            vacans.Status = OrderStatus.Rejected;
            _context.SaveChanges();
            TempData["CompanyReject"] = true;

            string recipientEmail = vacans.Company.Email;


            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/StickyrejectedMail.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{userFullName}}", vacans.Company.User.FullName);
            body = body.Replace("{{position}}", vacans.Position);
            body = body.Replace("{{companyName}}", vacans.Company.Name);

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
            TempData["Deleted"] = false;
            Vacans? vacans = _context.Vacans.Include(v => v.BusinessArea).
                  Include(e => e.Education).
                Include(e => e.Experience).
                Include(c => c.City).
                Include(c => c.BusinessArea).
                Include(i => i.infoEmployeers).
                 Include(i => i.InfoWorks).
                Include(o => o.OperatingMode).FirstOrDefault(x => x.Id == id);
            List<WishListItem> wishlistItems = _context.WishListItems.Where(w => w.VacansId == vacans.Id).ToList();
            _context.WishListItems.RemoveRange(wishlistItems);

            _context.Vacans.Remove(vacans);
            _context.SaveChanges();
            TempData["Deleted"] = true;
            return RedirectToAction(nameof(Index));
        }

    }
}
