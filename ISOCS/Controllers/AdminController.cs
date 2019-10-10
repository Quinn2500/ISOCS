using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISOCS.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AdminLogic _adminLogic = new AdminLogic();

        public AdminController( UserManager<ApplicationUser> userManager)
        { _userManager = userManager;}

        public async Task<IActionResult> Index()
        {
            ViewBag.Roles = _userManager;
            ApplicationUser loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
            List<ApplicationUser> result = new List<ApplicationUser>();
            foreach (string userEmail in _adminLogic.GetAllUsersEmailAdresses(loggedInUser.CompanyName))
            {
                result.Add(await _userManager.FindByEmailAsync(userEmail));
            }

            ViewBag.UsersFromCompany = result;
            return View();
        }
    }
}