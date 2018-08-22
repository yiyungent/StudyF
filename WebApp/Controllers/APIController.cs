using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace WebApp.Controllers
{
    public class APIController : Controller
    {
        private TimeMsgBll _timeMsgBll = new TimeMsgBll();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TimeMsg()
        {
            return View();
        }


        [HttpPost]
        public IActionResult TimeMsg(string inputMessage)
        {
            #region MySql EF Core Test
            //var optionsBuilder = new DbContextOptionsBuilder<TimeMsgDbContext>();
            //optionsBuilder.UseMySQL("server=localhost;user id=root;pwd=admin;database=studyfcoredb;");
            //TimeMsgDbContext timeMsgDbContext = new TimeMsgDbContext(optionsBuilder.Options);
            //TimeMsg timeMsg = new TimeMsg();
            //timeMsg.Message = inputMessage;
            //timeMsg.SenderIP = HttpContext.Connection.RemoteIpAddress.ToString();
            //timeMsg.SendTime = DateTime.Now;
            //timeMsgDbContext.TimeMsg.Add(timeMsg);
            //timeMsgDbContext.SaveChanges(); 
            #endregion
            inputMessage = inputMessage.Length > 50 ? inputMessage.Substring(0, 44) + "......" : inputMessage;
            TimeMsg timeMsg = new TimeMsg();
            timeMsg.Message = inputMessage;
            //timeMsg.SenderIP = HttpContext.Connection.RemoteIpAddress.ToString();
            timeMsg.SenderIP = GetUserIp(HttpContext);
            timeMsg.SendTime = DateTime.Now;
            if (_timeMsgBll.Add(timeMsg))
            {
                return Json(new { isSuccess = true });
            }
            else
            {
                return Json(new { isSuccess = false });
            }
        }

        public IActionResult RankingData()
        {
            List<TimeMsg> timeMsgs = _timeMsgBll.GetList();
            return Json(new { isSuccess = true, tipMsg = "加载成功", dataList = timeMsgs });
        }

        private string GetUserIp(Microsoft.AspNetCore.Http.HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}