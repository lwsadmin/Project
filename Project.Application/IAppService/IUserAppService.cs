using Domain.Entity;
using Project.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.IAppService
{
    public interface IUserAppService
    {
        Task CreateOrEditAsync(User model);
        Task<User> GetByIdAsync(int Id);
        Task DeleteAsync(int id);
    }
}
