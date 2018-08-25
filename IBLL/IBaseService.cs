using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IBLL
{
    public interface IBaseService<T>
        where T:class,new()
    {
        IDAL.IBaseDal<T> CurrentDal { get; set; }

        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLamda);

        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLamda, Expression<Func<T, S>> orderbyLamda, bool isAsc);

        bool DeleteEntity(T entity);

        bool EditEntity(T entity);

        T AddEntity(T entity);
    }
}
