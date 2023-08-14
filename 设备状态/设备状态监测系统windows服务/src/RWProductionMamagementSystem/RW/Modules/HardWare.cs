using System;
using System.Collections.Generic;
using System.Text;
using RW.PMS.Utils.Configuration;

namespace RW.PMS.Utils.Modules
{
    /// <summary>
    /// ��������֧����������Ӳ��ģ��
    /// </summary>
    public class Hardware : IniConfig, IHardware
    {
        public virtual event ValueHandler GainValueChanged;
        public virtual event ValueHandler ZeroValueChanged;
        public virtual event ValueHandler ValueChanged;
        public virtual event ValueHandler BaseValueChanged;
        public virtual event ValueHandler<int> RadixValueChanged;

        private double valueBase;
        /// <summary>
        /// ����������
        /// </summary>
        public virtual double ValueBase
        {
            get { return valueBase / Math.Pow(10, this.Radix); }
            set
            {
                valueBase = value;
                if (BaseValueChanged != null)
                    BaseValueChanged(this, this.ValueBase);
                SetActual();
            }
        }

        private double _value;
        /// <summary>
        /// ʵ��ֵ
        /// </summary>
        public virtual double Value
        {
            get { return _value; }
            protected set
            {
                _value = value;
                if (ValueChanged != null)
                    ValueChanged(this, value);
            }
        }

        private bool setLocal = true;
        /// <summary>
        /// �ɱ��ؼ���ʵ��ֵ�������豸�������󷵻�
        /// </summary>
        public bool SetLocal
        {
            get { return setLocal; }
            set { setLocal = value; }
        }


        protected virtual void SetActual()
        {
            if (SetLocal)
                Value = ValueBase * GainValue - ZeroValue;
            else
                Value = ValueBase;
        }

        private double GetActual()
        {
            return Value;
        }

        private double gainValue = 1;
        /// <summary>
        /// ����ֵ
        /// </summary>
        public virtual double GainValue
        {
            get { return gainValue; }
            set
            {
                gainValue = value;
                if (GainValueChanged != null)
                    GainValueChanged(this, value);
                SetActual();
            }
        }

        private double zeroValue;
        /// <summary>
        /// ���ֵ
        /// </summary>
        public virtual double ZeroValue
        {
            get { return zeroValue; }
            set
            {
                zeroValue = value;
                if (ZeroValueChanged != null)
                    ZeroValueChanged(this, value);
                SetActual();
            }
        }

        private int radix;
        /// <summary>
        /// С����λ����Ĭ��Ϊ0
        /// </summary>
        public virtual int Radix
        {
            get { return radix; }
            set
            {
                radix = value;
                if (RadixValueChanged != null)
                    RadixValueChanged(this, value);
                SetActual();
            }
        }

        #region IHardware ��Ա

        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{Value:{0},ValueBase:{1},GainValue:{2},ZeroValue:{3},Radix:{4}}", Value, ValueBase, GainValue, ZeroValue);
            //return base.ToString();
        }
    }

    [Obsolete("�������������⣬��ʹ��Hardware��������HardWare")]
    public class HardWare : Hardware
    {
    }
}
