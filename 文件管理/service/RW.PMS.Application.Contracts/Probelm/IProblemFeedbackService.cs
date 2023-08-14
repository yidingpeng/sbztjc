using RW.PMS.Domain.Entities.Probelem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Problem;

namespace RW.PMS.Application.Contracts.Probelm
{
    public interface IProblemFeedbackService : ICrudApplicationService<ProblemfeedbackEntity, int>
    {
        PagedResult<ProblemFeedbackDto> GetPagedList(ProblemFeedbackSerchDto input);

        /// <summary>
        /// 根据查询文本获取所有问题反馈信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<ProblemFeedbackDto> GetAllList(string key, int PageNo, int PageSize);

        /// <summary>
        /// 根据id查询问题反馈信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ProblemFeedbackDto> GetAllListById(int id);

        /// <summary>
        /// 判断该项目号下是否已存在该问题类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(ProblemFeedbackDto input);
        /// <summary>
        /// 编辑问题反馈状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool UpdateProState(int Id, string proState);

        /// <summary>
        /// 发送消息给接收人员进行问题处理
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="userId"></param>
        /// <param name="link"></param>
        /// <param name="esburl"></param>
        /// <returns></returns>
        bool MessagePush(int proId, int userId, string esburl);

        /// <summary>
        /// 根据ID获取问题反馈信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ProblemFeedbackDto GetProblemFeedbackInfo(int Id);

        /// <summary>
        /// 发送消息给责任判定通知人
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="esburl"></param>
        /// <returns></returns>
        bool MessagePushPD(int proId, string esburl);

        /// <summary>
        /// 发送消息给质量检验通知人
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="esburl"></param>
        /// <returns></returns>
        bool MessagePushJY(int proId, string esburl);

        /// <summary>
        /// 所有问题反馈信息集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ProblemFeedbackDto> GetAllList(ProblemFeedbackSerchDto input);
    }
}
