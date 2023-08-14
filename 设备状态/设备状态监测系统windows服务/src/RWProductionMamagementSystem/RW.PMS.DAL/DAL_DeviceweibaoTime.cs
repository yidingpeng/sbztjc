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
    public class DAL_DeviceweibaoTime : IDAL_DeviceweibaoTime
    {
        public List<Device_weibaoTimeModel> GetOneDevice_weibaoTimeList(string deviceName, string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from device_weibaotime where  roomName like @roomName  and deviceName=@deviceName order by id desc";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<Device_weibaoTimeModel> list = db.ExecuteList<Device_weibaoTimeModel>(sql, pList.ToArray());
            return list;
        }
    }
}
