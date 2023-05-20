using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HelloJobBackEnd.Services
{
    public class LayoutService
    {
        private readonly HelloJobDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<User> _userManager;

        public LayoutService(HelloJobDbContext context, IHttpContextAccessor accessor, UserManager<User> userManager)
        {
            _context = context;
            _accessor = accessor;
            _userManager = userManager;
        }

        public async Task<string> GetUserFullName()
        {
            var user = await _userManager.GetUserAsync(_accessor.HttpContext.User);
            return user?.FullName;
        }

        public Dictionary<string, string> GetSettings()
        {
            Dictionary<string, string> settings = _context.Settings.ToDictionary(s => s.Key, s => s.Value);

            return settings;
        }


    }
}
