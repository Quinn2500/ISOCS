using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using ISOCS.Models.OwnerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ISOCS.Controllers
{
    public class OwnerController : Controller
    {
        private readonly OwnerLogic _logic = new OwnerLogic();

        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                CompanyModels = _logic.GetAllCompanyModels()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddCompany(string name, string userEmail)
        {
            _logic.AddCompany(name);
            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteCompany(string name)
        {
            _logic.DeleteCompany(name);
            return Ok();
        }
    }
}