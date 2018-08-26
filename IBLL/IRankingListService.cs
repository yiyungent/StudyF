using System;
using System.Collections.Generic;
using System.Text;

namespace IBLL
{
    public interface IRankingListService : IBaseService<Model.RankingList>
    {
        /// <summary>
        /// 获取当天早起打卡排行榜数据
        /// </summary>
        /// <returns></returns>
        List<Model.RankingList> GetToDayRanking();

        /// <summary>
        /// 当天早起打卡
        /// </summary>
        /// <param name="rankingList">打卡信息</param>
        /// <param name="msg">返回的提示消息(eg. 请在4.00--7.00内打卡)</param>
        /// <param name="rankingID">此次成功打卡后的当天名次</param>
        /// <returns></returns>
        bool PunchClock(Model.RankingList rankingList, out string msg, out int rankingID);
    }
}
