using RW.PMS.Common;
using RW.PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.IDAL
{
   public interface IDAL_DRoomTestRoomStatus:IDependence
    {
        int getRoomId(string roomName);
        int updateTestRoomOpertionStatus(string roomName, string opertionStatus);
        int updateDRoomtotalEffectiveRunningTime(string roomName, double totalEffectiveRunningTime);
        int updateDRoomtotalTestStopTime(string roomName,  double totalTestStopTime);
        int updateDRoomtotalFaultTime(string roomName,  double totalFaultTime);
        int updateDRoomtotalStandbyTime(string roomName, double totalStandbyTime);
        int updateDRoomtotalFreeTime(string roomName, double totalFreeTime);
        int updateDRoomtotalweibaoTime(string roomName, double totalweibaoTime);
        int updateDRoomtotalUtilizationRate(string roomName, double totalUtilizationRate);
        int updateDRoomtotaldeviceDebugRunTime(string roomName, double totaldeviceDebugRunTime);
        List<DRoom_TestRoomStatusModel> getAllTestRoomStatusList();
    }
}
