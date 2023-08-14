using OPCAutomation;
using RW.PMS.Common.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace RW.PMS.Utils.Driver
{
    public delegate void ValueChangeHandler(string key, object value);
    public delegate void AsyncWriteHandler(int TransactionID, int NumItems, string[] keys, int[] errors);
    public delegate void AsyncReadHandler(int TransactionID, int NumItems, string[] keys, object[] values, int[] qualities, DateTime[] TimeStamps, int[] Errors);

    /// <summary>
    /// OPC����
    /// 
    /// ���������ʾ��
    /// 0xC0040007��OPCδ����ָ�������ƣ�
    /// </summary>
    public class OPCDriver : Component, IDisposable, IDriver
    {
        #region ˽�б���
        OPCServer server = null;
        OPCGroup Group;
        //string serverName = "KEPware.KEPServerEx.V4";
        string serverName = "KEPware.KEPServerEx.V6";

        string prefix = "";
        string ip;
        Dictionary<string, OPCItem> dicKeyItem = new Dictionary<string, OPCItem>();
        object dicLock = new object();
        int clientIndex = 0;
        #endregion

        #region ��������
        public OPCServer Server
        {
            get { return server; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        /// <summary>
        /// ǰ׺
        /// </summary>
        public string Prefix
        {
            get { return prefix; }
            set { prefix = value; }
        }

        /// <summary>
        /// �ⲿ��IP��ַ��Ϊ�ձ�ʾΪ�������ӡ�
        /// </summary>
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        public object this[string key]
        {
            get { return this.Read(key); }
            set { this.Write(key, value); }
        }

        public event AsyncWriteHandler AsyncWriteComplete;

        public event AsyncReadHandler AsyncReadComplete;
        #endregion

        #region ���캯��
        public OPCDriver()
        {

        }

        public OPCDriver(bool autoConnect)
        {
            if (autoConnect)
            {
                Connect();
            }
        }

        public OPCDriver(string serverName)
        {
            this.ServerName = serverName;
        }

        public OPCDriver(string serverName, string prefix)
        {
            this.ServerName = serverName;
            this.Prefix = prefix;
        }

        public OPCDriver(string serverName, string prefix, string ip)
        {
            this.ServerName = serverName;
            this.Prefix = prefix;
            this.IP = ip;
        }
        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="config"></param>
        public void LoadConfig(OPCConfig config)
        {
            this.ServerName = config.ServerName;
            this.Prefix = config.Prefix;
            this.IP = config.IP;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="config"></param>
        public void SaveConfig(OPCConfig config)
        {
            config.ServerName = this.ServerName;
            config.Prefix = this.Prefix;
            config.IP = this.IP;
        }

        /// <summary>
        /// ��ȡOPC�����������нڵ�
        /// </summary>
        /// <param name="oPCBrowser"></param>
        public List<string> RecurBrowse()
        {
            OPCBrowser oPCBrowser = this.Server.CreateBrowser();

            //չ����֧
            oPCBrowser.ShowBranches();
            //չ��Ҷ��
            oPCBrowser.ShowLeafs(true);

            List<string> lstServers = new List<string>();

            foreach (object turn in oPCBrowser)
            {
                lstServers.Add(turn + "");
            }
            return lstServers;
        }


        /// <summary>
        /// ����ǰ׺��ȡ�����ļ�ֵ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetFullKey(string key)
        {
            return this.Prefix + key;
        }

        public OPCItem Items(string key)
        {
            if (!dicKeyItem.ContainsKey(key))
            {
                throw new KeyNotFoundException("[" + key + "] not found in [" + ServerName + "].");
            }
            return dicKeyItem[key];
        }

        /// <summary>
        /// �ж�ָ���ĵ�λ��ͨѶ�����Ƿ�����
        /// </summary>
        /// <param name="key">ָ���ĵ�λ����</param>
        /// <returns>ͨѶ�Ƿ�����</returns>
        public bool IsGood(string key)
        {
            OPCItem item = GetOrAddItem(key);//�ж�ָ���ĵ�λ��ͨѶ�����Ƿ�����
            return item.Quality == 192;
        }

        /// <summary>
        /// ����ָ���ĵ�λ����״̬
        /// </summary>
        /// <param name="key">ָ���ĵ�λ����</param>
        /// <param name="value">ָ����ֵ����</param>
        public void SetActive(string key, bool value)
        {
            OPCItem item = GetOrAddItem(key);//δ������ ����ָ���ĵ�λ����״̬
            item.IsActive = value;
        }


        public object[] this[string[] keys]
        {
            get
            {
                int[] handlers = new int[keys.Length];
                for (int i = 0; i < keys.Length; i++)
                {
                    handlers[i] = GetOrAddItem(keys[i]).ServerHandle;//δ������
                }
                int[] temphandlers = new int[keys.Length + 1];
                temphandlers[0] = 0;
                Array.Copy(handlers, 0, temphandlers, 1, handlers.Length);
                Array hs = (Array)temphandlers;
                Array values, errs;
                object q, ts;
                Group.SyncRead((short)OPCDataSource.OPCDevice, keys.Length, ref hs, out values, out errs, out q, out ts);

                object[] rvalues = new object[keys.Length];
                for (int i = 0; i < values.GetLength(0); i++)
                {
                    rvalues[i] = values.GetValue(i + 1);
                }

                return rvalues;
            }
            set
            {
                int[] handlers = new int[keys.Length];
                for (int i = 0; i < keys.Length; i++)
                {
                    handlers[i] = GetOrAddItem(keys[i]).ServerHandle;//δ������
                }
                //handlers.Add(dicKeyItem[k].ClientHandle);
                object[] temp = new object[value.Length + 1];
                temp[0] = 0;
                Array.Copy(value, 0, temp, 1, value.Length);

                Array arr = (Array)temp;

                int[] temphandlers = new int[keys.Length + 1];
                temphandlers[0] = 0;
                Array.Copy(handlers, 0, temphandlers, 1, handlers.Length);
                Array err;

                Array hs = (Array)temphandlers;
                Group.SyncWrite(keys.Length, ref hs, ref arr, out err);

                //(OPCAutomation.OPCErrors);
                string msg = Enum.GetName(typeof(OPCAutomation.OPCErrors), err.GetValue(1));
                //-2147352571


                Debug.WriteLine("sync write over error:" + msg);

            }
        }

        /// <summary>
        /// ���ڴ������µ����ݣ�����ÿ�ζ�ȡOPC������IO
        /// </summary>
        Dictionary<string, object> dicKeyValue = new Dictionary<string, object>();
        public object GetLastValue(string key)
        {
            if (dicKeyValue.ContainsKey(key))
                return dicKeyValue[key];
            return null;
        }

        /// <summary>
        /// ��ȡ�����Item
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private OPCItem GetOrAddItem(string key)
        {
            if (Server == null || Group == null)
            {
                Debug.WriteLine("Server:" + Server);
                throw new NullReferenceException("û������OPC");
            }
            if (string.IsNullOrEmpty(key)) return null;

            if (!dicKeyItem.ContainsKey(key))//����ģ�ͣ����̻߳������⡣���߳�ͬһ��key����ᵼ�������ͬkey
            {
                lock (dicLock)
                {
                    if (!dicKeyItem.ContainsKey(key))
                    {
                        dicIndexKey[clientIndex] = key;
                        string fullKey = GetFullKey(key);
                        dicKeyItem[key] = Group.OPCItems.AddItem(fullKey, clientIndex);
                        clientIndex++;
                    }
                }
            }
            return dicKeyItem[key];
        }

        public event ValueChangeHandler ValueChanged;

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="serverName"></param>
        public void Connect(string serverName)
        {
            if (Server != null && Server.ServerState == (int)OPCServerState.OPCRunning)
                throw new System.Runtime.InteropServices.COMException("OPCServer is Connected.");
            try
            {
                server = new OPCServer();
                server.OPCGroups.DefaultGroupIsActive = true;// Convert.ToBoolean(txtGroupIsActive.Text);
                server.OPCGroups.DefaultGroupUpdateRate = 50;// Convert.ToInt32(txtGroupDeadband.Text);
                //KepServer.Connect("S7200.OPCServer", null);
                server.Connect(serverName, ip);
                server.ServerShutDown += new DIOPCServerEvent_ServerShutDownEventHandler(server_ServerShutDown);

                if (Server.ServerState == (int)OPCServerState.OPCRunning)
                {
                    InitItem();
                    if (Connected != null)
                        Connected(this, EventArgs.Empty);
                }
                else
                {
                    Debug.WriteLine("����ʧ�ܣ�" + Server.ServerState);
                    //throw new Exception("�޷�");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("=============================================");
                Debug.WriteLine(ex.ToString());
                throw ex;
            }
        }

        void server_ServerShutDown(string Reason)
        {
            Debug.WriteLine("================================" + Reason + Server.ServerState.ToString() + "==============");
            //this.Connect();
            //throw new Exception("The method or operation is not implemented.");
        }

        public void Connect()
        {

            //Connect(serverName);
            //Connect("S7200.OPCServer");

            try
            {
                Connect(serverName);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("OPC����ʧ�ܣ�" + ex.ToString(), "��ܰ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Debug.WriteLine(ex.ToString());
                //Logger.Default.Error("OPC����ʧ�ܣ�", ex.ToString());
            }
        }

        public bool Contains(string key)
        {
            return dicKeyItem.ContainsKey(key);
        }

        public event EventHandler Connected;

        Dictionary<int, string> dicIndexKey = new Dictionary<int, string>();

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void InitItem()
        {
            //Group = Server.OPCGroups;
            Group = Server.OPCGroups.Add("AO");
            Group.IsSubscribed = true;
            Group.IsActive = true;// Convert.ToBoolean(txtGroupIsActive.Text);
            //Group.UpdateRate = 50;
            Group.UpdateRate = 100;//ɨ�����ڸ�Ϊ1000
            Group.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
            Group.AsyncReadComplete += new DIOPCGroupEvent_AsyncReadCompleteEventHandler(Group_AsyncReadComplete);
            Group.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(Group_AsyncWriteComplete);
        }

        void Group_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            string[] keys = new string[ClientHandles.Length];
            for (int i = 0; i < ClientHandles.Length; i++)
                keys[i] = dicIndexKey[Convert.ToInt32(ClientHandles.GetValue(i + 1))];
            int[] errors = new int[Errors.Length];
            Array.Copy(Errors, 1, errors, 0, errors.Length);

            //Array.Resize(ref keys, keys.Length + 1);

            if (AsyncWriteComplete != null) AsyncWriteComplete(TransactionID, NumItems, keys, errors);
        }

        void Group_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            string[] keys = new string[ClientHandles.Length];
            for (int i = 0; i < ClientHandles.Length; i++)
                keys[i] = dicIndexKey[Convert.ToInt32(ClientHandles.GetValue(i + 1))];
            object[] values = new object[ItemValues.Length];
            Array.Copy(ItemValues, 1, values, 0, values.Length);
            int[] qualities = new int[Qualities.Length];
            Array.Copy(Qualities, 1, qualities, 0, qualities.Length);
            DateTime[] times = new DateTime[TimeStamps.Length];
            Array.Copy(TimeStamps, 1, times, 0, times.Length);
            int[] errors = new int[Errors.Length];
            Array.Copy(Errors, 1, errors, 0, errors.Length);
            if (AsyncReadComplete != null) AsyncReadComplete(TransactionID, NumItems, keys, values, qualities, times, errors);
        }

        void KepGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                int handler = Convert.ToInt32(ClientHandles.GetValue(i));
                object value = ItemValues.GetValue(i);
                string itemName = dicIndexKey[handler];
                dicKeyValue[itemName] = value;
                //Debug.WriteLine(string.Format("dataChanged: handler={0},value={1},key={2}", handler, value, mapHandlerAndItemKey[handler]));
                if (ValueChanged != null)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        try
                        {
                            //Debug.WriteLine("group data change event: " + mapHandlerAndItemKey[handler] + "_" + handler + " changed:" + value);
                            ValueChanged(itemName, value);
                        }
                        catch (Exception ex)
                        {
                            //Debug.Write("map count:" + mapHandlerAndItemKey.Count + " handler:" + handler + "  value:" + value);
                            //Debug.WriteLine("===========ValueChanged Error:" + mapHandlerAndItemKey[handler] + "============");
                            Debug.WriteLine(ex);
                        }
                    });
                }
                //ValueChanged(newPLC[handler - 1], value);
            }
        }

        public void Close()
        {
            try
            {
                if (Group != null)
                    Group.DataChange -= KepGroup_DataChange;

                if (Server != null && Server.ServerState != (int)OPCServerState.OPCDisconnected)
                    Server.Disconnect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void Write(string key, object value)
        {
            Debug.WriteLine("write item [" + key + "]:[" + value + "]");
            OPCItem item = GetOrAddItem(key);//Write
            item.Write(value);
        }

        public void SyncWrite(string[] keys, object[] values)
        {
            if (keys.Length != values.Length)
                throw new ArgumentException("length not equals.");
            int len = keys.Length;
            Array handlers = GetHandlers(keys);
            Array setValues = GetValues(values);
            Array errors;
            Group.SyncWrite(len, ref handlers, ref setValues, out errors);
        }

        public int AsyncWrite(string[] keys, object[] values)
        {
            tid++;
            int tempID = tid;
            if (keys.Length != values.Length)
                throw new ArgumentException("length not equals.");
            int len = keys.Length;
            Array handlers = GetHandlers(keys);
            Array setValues = GetValues(values);
            Array errors;
            int cid;
            Group.AsyncWrite(len, ref handlers, ref setValues, out errors, tid, out cid);
            return tempID;
        }

        public object Read(string key)
        {
            object value = GetOrAddItem(key).Value;//Read
            Debug.WriteLine("read item [" + key + "]:[" + value + "]");
            return value;
        }

        public object[] SyncRead(string[] keys)
        {
            int len = keys.Length;
            Array handlers = GetHandlers(keys);
            Array values, errors;
            object qualities, times;
            Group.SyncRead((short)OPCDataSource.OPCDevice, len, ref handlers, out values, out errors, out qualities, out times);
            object[] retValues = new object[values.Length];
            values.CopyTo(retValues, 0);
            return retValues;
        }

        private static int tid = 0;
        public int AsyncRead(string[] keys)
        {
            tid++;
            int tempID = tid;
            int len = keys.Length;
            Array handlers = GetHandlers(keys);
            Array errors;
            int cid;
            Group.AsyncRead(len, ref handlers, out errors, tid, out cid);
            return tempID;
        }

        private Array GetHandlers(string[] keys)
        {
            List<int> handlers = new List<int>();
            handlers.Add(0);
            foreach (string k in keys)
            {
                handlers.Add(GetOrAddItem(k).ServerHandle);//GetHandlers
            }
            return (Array)handlers.ToArray();
        }

        private Array GetValues(object[] values)
        {
            Array arr = new object[values.Length + 1];
            values.CopyTo(arr, 1);
            return arr;

        }


        #region IDisposable ��Ա

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion
    }
}
