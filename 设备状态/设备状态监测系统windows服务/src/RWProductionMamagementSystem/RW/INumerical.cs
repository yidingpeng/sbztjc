using System;
using System.Collections.Generic;
using System.Text;

namespace RW.PMS.Utils
{
    /// <summary>
    /// ������������λ������
    /// </summary>
    public interface INumerical
    {
        string Text { get;set; }
        UnitEnum Unit { get;set; }
        double Value { get;set; }
        int DecimalNumber { get;set; }
        string FormatString { get;set; }
    }
}
