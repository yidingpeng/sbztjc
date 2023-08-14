using RW.PMS.IDAL;
using RW.PMS.Model.WorkStep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.DAL
{
    public class DAL_WorkStep : IDAL_WorkStep
    {
        public List<WorkStep> getWorkStep()
        {
            var datetime = (new DAL_BaseInfo()).GetServerDateTime();
            RW.PMS.Common.MySqlHelper db = new RW.PMS.Common.MySqlHelper();
           
            string sql = @"select * from WorkStep";
           
            List<WorkStep> list = db.ExecuteList<WorkStep>(sql);

            return list;
        }
    }
}
