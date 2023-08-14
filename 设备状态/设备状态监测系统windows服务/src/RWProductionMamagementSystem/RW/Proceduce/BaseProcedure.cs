using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using RW.Modules;
using RW.Proceduce.Result;

namespace RW.Proceduce
{
    public delegate bool TickHandler(object sender, int tick);
    public delegate bool DefaultHandler(int tick);
    /// <summary>
    /// ���д�����̵Ļ��࣬ͨ������Executeִ��
    /// �����Ⱥ�˳�򴥷���before��ing��end�¼� �޸���2016-10-19
    /// </summary>
    public class BaseProcedure : System.ComponentModel.Component, IProcedure
    {
        private BaseTestBed testBed1;

        public virtual BaseTestBed TestBed
        {
            get { return testBed1; }
            set { testBed1 = value; }
        }

        public BaseProcedure()
        {
        }

        public event EventHandler BeforeExecute;
        public event EventHandler Executing;
        public event EventHandler EndExecuted;
        public event TickHandler Ticked;

        private string model;
        /// <summary>
        /// ����Ʒ�ͺ�
        /// </summary>
        [DefaultValue(null)]
        [Description("����Ʒ�ͺ�")]
        public virtual string Model
        {
            get { return model; }
            set { model = value; }
        }

        /// <summary>
        /// ����һ�����һ��ʱ�䣬����ÿ�μ��ִ��һ������
        /// </summary>
        /// <param name="milliseconds">��ʱ��</param>
        /// <param name="interval">���</param>
        /// <param name="del">��Ҫί��ִ�еķ���</param>
        /// <returns></returns>
        protected bool OnTicked(double milliseconds, int interval, DefaultHandler del)
        {
            int t = 0;
            while ((t += interval) <= milliseconds && Testing && del(t))
            {
                if (Ticked != null && !Ticked(this, t))
                    return false;
                Thread.Sleep(interval);
            }
            return Testing && t <= milliseconds;
        }

        protected bool OnTicked(TimeSpan ts, int interval, DefaultHandler del)
        {
            return OnTicked(ts.TotalMilliseconds, interval, del);
        }

        protected bool OnTicked(double milliseconds)
        {
            return OnTicked(milliseconds, 1000, delegate { return true; });
        }

        protected bool OnTicked(double milliseconds, DefaultHandler del)
        {
            return OnTicked(milliseconds, 1000, del);
        }

        protected bool OnTicked(double milliseconds, int interval)
        {
            return OnTicked(milliseconds, interval, delegate { return true; });
        }

        public virtual void Execute()
        {
            StartExecute();
            Debug.WriteLine("׼����ʼ���飺" + this.GetType().Name);
            OnBeforeExecute();
            Debug.WriteLine("�����У�" + this.GetType().Name);
            OnExecuting();
            Debug.WriteLine("���������" + this.GetType().Name);
            OnEndExecute();
            StopExecute();
            Debug.WriteLine("�������");
        }

        public virtual void StartExecute()
        {
            Testing = true;
        }

        public virtual void StopExecute()
        {
            Testing = false;
        }

        private bool testing;

        public bool Testing
        {
            get { return testing; }
            set
            {
                testing = value;
                onTestingChange(value);
            }
        }

        protected virtual void OnBeforeExecute()
        {
            if (BeforeExecute != null && Testing) BeforeExecute(this, EventArgs.Empty);
        }
        protected virtual void OnExecuting()
        {
            if (Executing != null && Testing) Executing(this, EventArgs.Empty);
        }
        protected virtual void OnEndExecute()
        {
            if (EndExecuted != null && Testing) EndExecuted(this, EventArgs.Empty);
        }

        protected virtual void onTestingChange(bool value)
        {
            if (TestChanged != null) TestChanged(value);
        }

        public delegate void TestChangedHandler(bool value);
        public event TestChangedHandler TestChanged;

        public virtual BaseResult Collect()
        {
            return null;
        }


        protected virtual void AsyncTime(int ms, WaitCallback wait)
        {
            ThreadPool.QueueUserWorkItem(delegate(object state)
            {
                Thread.Sleep(ms);
                wait(state);
            });
        }
    }
}
