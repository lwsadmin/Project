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
namespace Services
{
    public class UserAppService : Repository<User>, IUserAppService
    {
        public UserAppService(IUnitOfWork uw):base(uw)
        {

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
    }
}
