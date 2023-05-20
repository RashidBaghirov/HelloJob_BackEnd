using HelloJobBackEnd.DAL;
using HelloJobBackEnd.Entities;
using HelloJobBackEnd.Utilities.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace HelloJobBackEnd.Controllers
{
    public class LikedController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly HelloJobDbContext _context;

        public LikedController(UserManager<User> usermanager, HelloJobDbContext context)
        {
            _usermanager = usermanager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);

            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<WishList> wishlists = _context.WishLists
                .Include(x => x.User)
       .Include(x => x.WishListItems)
           .ThenInclude(c => c.Cv)
       .Include(x => x.WishListItems)
           .ThenInclude(c => c.Vacans)
               .ThenInclude(v => v.Company)
       .Include(x => x.WishListItems)
           .ThenInclude(c => c.Vacans)
               .ThenInclude(v => v.BusinessArea)
                   .ThenInclude(ba => ba.BusinessTitle)
                   .Include(x => x.WishListItems)
           .ThenInclude(c => c.Cv)
               .ThenInclude(v => v.OperatingMode)
                  .Include(x => x.WishListItems)
           .ThenInclude(c => c.Cv)
               .ThenInclude(v => v.Experience)
       .Where(w => w.UserId == user.Id)
       .ToList();
            ViewBag.Setting = _context.Settings.ToDictionary(s => s.Key, s => s.Value);

            return View(wishlists);
        }

        public async Task<IActionResult> AddToWishlist(int itemId, string itemType)
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);

            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }

            WishList wishlist = _context.WishLists
                .Include(w => w.WishListItems)
                .FirstOrDefault(w => w.UserId == user.Id);

            if (wishlist is null)
            {
                wishlist = new WishList
                {
                    UserId = user.Id,
                    User = user,
                    WishListItems = new List<WishListItem>()
                };

                _context.WishLists.Add(wishlist);
            }
            WishListItem existingItem = wishlist.WishListItems.FirstOrDefault(item => item.VacansId == itemId || item.CvId == itemId);
            if (existingItem != null)
            {
                return RedirectToAction("Index", "Home");
            }
            WishListItem wishlistItem = new WishListItem
            {
                WishListId = wishlist.Id
            };

            if (itemType == "Cv")
            {
                Cv cv = _context.Cvs.FirstOrDefault(c => c.Id == itemId);
                if (cv is null)
                {
                    return RedirectToAction("Index", "Home");
                }

                wishlistItem.CvId = cv.Id;
                wishlistItem.IsLiked = true;
            }
            else if (itemType == "Vacans")
            {
                Vacans vacans = _context.Vacans.FirstOrDefault(v => v.Id == itemId);
                if (vacans is null)
                {
                    return RedirectToAction("Index", "Home");
                }

                wishlistItem.VacansId = vacans.Id;
                wishlistItem.IsLiked = true;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            wishlist.WishListItems.Add(wishlistItem);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> RemoveFromWishlist(int itemId, string itemType)
        {
            User user = await _usermanager.FindByNameAsync(User.Identity.Name);

            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }

            WishList? wishlist = _context.WishLists
                .Include(w => w.WishListItems)
                .FirstOrDefault(w => w.UserId == user.Id);

            if (wishlist is null)
            {
                return RedirectToAction("Index", "Home");
            }

            WishListItem wishlistItem = null;

            if (itemType == "Cv")
            {
                wishlistItem = wishlist.WishListItems.FirstOrDefault(item => item.CvId == itemId);
            }
            else if (itemType == "Vacans")
            {
                wishlistItem = wishlist.WishListItems.FirstOrDefault(item => item.VacansId == itemId);
            }

            if (wishlistItem != null)
            {
                wishlist.WishListItems.Remove(wishlistItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
