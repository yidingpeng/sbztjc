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
    public class DAL_DeviceRunTime : IDAL_DeviceRunTime
    {
        public List<Device_RunTimeModel> GetDevice_RunTimesdesc(string deviceName, string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from devcie_RunTime where  roomName like @roomName  and deviceName=@deviceName order by id desc limit 1";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            pList.Add(new MySqlParameter("@deviceName",  deviceName));

            List<Device_RunTimeModel> list = db.ExecuteList<Device_RunTimeModel>(sql, pList.ToArray());
            return list;
          
           
        }
        public int addRunTime(Device_RunTimeModel model)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into devcie_RunTime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        }

        public List<Device_RunTimeModel> GetOneDevice_RunTimeList(string deviceName, string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from devcie_RunTime where  roomName like @roomName  and deviceName=@deviceName order by id desc";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<Device_RunTimeModel> list = db.ExecuteList<Device_RunTimeModel>(sql, pList.ToArray());
            return list;
        }
    }
}
