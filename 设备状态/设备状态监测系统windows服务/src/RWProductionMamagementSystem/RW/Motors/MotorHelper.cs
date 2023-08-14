using System;
using System.Collections.Generic;
using System.Text;

namespace RW.PMS.Utils.Motors
{
    /// <summary>
    /// �ṩ�Ե����һЩ��������
    /// </summary>
    public static class MotorHelper
    {

        /// <summary>
        /// ���ݵ���ļ������ٶȻ�ȡ����Ƶ��
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="poles"></param>
        /// <returns></returns>
        public static double GetSpeedFrequency(double speed, int poles)
        {
            double frequency = speed * poles / 60;
            return frequency;

        }

        /// <summary>
        /// ��ȡ�����ǰ���õ������ٶ�
        /// </summary>
        public static double GetMotorSpeed(int poles, double frequency)
        {
            return 60 * frequency / poles;
        }
    }
}
