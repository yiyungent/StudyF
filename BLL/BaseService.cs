using DALFactory;
using IDAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLL
{
    public abstract class BaseService<T>
        where T : class, new()
    {
        public DbContext DbContext { get; set; }

        public IDbSession CurrentDbSession
        {
            get
            {
                return DbSessionFactory.CreateDbSession(this.DbContext);
            }
        }

        public IDAL.IBaseDal<T> CurrentDal { get; set; }

        public abstract void SetCurrentDal();

        public BaseService(DbContext dbContext)
        {
            this.DbContext = dbContext;

            // 子类一定要设置当前Dal
            SetCurrentDal();
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLamda)
        {
            return CurrentDal.LoadEntities(whereLamda);
        }

        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLamda, Expression<Func<T, S>> orderbyLamda, bool isAsc)
        {
            return CurrentDal.LoadPageEntities<S>(pageIndex, pageSize, out totalCount, whereLamda, orderbyLamda, isAsc);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return CurrentDbSession.SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return CurrentDbSession.SaveChanges();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            CurrentDbSession.SaveChanges();
            return entity;
        }
    }
}
