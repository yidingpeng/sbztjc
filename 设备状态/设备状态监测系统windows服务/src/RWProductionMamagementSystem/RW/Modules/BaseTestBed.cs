using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace RW.PMS.Utils.Modules
{
    /// <summary>
    /// ����̨���࣬������ͣ
    /// </summary>
    public class BaseTestBed : BaseModule
    {

        public BaseTestBed() { }

        public BaseTestBed(IContainer contianer) : base(contianer) { }

        public override void Init()
        {
            this.Register<bool>(ScramKey, OnScramChanged);
            base.Init();
        }

        //[Description("��ͣʱ�������¼�")]
        //public event ValueHandler<bool> Scramed;
        [Description("��ͣ״̬�����ı�ʱ�������¼�")]
        public event ValueHandler<bool> ScramChanged;

        protected virtual void OnScramChanged(bool value)
        {
            this.scram = value;
            if (ScramChanged != null) ScramChanged(this, value);
        }

        private bool scram;
        [Description("�Ƿ�ͣ")]
        [DefaultValue(false)]
        public bool Scram { get { return scram; } protected set { scram = value; } }

        private string scramKey;
        [Description("��ͣ���õ�Key")]
        [DefaultValue(null)]
        public string ScramKey
        {
            get { return scramKey; }
            set { scramKey = value; }
        }
    }
}
