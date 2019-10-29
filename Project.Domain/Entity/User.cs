using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Domain.Base;
using SqlSugar;
namespace Domain.Entity
{
    [SugarTable("TUsers")]
    public class User : BaseEntity
    {
        public int CreatorUserId { get; set; } = 0;

        //姓名
        [DisplayName("姓名")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入姓名")]

        public string Name { get; set; }

        public string UserName { get; set; }


        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PassWord { get; set; }

        //所属角色 一个管理员 可能同时拥有多个角色
        public string RoleId { get; set; }

    }
}
