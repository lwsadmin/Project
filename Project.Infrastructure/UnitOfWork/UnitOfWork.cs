using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Interface;
using Project.Infrastructure.EntityFrameworkCore;
using Project.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private bool disposed = false;
        private EFContext _dbContext;
        private IRepository<Role> _roleRepository;
        private IRepository<Permissions> _permissionsRepository;
        private IRepository<User> _userRepository;

        public UnitOfWork()
        {
            this._dbContext = new EFContext();
            //this._dbContext = dbContext;
        }
        public IRepository<Role> RoleRepository
        {
            get
            {
                if (this._roleRepository == null)
                {
                    this._roleRepository = new Repository<Role>(_dbContext);
                }
                return _roleRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new Repository<User>(_dbContext);
                }
                return _userRepository;
            }
        }

        public IRepository<Permissions> PermissionsRepository
        {
            get
            {
                if (this._permissionsRepository == null)
                {
                    this._permissionsRepository = new Repository<Permissions>(_dbContext);
                }
                return _permissionsRepository;
            }
        }
        public async Task<int> ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return await _dbContext.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public async void BeiginTran()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }
        public void CommitTran()
        {
            _dbContext.Database.CommitTransaction();
        }
        public void RollBackTran()
        {
            _dbContext.Database.RollbackTransaction();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            //必须为true
            Dispose(disposing: true);
            //通知垃圾回收器不再调用终结器
            GC.SuppressFinalize(this);
        }
    }
}
