using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace RW.PMS.Utils.Data
{
    /// <summary>
    /// ʹ��SQLServer�������ݿ�İ�����
    /// </summary>
    public class SqlDB : DbBase<SqlConnection, SqlCommand, SqlDataAdapter>, IDbBase
    {
        public SqlDB()
        {

        }

        public SqlDB(string source)
            : base(source)
        {

        }
    }

}
