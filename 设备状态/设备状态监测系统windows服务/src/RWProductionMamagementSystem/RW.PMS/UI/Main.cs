using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.WinForm.Module;
using RW.PMS.WinForm.OPC;
using RW.PMS.WinForm.UI.Assembly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Serilog;
using RW.PMS.WinForm.log;
using System.Timers;
using Timer = System.Timers.Timer;
using RW.PMS.Model;
using RW.PMS.IBLL.Programs;
using Quartz;
using Quartz.Impl;
using RW.PMS.Model.watchDevice;
using RW.PMS.Model.FileRequst;
using System.Configuration;

namespace RW.PMS.WinForm.UI
{
    public partial class Main : Form
    {
        IBLL_BaseInfo BLLBseInfo = DIService.GetService<IBLL_BaseInfo>();

        
        private static IBLL_BaseInfo BLL = DIService.GetService<IBLL_BaseInfo>();
        private static IBLL_DeviceTimes _device = DIService.GetService<IBLL_DeviceTimes>();
        private static IBLL_FaultReporting _faultReporting = DIService.GetService<IBLL_FaultReporting>();
        private static IBLL_DeviceStatus _deviceStatus = DIService.GetService<IBLL_DeviceStatus>();
        private static IBLL_DeviceRunTime _deviceRuntime = DIService.GetService<IBLL_DeviceRunTime>();
        private static IBLL_DeviceFaultDownTime _deviceFaultDowntime = DIService.GetService<IBLL_DeviceFaultDownTime>();
        private static IBLL_DRoomFaultTime _droomFaulttime = DIService.GetService<IBLL_DRoomFaultTime>();
        private static IBLL_DRoomTestOccupyTime _droomTestOccupytime = DIService.GetService<IBLL_DRoomTestOccupyTime>();
        private static IBLL_DeviceStopTime _deviceStoptime = DIService.GetService<IBLL_DeviceStopTime>();
        private static IBLL_DRoomTestRoomStatus _droomTestRoomStatus = DIService.GetService<IBLL_DRoomTestRoomStatus>();
        private static IBLL_DRoomStandByTime _droomStandByTime = DIService.GetService<IBLL_DRoomStandByTime>();
        private static IBLL_DRoomTestSTopTime _droomTestStopTime = DIService.GetService<IBLL_DRoomTestSTopTime>();
        private static IBLL_DRoomEffectiveRunningTime _droomEffectiveRunningTime = DIService.GetService<IBLL_DRoomEffectiveRunningTime>();
        private static IBLL_DeviceholidayTime _deviceholidayTime = DIService.GetService<IBLL_DeviceholidayTime>();
        private static IBLL_DeviceTrainningTime _devicetrainningTime = DIService.GetService<IBLL_DeviceTrainningTime>();
        private static IBLL_DeviceweibaoTime _deviceweibaoTime = DIService.GetService<IBLL_DeviceweibaoTime>();
        private static IBLL_DRoomZhiDuTime _droomzhiduTime = DIService.GetService<IBLL_DRoomZhiDuTime>();
        private static IBLL_DRoomjiaozhunTime _droomjiaozhunTime = DIService.GetService<IBLL_DRoomjiaozhunTime>();
        private static IBLL_DRoomWeiBaoTime _droomweibaoTime = DIService.GetService<IBLL_DRoomWeiBaoTime>();
        private static IBLL_DRoomHolidayTime _droomholidyTime = DIService.GetService<IBLL_DRoomHolidayTime>();
        private static IBBL_DeviceApiNumber _deviceApiNuber= DIService.GetService<IBBL_DeviceApiNumber>();
        private static IBLL_DRoomDeviceDebugRunTime _droomDeviceDebugRunTime = DIService.GetService<IBLL_DRoomDeviceDebugRunTime>();
        private static IBLL_DRoomApiStatus _droomapistatus= DIService.GetService<IBLL_DRoomApiStatus>();

        int workStepCount = 0;
        public int sendflag = 0;
        //OPCTagValueCharge 控件
        private OPCTagValueCharge _opcTagValueCharge1 = null;
        //OPC点位控制
        private OPC_Function _opcFunc = null;
        private object _objOPCTagLock = new object();
        private object _objapiLock = new object();

        bool flag = false;
        //装配管理数据操作
        private static AssemblyDataControl _asseDataControl = null;
        //是否开始监听点位
        bool _IsMonitor = false;
        //当前所选配方名称
        private string formula = null;
        private string _logTxt = "";
        private bool _IsStart = true;

        private bool _isJS = false;
        private int boltsnum = 0;

       
        public static Main frm1;





        public Main()
        {
            InitializeComponent();
            //Load1();
            //启动OPC
            frm1 = this;
            _opcTagValueCharge1 = new OPCTagValueCharge();

            _opcTagValueCharge1.NameValueChanged += opcTagValueCharge1_NameValueChanged;

            _opcTagValueCharge1.Init();

          

           
            DateTime now = DateTime.Now;
            int daysUntilNextFriday = ((int)DayOfWeek.Friday - (int)now.DayOfWeek + 7) % 7;
            TimeSpan timeUntilNextFriday = new TimeSpan(daysUntilNextFriday, 23, 17, 0) - now.TimeOfDay;

            // 创建一个定时器，设置间隔为下一个周五的时间差
            Timer timers = new Timer(timeUntilNextFriday.TotalMilliseconds);
            timers.Elapsed += Timer_Elapsed;
            timers.AutoReset = true;
            timers.Start();

          
        }
        private   void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           
            // 获取下一个周五的时间差
            DateTime now = DateTime.Now;
            int daysUntilNextFriday = ((int)DayOfWeek.Friday - (int)now.DayOfWeek + 7) % 7;
            TimeSpan timeUntilNextFriday = new TimeSpan(daysUntilNextFriday, 21, 02, 0);

            // 重新设置定时器的间隔为下一个周五的时间差
            Timer timer = (Timer)sender;
            timer.Interval = timeUntilNextFriday.TotalMilliseconds;
        }
    
    public void Load1()
        {
            try
            {

                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                _opcFunc = new OPC_Function();
              
                _asseDataControl = new AssemblyDataControl();
                BindFormulaDLL();

             

             
            }
            catch (Exception)
            {

                throw;
            }

        }

       

        #region 配方opc地址

        #region 绑定数据
        public void BindFormulaDLL()
        { 
                            
                    //var data = BLLBseInfo.GetBaseProductModel();
                    //cmbProdNo.DataSource = data;
                    //cmbProdNo.DisplayMember = "Pmodel";
                    //cmbProdNo.ValueMember = "ID";
                    //var formulamodel = BLLBseInfo.GetBaseProductModelId((int)cmbProdNo.SelectedValue);
                    //this.lblFormulaCode.Text = formulamodel.formulaNo;
                    //this.lblTorque.Text = formulamodel.tighteningTorque + "N.m.";
                    //flag = true;
                    //formula = cmbProdNo.Text;
                
            
           
        }

        #endregion


       

        //装配opc点位信息
        class formulaOPCInfo
        {
            public string FormulaName { get; set; }

            public string TagName { get; set; }

            public string OPCValue { get; set; }
        }


        #endregion



     

        //点位控制
        private void opcTagValueCharge1_NameValueChanged(object sender, string tagKey, object tagValue)
        {


         
            lock (_objOPCTagLock)
            {
                var RootStatus = "";//设备状态编码,默认设备开机
                if (!string.IsNullOrEmpty(tagKey))
                {


                    //获取所有点位信息
                    var AllPoint  = DIService.GetService<IBLL_PointInfo>().GetPointInfo(null, 0);
                    var tagKeys = tagKey.Substring(9);
                    //判断是否是报警信号
                    if (AllPoint.Where(t => t.Address == tagKeys).Count() > 0)
                    {
        #region 单个判断
                        //试验占用时间
                        if (tagKeys.Contains("试验占用"))
                        {
                            //时间累加

                            //状态时间添加
                            List<DRoom_TestOccupyTimeModel> list = _droomTestOccupytime.GetRoom_TestOccupyTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            bool value = _opcTagValueCharge1.Read(tagKey).ToBool();
                            string onOrOff = "";
                            switch (value.ToBool())
                            {
                                case true: onOrOff = "on"; break;
                                case false: onOrOff = "off"; break;
                            }
                            if (list.Count == 0)
                            {
                                if (onOrOff.Equals("on"))
                                {
                                    //添加
                                    _droomTestOccupytime.addRoom_TestOccupyTime(
                                        new DRoom_TestOccupyTimeModel()
                                        {
                                            deviceName = "all",
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = onOrOff,
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                    insertlog("占用", onOrOff, "试验台架" + tagKeys.Substring(7, 3) + onOrOff);
                                }
                                
                            }
                            else
                            {
                                if (!onOrOff.Equals(list[0].onOrOff))
                                {
                                    //添加
                                    _droomTestOccupytime.addRoom_TestOccupyTime(
                                        new DRoom_TestOccupyTimeModel()
                                        {
                                            deviceName = "all",
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = onOrOff,
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                    insertlog("占用", onOrOff, "试验台架" + tagKeys.Substring(7, 3) + onOrOff);
                                }
                            }
                            
                        }
                        //故障时间
                        if (tagKeys.Contains("故障"))
                        {
                           
                            //时间添加
                            List<Device_FaultDownTimeModel> devicelist = _deviceFaultDowntime.GetDevice_faultDownTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            List<DRoom_FaultTimeModel> roomlist = _droomFaulttime.GetDevice_faultTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            bool value = _opcTagValueCharge1.Read(tagKey).ToBool();
                            string onOrOff = "";
                            int roomid = _droomTestRoomStatus.getRoomId(tagKeys.Substring(7, 3));
                            if (value.ToBool())
                                _deviceStatus.updateDeviceOpertionStatusfault( roomid, "故障中");
                            else
                            {
                                var devciepointList = AllPoint.Where(t => t.Address.Contains(tagKeys.Substring(7, 3))).ToList();
                                var device = devciepointList.Where(t => t.Address.Contains("设备")).ToList();
                                device.ForEach(t => 
                                { 
                                    if(_opcTagValueCharge1.Read(t.TagName+t.Address).ToBool())
                                    _deviceStatus.updateDeviceOpertionStatus(t.Address.Substring(11), roomid, "运行中");
                                    else
                                        _deviceStatus.updateDeviceOpertionStatus(t.Address.Substring(11), roomid, "停机中");
                                });
                                
                            }
                            switch (value.ToBool())
                            {
                                case true: onOrOff = "on"; break;
                                case false: onOrOff = "off"; break;
                            }
                            //设备故障时间
                            if (devicelist.Count == 0)
                            {
                                if (onOrOff.Equals("on"))
                                {
                                    //添加
                                    _deviceFaultDowntime.addFaultDownTime(
                                        new Device_FaultDownTimeModel()
                                        {
                                            deviceName = "all",
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = onOrOff,
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                    insertlog("故障", onOrOff, "试验台架" + tagKeys.Substring(7, 3) + onOrOff);
                                }
                               
                            }
                            else
                            {
                                if (!onOrOff.Equals(devicelist[0].onOrOff))
                                {
                                    //添加
                                    _deviceFaultDowntime.addFaultDownTime(
                                        new Device_FaultDownTimeModel()
                                        {
                                            deviceName = "all",
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = onOrOff,
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                    if (onOrOff.Equals("off"))
                                    {
                                        List<Device_FaultDownTimeModel> devicelistid = _deviceFaultDowntime.GetDevice_faultDownTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));

                                        _faultReporting.addFaultReporting(new FaultReportingModel() { testRoom = "试验台架" + tagKeys.Substring(7, 3), faultDateTime = DateTime.Now, reportingStatus = 0, faultTimeid = devicelistid[0].id });
                                    }

                                    insertlog("故障", onOrOff, "试验台架" + tagKeys.Substring(7, 3) + onOrOff);
                                }
                            }
                           
                            //台架故障时间
                            if (roomlist.Count == 0)
                            {
                                if (onOrOff.Equals("on"))
                                {
                                    //添加
                                    _droomFaulttime.addFaultTime(
                                        new DRoom_FaultTimeModel()
                                        {
                                            deviceName = "all",
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = onOrOff,
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                   
                                }

                            }
                            else
                            {
                                if (!onOrOff.Equals(roomlist[0].onOrOff))
                                {
                                    //添加
                                    _droomFaulttime.addFaultTime(
                                        new DRoom_FaultTimeModel()
                                        {
                                            deviceName = "all",
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = onOrOff,
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                    
                                }
                            }
                            //时间累加
                          var devices=  _deviceStatus.getAllDeviceStatusList();
                            devices.ForEach(t=>{ 
                            var faultdowmrunlist = _deviceFaultDowntime.GetOneDevice_FaultDownTimeList(t.deviceName,t.deviceName.Substring(4,3));
                           
                            TimeSpan faultdowmtimetricks = TimeSpan.Zero;
                            
                            if (onOrOff.Equals("off"))
                            {//设备故障累加
                                if (faultdowmrunlist.Count % 2 == 0 && faultdowmrunlist.Count != 0)
                                {
                                    for (int i = 0; i < faultdowmrunlist.Count; i += 2)
                                    {
                                        faultdowmtimetricks += TimeSpan.FromTicks(faultdowmrunlist[i].alarmTime.Ticks - faultdowmrunlist[i + 1].alarmTime.Ticks);
                                    }
                                        int roomid = _droomTestRoomStatus.getRoomId(t.deviceName.Substring(4, 3));
                                        _deviceStatus.updateDevicetotalFaultDownTime(

                                       t.deviceName, roomid, faultdowmtimetricks.TotalSeconds
                                    );
                                }
                            }
                            });
                          
                        }
                        //设备运/停时间
                        if (tagKeys.Contains("设备"))
                        {
                            


                            //状态时间添加
                            //运行
                            List<Device_RunTimeModel> devicerunlist = _deviceRuntime.GetDevice_RunTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            int roomid=  _droomTestRoomStatus.getRoomId(tagKeys.Substring(7, 3));

                            bool value = _opcTagValueCharge1.Read(tagKey).ToBool();
                            string onOrOff = "";
                            switch (value.ToBool())
                            {
                                case true: onOrOff = "on"; break;
                                case false: onOrOff = "off"; break;
                            }
                            if (value.ToBool())
                                _deviceStatus.updateDeviceOpertionStatus(tagKeys.Substring(11), roomid, "运行中");
                            else
                                _deviceStatus.updateDeviceOpertionStatus(tagKeys.Substring(11), roomid, "停机中");
                            if (devicerunlist.Count==0)
                            {
                                if (onOrOff.Equals("on"))
                                {
                                    //添加
                                    _deviceRuntime.addRunTime(
                                        new Device_RunTimeModel()
                                        {
                                            deviceName = tagKeys.Substring(11),
                                            roomName =  tagKeys.Substring(7, 3),
                                            onOrOff = onOrOff,
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                    insertlog("设备", onOrOff, "试验台架" + tagKeys.Substring(7, 3)+ "试验设备" + tagKeys.Substring(11) + onOrOff);
                                }
                              
                            }
                            else
                            {
                                if (!onOrOff.Equals(devicerunlist[0].onOrOff))
                                {
                                    //添加
                                    _deviceRuntime.addRunTime(
                                        new Device_RunTimeModel()
                                        {
                                            deviceName = tagKeys.Substring(11),
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = onOrOff,
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                    insertlog("设备", onOrOff, "试验台架" + tagKeys.Substring(7, 3) + "试验设备" + tagKeys.Substring(11) + onOrOff);
                                }
                            }
                            //停机
                            
                            List<Device_StopTimeModel> devicestoplist = _deviceStoptime.GetDevice_StopTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            
                            if (devicestoplist.Count == 0)
                            {
                                if (onOrOff.Equals("off"))
                                {
                                    //添加
                                    _deviceStoptime.addDevice_StopTime(
                                        new Device_StopTimeModel()
                                        {
                                            deviceName = tagKeys.Substring(11),
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = onOrOff,
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                    insertlog("设备", onOrOff, "试验台架" + tagKeys.Substring(7, 3) + "试验设备" + tagKeys.Substring(11) + onOrOff);
                                }
                                
                            }
                            else
                            {
                                if (!onOrOff.Equals(devicestoplist[0].onOrOff))
                                {
                                    //添加
                                    _deviceStoptime.addDevice_StopTime(
                                        new Device_StopTimeModel()
                                        {
                                            deviceName = tagKeys.Substring(11),
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = onOrOff,
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                    
                                }
                            }
                            //时间累加
                            var runlist = _deviceRuntime.GetOneDevice_RunTimeList(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            var stoplist = _deviceStoptime.GetOneDevice_StopTimeList(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            var weibaolist = _deviceweibaoTime.GetOneDevice_weibaoTimeList("试验台架" + tagKeys.Substring(7, 3) + tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            var holidaylist = _deviceholidayTime.GetOneDevice_holidayTimeList("试验台架" + tagKeys.Substring(7, 3) + tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            var trainninglist = _devicetrainningTime.GetOneDevice_TrainingTimeList("试验台架"+tagKeys.Substring(7, 3) + tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            var faultdowmrunlist = _deviceFaultDowntime.GetOneDevice_FaultDownTimeList("试验台架" + tagKeys.Substring(7, 3) + tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            TimeSpan Runtimetricks = TimeSpan.Zero;
                              TimeSpan Stoptimetricks = TimeSpan.Zero; 
                           
                            TimeSpan faulttricks = TimeSpan.Zero;
                            double freetime = 0;
                            double weibaotime = 0;
                            double holidaytime = 0;
                            double trainningtime = 0;
                            if (onOrOff.Equals("off"))
                            {//运行累加
                                if (runlist.Count % 2 == 0 && runlist.Count != 0)
                                {
                                    for (int i = 0; i < runlist.Count; i += 2)
                                    {
                                        Runtimetricks += TimeSpan.FromTicks(runlist[i].alarmTime.Ticks - runlist[i + 1].alarmTime.Ticks);
                                    }

                                    _deviceStatus.updateDevicetotalRunTime(

                                       tagKeys.Substring(11), roomid, Runtimetricks.TotalSeconds
                                    );
                                }
                            }
                            else
                            {//停机累加
                                if (stoplist.Count % 2 == 0 && stoplist.Count != 0)
                                {
                                    for (int i = 0; i < stoplist.Count; i += 2)
                                    {
                                        Stoptimetricks += TimeSpan.FromTicks(stoplist[i].alarmTime.Ticks - stoplist[i + 1].alarmTime.Ticks);
                                    }

                                    _deviceStatus.updateDevicetotalStopTime(

                                       tagKeys.Substring(11), roomid, Stoptimetricks.TotalSeconds
                                    );
                                    //闲置时间 停机-故障-节假-维保-培训
                                    if (faultdowmrunlist.Count % 2 == 0 && faultdowmrunlist.Count != 0)
                                    {
                                        for (int i = 0; i < faultdowmrunlist.Count; i += 2)
                                        {
                                            faulttricks += TimeSpan.FromTicks(faultdowmrunlist[i].alarmTime.Ticks - faultdowmrunlist[i + 1].alarmTime.Ticks);
                                        }
                                    }
                                    else
                                    {
                                        if(faultdowmrunlist.Count!=0)
                                        faultdowmrunlist.RemoveAt(0);
                                        for (int i = 0; i < faultdowmrunlist.Count-1; i += 2)
                                        {
                                            faulttricks += TimeSpan.FromTicks(faultdowmrunlist[i].alarmTime.Ticks - faultdowmrunlist[i + 1].alarmTime.Ticks);
                                        }
                                    }
                                    weibaolist.ForEach(t=>{ weibaotime += t.alarmTime; });
                                    holidaylist.ForEach(t => { holidaytime += t.alarmTime; });
                                    trainninglist.ForEach(t => { trainningtime += t.alarmTime; });
                                    freetime = Stoptimetricks.TotalSeconds - (faulttricks.TotalSeconds + weibaotime*60*60 + holidaytime*60*60 + trainningtime*60*60);
                                    _deviceStatus.updateDevicetotalFreeTime(tagKeys.Substring(11), roomid, freetime);
                                }
                            }
                           
                        }
#endregion

                        #region 组合状态
                        var roomPointList=   AllPoint.Where(t => t.Address.Contains(tagKeys.Substring(7, 3))).ToList();
            
                        var occupy = roomPointList.FirstOrDefault(t => t.Address.Contains("试验占用"));
                        var fault = roomPointList.FirstOrDefault(t => t.Address.Contains("故障"));
                        var stopStatus = roomPointList.FirstOrDefault(t => t.Address.Contains("停机状态指示灯"));


                        var occupyValue = occupy == null ? (bool?)null : _opcTagValueCharge1.Read(occupy.TagName+ occupy.Address).ToBool();
                        var faultValue = fault == null ? (bool?)null : _opcTagValueCharge1.Read(fault.TagName+ fault.Address).ToBool();
                        var stopStatusValue = stopStatus == null ? (bool?)null : _opcTagValueCharge1.Read(stopStatus.TagName+ stopStatus.Address).ToBool();

                     
                        if ((occupyValue == true && faultValue == true && stopStatusValue == true)
                            || (occupyValue == true && faultValue == true && stopStatusValue == false)
                            || (occupyValue == false && faultValue == true && stopStatusValue == true)
                            || (occupyValue == false && faultValue == true && stopStatusValue == false)
                            )
                        {
                           
                            _droomTestRoomStatus.updateTestRoomOpertionStatus(tagKeys.Substring(7, 3), "故障中——设备故障停机中");
                           
                            
                         
                          
                            //时间添加
                            List<DRoom_StandByTimeModel> standbylist = _droomStandByTime.GetDRoom_StandByTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            List<DRoom_TestSTopTimeModel> testStoplist = _droomTestStopTime.GetDRoom_TestStopTimedesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            List<DRoom_EffectiveRunningTimeModel> effectiveRunninglist = _droomEffectiveRunningTime.GetDRoom_EffectiveRunningTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            List<DRoom_DeviceDebugRunTimeModel> deviceDebugRunlist = _droomDeviceDebugRunTime.GetDRoom_DeviceDebugRunTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            if (testStoplist.Count > 0 && !testStoplist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomTestStopTime.addTestStopTime(
                                    new DRoom_TestSTopTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            if (effectiveRunninglist.Count > 0 && !effectiveRunninglist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomEffectiveRunningTime.addEffectiveRunningTime(
                                    new DRoom_EffectiveRunningTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            if (standbylist.Count > 0 && !standbylist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomStandByTime.addStandByTime(
                                    new DRoom_StandByTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                           
                            if (deviceDebugRunlist.Count > 0 && !deviceDebugRunlist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomDeviceDebugRunTime.addDeviceDebugRunTime(
                                    new DRoom_DeviceDebugRunTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            //时间累加
                            taiJiaTotalTime(tagKeys);

                        }
                        //实验中
                        if (occupyValue == true && faultValue == false && stopStatusValue == true)
                        {
                           
                            _droomTestRoomStatus.updateTestRoomOpertionStatus(tagKeys.Substring(7, 3), "任务中——试验中");
                            


                            //时间添加
                            List<DRoom_EffectiveRunningTimeModel> list = _droomEffectiveRunningTime.GetDRoom_EffectiveRunningTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                           
                            if (list.Count == 0)
                            {
                               
                                    //添加
                                    _droomEffectiveRunningTime.addEffectiveRunningTime(
                                        new DRoom_EffectiveRunningTimeModel()
                                        {
                                            deviceName = "all",
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = "on",
                                            alarmTime = DateTime.Now
                                        }
                                        );


                              
                            }
                            else
                            {
                                if (!"on".Equals(list[0].onOrOff))
                                {
                                    //添加
                                    _droomEffectiveRunningTime.addEffectiveRunningTime(
                                        new DRoom_EffectiveRunningTimeModel()
                                        {
                                            deviceName = "all",
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = "on",
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                }
                               
                            }
                            List<DRoom_StandByTimeModel> standbylist = _droomStandByTime.GetDRoom_StandByTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            List<DRoom_TestSTopTimeModel> testStoplist = _droomTestStopTime.GetDRoom_TestStopTimedesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            if (testStoplist.Count > 0 && !testStoplist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomTestStopTime.addTestStopTime(
                                    new DRoom_TestSTopTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                           
                            if (standbylist.Count > 0 && !standbylist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomStandByTime.addStandByTime(
                                    new DRoom_StandByTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            List<DRoom_DeviceDebugRunTimeModel> deviceDebugRunlist = _droomDeviceDebugRunTime.GetDRoom_DeviceDebugRunTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            if (deviceDebugRunlist.Count > 0 && !deviceDebugRunlist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomDeviceDebugRunTime.addDeviceDebugRunTime(
                                    new DRoom_DeviceDebugRunTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            //时间累加

                            taiJiaTotalTime(tagKeys);
                        }

                        //试验暂停
                        if (occupyValue == true && faultValue == false && stopStatusValue == false)
                        {
                           
                            _droomTestRoomStatus.updateTestRoomOpertionStatus(tagKeys.Substring(7, 3), "任务中——试验暂停中");
                            //时间添加
                            List<DRoom_TestSTopTimeModel> list = _droomTestStopTime.GetDRoom_TestStopTimedesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            
                            if (list.Count == 0)
                            {

                                //添加
                                _droomTestStopTime.addTestStopTime(
                                    new DRoom_TestSTopTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "on",
                                        alarmTime = DateTime.Now
                                    }
                                    );


                              
                            }
                            else
                            {
                                if (!"on".Equals(list[0].onOrOff))
                                {
                                    //添加
                                    _droomTestStopTime.addTestStopTime(
                                   new DRoom_TestSTopTimeModel()
                                        {
                                       deviceName = "all",
                                            roomName = tagKeys.Substring(7, 3),
                                            onOrOff = "on",
                                            alarmTime = DateTime.Now
                                        }
                                        );
                                }
                              
                            }
                            List<DRoom_StandByTimeModel> standbylist = _droomStandByTime.GetDRoom_StandByTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            List<DRoom_EffectiveRunningTimeModel> effectiveRunninglist = _droomEffectiveRunningTime.GetDRoom_EffectiveRunningTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                          
                            if (effectiveRunninglist.Count > 0 && !effectiveRunninglist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomEffectiveRunningTime.addEffectiveRunningTime(
                                    new DRoom_EffectiveRunningTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            if (standbylist.Count > 0 && !standbylist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomStandByTime.addStandByTime(
                                    new DRoom_StandByTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            List<DRoom_DeviceDebugRunTimeModel> deviceDebugRunlist = _droomDeviceDebugRunTime.GetDRoom_DeviceDebugRunTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            if (deviceDebugRunlist.Count > 0 && !deviceDebugRunlist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomDeviceDebugRunTime.addDeviceDebugRunTime(
                                    new DRoom_DeviceDebugRunTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }

                            //时间累加

                            taiJiaTotalTime(tagKeys);

                        }
                         //状态预留
                        if (occupyValue == false && faultValue == false && stopStatusValue== true)
                        {

                           
                            _droomTestRoomStatus.updateTestRoomOpertionStatus(tagKeys.Substring(7, 3), "非任务中——设备调试运行");
                            //时间添加
                            List<DRoom_DeviceDebugRunTimeModel> list = _droomDeviceDebugRunTime.GetDRoom_DeviceDebugRunTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                          
                            if (list.Count == 0)
                            {

                                //添加
                                _droomDeviceDebugRunTime.addDeviceDebugRunTime(
                                    new DRoom_DeviceDebugRunTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "on",
                                        alarmTime = DateTime.Now
                                    }
                                    );


                              
                            }
                            else
                            {
                                if (!"on".Equals(list[0].onOrOff))
                                {
                                    //添加
                                    _droomDeviceDebugRunTime.addDeviceDebugRunTime(
                                   new DRoom_DeviceDebugRunTimeModel()
                                   {
                                       deviceName = "all",
                                       roomName = tagKeys.Substring(7, 3),
                                       onOrOff = "on",
                                       alarmTime = DateTime.Now
                                   }
                                        );
                                }
                            
                            }
                           
                            //时间添加
                            List<DRoom_StandByTimeModel> standbylist = _droomStandByTime.GetDRoom_StandByTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            List<DRoom_TestSTopTimeModel> testStoplist = _droomTestStopTime.GetDRoom_TestStopTimedesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            List<DRoom_EffectiveRunningTimeModel> effectiveRunninglist = _droomEffectiveRunningTime.GetDRoom_EffectiveRunningTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            if (testStoplist.Count > 0 && !testStoplist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomTestStopTime.addTestStopTime(
                                    new DRoom_TestSTopTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            if (effectiveRunninglist.Count > 0 && !effectiveRunninglist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomEffectiveRunningTime.addEffectiveRunningTime(
                                    new DRoom_EffectiveRunningTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            if (standbylist.Count > 0 && !standbylist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomStandByTime.addStandByTime(
                                    new DRoom_StandByTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            //时间累加
                        
                            taiJiaTotalTime(tagKeys);
                        }
                        //台架待机
                        if (occupyValue == false && faultValue == false && stopStatusValue == false)
                        {
                           
                            _droomTestRoomStatus.updateTestRoomOpertionStatus(tagKeys.Substring(7, 3), "等待中——设备待机中");
                            //     _deviceStatus.updateStatus(new Device_RoomModel { equipmentOperationStatus = "状态预留", equipmentTestStatus = "状态预留", roomName = tagKeys.Substring(7, 3) });
                         
                            //时间添加
                            List<DRoom_StandByTimeModel> standbylist = _droomStandByTime.GetDRoom_StandByTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            List<DRoom_TestSTopTimeModel> testStoplist = _droomTestStopTime.GetDRoom_TestStopTimedesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            List<DRoom_EffectiveRunningTimeModel> effectiveRunninglist = _droomEffectiveRunningTime.GetDRoom_EffectiveRunningTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));

                            if (standbylist.Count == 0)
                            {

                                //添加
                                _droomStandByTime.addStandByTime(
                                    new DRoom_StandByTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "on",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                             

                            }
                            else
                            {
                                if (!"on".Equals(standbylist[0].onOrOff))
                                {
                                    //添加
                                    _droomStandByTime.addStandByTime(
                                    new DRoom_StandByTimeModel()
                                    {
                                       deviceName = "all",
                                       roomName = tagKeys.Substring(7, 3),
                                       onOrOff = "on",
                                       alarmTime = DateTime.Now
                                   }
                                  );
                                }
                               
                            }
                            if (testStoplist.Count > 0 && !testStoplist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomTestStopTime.addTestStopTime(
                                    new DRoom_TestSTopTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            if (effectiveRunninglist.Count > 0 && !effectiveRunninglist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomEffectiveRunningTime.addEffectiveRunningTime(
                                    new DRoom_EffectiveRunningTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            List<DRoom_DeviceDebugRunTimeModel> deviceDebugRunlist = _droomDeviceDebugRunTime.GetDRoom_DeviceDebugRunTimesdesc(tagKeys.Substring(11), tagKeys.Substring(7, 3));
                            if (deviceDebugRunlist.Count > 0 && !deviceDebugRunlist[0].onOrOff.Equals("off"))
                            {
                                //添加
                                _droomDeviceDebugRunTime.addDeviceDebugRunTime(
                                    new DRoom_DeviceDebugRunTimeModel()
                                    {
                                        deviceName = "all",
                                        roomName = tagKeys.Substring(7, 3),
                                        onOrOff = "off",
                                        alarmTime = DateTime.Now
                                    }
                                    );

                            }
                            //时间累加
                            taiJiaTotalTime(tagKeys);
                        }
                        #endregion
                        #region 接口组态
              
                       
                        if (faultValue==true)
                        {
                            RootStatus = "USCD_01";
                            //故障中
                          var apistatus=  _droomapistatus.GetDRoom_ApiStatusOne(tagKeys.Substring(7, 3));
                            if (apistatus != null)
                            {
                                if (apistatus.status == null) { apistatus.status = ""; }
                                if (!apistatus.status.Equals("故障中"))
                                {
                                    var deviceApiNuberlist = _deviceApiNuber.getOneApiNumber(tagKeys.Substring(7, 3));
                                    if (deviceApiNuberlist.Count > 0) { sendApi(RootStatus, deviceApiNuberlist[0].deviceNumber); }
                                    _droomapistatus.updateApiStatusOne(tagKeys.Substring(7, 3), "故障中");
                                }
                            }
                            
                        }
                        if (occupyValue==true&&faultValue==false)
                        {
                            //生产运行
                            RootStatus = "PROD_01";
                            var apistatus = _droomapistatus.GetDRoom_ApiStatusOne(tagKeys.Substring(7, 3));
                            if (apistatus != null)
                            {
                                if (apistatus.status == null) { apistatus.status = ""; }
                                if (!apistatus.status.Equals("生产执行"))
                                {
                                    var deviceApiNuberlist = _deviceApiNuber.getOneApiNumber(tagKeys.Substring(7, 3));
                                    if (deviceApiNuberlist.Count > 0) { sendApi(RootStatus, deviceApiNuberlist[0].deviceNumber); }
                                    _droomapistatus.updateApiStatusOne(tagKeys.Substring(7, 3), "生产执行");
                                }
                            }
                        }
                        if (occupyValue == false && stopStatusValue == true && faultValue == false)
                        {
                            //生产调试
                            RootStatus = "MAIN_02";
                            var apistatus = _droomapistatus.GetDRoom_ApiStatusOne(tagKeys.Substring(7, 3));
                            if (apistatus != null)
                            {
                                if (apistatus.status == null) { apistatus.status = ""; }
                                if (!apistatus.status.Equals("生产调试"))
                                {
                                    var deviceApiNuberlist = _deviceApiNuber.getOneApiNumber(tagKeys.Substring(7, 3));
                                    if (deviceApiNuberlist.Count > 0) { sendApi(RootStatus, deviceApiNuberlist[0].deviceNumber); }
                                    _droomapistatus.updateApiStatusOne(tagKeys.Substring(7, 3), "生产调试");
                                }
                            }
                        }
                        if (occupyValue == false && stopStatusValue == false && faultValue == false)
                        {
                            //生产完工
                            RootStatus = "STAN_01";
                            var apistatus = _droomapistatus.GetDRoom_ApiStatusOne(tagKeys.Substring(7, 3));
                            if (apistatus != null)
                            {
                                if (apistatus.status == null) { apistatus.status = ""; }
                                if (!apistatus.status.Equals("生产完工"))
                                {
                                    var deviceApiNuberlist = _deviceApiNuber.getOneApiNumber(tagKeys.Substring(7, 3));
                                    if (deviceApiNuberlist.Count > 0) { sendApi(RootStatus, deviceApiNuberlist[0].deviceNumber); }
                                    _droomapistatus.updateApiStatusOne(tagKeys.Substring(7, 3), "生产完工");
                                }
                            }
                        }
                        #endregion
                    }

                }
            }
        }

        


        #region 窗体控件

        private void toolbtnExit_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            Application.Exit();
        }

      

        private void toolbtnPoint_Click(object sender, EventArgs e)
        {
            FrmPointInfo frm = new FrmPointInfo();
            frm.ShowDialog();
        }
        private void toolbtnShutdown_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否关闭当前计算机！", "关机确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                using (var process = new Process())
                {
                    var startInfo = new ProcessStartInfo();
                    startInfo.FileName = "shutdown ";
                    startInfo.Arguments = " -s -t 0 -f";
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardInput = true;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.CreateNoWindow = true;
                    process.StartInfo = startInfo;
                    startInfo.Verb = "RunAs";
                    process.Start();
                }
            }
        }


        List<formulaOPCInfo> formulaopcinfo = null;

       
        private void cmbformula_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            //RecoveryOpc();
        }

        /// <summary>
        /// opc点位恢复 
        /// </summary>
        public void RecoveryOpc()
        {
            if (formulaopcinfo != null)
            {
                foreach (var item in formulaopcinfo)
                {
                    _opcFunc.OpcTagWriteValue(item.TagName, 0.ToDouble(), _opcTagValueCharge1);
                }
                formulaopcinfo = null;
            }
        }

        #endregion

      

      

     


       
        /// <summary>
        /// 将byte[]转换成int类型
        /// </summary>
        /// <param name="b">需转换的字节数组</param>
        /// <returns>转换成的整型数字</returns>
        public static int byteToInt(byte[] b)//将字节数组转换成int类型
        {

            int mask = 0xff;
            int temp = 0;
            int n = 0;
            for (int i = 0; i < b.Length; i++)
            {
                n <<= 8;
                temp = b[i] & mask;
                n |= temp;
            }
            return n;
        }

        /// <summary>
        /// string转16进制字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public byte[] stringTo16Bytes(string s)
        {
            return Enumerable.Range(0, s.Length)
                 .Where(x => x % 2 == 0)
                 .Select(x => Convert.ToByte(s.Substring(x, 2), 16))
                 .ToArray();
        }

       
      

    
        /// <summary>
        /// 追加写入txt数据
        /// </summary>
        /// <param name="FilePath">txt完整路径</param>
        /// <param name="str">要写入的内容</param>
        /// <remarks></remarks>
        public void WriteTXT(string str)
        {
            try
            {
                byte[] s = System.Text.Encoding.Default.GetBytes(str.ToString());
                FileStream fs = new FileStream("D:\\log.txt", FileMode.Append, FileAccess.Write);
                fs.Write(s, 0, s.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
                //WriteLog("WriteTXT", ex.ToString());
            }
        }

        private  void Main_Load_1(object sender, EventArgs e)
        {


            

            Load1();

          

        }

        public void taiJiaTotalTime(string tagKeys)
        {
            var totalstandbylist = _droomStandByTime.GetOneDRoom_StandByTimeList(tagKeys.Substring(7, 3));
            var totalTestStoplist = _droomTestStopTime.GetOneDRoom_TestSTopTimeList(tagKeys.Substring(7, 3));
            var totalEffectiveRunninglist = _droomEffectiveRunningTime.GetOneDRoom_EffectiveRunningTimeList(tagKeys.Substring(7, 3));
            var totalfaultlist = _droomFaulttime.GetOneDRoom_FaultTimeList(tagKeys.Substring(7, 3));
            var totalDeviceDebugRunlist = _droomDeviceDebugRunTime.GetOneDRoom_DeviceDebugRunTimeList(tagKeys.Substring(7, 3));
            TimeSpan totalstandby = TimeSpan.Zero;
            TimeSpan totalTestStop = TimeSpan.Zero;
            TimeSpan totalEffectiveRunning = TimeSpan.Zero;
            TimeSpan totalfaulttime = TimeSpan.Zero;
            TimeSpan totalDeviceDebugTime = TimeSpan.Zero;
            //待机时间
            if (totalstandbylist.Count % 2 == 0 && totalstandbylist.Count != 0)
            {
                for (int i = 0; i < totalstandbylist.Count; i += 2)
                {
                    totalstandby += TimeSpan.FromTicks(totalstandbylist[i].alarmTime.Ticks - totalstandbylist[i + 1].alarmTime.Ticks);
                }

                _droomTestRoomStatus.updateDRoomtotalStandbyTime(

                  tagKeys.Substring(7, 3), totalstandby.TotalSeconds
                );
            }
            else
            {
                if(totalstandbylist.Count!=0)
                totalstandbylist.RemoveAt(0);
                for (int i = 0; i < totalstandbylist.Count; i += 2)
                {
                    totalstandby += TimeSpan.FromTicks(totalstandbylist[i].alarmTime.Ticks - totalstandbylist[i + 1].alarmTime.Ticks);
                }
                _droomTestRoomStatus.updateDRoomtotalStandbyTime(

                tagKeys.Substring(7, 3), totalstandby.TotalSeconds
              );
            }
            //试验暂停时间
            if (totalTestStoplist.Count % 2 == 0 && totalTestStoplist.Count != 0)
            {
                for (int i = 0; i < totalTestStoplist.Count; i += 2)
                {
                    totalTestStop += TimeSpan.FromTicks(totalTestStoplist[i].alarmTime.Ticks - totalTestStoplist[i + 1].alarmTime.Ticks);
                }

                _droomTestRoomStatus.updateDRoomtotalTestStopTime(

                  tagKeys.Substring(7, 3), totalTestStop.TotalSeconds
                );
            }
            else
            {   if(totalTestStoplist.Count!=0)
                totalTestStoplist.RemoveAt(0);
                for (int i = 0; i < totalTestStoplist.Count - 1; i += 2)
                {
                    totalTestStop += TimeSpan.FromTicks(totalTestStoplist[i].alarmTime.Ticks - totalTestStoplist[i + 1].alarmTime.Ticks);
                }
                _droomTestRoomStatus.updateDRoomtotalTestStopTime(

                 tagKeys.Substring(7, 3), totalTestStop.TotalSeconds
               );
            }
            //故障时间
            if (totalfaultlist.Count % 2 == 0 && totalfaultlist.Count != 0)
            {
                for (int i = 0; i < totalfaultlist.Count; i += 2)
                {
                    totalfaulttime += TimeSpan.FromTicks(totalfaultlist[i].alarmTime.Ticks - totalfaultlist[i + 1].alarmTime.Ticks);
                }

                _droomTestRoomStatus.updateDRoomtotalFaultTime(

                  tagKeys.Substring(7, 3), totalfaulttime.TotalSeconds
                );
            }
            else
            {
                if (totalfaultlist.Count != 0)
                    totalfaultlist.RemoveAt(0);
                for (int i = 0; i < totalfaultlist.Count - 1; i += 2)
                {
                    totalfaulttime += TimeSpan.FromTicks(totalfaultlist[i].alarmTime.Ticks - totalfaultlist[i + 1].alarmTime.Ticks);
                }
                _droomTestRoomStatus.updateDRoomtotalFaultTime(

                 tagKeys.Substring(7, 3), totalfaulttime.TotalSeconds
               );
            }
            //试验中时间 有效运行时间
            if (totalEffectiveRunninglist.Count % 2 == 0 && totalEffectiveRunninglist.Count != 0)
            {
                for (int i = 0; i < totalEffectiveRunninglist.Count; i += 2)
                {
                    totalEffectiveRunning += TimeSpan.FromTicks(totalEffectiveRunninglist[i].alarmTime.Ticks - totalEffectiveRunninglist[i + 1].alarmTime.Ticks);
                }

                _droomTestRoomStatus.updateDRoomtotalEffectiveRunningTime(
                  tagKeys.Substring(7, 3), totalEffectiveRunning.TotalSeconds
                );
            }
            else
            {
                if(totalEffectiveRunninglist.Count!=0)
                totalEffectiveRunninglist.RemoveAt(0);
                for (int i = 0; i < totalEffectiveRunninglist.Count - 1; i += 2)
                {
                    totalEffectiveRunning += TimeSpan.FromTicks(totalEffectiveRunninglist[i].alarmTime.Ticks - totalEffectiveRunninglist[i + 1].alarmTime.Ticks);
                }
                _droomTestRoomStatus.updateDRoomtotalEffectiveRunningTime(
                tagKeys.Substring(7, 3), totalEffectiveRunning.TotalSeconds
              );
            }
            //非任务中调试时间 
            if (totalDeviceDebugRunlist.Count % 2 == 0 && totalDeviceDebugRunlist.Count != 0)
            {
                for (int i = 0; i < totalDeviceDebugRunlist.Count; i += 2)
                {
                    totalDeviceDebugTime += TimeSpan.FromTicks(totalDeviceDebugRunlist[i].alarmTime.Ticks - totalDeviceDebugRunlist[i + 1].alarmTime.Ticks);
                }

                _droomTestRoomStatus.updateDRoomtotaldeviceDebugRunTime(
                  tagKeys.Substring(7, 3), totalDeviceDebugTime.TotalSeconds
                );
            }
            else
            {
                if (totalDeviceDebugRunlist.Count != 0)
                    totalDeviceDebugRunlist.RemoveAt(0);
                for (int i = 0; i < totalDeviceDebugRunlist.Count - 1; i += 2)
                {
                    totalDeviceDebugTime += TimeSpan.FromTicks(totalDeviceDebugRunlist[i].alarmTime.Ticks - totalDeviceDebugRunlist[i + 1].alarmTime.Ticks);
                }
                _droomTestRoomStatus.updateDRoomtotaldeviceDebugRunTime(
                tagKeys.Substring(7, 3), totalDeviceDebugTime.TotalSeconds
              );
            }
            ////稼动率
            //var totalzhiduTime = (totalEffectiveRunning + totalTestStop).TotalSeconds / _droomzhiduTime.GetDRoom_ZhiduTimeOne(tagKeys.Substring(7, 3))[0].alarmTime*60*60;
            //_droomTestRoomStatus.updateDRoomtotalUtilizationRate(
            //   tagKeys.Substring(7, 3), totalzhiduTime
            // );
            //闲置时间
            double holidaytime = 0;
            double weibaotime = 0;
            double jiaozhuntime = 0;
            var holidaytimelist = _droomholidyTime.GetOneDRoom_holidayTimeList(tagKeys.Substring(7, 3));
            var weibaotimelist = _droomweibaoTime.GetOneDRoom_weibaoTimeList(tagKeys.Substring(7, 3));
            var jiaozhuntimelist = _droomjiaozhunTime.GetOneDRoom_jiaozhunTimeList(tagKeys.Substring(7, 3));
            holidaytimelist.ForEach(t => holidaytime += t.alarmTime);
            weibaotimelist.ForEach(t => weibaotime += t.alarmTime);
            jiaozhuntimelist.ForEach(t => jiaozhuntime += t.alarmTime);
            var freetime = totalstandby.TotalSeconds -(holidaytime*60*60 + weibaotime*60*60 + jiaozhuntime*60*60);
           // _droomTestRoomStatus.updateDRoomtotalweibaoTime(tagKeys.Substring(7, 3), weibaotime*60*60);
            if (!double.IsNaN(freetime))
            _droomTestRoomStatus.updateDRoomtotalFreeTime(tagKeys.Substring(7, 3), freetime);
        }
        public  void insertlog(string type,string executeResult,string desc)
        {
            var logger = Log.ForContext<CustomLogEvent>();

            var customLog = new CustomLogEvent
            {
                Account = "admin",
                Type = type,
                Result = 1,
                ExecuteResult =executeResult,
                Desc = desc,
                Ip = NetworkHelper.GetLocalIP(),
                Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            logger.Information("This is a custom log message {@CustomLogEvent}", customLog);
        }
        public void sendApi(string RootStatus, string machineNumber)
        {
            lock (_objapiLock) {
                #region 请求API接口
                string Url = ConfigurationManager.AppSettings["apiurl"];//请求路径
                RootData rootData = new RootData()
                {
                    LINE_ID = "2023C07002",//(Y)
                    OPER = "",//(N)
                    MACHINE_ID = machineNumber,//(Y)
                    EVENT_ID = RootStatus,//(Y)
                    COMMENT = "1"//(N)
                };
                RootRequest requestModel = new RootRequest()
                {
                    INTERFACE_ID = "OPC_TO_MES_SEND_EQUIP_STATUS",//(Y)
                    FACTORY = "1010",//(Y)
                    USER = "2023C07MES",//(Y)
                    PASSWORD = "Qcmes.2021",//(Y)
                    TRAN_USER_ID = "2023111",//(Y)
                    TRAN_TIME = DateTime.Now.ToString("yyyyMMddHHmmss"),//(Y)
                    DATA = rootData//(Y)
                };
                string Json = RequestHelper.ObjectToJSON(requestModel);
                var ResponseJSON = RequestHelper.HttpServerRequstJson(Url, Json, "Application/json", 300);
                // FileResponse ResModel = RequestHelper.JsonToObject<FileResponse>(ResponseJSON);
                FileResponse ResModel = new FileResponse() { CODE = ResponseJSON };
                if (ResModel != null)
                {
                    if (ResModel.CODE.Contains("OK"))
                    {
                        //请求成功时要做的事情
                        insertlog("接口", "请求成功", "设备编码：" + machineNumber + "，状态：" + RootStatus + "请求成功");
                    }
                    else
                    {
                        //请求失败时要做的事情，写日志？
                        insertlog("接口", "请求失败", "设备编码：" + machineNumber + "，状态：" + RootStatus + "请求失败");
                    }
                }
                else
                {
                    //请求失败，写日志？

                }
                #endregion
            }
        }
    }
}

