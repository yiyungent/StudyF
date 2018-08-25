using IDAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DALFactory
{
    public class DbSession : IDbSession
    {
        public DbContext Db { get; set; }

        private IRankingListDal _rankingListDal;

        public IRankingListDal RankingListDal
        {
            get
            {
                if (_rankingListDal == null)
                {
                    AbstractDalFactory abstractDalFactory = new AbstractDalFactory(this.Db);
                    _rankingListDal = abstractDalFactory.CreateRankingListDal();
                }
                return _rankingListDal;
            }
            set
            {
                _rankingListDal = value;
            }
        }

        public DbSession(DbContext dbContext)
        {
            this.Db = dbContext;
        }

        // 只连接一次数据库，完成对多张表数据的操作，提高性能
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
    }
}
