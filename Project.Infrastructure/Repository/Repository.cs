
using java.rmi.server;
using Project.Domain.IRepository;
using Project.Domain.IUnitOfWork;
using Project.Infrastructure.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    /// <summary>
    /// 泛型仓储，实现泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IDisposable where TEntity : class, new()
    {
        //private SqlContext<TEntity> _sqlContext;
        private ISqlSugarClient _db;
        private readonly IUnitOfWork _unitOfWork;

        //public Repository()
        //{
        //    this._sqlContext = new SqlContext<TEntity>();
        //}
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _db = unitOfWork.GetDbClient();
            // DbContext.Init(BaseDBConfig.ConnectionString, (DbType)BaseDBConfig.DbType);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(TEntity obj)
        {
            var insert = _db.Insertable(obj);
            return await insert.ExecuteReturnIdentityAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            System.Data.DataTable t = _db.SqlQueryable<System.Data.DataTable>("select * from  TUsers").ToDataTable();//.ToPageList(1, 2);
            return await _db.Queryable<TEntity>().In(id).SingleAsync();
        }

        public ISugarQueryable<T> GetFields<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TEntity obj)
        {
            ////这种方式会以主键为条件
            //var i = await Task.Run(() => _db.Updateable(entity).ExecuteCommand());
            //return i > 0;
            //这种方式会以主键为条件
            return await _db.Updateable(obj).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            //var i = await Task.Run(() => _db.Deleteable(entity).ExecuteCommand());
            //return i > 0;
            // await _db.Deleteable(entity).ExecuteCommandHasChangeAsync();

            return await _db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }
    }
}
