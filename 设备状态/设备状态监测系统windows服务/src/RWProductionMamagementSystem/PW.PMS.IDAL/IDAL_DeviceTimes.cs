using RW.PMS.Common;
using RW.PMS.Model;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IDAL
{
   public  interface IDAL_DeviceTimes : IDependence
    {
        int addeffectiveOperationTime(Device_TimesModel device_Times);
        int addtotalOperationTime(Device_TimesModel device_Times);
        int addtotalHaltTime(Device_TimesModel device_Times);
        int addfaultTime(Device_TimesModel device_Times);
        int getFirstfaultTime();
        int addtestStopTime(Device_TimesModel device_Times);
        int addtestTime(Device_TimesModel device_Times);
        List<Device_TimesModel> GettestStopTime(string roomName);
        List<Device_TimesModel> GeteffectiveOperationTime(string roomName);
       List<Device_TimesModel> GetfaultTime(string roomName, string deviceName);//停机时间
        List<Device_TestandWeibaoModel> GetHolidayTime(int id);//节假日时间
        List<Device_TestandWeibaoModel> GetweibaoTime(int id);//维保时间
       List<Device_TimesModel> GettotalOperationTime(string roomName, string deviceName);//获取总运行时间

        List<Device_FaultNumberTimeModel> GetFaultNumberTimeWeek(string roomName, string deviceName, DateTime startTime, DateTime endTime);
        List<Device_TimesModel> GettotalOperationTimeWeek(string roomName, string deviceName, DateTime startTime, DateTime endTime);


    }
}
