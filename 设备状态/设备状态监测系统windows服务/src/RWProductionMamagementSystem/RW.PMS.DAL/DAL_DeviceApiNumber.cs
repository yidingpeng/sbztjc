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
    public class DAL_DeviceApiNumber : IDAL_DeviceApiNumber
    {
        public List<Device_ApiNumberModel> getOneApiNumber(string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql1 = @"select * from device_apinumber where roomName like @roomName";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            List<Device_ApiNumberModel> list = db.ExecuteList<Device_ApiNumberModel>(sql1, pList.ToArray());
            return list;
        }
    }
}
