using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;

namespace RW.PMS.Utils.Modules
{
    [Obsolete("��ʹ��BaseSensor�࣬�����Ѿ�����")]
    public class BaseReadModule : BaseModule, IComponent, IHardware
    {
        public BaseReadModule() { }

        public BaseReadModule(IContainer container) : base(container) { }

        public event EventHandler ChangedError;

        public virtual void OnValueChanged(double value) { if (ValueChanged != null) ValueChanged(this, value); }
        public virtual void OnGainValueChanged(double value) { if (GainValueChanged != null) GainValueChanged(this, value); }
        public virtual void OnZeroValueChanged(double value) { if (ZeroValueChanged != null) ZeroValueChanged(this, value); }
        public virtual void OnChangedError() { if (ChangedError != null) ChangedError(this, EventArgs.Empty); }

        public override void Init()
        {
            this.Register<double>(ReadKey, delegate(double value)
           {
               this.Value = value;
               OnValueChanged(value);
           });
            this.Register<double>(GainKey, delegate(double value)
            {
                gainValue = value;
                OnGainValueChanged(value);
            });
            this.Register<double>(ZeroKey, delegate(double value)
            {
                zeroValue = value;
                OnZeroValueChanged(value);
            });
            base.Init();
        }

        private string readKey;
        [Description("ָ����ȡ��OPC���������")]
        [DefaultValue(null)]
        public virtual string ReadKey
        {
            get { return readKey; }
            set { readKey = value; }
        }

        private string gainKey;
        /// <summary>
        /// ����ֵ
        /// </summary>
        [Description("��ȡ��д��OPC������ֵ��")]
        [DefaultValue(null)]
        public virtual string GainKey
        {
            get { return gainKey; }
            set { gainKey = value; }
        }

        private string zeroKey;
        /// <summary>
        /// ���ֵ
        /// </summary>
        [Description("��ȡ��д��OPC�����ֵ��")]
        [DefaultValue(null)]
        public virtual string ZeroKey
        {
            get { return zeroKey; }
            set { zeroKey = value; }
        }


        #region IGain ��Ա

        private double value;
        public virtual double Value
        {
            get { return value; }
            protected set { this.value = value; }
        }

        private double gainValue = 1d;
        [Description("����ֵ��ʵ��ֵ=������ֵ*����ֵ-���ֵ")]
        [DefaultValue(1d)]
        public virtual double GainValue
        {
            get { return gainValue; }
            set { this.Write(gainKey, value); }
        }

        private double zeroValue;
        [Description("���ֵ,ʵ��ֵ=������ֵ*����ֵ-���ֵ")]
        [DefaultValue(0d)]
        public virtual double ZeroValue
        {
            get { return zeroValue; }
            set { this.Write(zeroKey, value); }
        }

        public event ValueHandler ValueChanged;
        public event ValueHandler GainValueChanged;
        public event ValueHandler ZeroValueChanged;

        #endregion
    }
}
