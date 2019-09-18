using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity;
namespace Project.Domain.Interface
{
    /// <summary>
    /// IPermissionsRepository 接口
    /// 用到的业务对象，是领域对象
    /// 继承通用的IRepository接口
    /// </summary>
    public interface IPermissionsRepository : IRepository<Permissions>
    {
        //权限逻辑独有的接口
        IEnumerable<Permissions> GetByRoleId(int RoleId);
    }
}
