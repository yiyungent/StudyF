using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class TimeMsgDal
    {
        private readonly static string _connectionString = "server=localhost;user id=root;pwd=admin;database=studyfcoredb;SslMode=none;";

        public int Insert(TimeMsg timeMsg)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "INSERT INTO TimeMsg(`Message`, `SendTime`, `SenderIP`) VALUES(@Message, @SendTime, @SenderIP)";
                    MySqlParameter parOfMsg = new MySqlParameter("@Message", MySqlDbType.VarString, 100);
                    MySqlParameter parOfSendTime = new MySqlParameter("@SendTime", MySqlDbType.DateTime);
                    MySqlParameter parOfSenderIP = new MySqlParameter("@SenderIP", MySqlDbType.VarString, 20);
                    parOfMsg.Value = timeMsg.Message;
                    parOfSendTime.Value = timeMsg.SendTime;
                    parOfSenderIP.Value = timeMsg.SenderIP;
                    cmd.Parameters.Add(parOfMsg);
                    cmd.Parameters.Add(parOfSendTime);
                    cmd.Parameters.Add(parOfSenderIP);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TimeMsg> GetList()
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT `Message`, `SendTime`, `SenderIP` FROM TimeMsg ORDER BY 'ID' ASC", _connectionString))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return DataTableToList(dt);
            }
        }

        private List<TimeMsg> DataTableToList(DataTable dt)
        {
            List<TimeMsg> list = new List<TimeMsg>();
            foreach (DataRow row in dt.Rows)
            {
                TimeMsg model = new TimeMsg();
                if (row["Message"] != null)
                {
                    model.Message = row["Message"].ToString();
                }
                if (row["SendTime"] != null)
                {
                    model.SendTime = Convert.ToDateTime(row["SendTime"]);
                }
                if (row["SenderIP"] != null)
                {
                    model.SenderIP = row["SenderIP"].ToString();
                }
                list.Add(model);
            }

            return list;
        }
    }
}
