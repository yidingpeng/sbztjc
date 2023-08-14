using RW.PMS.Common;
using RW.PMS.Model.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IDAL
{
    public interface IDAL_Graph: IDependence
    {
        int AddGraphData(GraphModel model);
         List<GraphModel> GetGraphData(int boltNo, string prodNo, string pmodel);
    }
}
