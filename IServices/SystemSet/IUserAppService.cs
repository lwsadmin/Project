using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Project.Domain.IRepository;

namespace IService
{
    public interface IUserAppService : IRepository<User>
    {
        //void CreateOrEdit(User model);
        //User Get(Expression<Func<User, bool>> predicate);
        //void Delete(dynamic[] id);

        //ISugarQueryable<T> GetFields<T>(Expression<Func<User, T>> selector, Expression<Func<User, bool>> predicate);
    }
}
