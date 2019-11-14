using System.IO;
using System.Threading.Tasks;
using Business;
using DataModels;
using ISOCS.Models.NotificationViewModels;
using ISOCS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace ISOCS.Controllers
{
    public class NotificationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IViewRenderService _viewRenderService;

        public NotificationController(UserManager<ApplicationUser> userManager, IViewRenderService viewRenderService)
        {
            _userManager = userManager;
            _viewRenderService = viewRenderService;
        }
        private readonly  NotificationLogic _notificationLogic = new NotificationLogic();

        public async Task<IActionResult> DailyTrigger()
        {
            TestViewModel test = new TestViewModel
            {
                Title = "test"
            };

            string result = await _viewRenderService.RenderToStringAsync("Notification/ExecuteEmail", test);
            _notificationLogic.DailyChecks();

            return StatusCode(418);
        }

    }
}