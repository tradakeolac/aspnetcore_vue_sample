using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Saleman.Web.Api.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult CPanel()
        {
            return View();
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Stores()
        {
            return View();
        }
        
        public IActionResult Categories()
        {
            return View();
        }

        public IActionResult Media()
        {
            return View();
        }

        public IActionResult Tables()
        {
            return View();
        }

        public IActionResult FormCommon()
        {
            return View();
        }

        public IActionResult FormValidation()
        {
            return View();
        }

        public IActionResult FormWinzard()
        {
            return View();
        }
    }
}
