using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Unkcon.Models;

namespace Unkcon.Authentication
{
    public class UserManagerDecorator
    {
        private UserManager<ApplicationUser> _userManager;

        public UserManagerDecorator(ApplicationDbContext applicationDbContext)
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(applicationDbContext));
        }

        public ApplicationUser GetCurrentUser(string userId)
        {
            if (userId == null)
            {
                return null;
            }

            ApplicationUser currentUser = _userManager.FindById<ApplicationUser>(userId);

            if (currentUser == null)
            {
                return null;
            }

            return currentUser;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync(string userId)
        {
            if (userId == null)
            {
                return null;
            }

            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

            if (currentUser == null)
            {
                return null;
            }

            return currentUser;
        }
    }
}