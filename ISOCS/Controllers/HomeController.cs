using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business;
using DataModels;
using Microsoft.AspNetCore.Mvc;
using ISOCS.Models;
using Org.BouncyCastle.Crypto.Tls;

namespace ISOCS.Controllers
{
    public class HomeController : Controller
    {
        private UserLogic userLogic = new UserLogic();

        public IActionResult Index(LoginModel model)
        {
            CertificateModel cer = new CertificateModel
            {
                Name = "test"
            };
            CertificateModel cer1 = new CertificateModel
            {
                Name = "test1"
            };
            List<CertificateModel> list = new List<CertificateModel>();
            list.Add(cer);
            list.Add(cer1);
            ViewBag.Certificates = list;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (userLogic.CheckPassword(model.email, model.password))
                {
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    {
                        return View(model);
                    }
                }
            }
        }

        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                UserModel user = new UserModel
                {
                    Email = model.email,
                    Firstname = model.firstname,
                    Lastname = model.lastname,
                    Preposition = model.preposition
                };
                userLogic.RegisterUser(user, Security.HashPassword(model.password));
                ViewBag.user = user;
                return View();

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
