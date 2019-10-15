using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IService;
using Project.Common;
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
        //    ViewBag.User = _userAppService.Get(c=>c.Id==3);
            ViewBag.List = _userAppService.GetFields(c => c.Name, c => c.Id > 0);
            var userList = _userAppService.GetFields(c => new { c.Id, c.UserName }, c => c.Id > 0).ToList();
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
        [AllowAnonymous]
        public ActionResult ExportExcel()
        {
            ExcelHelp excel = new ExcelHelp();
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("性别", typeof(string));
            dt.Columns.Add("电话", typeof(string));

            dt.Rows.Add("李磊", "男", "15898653259");
            dt.Rows.Add("张执先", "男", "19158974563");
            dt.Rows.Add("韩志成", "女", "16498653245");


            var ms = excel.ExportDataTableToExcel(dt, "MyTest");

            return File(ms.ToArray(), "application/vnd.ms-excel", "导出.xlsx");
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
