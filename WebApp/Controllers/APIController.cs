using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.EF;

namespace WebApp.Controllers
{
    public class APIController : Controller
    {
        private BaseDbContext _baseDbContext;

        private IBLL.IRankingListService _rankingListService;

        public APIController(BaseDbContext baseDbContext)
        {
            this._baseDbContext = baseDbContext;
            this._rankingListService = new BLL.RankingListService(baseDbContext);
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Time早起打卡
        [HttpPost]
        public IActionResult PunchClock()
        {
            string sendMessage = Request.Form["inputMessage"];
            DateTime sendTime = DateTime.Now;
            string senderIP = HttpContext.Connection.RemoteIpAddress.ToString();
            Model.RankingList model = new Model.RankingList() { SendMessage = sendMessage, SendTime = sendTime, SenderIP = senderIP };
            string msg;
            int rankingID;
            bool isSuccess = this._rankingListService.PunchClock(model, out msg, out rankingID);
            // 打卡信息，如果未打卡，RankingID 为 -1
            Models.BaseViewModels.PunchInfoViewModel punchInfo = new Models.BaseViewModels.PunchInfoViewModel { RankingID = rankingID, PunchTime = model.SendTime, PunchIP = model.SenderIP, PunchMessage = model.SendMessage };
            if (isSuccess)
            {
                // 打卡成功
                return Json(new { code = 1, msg, punchInfo });
            }
            else
            {
                // 打卡失败
                return Json(new { code = -1, msg, punchInfo });
            }
        }
        #endregion

        #region 获取排行榜数据
        public IActionResult GetRankingList()
        {
            List<Model.RankingList> rankingList = this._rankingListService.GetToDayRanking();
            return Json(new { code = 1, dataList = rankingList });
        }
        #endregion
    }
}