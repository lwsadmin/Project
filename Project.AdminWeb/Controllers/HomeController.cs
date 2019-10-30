using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.AdminWeb.Models;
using Service;
namespace Project.AdminWeb.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserAppService _userAppService;
        public HomeController(ILogger<HomeController> logger, IUserAppService userAppService)
        {
            _logger = logger;
            _userAppService = userAppService;
        }

        public IActionResult Index()
        {
            string sql = @$"SELECT Id,fullName,HeadImg,Mobile,case Sex
when 1 then '男'
else '女'
end Sex
FROM TMember ORDER BY id offset 100 rows FETCH next 10 rows ONLY";
            var s = _userAppService.GetDataTableAsync("");
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
