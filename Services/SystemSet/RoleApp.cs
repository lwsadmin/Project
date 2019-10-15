using Domain.Entity;
using IService;
using Project.Domain.IUnitOfWork;
using Project.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class RoleApp : Repository<Role>, IRoleApp
    {
        public RoleApp(IUnitOfWork uw) : base(uw)
        {

        }
    }
}
