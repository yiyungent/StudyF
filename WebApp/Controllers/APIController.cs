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

        #region Time打卡
        [HttpPost]
        public IActionResult PunchClock()
        {
            string sendMessage = Request.Form["inputMessage"];
            DateTime sendTime = DateTime.Now;
            string senderIP = HttpContext.Connection.RemoteIpAddress.ToString();

            Model.RankingList model = new Model.RankingList() { SendMessage = sendMessage, SendTime = sendTime, SenderIP = senderIP };
            Model.RankingList addModel = this._rankingListService.AddEntity(model);

            return Json(new { isSuccess = true, punchInfo = addModel });
        }
        #endregion

        #region 获取排行榜数据
        public IActionResult GetRankingList()
        {
            List<Model.RankingList> rankingList = this._rankingListService.GetToDayRanking();
            return Json(new { isSuccess = true, dataList = rankingList });
        }
        #endregion
    }
}