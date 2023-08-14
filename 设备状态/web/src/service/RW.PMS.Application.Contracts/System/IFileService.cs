using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.System;
using System.Collections.Generic;
using System.IO;

namespace RW.PMS.Application.Contracts.System
{
    public interface IFileService : ICrudApplicationService<FileEntity, int>
    {
        FileModelDto GetFile(int id);

        FileModelDto GetFile2(int id);

        //通过文件完整路径获取文件
        FileModelDto GetFileByPath(string fullPath);

        PagedResult<FileListDto> GetPagedList(FileQueryDto input);

        FileOutputDto Upload(FileInputDto input);

        void Remove(int id);
        FileReDto Upload2(FileDto input);

        /// <summary>
        /// 获取目录下所有文件夹
        /// </summary>
        /// <returns></returns>
        List<FoldersInfoDto> GetAllFolders(string path);

        /// <summary>
        /// 获取指定目录下所有文件
        /// </summary>
        /// <returns></returns>
        FileInfo[] GetFileInfo(string FolderName);

        /// <summary>
        /// 根据项目文件夹获取pdf存放文件夹
        /// </summary>
        /// <param name="ProFolder"></param>
        /// <param name="FileFolder"></param>
        /// <returns></returns>
        FoldersInfoDto GetFileByFolderName(string ProFolder, string FileFolder);

        /// <summary>
        /// 根据项目文件夹名称获取路径
        /// </summary>
        /// <param name="ProFolder"></param>
        /// <returns></returns>
        FoldersInfoDto GetFileByProFolderName(string serverUrl, string ProFolder, bool isAll);

        //给文件添加水印，并返回该文件
        FileModelDto SetWatermark(string filePath);
    }
}