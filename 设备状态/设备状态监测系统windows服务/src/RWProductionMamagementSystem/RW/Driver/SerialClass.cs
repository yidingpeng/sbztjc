using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Globalization;


namespace RW.PMS.Utils.Driver
{
    public class SerialClass
    {
        SerialPort _serialPort = null;

        //����ί��
        public delegate void SerialPortDataReceiveEventArgs(object sender, SerialDataReceivedEventArgs e, byte[] bits);

        /// <summary>
        /// ������������¼�
        /// </summary>
        public event SerialPortDataReceiveEventArgs DataReceived;

        //������մ����¼�
        //public event SerialErrorReceivedEventHandler Error;

        /// <summary>
        /// �����¼��Ƿ���Ч false��ʾ��Ч
        /// </summary>
        public bool ReceiveEventFlag = false;

        #region ��ȡ������
        private string protName;
        /// <summary>
        /// ������
        /// </summary>
        public string PortName
        {
            get { return _serialPort.PortName; }
            set
            {
                _serialPort.PortName = value;
                protName = value;
            }
        }

        #endregion

        #region ��ȡ������
        private int baudRate;
        /// <summary>
        /// ������
        /// </summary>
        public int BaudRate
        {
            get { return _serialPort.BaudRate; }
            set
            {
                _serialPort.BaudRate = value;
                baudRate = value;
            }

        }

        #endregion

        #region Ĭ�Ϲ��캯��
        /// <summary>
        /// Ĭ�Ϲ��캯��������COM1���ٶ�Ϊ9600��û����żУ�飬8λ�ֽڣ�ֹͣλΪ1 "COM1", 9600, Parity.None, 8, StopBits.One
        /// </summary>
        public SerialClass()
        {
            _serialPort = new SerialPort();
        }

        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��,
        /// </summary>
        /// <param name="comPortName"></param>
        public SerialClass(string comPortName)
        {
            _serialPort = new SerialPort(comPortName);
            _serialPort.BaudRate = 38400;
            _serialPort.Parity = Parity.Even;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.None;
            _serialPort.RtsEnable = true;
            _serialPort.ReadTimeout = 2000;
            setSerialPort();
        }

        #endregion

        #region ���캯��,�����Զ��崮�ڵĳ�ʼ������
        /// <summary>
        /// ���캯��,�����Զ��崮�ڵĳ�ʼ������
        /// </summary>
        /// <param name="comPortName">��Ҫ������COM������</param>
        /// <param name="baudRate">COM���ٶ�</param>
        /// <param name="parity">��żУ��λ</param>
        /// <param name="dataBits">���ݳ���</param>
        /// <param name="stopBits">ֹͣλ</param>
        public SerialClass(string comPortName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            _serialPort = new SerialPort(comPortName, baudRate, parity, dataBits, stopBits);
            _serialPort.RtsEnable = true;  //�Զ�����
            _serialPort.ReadTimeout = 3000;//��ʱ
            setSerialPort();
        }

        #endregion

        #region ��������
        /// <summary>
        /// �����������رմ���
        /// </summary>
        ~SerialClass()
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
        }
        #endregion

        #region ���ô��ڲ���
        /// <summary>
        /// ���ô��ڲ���
        /// </summary>
        /// <param name="comPortName">��Ҫ������COM������</param>
        /// <param name="baudRate">COM���ٶ�</param>
        /// <param name="dataBits">���ݳ���</param>
        /// <param name="stopBits">ֹͣλ</param>
        public void setSerialPort(string comPortName, int baudRate, int dataBits, int stopBits)
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
            _serialPort.PortName = comPortName;
            _serialPort.BaudRate = baudRate;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = dataBits;
            _serialPort.StopBits = (StopBits)stopBits;
            _serialPort.Handshake = Handshake.None;
            _serialPort.RtsEnable = false;
            _serialPort.ReadTimeout = 3000;
            _serialPort.NewLine = "/r/n";
            setSerialPort();

        }

        #endregion

        #region ���ý��պ���
        /// <summary>
        /// ���ô�����Դ,�������ض�����ô��ڵĺ���
        /// </summary>
        void setSerialPort()
        {
            if (_serialPort != null)
            {
                //���ô���DataReceived�¼����ֽ���Ϊ1
                _serialPort.ReceivedBytesThreshold = 1;
                //���յ�һ���ֽ�ʱ��Ҳ�ᴥ��DataReceived�¼�
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);
                //�������ݳ���,�����¼�
                _serialPort.ErrorReceived += new SerialErrorReceivedEventHandler(_serialPort_ErrorReceived);
                //�򿪴���
                //openPort();
            }
        }
        #endregion

        #region �򿪴�����Դ
        /// <summary>
        /// �򿪴�����Դ
        /// </summary>
        /// <returns>����bool����</returns>
        public bool openPort()
        {
            bool ok = false;
            //��������Ǵ򿪵ģ��ȹر�
            if (_serialPort.IsOpen)
                _serialPort.Close();
            try
            {
                //�򿪴���
                _serialPort.Open();
                ok = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return ok;
        }

        #endregion

        #region �رմ���
        /// <summary>
        /// �رմ�����Դ,������ɺ�,һ��Ҫ�رմ���
        /// </summary>
        public void closePort()
        {
            //������ڴ��ڴ�״̬,��ر�
            if (_serialPort.IsOpen)
                _serialPort.Close();
        }

        #endregion

        #region ���մ��������¼�
        /// <summary>
        /// ���մ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //��ֹ�����¼�ʱֱ���˳�
            if (ReceiveEventFlag)
            {
                return;
            }
            try
            {
                Thread.Sleep(20);
                byte[] _data = new byte[_serialPort.BytesToRead];
                _serialPort.Read(_data, 0, _data.Length);
                if (_data.Length == 0) { return; }
                if (DataReceived != null)
                {
                    DataReceived(sender, e, _data);
                }
                //_serialPort.DiscardInBuffer();  //��ս��ջ�����  
            }
            catch //(Exception ex)
            {
                //throw ex;
            }

        }

        #endregion

        #region �������ݳ����¼�
        /// <summary>
        /// �������ݳ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }

        #endregion

        #region ��������string����
        /// <summary>
        /// ��������string����
        /// </summary>
        /// <param name="data"></param>
        public void SendData(string data)
        {
            //��������
            //��ֹ�����¼�ʱֱ���˳�
            if (ReceiveEventFlag)
            {
                return;
            }
            if (_serialPort.IsOpen)
            {
                _serialPort.Write(data);
            }

        }

        #endregion

        #region ��������byte[]����

        /// <summary>
        /// ��������byte����
        /// </summary>
        /// <param name="data">Ҫ���͵������ֽ�</param>
        /// <param name="offset">������ʼλ</param>
        /// <param name="count">����</param>
        public void SendData(byte[] data, int offset, int count)
        {
            //��ֹ�����¼�ʱֱ���˳�
            if (ReceiveEventFlag)
            {
                return;
            }
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.DiscardInBuffer();//��ս��ջ�����
                    _serialPort.Write(data, offset, count);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ����������ؽ��
        /// <summary>
        /// �������� �����ؽ��
        /// </summary>
        /// <param name="SendData">��������</param>
        /// <param name="ReceiveData">��������</param>
        /// <param name="Overtime">��ʱʱ��</param>
        /// <returns></returns>
        public int SendCommand(byte[] SendData, ref  byte[] ReceiveData, int Overtime)
        {
            if (_serialPort.IsOpen)
            {
                try
                {
                    ReceiveEventFlag = true;        //�رս����¼�
                    _serialPort.DiscardInBuffer();  //��ս��ջ�����    
                    _serialPort.DiscardOutBuffer();//test
                    Thread.Sleep(2);
                    _serialPort.Write(SendData, 0, SendData.Length);
                    //System.Threading.Thread.Sleep(100);
                    int num = 0, ret = 0;
                    Thread.Sleep(10);
                    //ReceiveEventFlag = false;      //���¼�
                    while (num++ < Overtime)
                    {
                        if (_serialPort.BytesToRead >= ReceiveData.Length)
                            break;
                        Thread.Sleep(10);
                    }
                    if (_serialPort.BytesToRead >= ReceiveData.Length)
                    {
                        ret = _serialPort.Read(ReceiveData, 0, ReceiveData.Length);
                    }
                    else
                    {
                        ret = _serialPort.Read(ReceiveData, 0, _serialPort.BytesToRead);
                    }
                    ReceiveEventFlag = false;      //���¼�
                    return ret;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("����/������Ϣʧ�ܣ�" + ex.ToString());
                    ReceiveEventFlag = false;
                    //throw ex;
                }
            }

            return -1;

        }

        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ���������Ӷ���è�豸�Ĵ���
        /// </summary>
        /// <returns></returns>
        public string[] serialsIsConnected()
        {
            List<string> lists = new List<string>();
            string[] seriallist = getSerials();
            foreach (string s in seriallist)
            {

            }
            return lists.ToArray();
        }

        #endregion

        #region ��ȡ��ǰȫ��������Դ
        /// <summary>
        /// ��õ�ǰ�����ϵ����д�����Դ
        /// </summary>
        /// <returns></returns>
        public string[] getSerials()
        {
            return SerialPort.GetPortNames();
        }

        #endregion

        #region �ֽ���ת��16
        /// <summary>
        /// ���ֽ���ת����ʮ�������ַ���
        /// </summary>
        /// <param name="InBytes"></param>
        /// <returns></returns>
        public static string ByteToString(byte[] InBytes)
        {
            string StringOut = "";
            foreach (byte InByte in InBytes)
            {
                StringOut = StringOut + String.Format("{0:X2} ", InByte);
            }
            return StringOut;
        }

        #endregion

        #region ʮ�������ַ���ת�ֽ���
        /// <summary>
        /// ��ʮ�������ַ���ת�����ֽ���(����1)
        /// </summary>
        /// <param name="InString"></param>
        /// <returns></returns>
        public static byte[] StringToByte(string InString)
        {
            string[] ByteStrings;
            ByteStrings = InString.Split(" ".ToCharArray());
            byte[] ByteOut;
            ByteOut = new byte[ByteStrings.Length];
            for (int i = 0; i <= ByteStrings.Length - 1; i++)
            {
                //ByteOut[i] = System.Text.Encoding.ASCII.GetBytes(ByteStrings[i]);
                ByteOut[i] = Byte.Parse(ByteStrings[i], NumberStyles.HexNumber);
                //ByteOut[i] =Convert.ToByte("0x" + ByteStrings[i]);
            }
            return ByteOut;
        }

        #endregion

        #region ʮ�������ַ���ת�ֽ���
        /// <summary>
        /// �ַ���ת16�����ֽ�����(����2)
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        #endregion

        #region �ֽ���תʮ�������ַ���
        /// <summary>
        /// �ֽ�����ת16�����ַ���
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2") + " ";
                }
            }
            return returnStr;
        }
        #endregion

    }
}
