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
    public class BLL_DeviceStatus : IBLL_DeviceStatus
    {
        private IDAL_DeviceStatus _DAl = null;
        public BLL_DeviceStatus()
        {
            _DAl = DIService.GetService<IDAL_DeviceStatus>();
        }

        public List<Dvice_DeviceStatusModel> getAllDeviceStatusList()
        {
            return _DAl.getAllDeviceStatusList();
        }

        public int updateDeviceOpertionStatus(string deviceName, int roomid, string opertionStatus)
        {
            return _DAl.updateDeviceOpertionStatus(deviceName, roomid, opertionStatus);
        }

        public int updateDeviceOpertionStatusfault(int roomid, string opertionStatus)
        {
            return _DAl.updateDeviceOpertionStatusfault(roomid, opertionStatus);
        }

        public int updateDevicetotalFaultDownTime(string deviceName, int roomid, double totalFaultDownTime)
        {
            return _DAl.updateDevicetotalFaultDownTime(deviceName, roomid, totalFaultDownTime);
        }

        public int updateDevicetotalFreeTime(string deviceName, int roomid, double totalFreeTime)
        {
            return _DAl.updateDevicetotalFaultDownTime(deviceName, roomid, totalFreeTime);
        }

        public int updateDevicetotalRunTime(string deviceName, int roomid, double totalRunTime)
        {
            return _DAl.updateDevicetotalRunTime(deviceName, roomid, totalRunTime);
        }

        public int updateDevicetotalStopTime(string deviceName, int roomid, double totalStopTime)
        {
            return _DAl.updateDevicetotalStopTime(deviceName, roomid, totalStopTime);
        }

        public int updateDevicetotalweibaoTime(string deviceName, int roomid, double weibaoTime)
        {
            return _DAl.updateDevicetotalweibaoTime(deviceName, roomid, weibaoTime);
        }

        //public List<Dvice_DeviceStatusModel> getDevices()
        //{
        //    return _DAl.getDevices();
        //}

        //public List<Device_TestRoomStatusModel> getRooms()
        //{
        //    return _DAl.getRooms();
        //}

        //public int updateDeviceStatus(Dvice_DeviceStatusModel model)
        //{
        //    return _DAl.updateDeviceStatus(model);
        //}

        //public int updateStatus(Device_TestRoomStatusModel model)
        //{
        //    return _DAl.updateStatus(model);
        //}
    }
}
