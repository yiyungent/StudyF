using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EF
{
    #region 供ef migrations使用
    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
    //{
    //    public BaseDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();
    //        optionsBuilder.UseMySQL("server=localhost;user=root;pwd=admin;database=StudyFDb;SslMode=none;");

    //        return new BaseDbContext(optionsBuilder.Options);
    //    }
    //} 
    #endregion
}
