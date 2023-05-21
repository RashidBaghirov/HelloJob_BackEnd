using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.EntityFrameworkCore;

namespace HelloJobBackEnd.Services
{
    public class RequestService
    {
        private readonly HelloJobDbContext _context;
        private readonly EmailService _emailService;

        public RequestService(HelloJobDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public Request GetRequestWithRelatedData(int requestId, User user)
        {
            Request? request = _context.Requests
                .Include(r => r.RequestItems)
                .ThenInclude(ri => ri.Cv)
                .Include(r => r.RequestItems)
                .ThenInclude(ri => ri.Cv.User)
                .Include(r => r.RequestItems)
                .ThenInclude(ri => ri.Vacans)
                .ThenInclude(v => v.Company)
                .FirstOrDefault(r => r.Id == requestId && (r.User == user || r.RequestItems.Any(ri => ri.Vacans.Company.User == user)));
            return request;
        }

        public void AcceptRequestItem(RequestItem requestItem)
        {
            requestItem.Status = OrderStatus.Accepted;
            _context.SaveChanges();

            string recipientEmail = requestItem.Cv.Email;

            string body = GetAcceptanceEmailBody(requestItem);

            _emailService.SendEmail(recipientEmail, "Bildiriş", body);
        }

        public void RejectRequestItem(RequestItem requestItem)
        {
            requestItem.Status = OrderStatus.Rejected;
            _context.SaveChanges();

            string recipientEmail = requestItem.Cv.Email;

            string body = GetRejectionEmailBody(requestItem);

            _emailService.SendEmail(recipientEmail, "Bildiriş", body);
        }

        private string GetAcceptanceEmailBody(RequestItem requestItem)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/acceptedmail.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{userFullName}}", string.Concat(requestItem.Cv.Name, " ", requestItem.Cv.Surname));
            body = body.Replace("{{companyName}}", requestItem.Vacans.Company.Name);
            body = body.Replace("{{position}}", requestItem.Vacans.Position);

            return body;
        }

        private string GetRejectionEmailBody(RequestItem requestItem)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/RejectedMail.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{userFullName}}", string.Concat(requestItem.Cv.Name, " ", requestItem.Cv.Surname));
            body = body.Replace("{{companyName}}", requestItem.Vacans.Company.Name);
            body = body.Replace("{{position}}", requestItem.Vacans.Position);

            return body;
        }
    }
}
