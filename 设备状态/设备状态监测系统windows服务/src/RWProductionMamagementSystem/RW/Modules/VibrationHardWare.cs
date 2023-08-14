using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace RW.PMS.Utils.Modules
{
    public class VibrationHardWare : Hardware, IHardwareVibration
    {
        private double sensitivity = 1000d;

        /// <summary>
        /// ������
        /// </summary>
        [DefaultValue(1000d)]
        [Description("������")]
        public double Sensitivity
        {
            get { return sensitivity; }
            set
            {
                sensitivity = value;
                if (SensitivityChanged != null) SensitivityChanged(this, value);
                SetActual();
            }
        }

        const double g = 9.80665;

        private bool calSensitivity = true;
        [DefaultValue(true)]
        [Description("��SetLocal=trueʱ���Ǳ��Ƿ��Ѿ������ֵ")]
        public bool CalSensitivity
        {
            get { return calSensitivity; }
            set { calSensitivity = value; }
        }


        protected override void SetActual()
        {
            if (SetLocal)
            {
                double v = 0;
                if (CalSensitivity)
                    v = ValueBase;
                else
                    v = Math.Abs(Math.Round(ValueBase * g * 10000 / sensitivity, 4));//�ⶨֵΪ0-5V��ѹ��ʵ��ֵ=�ⶨ��ѹ*98066.5/������
                Value = (v - ZeroValue) * GainValue;
            }
            else
                Value = ValueBase;
        }

        public event ValueHandler SensitivityChanged;
    }
}
