using Core.Model;
using Core.MySqlSelect;
using RW_Monitor.log;
using RW_Monitor.Log4net;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW_Monitor
{
    public class FileMonitorHelp
    {
        private List<FileSystemWatcher> _watchers;

        private Sql sql = new Sql();

        public FileMonitorHelp()
        {
        }
        public FileMonitorHelp(List<string> paths)
        {
            try
            {
                if (paths.Count > 0)
                {
                    this._watchers = new List<FileSystemWatcher>(); /* = new FileSystemWatcher();*/

                    foreach (var item in paths)
                    {
                        FileSystemWatcher _watcher = new FileSystemWatcher();
                        //if (_watcher.EnableRaisingEvents) Stop();
                        _watcher.Path = item;
                        _watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.DirectoryName;
                        _watcher.IncludeSubdirectories = true;
                        _watcher.Created += new FileSystemEventHandler(FileWatcher_Created);
                        _watcher.Changed += new FileSystemEventHandler(FileWatcher_Changed);
                        _watcher.Deleted += new FileSystemEventHandler(FileWatcher_Deleted);
                        _watcher.Renamed += new RenamedEventHandler(FileWatcher_Renamed);
                        _watchers.Add(_watcher);
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveMessage("文件监听", "", "文件监听初始化错误：" + ex.Message, 4);
                insertlog("文件监听", "", "文件监听初始化错误：" + ex.Message, 4);
                //Console.WriteLine("Error:" + ex.Message);
            }
        }


        public void Start()
        {
            //this._watcher.EnableRaisingEvents = true;
            if (_watchers.Count > 0)
            {
                foreach (var item in _watchers)
                {
                    item.EnableRaisingEvents = true;
                }
            }
        }

        public void Stop()
        {
            //this._watcher.EnableRaisingEvents = false;
            //this._watcher.Dispose();
            //this._watcher = null;
            if (_watchers.Count > 0)
            {
                foreach (var item in _watchers)
                {
                    item.EnableRaisingEvents = false;
                    item.Dispose();
                }
                _watchers.Clear();
            }
        }

        protected void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                var Host = ConfigurationManager.AppSettings["MonitorLocalIP"]?.ToString();
                var FileName = "";
                if (e.Name.LastIndexOf("\\") >= 0)
                {
                    FileName = e.Name.Substring(e.Name.LastIndexOf("\\") + 1);
                }
                else
                {
                    FileName = e.Name;
                }
                var RelativePath = (Host + e.Name).Replace("\\","/");
             //   LogHelper.SaveMessage("文件监听", e.FullPath, e.ChangeType + "新建文件：" + FileName, 1);
                insertlog("文件监听", e.FullPath, e.ChangeType + "新建文件：" + FileName, 1);
                FileInformation Filemodel = new FileInformation()
                {
                    FilePath = e.FullPath,
                    FileRelativePath = RelativePath,
                    FileName = FileName,
                    DateCreateTime = DateTime.Now
                };
                var res = sql.InsertFileInformation(Filemodel);
                if (res)
                {
                  //  LogHelper.SaveMessage("文件监听", "数据记录：" + e.FullPath + " 保存成功!", e.ChangeType + "文件记录数据保存成功!", 1);
                    insertlog("文件监听", "数据记录：" + e.FullPath + " 保存成功!", e.ChangeType + "文件记录数据保存成功!", 1);
                }
                else
                {
                  //  LogHelper.SaveMessage("文件监听", "数据记录：" + e.FullPath + " 保存失败!", e.ChangeType + "文件记录数据保存失败!", 3);
                    insertlog("文件监听", "数据记录：" + e.FullPath + " 保存失败!", e.ChangeType + "文件记录数据保存失败!", 3);
                }
            }
            catch (Exception ex)
            {
               // LogHelper.SaveMessage("文件监听", "数据记录：" + e.FullPath + " 保存时出现异常! 原因：", ex.Message, 4);
                insertlog("文件监听", "数据记录：" + e.FullPath + " 保存时出现异常! 原因：", ex.Message, 4);
            }
            //Console.WriteLine("新增:" + e.ChangeType + ";" + e.FullPath + ";" + e.Name);
        }

        protected void FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
           // LogHelper.SaveMessage("文件监听", e.FullPath, e.ChangeType + "文件变更：" + e.Name, 1);
            insertlog("文件监听", e.FullPath, e.ChangeType + "文件变更：" + e.Name, 1);
            //Console.WriteLine("变更:" + e.ChangeType + ";" + e.FullPath + ";" + e.Name);
        }

        protected void FileWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                var FileName = "";
                if (e.Name.LastIndexOf("\\") >= 0)
                {
                    FileName = e.Name.Substring(e.Name.LastIndexOf("\\") + 1);
                }
                else
                {
                    FileName = e.Name;
                }
               // LogHelper.SaveMessage("文件监听", e.FullPath, e.ChangeType + "文件删除：" + FileName, 1);
                insertlog("文件监听", e.FullPath, e.ChangeType + "文件删除：" + FileName, 1);
                FileInformation Filemodel = new FileInformation()
                {
                    FilePath = e.FullPath,
                    FileName = FileName,
                };
                var res = sql.DeleteFileInformation(Filemodel);
                if (res)
                {
                  //  LogHelper.SaveMessage("文件监听", "数据记录：" + e.FullPath + " 删除成功!", e.ChangeType + "文件记录数据保存成功!", 1);
                    insertlog("文件监听", "数据记录：" + e.FullPath + " 删除成功!", e.ChangeType + "文件记录数据保存成功!", 1);
                }
                else
                {
                  //  LogHelper.SaveMessage("文件监听", "数据记录：" + e.FullPath + " 删除失败!", e.ChangeType + "文件记录数据保存成功!", 1);
                    insertlog("文件监听", "数据记录：" + e.FullPath + " 删除失败!", e.ChangeType + "文件记录数据保存成功!", 1);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.SaveMessage("文件监听", e.FullPath, e.ChangeType + "删除文件：" + e.Name + " 记录失败！ 原因：" + ex.Message, 1);
                insertlog("文件监听", e.FullPath, e.ChangeType + "删除文件：" + e.Name + " 记录失败！ 原因：" + ex.Message, 1);
            }
            //Console.WriteLine("删除:" + e.ChangeType + ";" + e.FullPath + ";" + e.Name);
        }

        protected void FileWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            try
            {
                var Host = ConfigurationManager.AppSettings["MonitorLocalIP"]?.ToString();
                var FileName = "";
                if (e.Name.LastIndexOf("\\") >= 0)
                {
                    FileName = e.Name.Substring(e.Name.LastIndexOf("\\") + 1);
                }
                else
                {
                    FileName = e.Name;
                }
                var oldFileName = "";
                if (e.Name.LastIndexOf("\\") >= 0)
                {
                    oldFileName = e.OldName.Substring(e.OldName.LastIndexOf("\\") + 1);
                }
                else
                {
                    oldFileName = e.OldName;
                }
                var RelativePath = (Host + e.Name).Replace("\\", "/");
               // LogHelper.SaveMessage("文件监听", "旧链接：" + e.OldFullPath + " 新链接：" + e.FullPath, " 文件重命名→旧名：" + e.OldName + " 新名：" + e.Name, 1);
                insertlog("文件监听", "旧链接：" + e.OldFullPath + " 新链接：" + e.FullPath, " 文件重命名→旧名：" + e.OldName + " 新名：" + e.Name, 1);
                FileInformation Filemodel = new FileInformation()
                {
                    OldFullPath = e.OldFullPath,
                    FilePath = e.FullPath,
                    FileRelativePath = RelativePath,
                    OldFiledName = oldFileName,
                    FileName = FileName
                };

                var res = sql.UpdateFileInformation(Filemodel);
                if (res)
                {
                   // LogHelper.SaveMessage("文件监听", "数据记录 旧链接：" + e.OldFullPath + " 新链接：" + e.FullPath + " 修改成功！", "文件重命名→旧名：" + oldFileName + " 新名：" + FileName + " 数据记录保存成功!", 1);
                    insertlog("文件监听", "数据记录 旧链接：" + e.OldFullPath + " 新链接：" + e.FullPath + " 修改成功！", "文件重命名→旧名：" + oldFileName + " 新名：" + FileName + " 数据记录保存成功!", 1);
                }
                else
                {
                  //  LogHelper.SaveMessage("文件监听", "数据记录 旧链接：" + e.OldFullPath + " 新链接：" + e.FullPath + " 修改失败！", "文件重命名→旧名：" + oldFileName + " 新名：" + FileName + " 数据记录保存失败!", 3);
                    insertlog("文件监听", "数据记录 旧链接：" + e.OldFullPath + " 新链接：" + e.FullPath + " 修改失败！", "文件重命名→旧名：" + oldFileName + " 新名：" + FileName + " 数据记录保存失败!", 3);
                }
            }
            catch (Exception ex)
            {
               // LogHelper.SaveMessage("文件监听", "数据记录 旧链接：" + e.OldFullPath + " 新链接：" + e.FullPath + " 修改时出现异常！ 原因：", ex.Message, 4);
                insertlog("文件监听", "数据记录 旧链接：" + e.OldFullPath + " 新链接：" + e.FullPath + " 修改时出现异常！ 原因：", ex.Message, 4);
            }
            //Console.WriteLine("重命名: OldPath:{0} NewPath:{1} OldFileName{2} NewFileName:{3}", e.OldFullPath, e.FullPath, e.OldName, e.Name);
        }

        public void insertlog(string requesttype, string path, string message, int level)
        {
            var logger = Log.ForContext<CustomLogEvent>();

            var customLog = new CustomLogEvent
            {
              LogDate=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),LogThread="11",LogLevel= "INFO",LogLogger= "loginfo",LogMessage= message,
                LogActionClick="",RequestType= requesttype,
                Path=path,CreatedAt= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),LastModifiedAt= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            logger.Information("This is a custom log message {@CustomLogEvent}", customLog);
        }
    }
}
