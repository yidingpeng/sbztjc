using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Module;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using RW.PMS.Domain.Repositories.System;

namespace RW.PMS.Application.System;

public class ModuleService : CrudApplicationService<ModuleEntity, int>, IModuleService, IDataVerification
{
    private readonly IOperationRepository _operationRepository;
    private readonly IRoleOperationRepository _roleOperationRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserOptionRepository _userOptionRepository;

    public ModuleService(IDataValidatorProvider dataValidator, IRepository<ModuleEntity, int> moduleRepository,
        IMapper mapper,
        Lazy<ICurrentUser> currentUser, IOperationRepository operationRepository,
        IRoleOperationRepository roleOperationRepository,
        IUserRoleRepository userRoleRepository, IUserOptionRepository userOptionRepository) :
        base(dataValidator, moduleRepository, mapper, currentUser)
    {
        _operationRepository = operationRepository;
        _roleOperationRepository = roleOperationRepository;
        _userRoleRepository = userRoleRepository;
        _userOptionRepository = userOptionRepository;
    }

    public DataValidatorResult TryValidate()
    {
        return new DataValidatorResult
        {
            IsValid = false,
            Result = new List<FieldValidationResult>
            {
                //new("ModuleCode","重复数据")
            }
        };
    }

    public IList<RouterOutput> GetRouter()
    {
        //todo: 临时方案
        var roleModule = Repository.Select.From<RoleModuleEntity>(
                (m, rm) => m
                    .InnerJoin(t => t.Id == rm.ModuleId))
            .Where(t => CurrentUser.Value.RoleId.Contains(t.t2.RoleId))
            .ToList(t => t.t1.Id);
        var list = Repository.Where(t => roleModule.Contains(t.Id) || t.Required).ToList();
        return BuildTreeList<RouterOutput, ModuleEntity>(list, 0, x => x.OrderIndex);
    }

    public IList<ModuleListDto> GetTreeList()
    {
        var list = GetList();
        var treeList = BuildTreeList<ModuleListDto, ModuleEntity>(list, 0, x => x.OrderIndex);
        return treeList;
    }

    public IList<ModuleWithOperationDto> GetTreeListForRole(int roleId)
    {

        var moduleList = GetList();
        var operationList = _operationRepository.GetList();
        var treeList = BuildTreeList<ModuleWithOperationDto, ModuleEntity>(moduleList);
        var roleChecked = _roleOperationRepository.Where(t => t.RoleId == roleId).ToList(t => t.OperationId);
        SetModuleOperation(treeList, operationList, roleChecked);
        return treeList;
    }

    public IList<ModuleWithOperationDto> GetTreeListForUser(int userId)
    {
        var moduleList = GetList();
        var operationList = _operationRepository.GetList();
        var treeList = BuildTreeList<ModuleWithOperationDto, ModuleEntity>(moduleList);
        var userRole = _userRoleRepository.Where(t => t.UserId == userId).ToList().Select(t => t.RoleId);
        var roleChecked = _roleOperationRepository.Where(t => userRole.Contains(t.RoleId)).ToList(t => t.OperationId)
            .Distinct().ToList();
        var userChecked = _userOptionRepository.Where(t => t.UserId == userId).ToList(t => t.OperationId);
        SetModuleOperation(treeList, operationList, roleChecked, userChecked);
        return treeList;
    }

    public IList<OperationListDto> GetOperation(int moduleId)
    {
        var list = _operationRepository.GetList(moduleId);
        return Mapper.Map<List<OperationListDto>>(list);
    }

    private void SetModuleOperation(List<ModuleWithOperationDto> list, List<OperationEntity> opList, List<int> roleChecked, List<int> userChecked = null)
    {
        foreach (var item in list)
        {
            var itemOpList = opList.Where(t => t.ModuleId == item.Id).ToList();
            var itemOpId = itemOpList.Select(t => t.Id).ToList();
            item.Operation = Mapper.Map<List<OperationDto>>(itemOpList);
            if (roleChecked.Any())
            {
                item.RoleChecked = itemOpId.Intersect(roleChecked).ToList();
            }
            if (userChecked != null && userChecked.Any())
            {
                item.UserChecked = itemOpId.Intersect(userChecked).ToList();
            }

            if (item.Children != null && item.Children.Any())
                SetModuleOperation(item.Children, opList, roleChecked, userChecked);
        }
    }

    public bool HasModuleOperation(int userId, string operation)
    {
        //select *  from sys_user_role a,sys_users b,sys_role_operation c,sys_operation d where a.UserId=b.id and a.RoleId=c.RoleId and c.OperationId=d.Id and Account='admin' and d.OperationCode='bom:manager'
        return this.Repository.Orm.Select<UserEntity, UserRoleEntity, RoleOperationEntity, OperationEntity>()
             //内联关系
             .Where((a, b, c, d) => a.Id == b.UserId && b.RoleId == c.RoleId && c.OperationId == d.Id)
             //用户权限
             .Where((a, b, c, d) => a.Id == userId && d.OperationCode == operation)
             .Any();
    }

    public bool HasModuleOperation(string operation)
    {
        var uid = CurrentUser.Value.Id;
        return HasModuleOperation(uid, operation);
    }

    public int Sort(SortDto dto)
    {
        var item = Get(dto.Id);
        var srcIndex = item.OrderIndex;
        int c = 0;
        //上移
        if (dto.Index < srcIndex)
        {
            c = Repository.Orm.Update<ModuleEntity>().Set(x => x.OrderIndex + 1).Where(x => x.OrderIndex > dto.Index && x.OrderIndex < srcIndex).ExecuteAffrows();
        }
        else
        {
            c = Repository.Orm.Update<ModuleEntity>().Set(x => x.OrderIndex - 1).Where(x => x.OrderIndex > srcIndex && x.OrderIndex < dto.Index).ExecuteAffrows();
        }
        return c;
    }

    public bool UpdateSort(SortDto dto)
    {
        var count = Repository.Orm.Update<ModuleEntity>(dto.Id).Set(x => x.OrderIndex, dto.Index).ExecuteAffrows();
        return count > 0;
    }
}