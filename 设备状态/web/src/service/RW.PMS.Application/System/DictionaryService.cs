using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;

namespace RW.PMS.Application.System
{
    public class DictionaryService : CrudApplicationService<DictionaryEntity, int>, IDictionaryService
    {
        public DictionaryService(IDataValidatorProvider dataValidator, IRepository<DictionaryEntity, int> repository,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
        }

        public DictionaryView GetView(int id)
        {
            var select = Repository.Select
                .From<DictionaryEntity>((c, p) => c
                    .LeftJoin(a => a.ParentId == p.Id))
                .Where(t => t.t1.Id == id)
                .ToOne(t => new
                {
                    Entity = t.t1,
                    Parent = new { t.t2.Id, t.t2.DictionaryText }
                });
            var view = Mapper.Map<DictionaryView>(select.Entity);
            view.Parent = new ExpandoObject();
            view.Parent.Id = select.Parent.Id;
            view.Parent.DictionaryText = select.Parent.DictionaryText;
            return view;
        }

        public PagedResult<DictionaryOutput> GetPagedList(DictionarySearchInput input)
        {
            var list = Repository.Select
                .WhereIf(input.ParentId.HasValue, t => t.ParentId == input.ParentId.Value)
                .WhereIf(input.DictionaryText.NotNullOrWhiteSpace(),
                    t => t.DictionaryText.Contains(input.DictionaryText))
                .WhereIf(input.DictionaryValue.NotNullOrWhiteSpace(),
                    t => t.DictionaryValue.Contains(input.DictionaryValue))
                .Count(out var total)
                .OrderByDescending(a=>a.Id)
                .Page(input.PageNo, input.PageSize).ToList();
            return new PagedResult<DictionaryOutput>(Mapper.Map<List<DictionaryOutput>>(list), total);
        }

        public IList<DictionaryOutput> GetList(DictionarySearchInput input)
        {
            var list = Repository.Select
                .WhereIf(input.ParentId.HasValue, t => t.ParentId == input.ParentId.Value)
                .WhereIf(input.DictionaryText.NotNullOrWhiteSpace(),
                    t => t.DictionaryText.Contains(input.DictionaryText))
                .WhereIf(input.DictionaryValue.NotNullOrWhiteSpace(),
                    t => t.DictionaryValue.Contains(input.DictionaryValue))
                .OrderByDescending(a=>a.Id)
                .ToList();
            return Mapper.Map<List<DictionaryOutput>>(list);
        }

        public IList<DictionaryOutput> GetSubItemList(string value)
        {
            var model = Repository.Select
                .WhereIf(value.NotNullOrWhiteSpace(), t => t.DictionaryValue == value)
                .First();

            if (model == null) return new List<DictionaryOutput>();

            var list = Repository.Select.Where(t => t.ParentId == model.Id).ToList();

            return Mapper.Map<List<DictionaryOutput>>(list);
        }

        public PagedResult<DictionaryOutput> GetPageList(DictionarySearchInput input)
        {
            var model = Repository.Select
                .WhereIf(input.DictionaryValue.NotNullOrWhiteSpace(), t => t.DictionaryValue == input.DictionaryValue)
                .First();

            if (model == null)
                return new PagedResult<DictionaryOutput>(
                    Mapper.Map<List<DictionaryOutput>>(new List<DictionaryOutput>()), 0);

            var list = Repository.Select
                .Where(t => t.ParentId == model.Id)
                .Count(out var total)
                .Page(input.PageNo, input.PageSize).ToList();

            return new PagedResult<DictionaryOutput>(Mapper.Map<List<DictionaryOutput>>(list), total);
        }

        public List<DictionaryAppOutput> GetDictionaryList(string dictionaryValue)
        {
            var model = Repository.Select
                  .Where(t => t.DictionaryValue == dictionaryValue)
                  .First();

            if (model == null) return new List<DictionaryAppOutput>();

            var list = Repository.Select
                 .Where(r => r.ParentId == model.Id)
                 .ToList(r => new
                 {
                     model = r
                 }).Select(t =>
                 {
                     DictionaryAppOutput info = new DictionaryAppOutput();
                     info.value = t.model.Id;
                     info.text = t.model.DictionaryText;

                     return info;
                 }).ToList();

            return list;
        }

        public bool DictionaryTextExist(DictionaryInput input, string type)
        {
            var exist = new DictionaryEntity();
            switch (type)
            {
                case "DictionaryText":
                    exist = Repository.Where(t => t.DictionaryText == input.DictionaryText).ToOne();
                    if (exist == null) return false;
                    if (input.Id.HasValue)
                        return input.Id.Value != exist.Id;
                    break;
                case "DictionaryValue":
                    exist = Repository.Where(t => t.DictionaryValue == input.DictionaryValue).ToOne();
                    if (exist == null) return false;
                    if (input.Id.HasValue)
                        return input.Id.Value != exist.Id;
                    break;
                case "Sort":
                    exist = Repository.Select.From<DictionaryEntity>(
                        (r, d) => r.LeftJoin(a => a.ParentId == d.Id)
                        .Where(t => t.ParentId == input.ParentId)
                        .Where(t => t.Sort == input.Sort)
                        ).ToOne();
                    if (exist == null) return false;
                    if (input.Id.HasValue)
                        return input.Id.Value != exist.Id;
                    break;
            }
            return true;
        }
        public int GetMaxCode()
        {
            return Repository.Select.Max(t => t.Sort) + 1;
        }

        public int DeleteAndChildren(int[] ids)
        {
            int oneselfDelResult = Repository.Delete(ids);
            int childrenDelResult = Repository.Delete(t => ids.Contains(t.ParentId));
            return oneselfDelResult;
        }
        public List<KeyValueDto> GetKeyValue(string type)
        {
            return Repository.Select
                 .From<DictionaryEntity>((x, p) => x.LeftJoin(a => a.ParentId == p.Id))
                 .Where(x => x.t2.DictionaryText.Contains(type))
                 .ToList()
                 .Select(x => new KeyValueDto(x.DictionaryText, x.DictionaryValue))
                 .ToList()
                 ;
        }

        public Dictionary<string, List<KeyValueDto>> GetAll()
        {
            var lst = Repository.Select.ToList();
            var parents = lst.Where(x => x.ParentId == 0)
                .ToDictionary(
                x => x.DictionaryValue,
                x => lst.Where(a => a.ParentId == x.Id)
                        .Select(a => new KeyValueDto(a.DictionaryText, a.DictionaryValue))
                        .ToList()
                );

            return parents;
        }

        List<KeyValueDto> types = null;
        public string GetCacheValue(string type, string key, string defaultValue)
        {
            if (types == null)
                types = GetKeyValue(type);
            return types.Where(x => x.Key == key).FirstOrDefault()?.Value ?? defaultValue ?? key;
        }
    }
}