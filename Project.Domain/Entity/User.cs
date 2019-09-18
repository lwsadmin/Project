using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User
    {
        public int Id { get; set; }

        //登录名
        public string UserName { get; set; }

        public int CreateUserId { get; set; } = 0;

        public DateTime CreationTime { get; set; } = DateTime.Now;
        //租户Id
        public int TenantId { get; set; } = 0;

        //姓名
        public string Name { get; set; }

        public string PassWord { get; set; }

        //所属角色 一个管理员 可能同时拥有多个角色
        public string RoleId { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
    }
}
