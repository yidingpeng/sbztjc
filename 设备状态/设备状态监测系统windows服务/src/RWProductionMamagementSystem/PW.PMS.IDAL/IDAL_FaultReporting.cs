using RW.PMS.Common;
using RW.PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IDAL
{
   public interface IDAL_FaultReporting : IDependence
    {
        int addFaultReporting(FaultReportingModel faultReportingModel);
    }
}
