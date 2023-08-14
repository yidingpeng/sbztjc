using System;
using System.Collections.Generic;
using System.Text;

namespace RW.PMS.Utils
{
    /// <summary>
    /// ���������ɵ���Ŀؼ�
    /// </summary>
    public interface IClickable
    {
        bool CanClick { get;set; }
        bool ClickSwitch { get;set; }

        event EventHandler Click;
    }

    /// <summary>
    /// ��������֧���첽����Ŀؼ�
    /// </summary>
    public interface IAsyncClickabe : IClickable
    {
        bool AsyncClick { get;set; }
    }
}
