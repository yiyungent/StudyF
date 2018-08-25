using IBLL;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLL
{
    public class RankingListService : BaseService<Model.RankingList>, IRankingListService
    {
        public RankingListService(DbContext dbContext) : base(dbContext)
        { }

        public override void SetCurrentDal()
        {
            this.CurrentDal = this.CurrentDbSession.RankingListDal;
        }

        #region 获取当天排行榜数据(前100名)
        public List<Model.RankingList> GetToDayRanking()
        {
            int totalCount;
            var ranking = this.CurrentDal.LoadPageEntities(1, 100, out totalCount, r => r.SendTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"), r => r.SendTime, true);
            return ranking.ToList<Model.RankingList>();
        } 
        #endregion
    }
}
