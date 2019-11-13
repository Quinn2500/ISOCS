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
    public class NotificationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        private readonly  NotificationLogic _notificationLogic = new NotificationLogic();

        public IActionResult DailyTrigger()
        {
            _notificationLogic.DailyChecks();
            return StatusCode(418);
        }

        public async Task<IActionResult> ExecuteTask(string token, int actionHistoryId, string actionName, string certificateName, bool executionSucces, string userEmail)
        {
            _notificationLogic.ExecuteTask(token, actionHistoryId, actionName, certificateName, executionSucces, await _userManager.FindByEmailAsync(userEmail));
            return StatusCode(200);
        }
    }
}