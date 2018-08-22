using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class TimeMsgBll
    {
        private TimeMsgDal _timeMsgDal = new TimeMsgDal();

        public bool Add(TimeMsg timeMsg)
        {
            return _timeMsgDal.Insert(timeMsg) > 0 ? true : false;
        }

        public List<TimeMsg> GetList()
        {
            return _timeMsgDal.GetList();
        }
    }
}
