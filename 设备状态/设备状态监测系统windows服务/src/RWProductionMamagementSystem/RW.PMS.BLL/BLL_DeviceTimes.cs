using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.IDAL;
using RW.PMS.Model;
using RW.PMS.Model.BaseInfo;
using RW.PMS.Model.Device;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.BLL
{
    public class BLL_DeviceTimes : IBLL_DeviceTimes
    {
        private IDAL_DeviceTimes _DAL = null;

        public BLL_DeviceTimes()
        {
            _DAL = DIService.GetService<IDAL_DeviceTimes>();
        }
        public int addeffectiveOperationTime(Device_TimesModel device_Times)
        {
            return _DAL.addeffectiveOperationTime(device_Times);
        }

        public int addfaultTime(Device_TimesModel device_Times)
        {
            return _DAL.addfaultTime(device_Times);
        }

        public int addtestStopTime(Device_TimesModel device_Times)
        {
            return _DAL.addtestStopTime(device_Times);
        }

        public int addtestTime(Device_TimesModel device_Times)
        {
            return _DAL.addtestTime(device_Times);
        }

        public int addtotalHaltTime(Device_TimesModel device_Times)
        {
            return _DAL.addtotalHaltTime(device_Times);
        }

        public int addtotalOperationTime(Device_TimesModel device_Times)
        {
            return _DAL.addtotalOperationTime(device_Times);
        }

        

        public int getFirstfaultTime()
        {
            return _DAL.getFirstfaultTime();    
        }

        public bool GettestStopTime(string roomName)
        {
           var list= _DAL.GettestStopTime(roomName);
            if (list.Where(t => t.onOrOff.Equals("on")).Count() == list.Where(t => t.onOrOff.Equals("off")).Count()&&list.Count!=0)
                return true;
            else return false;
        }
        public bool GeteffectiveOperationTime(string roomName)
        {
            var list = _DAL.GeteffectiveOperationTime(roomName);
            if (list.Where(t => t.onOrOff.Equals("on")).Count() == list.Where(t => t.onOrOff.Equals("off")).Count() && list.Count != 0)
                return true;
            else return false;
        }

        public List<Device_TimesModel> GetfaultTime(string roomName, string deviceName)
        {
              return _DAL.GetfaultTime(roomName,deviceName);
        }

        public List<Device_TestandWeibaoModel> GetHolidayTime(int  id)
        {
            return _DAL.GetHolidayTime(id);
        }

        public List<Device_TestandWeibaoModel> GetweibaoTime(int id)
        {
            return _DAL.GetweibaoTime( id);
        }

        public List<Device_TimesModel> GettotalOperationTime(string roomName,string deviceName)
        {
            return _DAL.GettotalOperationTime(roomName,deviceName);
        }

        public List<Device_FaultNumberTimeModel> GetFaultNumberTimeWeek(string roomName, string deviceName, DateTime startTime, DateTime endTime)
        {
            return _DAL.GetFaultNumberTimeWeek(roomName, deviceName,startTime,endTime);
        }

        public List<Device_TimesModel> GettotalOperationTimeWeek(string roomName, string deviceName, DateTime startTime, DateTime endTime)
        {
            return _DAL.GettotalOperationTimeWeek(roomName, deviceName,startTime,endTime);
        }
    }
}
