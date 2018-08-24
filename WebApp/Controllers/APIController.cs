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

        public APIController(BaseDbContext baseDbContext)
        {
            _baseDbContext = baseDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestAdd()
        {
            Random r = new Random();
            Model.RankingList rankingList = new Model.RankingList { SendTime = DateTime.Now, SenderIP = HttpContext.Connection.RemoteIpAddress.ToString(), SendMessage = "我就测试一下" + r.Next(0, 10) };
            this._baseDbContext.RankingList.Add(rankingList);

            this._baseDbContext.SaveChanges();

            return Content("success");
        }

        #region 获取排行榜数据
        public IActionResult GetRankingList()
        {
            // 等待实现
            // 以下临时实现
            var ranking = (from r in this._baseDbContext.RankingList
                           where r.SendTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")
                           orderby r.SendTime ascending
                           select r).Take<Model.RankingList>(100);

            return Json(new { isSuccess = true, dataList = ranking.ToList<Model.RankingList>() });
        }
        #endregion
    }
}