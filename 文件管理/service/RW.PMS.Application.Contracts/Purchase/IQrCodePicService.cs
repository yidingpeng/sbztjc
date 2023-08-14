using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Problem;
using RW.PMS.Domain.Entities.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Purchase
{
    public interface IQrCodePicService : ICrudApplicationService<Qr_codePicEntity, int>
    {
        /// <summary>
        /// 上传质检图片（二维码表）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FileReDto Upload(FileDto input);

        /// <summary>
        /// 修改已上传的图片pid
        /// </summary>
        /// <param name="imgFilesId"></param>
        /// <param name="pId"></param>
        void UpdateImgPid(string imgFilesId, int? pId);
    }
}
