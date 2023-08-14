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
    public class DAL_DRoomEffectiveRunningTime : IDAL_DRoomEffectiveRunningTime
    {
        public int addEffectiveRunningTime(DRoom_EffectiveRunningTimeModel model)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into droom_EffectiveRunningTime(deviceName,roomName,onOrOff,alarmTime) 
                                    values(@deviceName,@roomName,@onOrOff,@alarmTime)";
            pList.Add(new MySqlParameter("@deviceName", model.deviceName));
            pList.Add(new MySqlParameter("@roomName", model.roomName));
            pList.Add(new MySqlParameter("@onOrOff", model.onOrOff));
            pList.Add(new MySqlParameter("@alarmTime", model.alarmTime));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        }

        public List<DRoom_EffectiveRunningTimeModel> GetDRoom_EffectiveRunningTimesdesc(string deviceName, string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from droom_EffectiveRunningTime where  roomName like @roomName   order by id desc limit 1";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            //pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<DRoom_EffectiveRunningTimeModel> list = db.ExecuteList<DRoom_EffectiveRunningTimeModel>(sql, pList.ToArray());
            return list;
        }

        public List<DRoom_EffectiveRunningTimeModel> GetOneDRoom_EffectiveRunningTimeList(string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from droom_EffectiveRunningTime where  roomName like @roomName  order by id desc";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            //pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<DRoom_EffectiveRunningTimeModel> list = db.ExecuteList<DRoom_EffectiveRunningTimeModel>(sql, pList.ToArray());
            return list;
        }
    }
}
