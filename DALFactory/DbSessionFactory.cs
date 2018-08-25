using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DALFactory
{
    public class DbSessionFactory
    {
        public static IDAL.IDbSession CreateDbSession(DbContext dbContext)
        {
            IDAL.IDbSession dbSession = new DbSession(dbContext);
            return dbSession;
        }
    }
}
