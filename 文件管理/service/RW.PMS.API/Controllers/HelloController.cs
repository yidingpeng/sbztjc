using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace RW.PMS.API.Controllers
{
    [Route("/[controller]")]
    public class HelloController
    {
        IFreeSql sql;
        public HelloController(IFreeSql sql)
        {
            this.sql = sql;
        }

        [HttpGet]
        public string SayHello()
        {
            return "Hello Wrold";
        }

        [HttpGet("db")]
        public string Db()
        {
            var instance = MySql.Data.MySqlClient.MySqlClientFactory.Instance;
            var conn = instance.CreateConnection();
            conn.ConnectionString = "server=192.168.0.53;uid=root;pwd=RWrw@!$%*123456;database=rw.pms.all";
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "select * from sys_log limit 100";
            var adap = instance.CreateDataAdapter();
            adap.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adap.Fill(ds);

            return $"ok [select {ds.Tables[0].Rows.Count}]";
        }

        [HttpGet("free")]
        public string FreeDb()
        {
            //DataSet ds = new DataSet();
            var ds = sql.Ado.ExecuteDataSet("select * from sys_log limit 100");


            return $"ok[{ds.Tables[0].Rows.Count}]";
        }

        [HttpGet("error")]
        public string Error(int code = 0, string msg = "")
        {
            throw new Exception(msg);
        }
    }
}
