using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Permissions
    {
        public int Id { get; set; }
        //创建时间
        public DateTime CreationTime { get; set; } = DateTime.Now;

        public int CreateUserId { get; set; } = 0;

        public string Name { get; set; }

        //租户Id
        public int TenantId { get; set; } = 0;


    }
}
