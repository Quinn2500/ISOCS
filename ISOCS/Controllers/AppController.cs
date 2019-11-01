using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using DataModels;
using ISOCS.Models.AppViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISOCS.Controllers
{
    [Authorize(Roles = "Employee, Manager")]
    public class AppController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppLogic _appLogic;

        public AppController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _appLogic = new AppLogic();
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
            IList<string> roles = await _userManager.GetRolesAsync(loggedInUser);
            ViewBag.userRole = roles[0];
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CreateCertificate()
        {
            List<ApplicationUser> managers = new List<ApplicationUser>();
            ApplicationUser loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
            foreach (ApplicationUser usr in _userManager.Users.Where(u => u.CompanyName == loggedInUser.CompanyName).ToList())
            {
                foreach (string role in await _userManager.GetRolesAsync(usr))
                {
                    if (role == "Manager")
                    {
                        managers.Add(usr);
                        break;
                    }
                }
            }

            ViewBag.Managers = new SelectList(managers, "Email", "FullName"); 
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCertificate(CertificateViewModel model)
        {
            List<ApplicationUser> managers = new List<ApplicationUser>();
            ApplicationUser loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
            CertificateModel certificateModel = new CertificateModel
            {
                Name = model.Title,
                Description = model.Description,
                ResponsibleUserEmail  = model.ResponsibleUser,
                CompanyName = loggedInUser.CompanyName,
                CreatedByEmail= loggedInUser.Email,
                CreatedOn = DateTime.Now,
                EnableNotifications = model.EnableNotifications
            };

            foreach (ApplicationUser usr in _userManager.Users.Where(u => u.CompanyName == loggedInUser.CompanyName).ToList())
            {
                foreach (string role in await _userManager.GetRolesAsync(usr))
                {
                    if (role == "Manager")
                    {
                        managers.Add(usr);
                        break;
                    }
                }
            }

            ViewBag.Managers = new SelectList(managers, "Email", "FullName");

            _appLogic.CreateCertificateinDatabase(certificateModel);

            return RedirectToAction("CertificateOverview", new { id = certificateModel.Name });
        }

        [HttpGet]
        public async Task<IActionResult> CreateAction(string id)
        {
            List<ApplicationUser> managersAndEmployees = new List<ApplicationUser>();
            ApplicationUser loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
            foreach (ApplicationUser usr in _userManager.Users.Where(u => u.CompanyName == loggedInUser.CompanyName).ToList())
            {
                foreach (string role in await _userManager.GetRolesAsync(usr))
                {
                    if (role != "Admin" && role != "Owner")
                    {
                        managersAndEmployees.Add(usr);
                        break;
                    }
                }
            }

            ActionViewModel model = new ActionViewModel()
            {
                CertificateName = id
            };
            ViewBag.Users = new SelectList(managersAndEmployees, "Email", "FullName");
            ViewBag.Enums = new SelectList(Enum.GetValues(typeof(OccurenceEnum)).OfType<OccurenceEnum>().ToList());
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAction(ActionViewModel model)
        {
            ApplicationUser loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ActionModel actionModel = new ActionModel()
            {
                BeginDateTime = model.StartDate,
                Comments = null,
                CreatedByEmail = loggedInUser.Email,
                CreatedOn = DateTime.Now,
                Description = model.Description,
                Name = model.Title,
                Occurence = model.Occurence,
                ResponsibleUserEmail = model.ResponsibleUser
            };
            _appLogic.CreateActionInDatabase(actionModel, loggedInUser.CompanyName ,model.CertificateName);
            return RedirectToAction("CertificateOverview", new { id = model.CertificateName });
        }

        [HttpGet]
        public async Task<ActionResult> CertificateOverview(string id)
        {
            ApplicationUser loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
            CertificateModel certificateModel = _appLogic.GetCertificateModel(id, loggedInUser.CompanyName);
            CertificateOverviewModel viewModel = new CertificateOverviewModel()
            {
                Description = certificateModel.Description,
                EnableNotifications = certificateModel.EnableNotifications,
                ResponsibleUser = certificateModel.ResponsibleUserEmail,
                Title = certificateModel.Name,
                TaskNamesWeekly = new List<string>(),
                TaskNamesDaily = new List<string>(),
                TaskNamesYearly = new List<string>(),
                TaskNamesMonthly = new List<string>(),
                TaskNamesQuarterly = new List<string>(),
            };
            foreach (ActionModel action in certificateModel.Actions)
            {
                switch (action.Occurence)
                {
                    case OccurenceEnum.Daily:
                        viewModel.TaskNamesDaily.Add(action.Name);
                        break;
                    case OccurenceEnum.Weekly:
                        viewModel.TaskNamesWeekly.Add(action.Name);
                        break;
                    case OccurenceEnum.Monthly:
                        viewModel.TaskNamesMonthly.Add(action.Name);
                        break;
                    case OccurenceEnum.Quarterly:
                        viewModel.TaskNamesQuarterly.Add(action.Name);
                        break;
                    case OccurenceEnum.Yearly:
                        viewModel.TaskNamesYearly.Add(action.Name);
                        break;
                    default:
                        break;
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteCertificate(string id)
        {
            ApplicationUser loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
            _appLogic.DeleteCertificate(id, loggedInUser.CompanyName);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAction(string actionName, string certificateName)
        {
            ApplicationUser loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);
            _appLogic.DeleteAction(actionName, certificateName, loggedInUser.CompanyName);
            return RedirectToAction("CertificateOverview", new { id = certificateName });
        }

    }
}