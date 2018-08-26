using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    public interface IBaseDal<T>
        where T : class, new()
    {
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda);

        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLamda, System.Linq.Expressions.Expression<Func<T, S>> orderbyLamda, bool isAsc);

        bool DeleteEntity(T entity);

        bool EditEntity(T entity);

        /// <summary>
        /// 标记添加实体，不一定立即执行，由会话层DbSession处SaveChanges()
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>返回添加的实体</returns>
        T AddEntity(T entity);
    }
}
