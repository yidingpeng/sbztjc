using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace RW.PMS.Utils.Modules
{
    /// <summary>
    /// ��������Ӳ����ͳһ�ӿڣ�����ֵ���¼���
    /// </summary>
    public interface IHardwareGroup : IAnalogGroup
    {
        HardwareGroup Hardwares { get; }

        event ValueGroupHandler<int> IndexGroupChanged;
    }

    /// <summary>
    /// ��������ģ������֧�����������¼�
    /// </summary>
    public interface IAnalogGroup : IIndexes
    {
        ///// <summary>
        ///// ֵ����ʼ��ַ
        ///// </summary>
        //[DefaultValue(0)]
        //int ValueStart { get; set; }
        ///// <summary>
        ///// ������ʼ��ַ
        ///// </summary>
        //[DefaultValue(0)]
        //int ZeroStart { get; set; }
        ///// <summary>
        ///// �������ʼ��ַ
        ///// </summary>
        //[DefaultValue(0)]
        //int GainStart { get; set; }

        //double GetGainValue(int index);
        //double GetZeroValue(int index);

        event ValueGroupHandler ValueGroupChanged;
        event ValueGroupHandler GainValueGroupChanged;
        event ValueGroupHandler ZeroValueGroupChanged;
    }

    public interface IDigitalGroup : IIndexes<bool>
    {
        int Start { get; set; }
        int Offset { get; set; }

        event ValueGroupHandler<bool> ValueGroupChanged;
    }
}
