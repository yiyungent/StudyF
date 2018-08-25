using System;
using System.Collections.Generic;
using System.Text;

namespace IBLL
{
    public interface IRankingListService : IBaseService<Model.RankingList>
    {
        List<Model.RankingList> GetToDayRanking();
    }
}
