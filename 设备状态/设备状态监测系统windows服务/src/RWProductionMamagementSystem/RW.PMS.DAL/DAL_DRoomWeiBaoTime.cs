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
    public class DAL_DRoomWeiBaoTime : IDAL_DRoomWeiBaoTime
    {
        public List<DRoom_weibaoTimeModel> GetOneDRoom_weibaoTimeList(string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from droom_weibaoTime where  roomName like @roomName   order by id desc";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            //pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<DRoom_weibaoTimeModel> list = db.ExecuteList<DRoom_weibaoTimeModel>(sql, pList.ToArray());
            return list;
        }
    }
}
