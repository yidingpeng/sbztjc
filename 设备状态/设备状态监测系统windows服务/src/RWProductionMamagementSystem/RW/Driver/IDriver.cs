using System;
using System.Collections.Generic;
using System.Text;

namespace RW.PMS.Utils.Driver
{
    /// <summary>
    /// �����ӿ�
    /// </summary>
    public interface IDriver : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// ��
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Read(string key);

        /// <summary>
        /// д
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Write(string key, object value);

        /// <summary>
        /// ����
        /// </summary>
        void Connect();

        /// <summary>
        /// �ر�
        /// </summary>
        void Close();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        /// �����¼�
        /// </summary>
        event EventHandler Connected;

        /// <summary>
        /// ֵ���¼�
        /// </summary>
        event ValueChangeHandler ValueChanged;
    }
}
