using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    public interface IDbSession
    {
        DbContext Db { get; set; }

        IRankingListDal RankingListDal { get; set; }

        // 一个业务中经常涉及到对多张表操作，通过只连接一次数据库，完成对多张表的数据操作，提高性能
        bool SaveChanges();
    }
}
