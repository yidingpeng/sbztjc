using RW.PMS.Common;
using RW.PMS.Model.WorkStep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IBLL
{
   public interface IBLL_WorkStep: IDependence
    {
        List<WorkStep> getWorkStep();

    }
}
