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

        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            string sql = @$"SELECT Id,fullName,HeadImg,Mobile,case Sex
when 1 then '男'
else '女'
end Sex,RegTime,Integral,Email
FROM TMember ORDER BY id offset {(page - 1) * 10} rows FETCH next {pageSize} rows ONLY";
            var table = _userAppService.GetDataTableAsync(sql).Result;
            int total = 0;
            IPagedList pageList = new PagedList<DataRow>(table.Select(), page, pageSize, total);
            return View(pageList);
        }
    }
}