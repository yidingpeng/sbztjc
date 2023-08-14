using System;
using System.Collections.Generic;
using System.Text;

namespace RW.PMS.Utils.Modules
{
    /// <summary>
    /// ��������Ӳ����ͳһ�ӿڣ�����ֵ���¼���
    /// </summary>
    public interface IHardwareArray
    {
        HardwareList Hardwares { get; }
        //HardwareGroup SortedHardwares { get; }

        double GetGainValue(int index);
        double GetZeroValue(int index);

        event ValueListHandler ValuesChanged;
        event ValueListHandler GainValuesChanged;
        event ValueListHandler ZeroValuesChanged;
    }
}
