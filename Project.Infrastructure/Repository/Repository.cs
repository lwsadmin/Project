using Project.Domain.Interface;
using Project.Infrastructure.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    /// <summary>
    /// 泛型仓储，实现泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private Microsoft.EntityFrameworkCore.DbSet<TEntity> _dbSet;

        public Repository(EFContext dbContext)
        {
            this._dbSet = dbContext.Set<TEntity>();
        }

        public async void Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetFields<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).Select(selector);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(int id)
        {
            var n = GetById(id);
            _dbSet.Remove(n.Result);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
