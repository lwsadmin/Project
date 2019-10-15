using Domain.Entity;

using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Project.Infrastructure.SqlSugar
{
    public class SqlContext<T> where T : class, new()
    {

        public SqlSugarClient Db;

        public SqlContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Server=.;Database=Project;User=sa;Password=123456;",
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样
            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
        }
        public SimpleClient<Permissions> Permissions { get { return new SimpleClient<Permissions>(Db); } }
        public SimpleClient<Role> Role { get { return new SimpleClient<Role>(Db); } }

        public SimpleClient<User> User { get { return new SimpleClient<User>(Db); } }

        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来处理T表的常用操作



        //public virtual List<T> GetList(Expression<Func<T, bool>> predicate)
        //{
        //    return CurrentDb.GetList(predicate);
        //}

        //public virtual bool Delete(dynamic[] id)
        //{
        //    return CurrentDb.DeleteByIds(id);
        //}

        //public virtual bool Update(T obj)
        //{
            
        //    return CurrentDb.Update(obj);
        //}

        //public virtual bool Insert(T obj)
        //{
        //    return CurrentDb.Insert(obj);
        //}

        //public virtual T Get(Expression<Func<T, bool>> predicate)
        //{
        //    return CurrentDb.GetSingle(predicate);
        //}
        //public virtual T Get(dynamic id)
        //{
        //    return CurrentDb.GetById(id);
        //}
        //public virtual ISugarQueryable<T> GetFields<U>(Expression<Func<U, T>> selector, Expression<Func<U, bool>> predicate)
        //{
        //    return Db.Queryable<U>().Where(predicate).Select(selector);
        //}
    }
}
