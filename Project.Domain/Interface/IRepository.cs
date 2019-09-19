using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interface
{

    /// <summary>
    /// 泛型仓储，写入通用的方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        void Add(TEntity obj);
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
        IQueryable<T> GetFields<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据对象进行更新
        /// </summary>
        /// <param name="obj"></param>
        void Update(TEntity obj);
        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);
    }
}
