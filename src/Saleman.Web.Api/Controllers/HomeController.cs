using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Saleman.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Aspnet core ecommerce";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

