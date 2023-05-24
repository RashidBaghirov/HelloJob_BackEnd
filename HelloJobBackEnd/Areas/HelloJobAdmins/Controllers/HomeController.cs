using HelloJobBackEnd.Areas.HelloJobAdmins.ViewModel;
using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class HomeController : Controller
    {
        private readonly HelloJobDbContext _context;

        public HomeController(HelloJobDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            DashboardModel model = new()
            {
                CVs = _context.Cvs.Include(x => x.BusinessArea).
                Include(x => x.User).
                ToList(),
                Companies = _context.Companies.
                Include(x => x.Vacans).
                ThenInclude(x => x.BusinessArea).
                Include(x => x.User).
                ToList()
            };
            return View(model);
        }
    }

}

