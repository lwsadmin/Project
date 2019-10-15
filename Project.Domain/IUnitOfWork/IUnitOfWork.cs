using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // 创建 sqlsugar client 实例
        ISqlSugarClient GetDbClient();
        // 开始事务
        void BeginTran();
        // 提交事务
        void CommitTran();
        // 回滚事务
        void RollbackTran();
    }
}
