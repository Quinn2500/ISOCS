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
        private CertificateModel certificateToCreate;

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
            certificateToCreate = new CertificateModel(); 
            return View();
        }

        [HttpPost]
        public ActionResult CreateCertificate(CertificateViewModel model)
        {
            ViewBag.Actions = certificateToCreate.Actions;
            certificateToCreate = new CertificateModel();
            dynamic mymodel = new ExpandoObject();
            mymodel.model = model;
            mymodel.actions = certificateToCreate.Actions;
            return View(mymodel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAction()
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

            ViewBag.Users = new SelectList(managersAndEmployees, "Email", "FullName");
            ViewBag.Enums = new SelectList(Enum.GetValues(typeof(OccurenceEnum)).OfType<OccurenceEnum>().ToList());
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAction(ActionViewModel model)
        {
            ActionModel actionModel = new ActionModel()
            {
                BeginDateTime = model.StartDate,
                Comments = null,
                CreatedBy = await _userManager.FindByNameAsync(User.Identity.Name),
                CreatedOn = DateTime.Now,
                Description = model.Description,
                Name = model.Title,
                Occurence = model.Occurence,
                ResponsibleUser = await _userManager.FindByEmailAsync(model.ResponsibleUser)

            };

            certificateToCreate.Actions.Add(actionModel);
            ViewBag.Actions = certificateToCreate.Actions;
            return View(model);
        }

        [HttpGet]
        public AllActionsPartialViewModel GetActionsPartialViewModel()
        {
            AllActionsPartialViewModel model = new AllActionsPartialViewModel
            {
                TasksDaily = new List<string>(),
                TasksWeekly = new List<string>(),
                TasksMonthly = new List<string>(),
                TasksQuarterly = new List<string>(),
                TasksYearly = new List<string>()
            };
            foreach (ActionModel action in _appLogic.GetAllActionsFromCertificate())
            {
                switch (action.Occurence)
                {
                    case OccurenceEnum.Daily:
                        model.TasksDaily.Add(action.Name);
                        break;
                    case OccurenceEnum.Weekly:
                        model.TasksWeekly.Add(action.Name);
                        break;
                    case OccurenceEnum.Monthly:
                        model.TasksMonthly.Add(action.Name);
                        break;
                    case OccurenceEnum.Quarterly:
                        model.TasksQuarterly.Add(action.Name);
                        break;
                    case OccurenceEnum.Yearly:
                        model.TasksYearly.Add(action.Name);
                        break;
                    default:
                        break;
                }
            }

            return model;
        }
    }
}