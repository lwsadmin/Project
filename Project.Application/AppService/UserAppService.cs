using Domain.Entity;
using Project.Application.IAppService;
using Project.Infrastructure.EntityFrameworkCore;
using Project.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.AppService
{
    public class UserAppService : IUserAppService
    {
        private readonly UnitOfWork _unitOfWork;
        public UserAppService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        public Task CreateOrEditAsync(User model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(int Id)
        {
            return await _unitOfWork.UserRepository.GetById(Id);
        }

        public IQueryable<T> GetFields<T>(Expression<Func<User, T>> selector, Expression<Func<User, bool>> predicate)
        {
            return _unitOfWork.UserRepository.GetFields(selector, predicate);
        }
    }
}
