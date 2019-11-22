using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using ISOCS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISOCS.Controllers
{
    public class RenderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IViewRenderService _viewRenderService;

        public RenderController(UserManager<ApplicationUser> userManager, IViewRenderService viewRenderService)
        {
            _userManager = userManager;
            _viewRenderService = viewRenderService;
        }
        public async Task<string> RenderViewToString(string viewLocation, object viewModel)
        {
            return await _viewRenderService.RenderToStringAsync(viewLocation, viewModel);
        }
    }
}