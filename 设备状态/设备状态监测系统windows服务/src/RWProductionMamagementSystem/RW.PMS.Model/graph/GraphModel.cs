using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Model.graph
{
    public class GraphModel: BaseModel
    {
        public GraphModel(DateTime dateTime, double torque, double angle ,string graphName, int boltNo, string prodNo, string pmodel)
        {
            this.dateTime = dateTime;
            this.torque = torque;
            this.angle = angle;
            this.graphName = graphName;
            this.boltNo = boltNo;
            this.prodNo = prodNo;
            this.pmodel = pmodel;
        }
        public GraphModel()
        {

        }
        public int id { get; set; }
        public DateTime dateTime { get; set; }
        public Double torque { get; set; }
        public Double angle { get; set; }
        public string graphName { get; set; }
        public int boltNo { get; set; }
        public string  prodNo { get; set; }
        public string pmodel { get; set; }

    }
}
