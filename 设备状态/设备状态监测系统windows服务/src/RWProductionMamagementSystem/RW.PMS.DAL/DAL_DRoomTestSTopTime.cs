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
    public class DAL_DRoomTestSTopTime : IDAL_DRoomTestSTopTime
    {
        public int addTestStopTime(DRoom_TestSTopTimeModel model)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into droom_TestStopTime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        }

        public List<DRoom_TestSTopTimeModel> GetDRoom_TestStopTimedesc(string deviceName, string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from droom_TestStopTime where  roomName like @roomName   order by id desc limit 1";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            //pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<DRoom_TestSTopTimeModel> list = db.ExecuteList<DRoom_TestSTopTimeModel>(sql, pList.ToArray());
            return list;
        }

        public List<DRoom_TestSTopTimeModel> GetOneDRoom_TestSTopTimeList(string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from droom_TestStopTime where  roomName like @roomName   order by id desc";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            //pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<DRoom_TestSTopTimeModel> list = db.ExecuteList<DRoom_TestSTopTimeModel>(sql, pList.ToArray());
            return list;
        }
    }
}
