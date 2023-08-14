using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Castle.Core.Configuration;
using RW.PMS.CrossCutting.Extensions;

namespace RW.PMS.CrossCutting.Security.Encrypt
{
    /// <summary>
    ///     加密
    /// </summary>
    public static class EncryptProvider
    {
        /// <summary>
        ///     Md5加密
        /// </summary>
        /// <param name="srcString">需要加密的字符串</param>
        /// <param name="length">加密长度<see cref="Md5Length" /></param>
        /// <returns></returns>
        public static string Md5(string srcString, Md5Length length = Md5Length.L32)
        {
            if (srcString.IsNullOrWhiteSpace()) throw new ArgumentException("要加密的字符不能为空", srcString);
            using var md5 = MD5.Create();
            var byteIn = Encoding.UTF8.GetBytes(srcString);
            var byteOut = md5.ComputeHash(byteIn);
            var md5Out = length == Md5Length.L32 ? BitConverter.ToString(byteOut) : BitConverter.ToString(byteOut, 4, 8);
            return md5Out.Replace("-", String.Empty);
        }
        /// <summary>
        /// 加密(密钥)
        /// </summary>
        /// <param name="Text">要加密的文本</param>
        /// <param name="sKey">秘钥</param>
        /// <returns></returns>
        public static string Encrypt(string Text, string sKey)
        {
            if (Text.IsNullOrWhiteSpace()) throw new ArgumentException("要加密的字符串不能为空", Text);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary>
        /// 解密(密钥)
        /// </summary>
        /// <param name="Text">加密的文本</param>
        /// <param name="sKey">密匙</param>
        /// <returns></returns>
        public static string Decrypt(string Text, string sKey)
        {
            if (Text.IsNullOrWhiteSpace()) throw new ArgumentException("要解密的字符串不能为空", Text);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string Md5Hash(string input)
        {
            //MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            var md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}