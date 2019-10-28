using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISOCS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AdminLogic _adminLogic = new AdminLogic();

        public AdminController( UserManager<ApplicationUser> userManager)
        { _userManager = userManager;}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ApplicationUser loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
            List<ApplicationUser> result = new List<ApplicationUser>();
            foreach (ApplicationUser user in _userManager.Users.Where(u => u.CompanyName == loggedInUser.CompanyName).ToList())
            {
                result.Add(user);
            }

            ViewBag.UsersFromCompany = result;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index), "Admin");
        }

        [HttpPost]
        public IActionResult InviteUsers(List<string> id)
        {
          
            return RedirectToAction(nameof(Index), "Admin");
        }

    }
}