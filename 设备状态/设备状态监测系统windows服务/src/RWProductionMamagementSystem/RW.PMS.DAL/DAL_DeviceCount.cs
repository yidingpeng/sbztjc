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
    public class DAL_DeviceCount : IDAL_DeviceCount
    {
        public int insertDataCount(Device_DataCountModel model)
        {

            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into device_DataCount(runTimeCount,stopTimeCount,faultNumberCount,deviceName,roomName,week,month,year) 
                                    values(@runTimeCount,@stopTimeCount,@faultNumberCount,@deviceName,@roomName,@week,@month,@year)";
            pList.Add(new MySqlParameter("@runTimeCount", model.runTimeCount));
            pList.Add(new MySqlParameter("@stopTimeCount", model.stopTimeCount));
            pList.Add(new MySqlParameter("@faultNumberCount", model.faultNumberCount));
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@week", model.week));
            pList.Add(new MySqlParameter("@month", model.month));
            pList.Add(new MySqlParameter("@year", model.year));

            return db.ExecuteNonQuery(sql, pList.ToArray());
        }

       
    }
}
