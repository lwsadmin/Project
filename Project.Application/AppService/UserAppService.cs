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
        public async void CreateOrEditAsync(User model)
        {
            if (model.Id <= 0)
            {
                _unitOfWork.UserRepository.Add(model);

            }
            else
            {
                //var s = _unitOfWork.UserRepository.GetById(model.Id).Result;
                //s.Name = model.Name;
                _unitOfWork.UserRepository.Update(model);
            }
            await _unitOfWork.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            _unitOfWork.UserRepository.Remove(id);
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
