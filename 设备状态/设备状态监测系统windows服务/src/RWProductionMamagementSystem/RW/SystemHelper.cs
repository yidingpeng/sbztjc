using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace RW.PMS.Utils
{
    public static class SystemHelper
    {
        public static void KillProcess(string processName)
        {
            try
            {
                Process[] ps = Process.GetProcessesByName("Excel");
                foreach (Process p in ps)
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("�رս��̱���");
                Debug.WriteLine(ex);
            }
            return;
        }

        /// <summary>
        /// ����.NET4.5���µİ汾������Ĭ��������Ϣ
        /// ��Ҫ�����ڶ��߳�ʱ�����Ի����޸ġ�
        /// </summary>
        /// <param name="culture"></param>
        public static void SetDefaultCulture(CultureInfo culture)
        {
            Type type = typeof(CultureInfo);

            //4.0�汾����
            try
            {
                type.InvokeMember("s_userDefaultCulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });

                type.InvokeMember("s_userDefaultUICulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });
            }
            catch { }
            //4.0���°汾����
            try
            {
                type.InvokeMember("m_userDefaultCulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });

                type.InvokeMember("m_userDefaultUICulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });
            }
            catch { }
        }

        /// <summary>
        /// �޸�ϵͳ���ԣ������̵߳�ǰ���ԡ�ϵͳĬ�����ԣ�֧�ֶ��̡߳�
        /// </summary>
        /// <param name="culture"></param>
        public static void ChangeLanguage(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            SetDefaultCulture(culture);
        }
    }
}
