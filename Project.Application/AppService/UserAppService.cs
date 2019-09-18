using Domain.Entity;
using Project.Application.IAppService;
using Project.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
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
            return _unitOfWork.UserRepository.GetById(Id);
        }
    }
}
