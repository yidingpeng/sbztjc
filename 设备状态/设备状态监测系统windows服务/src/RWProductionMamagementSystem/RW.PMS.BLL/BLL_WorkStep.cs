using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.IDAL;
using RW.PMS.Model.WorkStep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.BLL
{
   public class BLL_WorkStep:IBLL_WorkStep
    {
        private IDAL_WorkStep _DAl = null;
        public BLL_WorkStep()
        {
            _DAl = DIService.GetService<IDAL_WorkStep>();
        }

        public List<WorkStep> getWorkStep()
        {
            return _DAl.getWorkStep();
        }
    }
}
