using System;
using System.Collections.Generic;
using System.Text;

namespace RW.PMS.Utils
{
    /// <summary>
    /// ֧��Index�ֶ��Լ�this[T value]�������Ľӿڣ�ʵ����IIndexFiled��IIndexes&#60;T>�ӿ�
    /// </summary>
    public interface IIndex<T> : IIndexFiled, IIndexes<T>
    {

    }

    /// <summary>
    /// ֧��Index�ֶ��Լ�this[double value]�������Ľӿڣ�ʵ����IIndexFiled��IIndexes�ӿ�
    /// </summary>
    public interface IIndex : IIndexFiled, IIndexes
    {

    }

    /// <summary>
    /// ����IIndexFiled�Ľӿڣ�����ͳһ��������Index�ֶε���
    /// </summary>
    public interface IIndexFiled
    {
        /// <summary>
        /// ��ȡ������������
        /// </summary>
        int Index { get; set; }
    }

    /// <summary>
    /// ����IIndexes�Ľӿڣ�����ͳһ���������������࣬����ͨ��this���ʵ���
    /// </summary>
    public interface IIndexes<T>
    {
        /// <summary>
        /// ��ȡ������ָ��������ֵ
        /// </summary>
        /// <param name="index">��Ҫ��ȡ�����õ�����ֵ</param>
        /// <returns></returns>
        T this[int index] { get; }
    }

    public interface IIndexes : IIndexes<double>
    {
    }
}
