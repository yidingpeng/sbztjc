using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace RW.Modules
{
    /// <summary>
    /// ������
    /// ʹ��ʱ����������Readkey����ɻ�ȡValue��ValueChanged�¼���
    /// ������Ҫ�����������ֵ��ZeroValue��������ֵ(GainValue)
    /// </summary>
    public class Sensor : BaseReadModule
    {
        public Sensor()
        {

        }

        public Sensor(IContainer contianer)
            : base(contianer)
        {

        }
    }
}
