using System;
using System.Collections.Generic;
using System.Text;

namespace RW.PMS.Utils.Modules
{
    /// <summary>
    /// ��������Ӳ����ͳһ�ӿڣ�����ֵ���¼���
    /// </summary>
    public interface IHardwareVibrationGroup : IHardwareGroup
    {
        new VibrationHardWare[] Hardwares { get; }

        double GetSensitivityValue(int index);

        event ValueGroupHandler SensitivityGroupChanged;
    }
}
