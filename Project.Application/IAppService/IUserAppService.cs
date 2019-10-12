using Domain.Entity;
using Project.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.IAppService
{
    public interface IUserAppService
    {
        void CreateOrEditAsync(User model);
        Task<User> GetByIdAsync(int Id);
        void DeleteAsync(int id);

        IQueryable<T> GetFields<T>(Expression<Func<User, T>> selector, Expression<Func<User, bool>> predicate);
    }
}
