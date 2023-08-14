using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.IDAL;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.BLL
{
    public class BLL_DeviceTrainningTime : IBLL_DeviceTrainningTime
    {
        private IDAL_DeviceTrainningTime _DAl = null;
        public BLL_DeviceTrainningTime()
        {
            _DAl = DIService.GetService<IDAL_DeviceTrainningTime>();
        }
        public List<Device_TrainningTimeModel> GetOneDevice_TrainingTimeList(string deviceName, string roomName)
        {
            return _DAl.GetOneDevice_TrainingTimeList(deviceName, roomName);
        }
    }
}
