using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using X.PagedList;
namespace Project.AdminWeb.Areas.Member.Controllers
{
    [Area("Member")]

    public class MemberController : Controller
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IUserAppService _userAppService;
        public MemberController(ILogger<MemberController> logger, IUserAppService userAppService)
        {
            _logger = logger;
            _userAppService = userAppService;
        }

       // [ResponseCache(Duration = 1000)]
        public IActionResult Index(int page = 1, int pageSize = 10)
        {

            string sql = @$"SELECT Id,STUFF(fullName,2,1,'*') fullName,HeadImg,STUFF(Mobile ,4,4,'****') Mobile ,case Sex
when 1 then '男'
else '女'
end Sex,RegTime,Integral,Email
FROM TMember ORDER BY id offset {(page - 1) * 10} rows FETCH next {pageSize} rows ONLY";
            var table = _userAppService.GetDataTableAsync(sql).Result;
            int total = (int)_userAppService.GetDataTableAsync("select count(id) TotalCount from TMember").Result.Rows[0][0];
            // _logger.LogError(sql);
            IPagedList pageList = new PagedList<DataRow>(table.Select(), page, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            return View(pageList);
        }
    }
}