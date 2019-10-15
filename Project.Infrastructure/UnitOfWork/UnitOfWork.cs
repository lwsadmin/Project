using Domain.Entity;
using Project.Infrastructure.SqlSugar;
using Project.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project.Domain.IRepository;
using Project.Domain.IUnitOfWork;
using SqlSugar;

namespace Project.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed = false;
        private readonly ISqlSugarClient _sqlSugarClient;
        // 注入 sugar client 实例
        public UnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }
        // 保证每次 scope 访问，多个仓储类，都用一个 client 实例,注意，不是单例模型
        public ISqlSugarClient GetDbClient()
        {
            return _sqlSugarClient;
        }

        public void BeginTran()
        {
            GetDbClient().Ado.BeginTran();
        }

        public void CommitTran()
        {
            try
            {
                GetDbClient().Ado.CommitTran();
            }
            catch (Exception ex)
            {

                GetDbClient().Ado.RollbackTran();
            }
        }

        public void RollbackTran()
        {
            GetDbClient().Ado.RollbackTran();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                if (disposing)
                {
                    _sqlSugarClient.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            //必须为true
            Dispose(disposing: true);
            //通知垃圾回收器不再调用终结器
            GC.SuppressFinalize(this);
        }
    }
}
