using System.IO;
using System.Threading.Tasks;
using Business;
using DataModels;
using ISOCS.Models.NotificationViewModels;
using ISOCS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 

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
        private readonly EmailLogic _emailLogic = new EmailLogic();

        public IActionResult DailyTrigger()
        {
             SendTodayNotificationMails();
             Send3DayNotificationMails();
             SendNotCompletedActionMail();

            return StatusCode(418);
        }

        public async Task<ActionResult> ExecuteTask(string token, int actionHistoryId, string certificateName, string actionName, bool executionSucces ,string userEmail)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(userEmail);
           

            ExecuteTaskNotificationViewModel viewModel = new ExecuteTaskNotificationViewModel
            {
                ActionName = actionName,
                CertificateName = certificateName,
                FirstName = user.Firstname,
                ExecutionSucces = executionSucces
            };
            if (token.Equals(_notificationLogic.GetToken(actionHistoryId)))
            {
                viewModel.IsSucces = true;
                _notificationLogic.ExecuteTask(actionName, certificateName, executionSucces, user);
                return View(viewModel);
            }

            viewModel.IsSucces = false;
            return View(viewModel);

        }

        private async void SendTodayNotificationMails()
        {
            foreach (int id in _notificationLogic.CheckTodayNotifications())
            {
                ActionToComplete action = _notificationLogic.GetAction(id);
                if (action.Action.EnableNotifications)
                {
                    ApplicationUser user = await _userManager.FindByEmailAsync(action.Action.ResponsibleUserEmail);
                    ExecuteTaskMailViewModel viewModel = new ExecuteTaskMailViewModel
                    {
                        ActionId = id,
                        CertificateName = action.Action.CertificateName,
                        Description = action.Action.Description,
                        FirstName = user.Firstname,
                        Name = action.Action.Name,
                        Token = _notificationLogic.GetToken(id),
                        UserEmail = action.Action.ResponsibleUserEmail
                    };
                    string result = await _viewRenderService.RenderToStringAsync("Notification/ExecuteEmail", viewModel);
                    _emailLogic.SendEmail("ISOCS Handeling", result, "quinnvv2000@gmail.com");
                }               
            }
        }

        private async void Send3DayNotificationMails()
        {
            foreach (int id in _notificationLogic.Check3DayNotifications())
            {
                ActionToComplete action = _notificationLogic.GetAction(id);
                if (action.Action.EnableNotifications)
                {
                    ApplicationUser user = await _userManager.FindByEmailAsync(action.Action.ResponsibleUserEmail);
                    ThreeDayNotificationViewModel viewModel = new ThreeDayNotificationViewModel
                    {
                        CertificateName = action.Action.CertificateName,
                        FirstName = user.Firstname,
                        DateToExecute = action.DateToExecute,
                        ActionName = action.Action.Name,
                        Description = action.Action.Description
                    };
                    string result = await _viewRenderService.RenderToStringAsync("Notification/ThreeDayNotification", viewModel);
                    _emailLogic.SendEmail("ISOCS Handeling", result, "quinnvv2000@gmail.com");
                }
            }
        }

        private async void SendNotCompletedActionMail()
        {
            foreach (int id in _notificationLogic.CheckNotCompletedTasks())
            {
                ActionToComplete action = _notificationLogic.GetAction(id);
                string cerUserEmail = _notificationLogic.GetCertificateResponsibleUserEmail(id);
                ApplicationUser manager = await _userManager.FindByEmailAsync(cerUserEmail);
                ApplicationUser employee = await _userManager.FindByEmailAsync(action.Action.ResponsibleUserEmail);

                NotCompletedEmailManagerViewModel viewModelManager = new NotCompletedEmailManagerViewModel
                {
                    CertificateName = action.Action.CertificateName,
                    FirstNameManager = manager.Firstname,
                    DateToExecute = action.DateToExecute,
                    ActionName = action.Action.Name,
                    Description = action.Action.Description,
                    EmailEmployee = action.Action.ResponsibleUserEmail,
                    FullnameEmployee = employee.FullName
                };

                NotCompletedEmailEmployeeViewModel viewModelEmployee = new NotCompletedEmailEmployeeViewModel
                {
                    ActionId = id,
                    CertificateName = action.Action.CertificateName,
                    Description = action.Action.Description,
                    FirstName = employee.Firstname,
                    Name = action.Action.Name,
                    Token = _notificationLogic.GetToken(id),
                    UserEmail = action.Action.ResponsibleUserEmail,
                    DateToExecute = action.DateToExecute
                };

                string resultManager = await _viewRenderService.RenderToStringAsync("Notification/UncompletedTaskManager", viewModelManager);
                _emailLogic.SendEmail("ISOCS Handeling", resultManager, "quinnvv2000@gmail.com");

                string resultEmployee = await _viewRenderService.RenderToStringAsync("Notification/UncompletedTaskEmployee", viewModelEmployee);
                _emailLogic.SendEmail("ISOCS Handeling", resultEmployee, "quinnvv2000@gmail.com");
            }
        }

    }
}