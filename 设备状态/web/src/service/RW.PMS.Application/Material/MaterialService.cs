using System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.Domain.Repositories;
using AutoMapper;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.Material;
using RW.PMS.Domain.Entities.Material;
using RW.PMS.Application.Contracts.DTO.Material;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.CrossCutting.Extensions;
using System.Collections.Generic;
using RW.PMS.Application.Contracts.Message;
using Newtonsoft.Json;
using RW.PMS.Application.Contracts.Output.Material;
using Newtonsoft.Json.Linq;

namespace RW.PMS.Application.Problem
{
    public class MaterialService : CrudApplicationService<MaterialEntity, int>, IMaterialService
    {
        private readonly IProjectBasicsService _projectBasicsService;
        private readonly IThirdPartService _thirdPartService;
        public MaterialService(IDataValidatorProvider dataValidator, IProjectBasicsService projectBasicsService,
           IRepository<MaterialEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser,IThirdPartService thirdPartService) : base(
           dataValidator, repository, mapper, currentUser)
        {
            _projectBasicsService = projectBasicsService;
            _thirdPartService = thirdPartService;
        }
        /// <summary>
        /// 物料查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<MaterialEntity> GetPageList(Contracts.DTO.Material.MaterialSearchDto input,string url)
        {
            var param = new PageParam
            {
                pageIndex = input.PageNo,
                pageSize = input.PageSize
            };
            var datas = _thirdPartService.PostRequestAsync(url, param);
            JObject obj = JObject.Parse(datas.Result);
            string jsonStr = obj["data"].ToString();
            string total = obj["total"].ToString();
            List<MaterialOutput> jsondata = JsonConvert.DeserializeObject<List<MaterialOutput>>(jsonStr);
            List<MaterialEntity> materialList = new List<MaterialEntity>();
            foreach (var item in jsondata)
            {
                materialList.Add(new MaterialEntity
                {
                    MaterialCode=item.code,
                    MaterialName=item.name,
                    CodeName=item.alias,
                    Specific=item.model,
                    requirements=item.specification,
                    material=item.material,
                    Unit=item.basicUnit,
                    MaterialSource=item.source,
                    Importance=item.important,
                    CurrentState=item.codingStatus,
                    Weight=item.weight,
                    WeightUnit=item.weightUnit,
                    RulePath=item.categoryPath,
                    Category=item.categoryMaster,
                    Remark=item.remark,
                    ApplicationTime=item.createTime,
                    ReferencePrice=item.refPrice,
                    PriceUnit=item.priceUnit
                });
            }
            return new PagedResult<MaterialEntity>(materialList, Convert.ToInt32(total));
        }

        /// <summary>
        /// 物料换算基础信息查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<MaterialConversionEntity> GetHsPageList(MaterialConversionSearchDto input)
        {
            var list = Repository.Orm.Select<MaterialConversionEntity>()
                .WhereIf(input.Code.IsNullOrEmpty(), o => o.Code.Contains(input.Code))
                .WhereIf(input.Name.IsNullOrEmpty(), o => o.Name.Contains(input.Name))
                .Count(out var total)
                .OrderByDescending(t => t.Id)
                .Page(input.PageNo, input.PageSize).ToList();
            return new PagedResult<MaterialConversionEntity>(list, total);
        }

        public bool IsExist(MaterialDto input)
        {
            var exist = Repository.Where(t => t.MaterialCode == input.MaterialCode).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }

        /// <summary>
        /// 批量增加
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public bool BatchDoAdd(List<MaterialEntity> datas)
        {
            try
            {
                Repository.Orm.InsertOrUpdate<MaterialEntity>()
                    .SetSource(datas)
                    //.IfExistsDoNothing()//如果数据存在，啥事也不干
                    .ExecuteAffrows();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        /// <summary>
        /// 逻辑删除物料
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <returns></returns>
        public bool DeleteMaterial(string MaterialCode)
        {
            try
            {
                Repository.Orm.Update<MaterialEntity>()
                        .Set(a => a.IsDeleted, true)
                        .Where(a => a.MaterialCode == MaterialCode).ExecuteAffrows();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// 物料换算新增
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public bool MatHsDoAdd(MaterialConversionDto input)
        {
            try
            {
                var mapp = Mapper.Map<MaterialConversionEntity>(input);
                int result = Repository.Orm.Insert(mapp).ExecuteAffrows();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// 物料换算编辑
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public bool MatHsDoEdit(MaterialConversionDto input)
        {
            try
            {
                var mapp = Mapper.Map<MaterialConversionEntity>(input);
                int result = Repository.Orm.Update<MaterialConversionEntity>().SetSource(mapp).ExecuteAffrows();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// 物料换算删除
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public bool MatHsDoDelete(int Id)
        {
            try
            {
                int result = Repository.Orm.Update<MaterialConversionEntity>(Id).Set(a => a.IsDeleted, true).ExecuteAffrows();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
    public class PageParam
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
