using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobBackEnd.Areas.HelloJobAdmins.Controllers
{
    [Authorize(Roles = "superadmin")]
    [Area("HelloJobAdmins")]
    public class UserController : Controller
    {
        private readonly HelloJobDbContext _context;
        private readonly UserService _userService;

        public UserController(HelloJobDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public IActionResult Index()
        {
            List<User> userList = _userService.GetAllUsers();
            return View(userList);
        }
    }
}
