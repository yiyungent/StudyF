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

        // 注意：一个IP一天只能打一次卡（也就是说此表中一天内一个IP只能有一条记录），并且是在规定时间段内才能打卡

        #region 获取当天排行榜数据(前100名)
        public List<Model.RankingList> GetToDayRanking()
        {
            int totalCount;
            var ranking = this.CurrentDal.LoadPageEntities(1, 100, out totalCount, r => r.SendTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"), r => r.SendTime, true);
            return ranking.ToList<Model.RankingList>();
        }
        #endregion

        #region 当天早起打卡
        public bool PunchClock(Model.RankingList rankingList, out string msg, out int rankingID)
        {
            // 是否满足打卡时间
            int hour = rankingList.SendTime.Hour;
            if (!(hour > 4 && hour < 7))
            {
                msg = "请在每日4.00 -- 7.00内打卡";
                rankingID = -1;
                return false;
            }

            // 查询是否当日已打卡
            if (IsToDayPunchClock(rankingList.SenderIP))
            {
                msg = "今日已经打卡啦，明日继续保持哦";
                rankingID = GetTodayRankingID(rankingList.SenderIP);
                return false;
            }

            // 正常打卡
            RankingList addModel = this.CurrentDal.AddEntity(rankingList);
            // 会话层同一执行保存
            // 注意：这里必须先于rankingID获取，不然不先保存就无法获取 此次添加的实体的主键RID
            bool isSuccess = this.CurrentDbSession.SaveChanges();
            // 获取此次打卡在当天的排名名次
            rankingID = GetTodayRankingID(addModel.RID);
            msg = $"成功早起打卡，你的 今日早起打卡排名： 第{rankingID}名";
            return isSuccess;
        }
        #endregion

        /// <summary>
        /// 查询此IP今日是否已经打卡
        /// </summary>
        /// <param name="senderIP"></param>
        /// <returns></returns>
        public bool IsToDayPunchClock(string senderIP)
        {
            var query = this.CurrentDal.LoadEntities(r => r.SenderIP == senderIP && r.SendTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"));
            var list = query.ToList<Model.RankingList>();
            // 注意：当query 未查询到数据时（即未打卡）,ToList()后为 list.Count为0,但不为null
            //Model.RankingList model = list[0]; // 如果查不到此处将抛异常：索引超出范围
            bool isPunch = false;
            if (list.Count > 0)
            {
                // 今日已经打卡
                isPunch = true;
            }
            return isPunch;
        }

        /// <summary>
        /// 获取此打卡记录的今日打卡排名名次。
        /// 此方法存在问题：如果无此主键记录，则返回的非正确数据
        /// </summary>
        /// <param name="rID">打卡记录主键</param>
        /// <returns>今日打卡排名 名次</returns>
        public int GetTodayRankingID(int rID)
        {
            int rankingID = this.CurrentDal.LoadEntities(r => r.RID <= rID && r.SendTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")).Count<Model.RankingList>();
            return rankingID;
        }

        /// <summary>
        /// 获取此IP的 今日打卡排名名次。
        /// 若此 IP 无今日打卡记录，则返回 -1
        /// </summary>
        /// <param name="senderIP">IP地址</param>
        /// <returns>今日打卡排名 名次</returns>
        public int GetTodayRankingID(string senderIP)
        {
            var query = this.CurrentDal.LoadEntities(r => r.SenderIP == senderIP && r.SendTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"));
            var list = query.ToList<Model.RankingList>();
            int rankingID = -1;
            if (list.Count > 0)
            {
                rankingID = GetTodayRankingID(list[0].RID);
            }
            return rankingID;
        }
    }
}
