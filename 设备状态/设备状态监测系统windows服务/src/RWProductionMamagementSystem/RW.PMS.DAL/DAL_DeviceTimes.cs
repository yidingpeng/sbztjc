using MySql.Data.MySqlClient;
using RW.PMS.IDAL;
using RW.PMS.Model;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.DAL
{
    public class DAL_DeviceTimes : IDAL_DeviceTimes
    {
        public int addeffectiveOperationTime(Device_TimesModel model)
        {
            
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into device_effectiveOperationTime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));
            

            return db.ExecuteNonQuery(sql, pList.ToArray());
        }
        public int addtotalHaltTime(Device_TimesModel model)
        {

            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into device_totalHaltTime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        }
        public int addtotalOperationTime(Device_TimesModel model)
        {

            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into devcie_totalOperationTime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        }
        public int addfaultTime(Device_TimesModel model)
        {

            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into device_faultTime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        }
        public int addtestStopTime(Device_TimesModel model)
        {

            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into device_testStopTime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        }
        public int addtestTime(Device_TimesModel model)
        {

            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into device_testTime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        }

        public int getFirstfaultTime()
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
             string sql = @"select * from device_faulttime order by id desc limit 1";
             List<Device_TimesModel> list= db.ExecuteList<Device_TimesModel>(sql);
            return list[0].id;
        }

        public List<Device_TimesModel> GettestStopTime(string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from device_testStopTime where roomName=@roomName ";
            
            pList.Add(new MySqlParameter("@roomName",roomName));
           
            List<Device_TimesModel> list = db.ExecuteList<Device_TimesModel>(sql, pList.ToArray());
            return list ;
        }

        public List<Device_TimesModel> GeteffectiveOperationTime(string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from device_effectiveOperationTime where roomName=@roomName ";

            pList.Add(new MySqlParameter("@roomName", roomName));

            List<Device_TimesModel> list = db.ExecuteList<Device_TimesModel>(sql, pList.ToArray());
            return list;
        }

        public List<Device_TimesModel> GetfaultTime(string roomName, string deviceName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from device_faultTime where roomName=@roomName and deviceName=@deviceName ";

            pList.Add(new MySqlParameter("@roomName", roomName));
            pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<Device_TimesModel> list = db.ExecuteList<Device_TimesModel>(sql, pList.ToArray());
            return list;
        }

        public List<Device_TestandWeibaoModel> GetHolidayTime(int  id)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
           
            string sql1 = @"select * from device_holidaytime where roomid=@roomid ";

            pList.Add(new MySqlParameter("@roomid", id));


            List<Device_TestandWeibaoModel> list2 = db.ExecuteList<Device_TestandWeibaoModel>(sql1, pList.ToArray());
            return list2;
        }

        public List<Device_TestandWeibaoModel> GetweibaoTime(int id)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            


           
            
            string sql1 = @"select * from device_weibaoTime where roomid=@roomid ";

            pList.Add(new MySqlParameter("@roomid", id));


            List<Device_TestandWeibaoModel> list = db.ExecuteList<Device_TestandWeibaoModel>(sql1, pList.ToArray());
            return list;
        }

        public List<Device_TimesModel> GettotalOperationTime(string roomName, string deviceName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from devcie_totalOperationTime where roomName=@roomName and deviceName=@deviceName ";

            pList.Add(new MySqlParameter("@roomName", roomName));
            pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<Device_TimesModel> list = db.ExecuteList<Device_TimesModel>(sql, pList.ToArray());
            return list;
        }

        public List<Device_TimesModel> GettotalOperationTimeWeek(string roomName, string deviceName ,DateTime startTime,DateTime endTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from devcie_totalOperationTime where roomName=@roomName and deviceName=@deviceName and (alarmTime>@startTime and alarmTime<@endTime)";

            pList.Add(new MySqlParameter("@roomName", roomName));
            pList.Add(new MySqlParameter("@deviceName", deviceName));
            pList.Add(new MySqlParameter("@startTime", startTime));
            pList.Add(new MySqlParameter("@endTime", endTime));
            
            List<Device_TimesModel> list = db.ExecuteList<Device_TimesModel>(sql, pList.ToArray());
            return list;
        }
        public List<Device_FaultNumberTimeModel> GetFaultNumberTimeWeek(string roomName, string deviceName, DateTime startTime, DateTime endTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from device_faultNumberTime where roomName=@roomName and deviceName=@deviceName and (alarmTime>@startTime and alarmTime<@endTime)";

            pList.Add(new MySqlParameter("@roomName", roomName));
            pList.Add(new MySqlParameter("@deviceName", deviceName));
            pList.Add(new MySqlParameter("@startTime", startTime));
            pList.Add(new MySqlParameter("@endTime", endTime));

            List<Device_FaultNumberTimeModel> list = db.ExecuteList<Device_FaultNumberTimeModel>(sql, pList.ToArray());
            
            return list;
        }
    }
}
