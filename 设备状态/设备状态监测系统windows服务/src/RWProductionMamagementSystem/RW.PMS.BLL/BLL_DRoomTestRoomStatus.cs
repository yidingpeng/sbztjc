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
   public class BLL_DRoomTestRoomStatus: IBLL_DRoomTestRoomStatus
    {
        private IDAL_DRoomTestRoomStatus _DAl = null;
        public BLL_DRoomTestRoomStatus()
        {
            _DAl = DIService.GetService<IDAL_DRoomTestRoomStatus>();
        }

        public List<DRoom_TestRoomStatusModel> getAllTestRoomStatusList()
        {
            return _DAl.getAllTestRoomStatusList();
        }

        public int updateDRoomtotaldeviceDebugRunTime(string roomName, double totaldeviceDebugRunTime)
        {
            return _DAl.updateDRoomtotaldeviceDebugRunTime(roomName, totaldeviceDebugRunTime);
        }

        public int updateDRoomtotalEffectiveRunningTime(string roomName, double totalEffectiveRunningTime)
        {
            return _DAl.updateDRoomtotalEffectiveRunningTime(roomName, totalEffectiveRunningTime);
        }

        public int updateDRoomtotalFaultTime(string roomName, double totalFaultTime)
        {
            return _DAl.updateDRoomtotalFaultTime(roomName, totalFaultTime);
        }

        public int updateDRoomtotalFreeTime(string roomName, double totalFreeTime)
        {
            return _DAl.updateDRoomtotalFreeTime(roomName, totalFreeTime);
        }

        public int updateDRoomtotalStandbyTime(string roomName, double totalStandbyTime)
        {
            return _DAl.updateDRoomtotalStandbyTime(roomName, totalStandbyTime);
        }

        public int updateDRoomtotalTestStopTime(string roomName, double totalTestStopTime)
        {
            return _DAl.updateDRoomtotalTestStopTime(roomName, totalTestStopTime);
        }

        public int updateDRoomtotalUtilizationRate(string roomName, double totalUtilizationRate)
        {
            return _DAl.updateDRoomtotalUtilizationRate(roomName, totalUtilizationRate);
        }

        public int updateDRoomtotalweibaoTime(string roomName, double totalweibaoTime)
        {
            return _DAl.updateDRoomtotalweibaoTime(roomName, totalweibaoTime);
        }

        public int updateTestRoomOpertionStatus(string roomName, string opertionStatus)
        {
            return _DAl.updateTestRoomOpertionStatus(roomName, opertionStatus);
        }

        int IBLL_DRoomTestRoomStatus.getRoomId(string roomName)
        {
            return _DAl.getRoomId(roomName);
        }
    }
}
