using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.IBLL.Programs;
using RW.PMS.Model.Assembly;
using RW.PMS.Model.Sys;
using RW.PMS.Utils.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace RW.PMS.WinForm.Module
{
    /// <summary>
    /// 自定义OPC控件
    /// </summary>
    public partial class OPCTagValueCharge : BaseModule
    {
        #region 变量
        /// <summary>
        /// 当前键值对
        /// </summary>
        private Dictionary<string, object> dicCurKeyValue = new Dictionary<string, object>();

        /// <summary>
        /// 当前IP所有OPC点位
        /// </summary>
        public List<GwGJWLOPCPointModel> OPCPointList = new List<GwGJWLOPCPointModel>();

        /// <summary>
        /// 当前压机所有OPC点位
        /// </summary>
        public List<BaseDataModel> PressureList = new List<BaseDataModel>();


        #region 初始化成功标识
        private bool initSuccessFlag = true;
        /// <summary>
        /// 初始化成功标识
        /// </summary>
        public bool InitSuccessFlag { get { return initSuccessFlag; } }
        #endregion

        #endregion
      public  bool _IsMonitor = false;
        #region 构造函数
        /// <summary>
        /// 自定义OPC控件 构造函数
        /// </summary>
        public OPCTagValueCharge()
        {
            //TDOD:
            //启动OPC驱动
            this.Driver = Var.opcDriver;

        }
        #endregion

        public List<GwGJWLOPCPointModel> dtallTags = new List<GwGJWLOPCPointModel>();

        #region 初始化函数
        public override void Init()
        {
            try
            {
                dtallTags = DIService.GetService<IBLL_Assembly>().GetAllOPCTag(SysConfig.LocalIP, true);

           
                var OPCPointList2 = DIService.GetService<IBLL_PointInfo>().GetPointInfo(null, 0);

                //初始化异常opc点位
                foreach (var item in OPCPointList2)
                {
                    OPCPointList.Add(new GwGJWLOPCPointModel { OPCDeviceName =item.TagName, OPCTypeCode = "Error", OPCValue = item.Address, Remark = "" });
                    
                }
               
              
                #endregion

                //循环OPC点位集合
                foreach (var opc in OPCPointList)
                {
                    if (!string.IsNullOrEmpty(opc.OPCValue))
                    {
                        string key = opc.Value + "";

                        this.Register<object>(key, delegate (object value)
                        {
                            if (this.dicCurKeyValue.ContainsKey(key) && this.dicCurKeyValue[key] == value) return;
                            if (!this.dicCurKeyValue.ContainsKey(key))
                            {
                                this.dicCurKeyValue.Add(key, value);
                            }
                            else
                            {
                                this.dicCurKeyValue[key] = value;
                            }
                            if (NameValueChanged != null)
                                NameValueChanged(this, key, value);
                        });
                    }
                }

                



                base.Init();
              
                _IsMonitor = true;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                initSuccessFlag = false;
            }
        }
 

        #region 连接状态
        /// <summary>
        /// 获取PLC状态,判断是否通讯正常
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool PLCstatus(string key)
        {
            return Var.isGood(key);
        }
        #endregion

        #region 值变事件
        /// <summary>
        /// 指定的点位的值发生变化触发的事件
        /// </summary>
        public event NameValueHandler<object> NameValueChanged;
        #endregion
    }

    public delegate void NameValueHandler<T>(object sender, string name, T value);
}
