using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;

namespace RW.PMS.Utils.Data
{
    /// <summary>
    /// SQLite�������ݿ�İ�����
    /// </summary>
    public class SQLiteDB : DbBase<SQLiteConnection, SQLiteCommand, SQLiteDataAdapter>, IDbBase
    {
        public SQLiteDB()
        {

        }

        public SQLiteDB(string source)
            : base(source)
        {

        }

        public void CreateDatabase(string filename)
        {
            SQLiteConnection.CreateFile(filename);
        }
    }
}
