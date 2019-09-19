using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.AdminWeb.Models;
using Project.Application.AppService;
using Project.Application.IAppService;

namespace Project.AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAppService _userAppService;
        public HomeController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        public IActionResult Index()
        {
            ViewBag.User = _userAppService.GetByIdAsync(3).Result;

            //var list= _userAppService.

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
