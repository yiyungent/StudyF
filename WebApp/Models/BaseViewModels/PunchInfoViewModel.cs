using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.BaseViewModels
{
    public class PunchInfoViewModel
    {
        public int RankingID { get; set; }

        public DateTime PunchTime { get; set; }

        public string PunchIP { get; set; }

        public string PunchMessage { get; set; }
    }
}
