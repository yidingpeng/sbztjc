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
    public class DAL_DeviceStopTime : IDAL_DeviceStopTime
    {
        public int addDevice_StopTime(Device_StopTimeModel model)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into device_stoptime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        }

        public List<Device_StopTimeModel> GetDevice_StopTimesdesc(string deviceName, string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from device_stoptime where  roomName like @roomName  and deviceName=@deviceName order by id desc limit 1";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<Device_StopTimeModel> list = db.ExecuteList<Device_StopTimeModel>(sql, pList.ToArray());
            return list;

        }

        public List<Device_StopTimeModel> GetOneDevice_StopTimeList(string deviceName, string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from device_stoptime where  roomName like @roomName  and deviceName=@deviceName order by id desc";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<Device_StopTimeModel> list = db.ExecuteList<Device_StopTimeModel>(sql, pList.ToArray());
            return list;

        }
    }
}
