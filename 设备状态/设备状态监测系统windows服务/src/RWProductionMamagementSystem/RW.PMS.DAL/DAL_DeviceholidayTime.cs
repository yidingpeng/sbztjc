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
    public class DAL_DeviceholidayTime : IDAL_DeviceholidayTime
    {
        public List<Device_holidayTimeModel> GetOneDevice_holidayTimeList(string deviceName, string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from device_holidaytime where  roomName like @roomName  and deviceName=@deviceName order by id desc";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<Device_holidayTimeModel> list = db.ExecuteList<Device_holidayTimeModel>(sql, pList.ToArray());
            return list;
        }
    }
}
