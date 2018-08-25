using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL
{
    public class BaseDal<T>
        where T : class, new()
    {
        DbContext Db { get; set; }

        public BaseDal(object objDbContext)
        {
            this.Db = objDbContext as DbContext;
        }

        /// <summary>
        /// 查询过滤
        /// </summary>
        /// <param name="whereLamda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLamda)
        {
            return Db.Set<T>().Where<T>(whereLamda);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="totalCount">总共页数</param>
        /// <param name="whereLamda">过滤条件</param>
        /// <param name="orderbyLamda">顺序条件</param>
        /// <param name="isAsc">升序 或 降序</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLamda, Expression<Func<T, S>> orderbyLamda, bool isAsc)
        {
            var temp = Db.Set<T>().Where<T>(whereLamda);
            totalCount = temp.Count();
            if (isAsc)
            {
                // 升序
                temp = temp.OrderBy<T, S>(orderbyLamda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                // 降序
                temp = temp.OrderByDescending<T, S>(orderbyLamda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            Db.Set<T>().Add(entity);
            return entity;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            Db.Entry<T>(entity).State = EntityState.Deleted;
            return true;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            Db.Entry<T>(entity).State = EntityState.Modified;
            return true;
        }
    }
}
