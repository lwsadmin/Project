using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    /// <summary>
    /// 角色表
    /// </summary>
    [SugarTable("TRoles")]
    public class Role
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public int CreatorUserId { get; set; } = 0;

        public bool IsDefault { get; set; } = false;

        //租户Id
        public int TenantId { get; set; } = 0;

        public string Name { get; set; }

        public string Description { get; set; }
        public string ManageRole { get; set; }
    }
}
