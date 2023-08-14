using RW.PMS.Common;
using RW.PMS.Model;
using RW.PMS.Model.watchDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RW.PMS.IBLL
{
   public interface IBLL_DeviceTimes: IDependence
    {
        /// <summary>
        /// 有效运行时间
        /// </summary>
        /// <param name="device_Times"></param>
        /// <returns></returns>
        int addeffectiveOperationTime(Device_TimesModel device_Times);
        bool GeteffectiveOperationTime(string roomName);
        /// <summary>
        /// 总运行时间
        /// </summary>
        /// <param name="device_Times"></param>
        /// <returns></returns>
        int addtotalOperationTime(Device_TimesModel device_Times);

        /// <summary>
        /// 总停机时间
        /// </summary>
        /// <param name="device_Times"></param>
        /// <returns></returns>
        int addtotalHaltTime(Device_TimesModel device_Times);
        /// <summary>
        /// 故障时间
        /// </summary>
        /// <param name="device_Times"></param>
        /// <returns></returns>
        int addfaultTime(Device_TimesModel device_Times);
        /// <summary>
        /// 获取故障填报表的id
        /// </summary>
        /// <returns></returns>
        int getFirstfaultTime();
        /// <summary>
        /// 试验暂停等待时间
        /// </summary>
        /// <param name="device_Times"></param>
        /// <returns></returns>
        int addtestStopTime(Device_TimesModel device_Times);
        bool GettestStopTime(string roomName);
        /// <summary>
        /// 试验占用时间
        /// </summary>
        /// <param name="device_Times"></param>
        /// <returns></returns>
        int addtestTime(Device_TimesModel device_Times);

        List<Device_TimesModel> GetfaultTime( string roomName,string deviceName);//停机时间
        List<Device_TestandWeibaoModel> GetHolidayTime(int id);//节假日时间
        List<Device_TestandWeibaoModel> GetweibaoTime(int id);//维保时间
        List<Device_TimesModel> GettotalOperationTime(string roomName, string deviceName);//获取总运行时间
        List<Device_FaultNumberTimeModel> GetFaultNumberTimeWeek(string roomName, string deviceName, DateTime startTime, DateTime endTime);
        List<Device_TimesModel> GettotalOperationTimeWeek(string roomName, string deviceName, DateTime startTime, DateTime endTime);
    }
}
