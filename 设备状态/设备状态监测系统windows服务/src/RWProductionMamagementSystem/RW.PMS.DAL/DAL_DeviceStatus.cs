using MySql.Data.MySqlClient;
using RW.PMS.IDAL;
using RW.PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.DAL
{
    public class DAL_DeviceStatus : IDAL_DeviceStatus
    {
        public List<Dvice_DeviceStatusModel> getAllDeviceStatusList()
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql1 = @" select * from device_DeviceStatus ";

            List<Dvice_DeviceStatusModel> list = db.ExecuteList<Dvice_DeviceStatusModel>(sql1, pList.ToArray());
            return list;
        }

        //public List<Dvice_DeviceStatusModel> getDevices()
        //{
        //    RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

        //    List<MySqlParameter> pList = new List<MySqlParameter>();
        //    string sql1 = @" select * from device_DeviceStatus ";

        //    List<Dvice_DeviceStatusModel> list = db.ExecuteList<Dvice_DeviceStatusModel>(sql1, pList.ToArray());
        //    return list;
        //}

        //public List<Device_TestRoomStatusModel> getRooms()
        //{
        //    RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

        //    List<MySqlParameter> pList = new List<MySqlParameter>();
        //    string sql1 = @"select * from device_TestRoom ";

        //    List<Device_TestRoomStatusModel> list = db.ExecuteList<Device_TestRoomStatusModel>(sql1, pList.ToArray());
        //    return list;
        //}


        //public int updateDeviceStatus(Dvice_DeviceStatusModel model)
        //{
        //    //RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
        //    //List<MySqlParameter> pList = new List<MySqlParameter>();
        //    //string sql = "UPDATE device_DeviceStatus SET ";
        //    //List<string> setClauses = new List<string>();
        //    //List<string> whereClauses = new List<string>();
        //    //if (model.normalRunningTime!= 0)
        //    //{
        //    //    setClauses.Add("normalRunningTime = @normalRunningTime");
        //    //    pList.Add(new MySqlParameter("@normalRunningTime", model.normalRunningTime));
        //    //}

        //    //if (model.faultDowntime!=0)
        //    //{
        //    //    setClauses.Add("faultDowntime = @faultDowntime");
        //    //    pList.Add(new MySqlParameter("@faultDowntime", model.faultDowntime));
        //    //}

        //    //if (model.idleDownTime!= 0)
        //    //{
        //    //    setClauses.Add("idleDownTime = @idleDownTime");
        //    //    pList.Add(new MySqlParameter("@idleDownTime", model.idleDownTime));

        //    //}
        //    //if (model.experimentWaitingTime != 0)
        //    //{
        //    //    setClauses.Add("experimentWaitingTime = @experimentWaitingTime");
        //    //    pList.Add(new MySqlParameter("@experimentWaitingTime", model.experimentWaitingTime));
        //    //}
        //    //if (model.experimentOccupiedTime!= 0)
        //    //{
        //    //    setClauses.Add("experimentOccupiedTime = @experimentOccupiedTime");
        //    //    pList.Add(new MySqlParameter("@experimentOccupiedTime", model.experimentOccupiedTime));
        //    //}
        //    //if (model.holidayTime != 0)
        //    //{
        //    //    setClauses.Add("holidayTime = @holidayTime");
        //    //    pList.Add(new MySqlParameter("@holidayTime", model.holidayTime));
        //    //}
        //    //if (model.weibaoTime!= 0)
        //    //{
        //    //    setClauses.Add("weibaoTime=@weibaoTime");
        //    //    pList.Add(new MySqlParameter("@weibaoTime", model.weibaoTime));
        //    //}
        //    //if (model.stopRunTime != 0)
        //    //{
        //    //    setClauses.Add("stopRunTime=@stopRunTime");
        //    //    pList.Add(new MySqlParameter("@stopRunTime", model.stopRunTime));
        //    //}
        //    //if (model.roomId!=0)
        //    //{
        //    //    whereClauses.Add("WHERE roomId = @roomId");
        //    //    pList.Add(new MySqlParameter("@roomId", model.roomId));
        //    //}
        //    //if (!string.IsNullOrEmpty(model.deviceName))
        //    //{
        //    //    whereClauses.Add("WHERE deviceName = @deviceName");
        //    //    pList.Add(new MySqlParameter("@deviceName", model.deviceName));
        //    //}
        //    //if (setClauses.Count > 0)
        //    //{
        //    //    sql += string.Join(", ", setClauses);
        //    //    sql +=" "+whereClauses[0];
        //    //}
        //    //return db.ExecuteNonQuery(sql, pList.ToArray());
        //    return 1;
        //}

        //public int updateStatus(Device_TestRoomStatusModel model)
        //{
        //    RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

        //    List<MySqlParameter> pList = new List<MySqlParameter>();
        //    string sql1 = @"select * from device_TestRoom where roomName like @deviceName";
        //    pList.Add(new MySqlParameter("@deviceName", '%'+model.roomName+'%'));
        //    List<Device_TestRoomStatusModel> list = db.ExecuteList<Device_TestRoomStatusModel>(sql1,pList.ToArray());
        //    pList.Clear();
        //    string sql2 = @"update device_TestRoom SET  equipmentOperationStatus=@equipmentOperationStatus,equipmentTestStatus=@equipmentTestStatus WHERE id=@roomId";
        //    //pList.Add(new MySqlParameter("@equipmentOperationStatus", model.equipmentOperationStatus));
        //   // pList.Add(new MySqlParameter("@equipmentTestStatus", model.equipmentTestStatus));
        //    pList.Add(new MySqlParameter("@roomId", list[0].id));
        //    return db.ExecuteNonQuery(sql2, pList.ToArray());
        //}
        public int updateDeviceOpertionStatus(string deviceName,int roomid,string opertionStatus)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
           
            string sql2 = @"update device_DeviceStatus SET  operationStatus=@operationStatus WHERE roomId=@roomId and deviceName like @deviceName";
            pList.Add(new MySqlParameter("@operationStatus", opertionStatus));
             pList.Add(new MySqlParameter("@roomId", roomid));
             
            pList.Add(new MySqlParameter("@deviceName", '%' + deviceName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDeviceOpertionStatusfault(int roomid, string opertionStatus)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update device_DeviceStatus SET  operationStatus=@operationStatus WHERE roomId=@roomId ";
            pList.Add(new MySqlParameter("@operationStatus", opertionStatus));
            pList.Add(new MySqlParameter("@roomId", roomid));

          
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDevicetotalFaultDownTime(string deviceName, int roomid, double totalFaultDownTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update device_DeviceStatus SET  totalFaultDownTime=@totalFaultDownTime WHERE roomId=@roomId and deviceName like @deviceName";
            pList.Add(new MySqlParameter("@totalFaultDownTime", totalFaultDownTime));
            pList.Add(new MySqlParameter("@roomId", roomid));

            pList.Add(new MySqlParameter("@deviceName", '%' + deviceName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDevicetotalFreeTime(string deviceName, int roomid, double totalFreeTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update device_DeviceStatus SET  totalFreeTime=@totalFreeTime WHERE roomId=@roomId and deviceName like @deviceName";
            pList.Add(new MySqlParameter("@totalFreeTime", totalFreeTime));
            pList.Add(new MySqlParameter("@roomId", roomid));

            pList.Add(new MySqlParameter("@deviceName", '%' + deviceName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDevicetotalRunTime(string deviceName, int roomid, double totalRunTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update device_DeviceStatus SET  totalRunTime=@totalRunTime WHERE roomId=@roomId and deviceName like @deviceName";
            pList.Add(new MySqlParameter("@totalRunTime", totalRunTime));
            pList.Add(new MySqlParameter("@roomId", roomid));

            pList.Add(new MySqlParameter("@deviceName", '%' + deviceName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDevicetotalStopTime(string deviceName, int roomid, double totalStopTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update device_DeviceStatus SET  totalStopTime=@totalStopTime WHERE roomId=@roomId and deviceName like @deviceName";
            pList.Add(new MySqlParameter("@totalStopTime", totalStopTime));
            pList.Add(new MySqlParameter("@roomId", roomid));

            pList.Add(new MySqlParameter("@deviceName", '%' + deviceName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDevicetotalweibaoTime(string deviceName, int roomid, double weibaoTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update device_DeviceStatus SET  weibaoTime=@weibaoTime WHERE roomId=@roomId and deviceName like @deviceName";
            pList.Add(new MySqlParameter("@weibaoTime", weibaoTime));
            pList.Add(new MySqlParameter("@roomId", roomid));

            pList.Add(new MySqlParameter("@deviceName", '%' + deviceName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }
    }
}
