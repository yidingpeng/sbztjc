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
    public class DAL_DRoomjiaozhunTime : IDAL_DRoomjiaozhunTime
    {
        public List<DRoom_jiaozhunTimeModel> GetOneDRoom_jiaozhunTimeList(string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from droom_jiaozhunTime where  roomName like @roomName   order by id desc";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            //pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<DRoom_jiaozhunTimeModel> list = db.ExecuteList<DRoom_jiaozhunTimeModel>(sql, pList.ToArray());
            return list;
        }
    }
}
