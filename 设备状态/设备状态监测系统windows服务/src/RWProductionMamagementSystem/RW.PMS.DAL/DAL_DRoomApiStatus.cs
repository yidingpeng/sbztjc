using MySql.Data.MySqlClient;
using RW.PMS.IDAL;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.DAL
{
    public class DAL_DRoomApiStatus : IDAL_DRoomApiStatus
    {
        public DRoom_ApiStatusModel GetDRoom_ApiStatusOne(string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql1 = @"select * from droom_apistatus where roomName like @roomName";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            List<DRoom_ApiStatusModel> list = db.ExecuteList<DRoom_ApiStatusModel>(sql1, pList.ToArray());
            if (list .Count>0)
                return list[0];
            else
                return null;
        }

        public int updateApiStatusOne(string roomName, string status)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update droom_apistatus SET  status=@status WHERE  roomName like @roomName";
            pList.Add(new MySqlParameter("@status", status));
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }
    }
}
