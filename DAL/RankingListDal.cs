using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL
{
    public class RankingListDal : BaseDal<Model.RankingList>, IRankingListDal
    {
        public RankingListDal(object objDbContext) : base(objDbContext)
        { }
    }
}
