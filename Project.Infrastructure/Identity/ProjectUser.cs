using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
namespace Domain.Entity
{
    public class ProjectUser : IdentityUser//
    {
        // IdentityUser<Guid>
        // public override int Id { get; set; }

        public int CreatorUserId { get; set; } = 0;

        public DateTime CreationTime { get; set; } = DateTime.Now;
        //租户Id
        public int TenantId { get; set; } = 0;

        //姓名
        public string Name { get; set; }

        public string PassWord { get; set; }

        //所属角色 一个管理员 可能同时拥有多个角色
        public string RoleId { get; set; }
        public ProjectUser() : base()
        {

        }


    }
}
