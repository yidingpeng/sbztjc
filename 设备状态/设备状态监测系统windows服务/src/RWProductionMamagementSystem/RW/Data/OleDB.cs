using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Diagnostics;

namespace RW.PMS.Utils.Data
{
    /// <summary>
    /// ʹ��OleDbConnection�����ַ�������Ҫ����Access���ݿ⡣
    /// </summary>
    public class OleDB : DbBase<OleDbConnection, OleDbCommand, OleDbDataAdapter>, IDbBase
    {
        public OleDB()
        {

        }

        public OleDB(string source)
            : base(source)
        {

        }

        public void CreateDatabase()
        {
            this.CreateDatabase(this.ConnectionString);
        }

        public void  CreateDatabase(string connectionString)
        {
            try
            {
                ADOX.Catalog c = new ADOX.Catalog();
                c.Create(connectionString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
