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
    public class DAL_DRoomZhiDuTime : IDAL_DRoomZhiDuTime
    {
        public List<DRoom_zhiduTimeModel> GetDRoom_ZhiduTimeOne(string roomName)
        {
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();

            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"select * from droom_zhiduTime where  roomName like @roomName   order by id desc limit 1";
            pList.Add(new MySqlParameter("@roomName", '%' + roomName + '%'));
            //pList.Add(new MySqlParameter("@deviceName", deviceName));

            List<DRoom_zhiduTimeModel> list = db.ExecuteList<DRoom_zhiduTimeModel>(sql, pList.ToArray());
            return list;
        }
    }
}
