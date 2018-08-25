using IDAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DALFactory
{
    /// <summary>
    /// 通过反射形式创建xxxDal的实例
    /// 注意：需添加对DAL的引用
    /// </summary>
    public class AbstractDalFactory
    {
        private static readonly string AssemblyPath = "DAL";

        private static readonly string NameSpace = "DAL";

        private DbContext _dbContext;

        public AbstractDalFactory(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IRankingListDal CreateRankingListDal()
        {
            string fullClassName = NameSpace + ".RankingListDal";
            return CreateInstance(fullClassName) as IRankingListDal;
        }

        private object CreateInstance(string className)
        {
            Assembly assembly = Assembly.Load(AssemblyPath);
            object[] parameters = new object[1];
            parameters[0] = this._dbContext;
            return assembly.CreateInstance(className, true, BindingFlags.Default, null, parameters, null, null);
        }
    }
}
