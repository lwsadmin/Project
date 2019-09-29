using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Application.IAppService;
using Project.MobileWeb.Models;

namespace Project.MobileWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserAppService _userAppService;
        public HomeController(ILogger<HomeController> logger, IUserAppService userAppService)
        {
            _logger = logger;
            _userAppService = userAppService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.LogError(".NET Core3.0自带了日志---------------");
            ViewBag.User = _userAppService.GetByIdAsync(3).Result;
            ViewBag.List = _userAppService.GetFields(c => c.Name, c => c.Id!="");
            var userList = _userAppService.GetFields(c => new { c.Id, c.UserName }, c => c.Id!="");
            foreach (var item in userList)
            {
                var s = item.Id;
            }
            ViewBag.UserList = userList.ToList().Select(s => Tuple.Create(s.Id, s.UserName));
            return View();
        }
        private T GetData<T>(List<T> list, Func<T, bool> predicate)
        {
            var s = list.Where(predicate).FirstOrDefault();
            if (s == null)
            {
                s = System.Activator.CreateInstance<T>();
            }
            return s;
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
