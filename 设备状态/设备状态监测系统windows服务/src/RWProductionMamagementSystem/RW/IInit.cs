using System;
using System.ComponentModel;

namespace RW.PMS.Utils
{
    /// <summary>
    /// �������������Զ���ʼ����ͳһ�ӿ�
    /// </summary>
    public interface IInit
    {
        bool AutoInit { get; set; }
        void Init();
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        bool IsInitialized { get; }
    }

    public class ExceptionEventArgs : EventArgs
    {
        public ExceptionEventArgs()
        {

        }

        public ExceptionEventArgs(Exception ex)
        {
            this.Exception = ex;
        }

        private Exception exception = new Exception();

        public Exception Exception
        {
            get { return exception; }
            set { exception = value; }
        }
    }
}
