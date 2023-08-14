using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace RW.PMS.Utils
{
    /// <summary>
    /// �ṩһ�ֿ��ص����Ժ��¼��Ľӿ�
    /// </summary>
    public interface ISwitch
    {
        bool Switch { get; set; }
        event SwitchHandler SwitchChanged;
    }

    public delegate void SwitchHandler(object sender, bool value);


    /// <summary>
    /// �ṩһ�ִ���ɫ���صĽӿ�
    /// </summary>
    public interface ISwitchColor : ISwitch
    {
        Color TrueColor { get; set; }
        Color FalseColor { get; set; }
    }

    /// <summary>
    /// �ṩһ�ִ�ͼƬ�л����صĽӿ�
    /// </summary>
    public interface ISwitchImage : ISwitch
    {
        Image TrueImage { get; set; }
        Image FalseImage { get; set; }
    }
}
