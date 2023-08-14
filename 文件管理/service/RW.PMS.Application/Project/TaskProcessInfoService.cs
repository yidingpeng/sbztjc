using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Project
{
    public class TaskProcessInfoService : CrudApplicationService<TaskProcessInfoEntity, int>, ITaskProcessInfoService
    {
        public TaskProcessInfoService(IDataValidatorProvider dataValidator,
            IRepository<TaskProcessInfoEntity, int> repository,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {

        }
        /// <summary>
        /// 任务信息分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<TaskProcessInfoView> PagedList(TaskProcessInfoSearchDto input)
        {
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            var list = Repository.Orm.Select<TaskProcessInfoEntity, DictionaryEntity, FileEntity, FileEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClassID == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.InputCondition == a2.t3.Id.ToString())
                .LeftJoin(a3 => a3.t1.OutPutFile == a3.t4.Id.ToString())
                .WhereIf(input.TaskCode.NotNullOrWhiteSpace(), a => a.t1.TaskCode.Contains(input.TaskCode))
                .WhereIf(input.TaskName.NotNullOrWhiteSpace(), a => a.t1.TaskName.Contains(input.TaskName))
                .Where(a => a.t1.IsDeleted == false && a.t1.ParentId == 0)
                .OrderByDescending(a => a.t1.SeqNo)
                .Count(out var total)
                .Page(input.PageNo, input.PageSize)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                    Dictionary3 = t.t4,
                }).Select(t =>
                {
                    var Output = Mapper.Map<TaskProcessInfoView>(t.project);
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.PdfUrl = (t.Dictionary2 == null) ? "" : baseUrl[0] + t.Dictionary2.RelativePath;
                    Output.WordUrl = (t.Dictionary3 == null) ? "" : baseUrl[0] + t.Dictionary3.RelativePath;
                    Output.filePdfName = (t.Dictionary2 == null) ? "" : t.Dictionary2.FileName;
                    Output.fileWordName = (t.Dictionary3 == null) ? "" : t.Dictionary3.FileName;
                    int count = GetList().Where(s => s.ParentId == Output.Id).ToList().Count;
                    if (count > 0)
                    {
                        Output.HasChildren = true;
                    }
                    else
                    {
                        Output.HasChildren = false;
                    }
                    return Output;
                }).ToList();
            return new PagedResult<TaskProcessInfoView>(Mapper.Map<List<TaskProcessInfoView>>(list), total);
        }

        /// <summary>
        /// 根据父级ID查询信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<TaskProcessInfoView> GetByProIDList(int Id)
        {
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            var list = Repository.Orm.Select<TaskProcessInfoEntity, DictionaryEntity, AttachmentUploadEntity, AttachmentUploadEntity>()
                .LeftJoin(a1 => a1.t1.ProjectClassID == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.InputCondition == a2.t3.FileID)
                .LeftJoin(a3 => a3.t1.OutPutFile == a3.t4.FileID)
                .Where(a => a.t1.IsDeleted == false && a.t1.ParentId == Id)
                .OrderByDescending(a => a.t1.SeqNo)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                    Dictionary3 = t.t4,
                }).Select(t =>
                {
                    var Output = Mapper.Map<TaskProcessInfoView>(t.project);
                    Output.ProjectClassName = (t.Dictionary1 == null) ? "" : t.Dictionary1.DictionaryText;
                    Output.PdfUrl = (t.Dictionary2 == null) ? "" : baseUrl[0] + t.Dictionary2.RelativePath;
                    Output.WordUrl = (t.Dictionary3 == null) ? "" : baseUrl[0] + t.Dictionary3.RelativePath;
                    Output.filePdfName = (t.Dictionary2 == null) ? "" : t.Dictionary2.FileName;
                    Output.fileWordName = (t.Dictionary3 == null) ? "" : t.Dictionary3.FileName;
                    int count = GetList().Where(s => s.ParentId == Output.Id).ToList().Count;
                    if (count > 0)
                    {
                        Output.HasChildren = true;
                    }
                    else
                    {
                        Output.HasChildren = false;
                    }
                    return Output;
                }).ToList();
            return new List<TaskProcessInfoView>(list);
        }

        /// <summary>
        /// 任务信息新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(TaskProcessInfoView input)
        {
            var entity = Mapper.Map<TaskProcessInfoEntity>(input);

            base.Insert(entity);
            return true;
        }

        /// <summary>
        /// 任务信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(TaskProcessInfoView input)
        {
            base.Update(input.Id, input);
            return true;
        }
        /// <summary>
        /// 任务编号唯一
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ProOnly(string TaskCode, int Id)
        {
            var list = Repository.Select.Where(t => t.TaskCode.Equals(TaskCode) && t.Id != Id).ToList();
            return list.Count > 0;
        }

        /// <summary>
        /// 获取项目的父子集合信息
        /// </summary>
        /// <returns></returns>
        public IList<TaskProcessInfoView> GetTreeList()
        {
            var list = GetList().OrderByDescending(s => s.Id).ToList();
            var list2 = GetList();
            return BuildTreeList<TaskProcessInfoView, TaskProcessInfoEntity>(list);
        }
        /// <summary>
        /// 修改上移下移
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public bool UpdateSeqNo(int Id, int ParentId, string Type)
        {
            try
            {
                bool result = true;
                TaskProcessInfoEntity entity = Repository.Select.Where(s => s.Id == Id).First();

                int MaxNo = MaxSeqNo(ParentId);
                int MinNo = MinSeqNo(ParentId);
                int SeqNos = entity.SeqNo;
                if (Type == "Up" && SeqNos < MaxNo)
                {
                    //当前排序号的上一条数据
                    var last = Repository.Select.Where(m => m.ParentId == ParentId && m.SeqNo > entity.SeqNo).OrderBy(m => m.SeqNo).ToList().FirstOrDefault();
                    if (last != null)
                    {
                        Repository.Orm.Update<TaskProcessInfoEntity>(Id).Set(a => a.SeqNo, last.SeqNo).ExecuteAffrows();

                        Repository.Orm.Update<TaskProcessInfoEntity>(last.Id).Set(a => a.SeqNo, SeqNos).ExecuteAffrows();
                    }
                }
                else if (Type == "Down" && SeqNos > MinNo)
                {
                    //当前排序号的下一条数据
                    var first = Repository.Select.Where(m => m.ParentId == ParentId && m.SeqNo < entity.SeqNo).OrderBy(m => m.SeqNo).ToList().LastOrDefault();
                    if (first != null)
                    {
                        Repository.Orm.Update<TaskProcessInfoEntity>(Id).Set(a => a.SeqNo, first.SeqNo).ExecuteAffrows();

                        Repository.Orm.Update<TaskProcessInfoEntity>(first.Id).Set(a => a.SeqNo, SeqNos).ExecuteAffrows();
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// 获取当前层级最大排序号
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public int MaxSeqNo(int ParentId)
        {
            int MaxNo = Repository.Select.Where(s => s.ParentId == ParentId).Max(s => s.SeqNo);
            return MaxNo;
        }

        /// <summary>
        /// 获取当前层级最小排序号
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public int MinSeqNo(int ParentId)
        {
            int MinNo = Repository.Select.Where(s => s.ParentId == ParentId).Min(s => s.SeqNo);
            return MinNo;
        }
    }
}
