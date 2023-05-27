using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloJobBackEnd.Controllers
{
    public class MessagesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HelloJobDbContext _context;

        public MessagesController(HelloJobDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            //if (User.Identity.IsAuthenticated)
            //{
            //    ViewBag.CurrentUserName = currentUser.UserName;
            //}

            //var messages = await _context.Messages.ToListAsync();
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Message message)
        //{
        //    var currentUser = await _userManager.GetUserAsync(User);

        //    if (!await _userManager.IsInRoleAsync(currentUser, "Admin"))
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    var adminRoleNames = Enum.GetNames(typeof(AdminRoles));
        //    var adminUsers = new List<User>();

        //    foreach (var roleName in adminRoleNames)
        //    {
        //        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
        //        adminUsers.AddRange(usersInRole);
        //    }

        //    foreach (var adminUser in adminUsers)
        //    {
        //        var adminMessage = new Message
        //        {
        //            UserId = adminUser.Id,
        //            Content = message.Content
        //        };

        //        await _context.Messages.AddAsync(adminMessage);
        //    }

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Index");
        //}
    }

}
