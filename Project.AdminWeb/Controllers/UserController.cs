using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Entity;
using Project.Common;
using System.Data;
using IService;
using Service;
namespace Project.AdminWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserAppService _userAppService;
        private readonly IRoleApp _roleAppService;
        public UserController(ILogger<UserController> logger, IUserAppService userAppService, IRoleApp roleAppService)
        {
            _logger = logger;
            _userAppService = userAppService;
            _roleAppService = roleAppService;
        }
        // GET: User

        public ActionResult List()
        {
            try
            {
                //UserAppService s = new UserAppService();
                ViewBag.User = _userAppService.GetById(3).Result;
                //var userList = s.GetFields(c => new { c.Id, c.UserName }, c => c.Id > 0).ToList();
                // ViewBag.List = userList.ToList().Select(s => Tuple.Create(s.Id, s.UserName));
                ViewBag.Role = _roleAppService.GetById(3).Result;
                return View();
            }
            catch (Exception ex)
            {

                return Content(ex.Message + ex.StackTrace);
            }

        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create(int? id)
        {
            var user = new User();
            //if (id is null)
            //{
            //    return View(user);
            //}
            //user = _userAppService.GetByIdAsync((int)id).Result;
            return View(user);
        }

        // POST: User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // _userAppService.CreateOrEdit(user);
                    return RedirectToAction("List");
                }
                return Content("验证");
                // TODO: Add insert logic here

                //return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }


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

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}