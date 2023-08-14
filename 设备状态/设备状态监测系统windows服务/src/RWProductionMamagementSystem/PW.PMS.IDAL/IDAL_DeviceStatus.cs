using RW.PMS.Common;
using RW.PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IDAL
{
   public  interface IDAL_DeviceStatus : IDependence
    {
        //int updateStatus(Device_TestRoomStatusModel model);

        //int updateDeviceStatus(Dvice_DeviceStatusModel model);
        //List<Dvice_DeviceStatusModel> getDevices();
        //List<Device_TestRoomStatusModel> getRooms();
        int updateDeviceOpertionStatus(string deviceName, int roomid, string opertionStatus);
        int updateDeviceOpertionStatusfault(int roomid, string opertionStatus);
        int updateDevicetotalFaultDownTime(string deviceName, int roomid, double totalFaultDownTime);
        int updateDevicetotalFreeTime(string deviceName, int roomid, double totalFreeTime);
        int updateDevicetotalRunTime(string deviceName, int roomid, double totalRunTime);
        int updateDevicetotalStopTime(string deviceName, int roomid, double totalStopTime);
        int updateDevicetotalweibaoTime(string deviceName, int roomid, double weibaoTime);
        List<Dvice_DeviceStatusModel> getAllDeviceStatusList();
    }
}
