using System;
using System.Collections.Generic;
using System.Text;

namespace RW.PMS.Utils.Driver
{
    public class TCPDriver : IDriver
    {
        #region IDriver ��Ա

        public string ServerName
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public object Read(string key)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Write(string key, object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Connect()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Connect(string serverName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public event EventHandler Connected;

        public event ValueChangeHandler ValueChanged;

        #endregion

        #region IDriver ��Ա


        public object this[string key]
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion

        #region IDriver ��Ա


        public void Close()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable ��Ա

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
