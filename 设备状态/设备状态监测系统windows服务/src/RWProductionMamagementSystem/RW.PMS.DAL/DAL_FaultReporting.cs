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
    public class DAL_FaultReporting : IDAL_FaultReporting
    {
        public int addFaultReporting(FaultReportingModel model)
        {
     

            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
            List<MySqlParameter> pList = new List<MySqlParameter>();
            string sql = @"insert into device_FaultReporting(testRoom,faultDateTime,reportingStatus,faultTimeid) 
                                    values(@testRoom,@faultDateTime,@reportingStatus,@faultTimeid)";
            pList.Add(new MySqlParameter("@testRoom", model.testRoom));
            pList.Add(new MySqlParameter("@faultDateTime", model.faultDateTime));
            pList.Add(new MySqlParameter("@reportingStatus", model.reportingStatus));
            pList.Add(new MySqlParameter("@faultTimeid", model.faultTimeid));


            return db.ExecuteNonQuery(sql, pList.ToArray());
        
        }
    }
}
