using Microsoft.EntityFrameworkCore;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 负责创建 EF数据操作上下文实例，必须保证线程内唯一
    /// </summary>
    public class DbContextFactory
    {
        //public static DbContext CreateDbContext()
        //{
        //    DbContext dbContext = (DbContext)Common.CallContext.GetData("dbContext");
        //    if (dbContext == null)
        //    {
        //        dbContext = new BaseDbContext();
        //    }
        //}
    }
}
