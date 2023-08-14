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
    public class DAL_DeviceTrainningTime : IDAL_DeviceTrainningTime
    {
        public List<Device_TrainningTimeModel> GetOneDevice_TrainingTimeList(string deviceName, string roomName)
        {

            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from device_trainingtime where  roomName like @roomName  and deviceName=@deviceName order by id desc";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<Device_TrainningTimeModel> list = db.ExecuteList<Device_TrainningTimeModel>(sql, pList.ToArray());
            return list;
        }
    }
}
