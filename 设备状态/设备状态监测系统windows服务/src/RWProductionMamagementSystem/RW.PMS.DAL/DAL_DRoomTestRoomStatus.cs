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
    public class DAL_DRoomTestRoomStatus : IDAL_DRoomTestRoomStatus
    {
       

        public int getRoomId(string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql1 = @"select * from droom_TestRoomStatus where roomName like @deviceName";
            pList.Add(new MySqlParameter("@deviceName", '%' + roomName + '%'));
            List<DRoom_TestRoomStatusModel> list = db.ExecuteList<DRoom_TestRoomStatusModel>(sql1, pList.ToArray());
            if (list .Count>0)
                return list[0].id;
            else
                return 0;
        }

   

        public int updateTestRoomOpertionStatus(string roomName, string opertionStatus)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update droom_TestRoomStatus SET  operationStatus=@operationStatus WHERE  roomName like @roomName";
            pList.Add(new MySqlParameter("@operationStatus", opertionStatus));
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDRoomtotalEffectiveRunningTime(string roomName, double totalEffectiveRunningTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update droom_TestRoomStatus SET  totalEffectiveRunningTime=@totalEffectiveRunningTime WHERE  roomName like @roomName";
            pList.Add(new MySqlParameter("@totalEffectiveRunningTime", totalEffectiveRunningTime));
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDRoomtotalFaultTime(string roomName, double totalFaultTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update droom_TestRoomStatus SET  totalFaultTime=@totalFaultTime WHERE  roomName like @roomName";
            pList.Add(new MySqlParameter("@totalFaultTime", totalFaultTime));
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDRoomtotalFreeTime(string roomName, double totalFreeTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update droom_TestRoomStatus SET  totalFreeTime=@totalFreeTime WHERE  roomName like @roomName";
            pList.Add(new MySqlParameter("@totalFreeTime", totalFreeTime));
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDRoomtotalStandbyTime(string roomName, double totalStandbyTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update droom_TestRoomStatus SET  totalStandbyTime=@totalStandbyTime WHERE  roomName like @roomName";
            pList.Add(new MySqlParameter("@totalStandbyTime", totalStandbyTime));
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDRoomtotalTestStopTime(string roomName, double totalTestStopTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update droom_TestRoomStatus SET  totalTestStopTime=@totalTestStopTime WHERE  roomName like @roomName";
            pList.Add(new MySqlParameter("@totalTestStopTime", totalTestStopTime));
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDRoomtotalUtilizationRate(string roomName, double totalUtilizationRate)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update droom_TestRoomStatus SET  totalUtilizationRate=@totalUtilizationRate WHERE  roomName like @roomName";
            pList.Add(new MySqlParameter("@totalUtilizationRate", totalUtilizationRate));
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }

        public int updateDRoomtotalweibaoTime(string roomName, double totalweibaoTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update droom_TestRoomStatus SET  totalweibaoTime=@totalweibaoTime WHERE  roomName like @roomName";
            pList.Add(new MySqlParameter("@totalweibaoTime", totalweibaoTime));
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }
        public int updateDRoomtotaldeviceDebugRunTime(string roomName, double totaldeviceDebugRunTime)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();

            string sql2 = @"update droom_TestRoomStatus SET  totaldeviceDebugRunTime=@totaldeviceDebugRunTime WHERE  roomName like @roomName";
            pList.Add(new MySqlParameter("@totaldeviceDebugRunTime", totaldeviceDebugRunTime));
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            return db.ExecuteNonQuery(sql2, pList.ToArray());
        }
        public List<DRoom_TestRoomStatusModel> getAllTestRoomStatusList()
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql1 = @" select * from droom_TestRoomStatus";

            List<DRoom_TestRoomStatusModel> list = db.ExecuteList<DRoom_TestRoomStatusModel>(sql1, pList.ToArray());
            return list;
        }
    }
}
