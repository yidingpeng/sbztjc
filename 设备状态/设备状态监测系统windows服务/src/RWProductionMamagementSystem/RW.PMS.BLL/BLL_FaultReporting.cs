using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.IDAL;
using RW.PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.BLL
{
   public class BLL_FaultReporting : IBLL_FaultReporting
    {
        private IDAL_FaultReporting _DAL = null;

        public BLL_FaultReporting()
        {
            _DAL = DIService.GetService<IDAL_FaultReporting>();
        }
        public int addFaultReporting(FaultReportingModel faultReportingModel)
        {
            return _DAL.addFaultReporting( faultReportingModel);
        }
    }
}
