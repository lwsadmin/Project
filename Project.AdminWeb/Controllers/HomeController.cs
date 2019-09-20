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
            // throw new Exception($"{DateTime.Now}我扔出来一个异常！");
            ViewBag.User = _userAppService.GetByIdAsync(3).Result;
            ViewBag.List = _userAppService.GetFields(c => c.Name, c => c.Id > 0);
            var userList = _userAppService.GetFields(c => new { c.Id, c.UserName }, c => c.Id > 0);
            foreach (var item in userList)
            {
                var s = item.Id;
            }
            ViewBag.UserList = userList.ToList().Select(s => Tuple.Create(s.Id, s.UserName)); 

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
