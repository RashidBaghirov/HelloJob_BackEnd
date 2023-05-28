using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin, admin, moderator")]
    [Area("HelloJobAdmins")]
    public class SubscribeController : Controller
    {
        private readonly HelloJobDbContext _context;

        public SubscribeController(HelloJobDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Subscribe> subscribes = _context.Subscribe.ToList();
            return View(subscribes);
        }

        public IActionResult Delete(int id)
        {
            TempData["Delete"] = false;

            if (id == 0) return NotFound();


            Subscribe? subscribe = _context.Subscribe.FirstOrDefault(x => x.Id == id);

            if (subscribe is null) return NotFound();

            _context.Subscribe.Remove(subscribe);
            _context.SaveChanges();
            TempData["Delete"] = true;
            return RedirectToAction(nameof(Index));
        }

    }
}
