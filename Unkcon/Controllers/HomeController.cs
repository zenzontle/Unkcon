using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unkcon.Authentication;
using Unkcon.Models;

namespace Unkcon.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private UserManagerDecorator _userManager;

        public HomeController()
        {
            _userManager = new UserManagerDecorator(_db);
        }

        public async Task<ActionResult> Index()
        {
            ApplicationUser currentUser = await _userManager.GetCurrentUserAsync(User.Identity.GetUserId());

            if (currentUser == null)
            {
                return View("WelcomeScreen");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Tinder for messages/thoughts.";

            return View();
        }
    }
}