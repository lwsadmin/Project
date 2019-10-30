using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.IRepository
{

    /// <summary>
    /// 泛型仓储，写入CRUD通用方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        Task<int> AddAsync(TEntity obj);
        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetById(int id);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        ISugarQueryable<T> GetFields<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据对象进行更新
        /// </summary>
        /// <param name="obj"></param>
        Task<bool> UpdateAsync(TEntity obj);
        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        Task<bool> RemoveAsync(int id);

        Task<System.Data.DataTable> GetDataTableAsync(string sql);
    }
}
