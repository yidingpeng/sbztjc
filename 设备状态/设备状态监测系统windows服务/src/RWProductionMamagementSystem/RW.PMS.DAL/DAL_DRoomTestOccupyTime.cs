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
    public class DAL_DRoomTestOccupyTime : IDAL_DRoomTestOccupyTime
    {
        public int addRoom_TestOccupyTime(DRoom_TestOccupyTimeModel model)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into droom_testoccupyTime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        }

        public List<DRoom_TestOccupyTimeModel> GetRoom_TestOccupyTimesdesc(string deviceName, string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from droom_testoccupyTime where  roomName like @roomName  order by id desc limit 1";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
          //  pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<DRoom_TestOccupyTimeModel> list = db.ExecuteList<DRoom_TestOccupyTimeModel>(sql, pList.ToArray());
            return list;

        }
    }
}
