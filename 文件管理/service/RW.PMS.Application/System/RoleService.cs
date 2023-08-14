using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Role;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.AOP.Attributes;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.Authorization;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using RW.PMS.Domain.Repositories.System;

namespace RW.PMS.Application.System;

public class RoleService : CrudApplicationService<RoleEntity, int>, IRoleService
{
    private readonly IEventBus _eventBus;
    private readonly ILogService _log;
    private readonly IRepository<ModuleEntity, int> _moduleRepository;
    private readonly IRoleModuleRepository _roleModuleRepository;
    private readonly IRoleOperationRepository _roleOperationRepository;

    public RoleService(IDataValidatorProvider dataValidator,
        IRepository<RoleEntity, int> roleRepository,
        IMapper mapper,
        Lazy<ICurrentUser> currentUser,
        IRepository<ModuleEntity, int> moduleRepository,
        IRoleOperationRepository roleOperationRepository,
        IEventBus eventBus,
        ILogService log, IRoleModuleRepository roleModuleRepository) : base(dataValidator,
        roleRepository, mapper, currentUser)
    {
        _roleOperationRepository = roleOperationRepository;
        _moduleRepository = moduleRepository;
        _eventBus = eventBus;
        _log = log;
        _roleModuleRepository = roleModuleRepository;
    }

    public PagedResult<RoleListDto> GetPagedList(RoleSearchDto input)
    {
        var list = Repository
            .WhereIf(input.Role.NotNullOrWhiteSpace(), t => t.RoleName.Contains(input.Role))
            .OrderByDescending(o => o.Id)
            .Count(out var total)
            .Page(input.PageNo, input.PageSize)
            .ToList()
            .Select(t => Mapper.Map<RoleListDto>(t)).ToList();
        return new PagedResult<RoleListDto>(list, total);
    }
    /// <summary>
    /// 查询所有角色信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public List<RoleListDto> GetAllList()
    {
        var list = Repository.Select
            .ToList()
            .Select(t => Mapper.Map<RoleListDto>(t)).ToList();
        return list;
    }


    public int ChangeActive(int id, bool isActive)
    {
        var entity = Repository.Get(id);
        entity.IsActive = isActive;
        return Repository.Update(entity);
    }

    public bool RoleAuth(int roleId, string[] operation)
    {
        _roleOperationRepository.Delete(t => t.RoleId == roleId);
        if (operation.Any())
        {
            var moduleIds = _moduleRepository.Where(x => operation.Contains(x.Path)).ToList(x => x.Id);
            var list = moduleIds.Select(i => new RoleOperationEntity {RoleId = roleId, OperationId = i}).ToList();
            _roleOperationRepository.Insert(list);
        }

        return true;
    }

    public bool Check(int[] roles, string module, OperationType operation)
    {
        if (roles.Length == 0) return false;
        return _roleOperationRepository
            .WhereIf(roles.Length == 1, t => t.RoleId == roles[0] && t.OperationId == 1)
            .WhereIf(roles.Length > 1, t => roles.Contains(t.RoleId) && t.OperationId == 1)
            .Any();
    }

    public bool NameExist(string roleName)
    {
        var exist = Repository.Where(t => t.RoleName == roleName).ToOne();
        if (exist == null) return false;
        return true;
    }

    public PagedResult<string> GetRoleAuth(int roleId)
    {
        var arr = _moduleRepository.Select
            .LeftJoin<RoleOperationEntity>((a, b) => a.Id == b.OperationId)
            .Where<RoleOperationEntity>(a => a.RoleId == roleId)
            .ToList(x => x.Path);
        //保证至少能有首页权限
        if (arr.Count == 0) arr.Add("index");
        return new PagedResult<string>(arr, arr.Count);
    }

    public bool Update(RoleDetailDto input)
    {
        var result = base.Update(input.Id, input) > 0;
        result &= RoleAuth(input.Id, input.TreeArray.ToArray());
        _log.AddOperationLog(true, "更新成功", $"更新了角色[{input.Role}]");
        return result;
    }

    public RoleEntity Insert(RoleDetailDto input)
    {
        var result = base.Insert(input);
        RoleAuth(result.Id, input.TreeArray);
        _log.AddOperationLog(true, "添加成功", $"添加了角色[{input.Role}]");
        return result;
    }

    public override int Delete(int[] keys)
    {
        var ops = _roleOperationRepository.Delete(x => keys.Contains(x.RoleId));
        _log.AddOperationLog(true, "删除成功", $"添加了删除[{keys.ArrayToString()}]");
        return base.Delete(keys);
    }

    [Transaction]
    public void UpdateRolePermission(RolePermissionDto dto)
    {
        // //删除角色操作表
        _roleOperationRepository.Delete(t => t.RoleId == dto.RoleId);
        // //删除角色模块表
        _roleModuleRepository.Delete(t => t.RoleId == dto.RoleId);
        _roleOperationRepository.Insert(dto.RoleId, dto.Operation);
         _roleModuleRepository.Insert(dto.RoleId, dto.Module);
    }
}