using MySql.Data.MySqlClient;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MySqlSelect
{
    public class Sql
    {
        public List<MonitorConfig> GetMonitorPush()
        {
            Common.MySqlHelper db = new Common.MySqlHelper();

            List<MySqlParameter> param = new List<MySqlParameter>();

            string sql = @"select * from MonitorConfig where isdeleted != 1 ";

            var list = db.ExecuteList<MonitorConfig>(sql, param.ToArray());

            return list;
        }

        public bool InsertFileInformation(FileInformation model)
        {
            Common.MySqlHelper db = new Common.MySqlHelper();
            string sql = @"insert into FileInformation(FilePath,FileName,FileRelativePath,DateCreateTime) value (@FilePath,@FileName,@FileRelativePath,@DateCreateTime)";
            List<MySqlParameter> param = new List<MySqlParameter>()
            {
                new MySqlParameter("@FilePath",model.FilePath),
                new MySqlParameter("@FileRelativePath",model.FileRelativePath),
                new MySqlParameter("@FileName",model.FileName),
                new MySqlParameter("@DateCreateTime",model.DateCreateTime)
            };
            var res = db.ExecuteNonQuery(sql, param.ToArray());
            return res > 0;
        }

        public bool UpdateFileInformation(FileInformation model)
        {
            Common.MySqlHelper db = new Common.MySqlHelper();
            string sql = @"Update FileInformation set FilePath=@FilePath,FileRelativePath=@FileRelativePath,FileName=@FileName,DateUpdateTime=@DateUpdateTime where FilePath=@OldFullPath and FileName=@OldFiledName";
            List<MySqlParameter> param = new List<MySqlParameter>()
            {
                new MySqlParameter("@FilePath",model.FilePath),
                new MySqlParameter("@FileRelativePath",model.FileRelativePath),
                new MySqlParameter("@FileName",model.FileName),
                new MySqlParameter("@DateUpdateTime",DateTime.Now),
                new MySqlParameter("@OldFullPath",model.OldFullPath),
                new MySqlParameter("@OldFiledName",model.OldFiledName)
            };
            var res = db.ExecuteNonQuery(sql, param.ToArray());
            return res > 0;
        }

        public bool DeleteFileInformation(FileInformation model)
        {
            Common.MySqlHelper db = new Common.MySqlHelper();
            string sql = @"delete from FileInformation where FilePath=@FilePath and FileName=@FileName";
            List<MySqlParameter> param = new List<MySqlParameter>()
            {
                new MySqlParameter("@FilePath",model.FilePath),
                new MySqlParameter("@FileName",model.FileName)
            };
            var res = db.ExecuteNonQuery(sql, param.ToArray());
            return res > 0;
        }

    }
}
