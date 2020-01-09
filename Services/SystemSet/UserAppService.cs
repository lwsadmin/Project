using Domain.Entity;
using IService;
using Service;
using Project.Domain.IUnitOfWork;
using Project.Infrastructure.Repository;
using Project.Infrastructure.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project.Common;
namespace Services
{
    public class UserAppService : Repository<User>, IUserAppService
    {
        private ISqlSugarClient _db;
        private readonly IUnitOfWork _unitOfWork;
        public UserAppService(IUnitOfWork uw) : base(uw)
        {

            _unitOfWork = uw;
            _db = _unitOfWork.GetDbClient();
            // DbContext.Init(BaseDBConfig.ConnectionString, (DbType)BaseDBConfig.DbType);

        }

        //public void CreateOrEdit(User model)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(dynamic[] id)
        //{
        //    SqlContext<User> userContext = new SqlContext<User>();
        //    userContext.Delete(id);
        //}

        //public User Get(Expression<Func<User, bool>> predicate)
        //{
        //    //SqlContext<User> userContext = new SqlContext<User>();
        //    return base.Get(predicate);
        //}

        //public ISugarQueryable<T> GetFields<T>(Expression<Func<User, T>> selector, Expression<Func<User, bool>> predicate)
        //{
        //    SqlContext<User> userContext = new SqlContext<User>();
        //    return userContext.Db.Queryable<User>().Where(predicate).Select(selector);
        //}

        public override async Task<DataTable> GetDataTableAsync(string sql)
        {

            //RedisHelper.Zero.SetStringKey<string>("myTest", DateTime.Now.Millisecond.ToString(),TimeSpan.FromMinutes(10000));
 
            try
            {
                _unitOfWork.BeginTran();

                _db.AddQueue("update dbo.TGoodsCategory set note='22222222222222222' where Id=2");
                _db.AddQueue("update dbo.TGoodsCategory set note='11111111' where Id=1");
                _db.SaveQueues();

                
                System.Data.DataTable t = _db.SqlQueryable<System.Data.DataTable>(sql).ToDataTable();//.ToPageList(1, 2);
                throw new Exception("");
                //_unitOfWork.CommitTran();
                return t;


            }
            catch (Exception)
            {

                _unitOfWork.RollbackTran();
                System.Data.DataTable t = _db.SqlQueryable<System.Data.DataTable>(sql).ToDataTable();//.ToPageList(1, 2);
                return t;
            }



        }
    }
}
