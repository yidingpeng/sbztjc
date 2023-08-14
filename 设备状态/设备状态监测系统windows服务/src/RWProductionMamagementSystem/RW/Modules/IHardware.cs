using System;
using System.Collections.Generic;
using System.Text;

namespace RW.PMS.Utils.Modules
{
    /// <summary>
    /// ��������Ӳ����ͳһ�ӿڣ�����ֵ���¼���
    /// </summary>
    public interface IHardware
    {
        double Value { get; }
        double GainValue { get;set;}
        double ZeroValue { get;set;}

        event ValueHandler ValueChanged;
        event ValueHandler GainValueChanged;
        event ValueHandler ZeroValueChanged;
    }
}
