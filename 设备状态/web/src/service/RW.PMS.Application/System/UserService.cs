using AutoMapper;
using Microsoft.AspNetCore.Http;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.User;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.Application.Contracts.Service;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Application.Event;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Extender;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.Encrypt;
using RW.PMS.CrossCutting.Security.Token;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using RW.PMS.Domain.Repositories.System;
using RW.PMS.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace RW.PMS.Application.System
{
    public class UserService : CrudApplicationService<UserEntity, int>, IUserService
    {
        private readonly IAuthToken _authToken;
        private readonly IRepository<ModuleEntity, int> _moduleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleOperationRepository _roleOperationRepository;
        public readonly ICurrentUser _CurrentUser;
        public readonly IUserOptionRepository _userOptionRepository;
        private readonly IFileService _fileService;
        private readonly IStatService _statSerivce;
        private readonly IEventBus _eventBus;

        public UserService(
            IDataValidatorProvider dataValidator,
            IRepository<UserEntity, int> userRepository,
            IFileService fileService,
            IStatService statSerivce,
            IMapper mapper,
            IEventBus eventBus,
            IHttpContextAccessor httpContextAccessor,
            Lazy<ICurrentUser> currentUser,
            IRepository<ModuleEntity, int> moduleRepository,
            IUserRoleRepository userRoleRepository,
            IRoleOperationRepository roleOperationRepository, ICurrentUser CurrentUser, IUserOptionRepository userOptionRepository,
            IAuthToken authToken) : base(dataValidator, userRepository, mapper, currentUser)
        {
            _moduleRepository = moduleRepository;
            _userRoleRepository = userRoleRepository;
            _authToken = authToken;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
            _roleOperationRepository = roleOperationRepository;
            _CurrentUser = CurrentUser;
            _userOptionRepository = userOptionRepository;
            _statSerivce = statSerivce;
            _eventBus = eventBus;
        }

        public LoginResultDto Login(LoginDto input)
        {
            var entity = Repository.Where(t => t.Account == input.Username.Trim()).ToOne();
            if (entity == null) throw new LogicException(ExceptionCode.PasswordError);

            if (!string.Equals(EncryptProvider.Md5(input.Password), entity.Password,
                StringComparison.OrdinalIgnoreCase))
                throw new LogicException(ExceptionCode.PasswordError);

            var last = entity.CurrentLoginTime;
            entity.CurrentLoginTime = DateTime.Now;
            entity.LastLogin = last;
            entity.LoginCount++;
            Repository.Update(entity);
            _eventBus.Trigger(new LoginEventData
            {
                Result = true,
                Username = entity.Account,
                Realname = entity.RealName,
                LastLogin = entity.LastLogin,
            });

            var dto = GetToken(entity);
            return dto;
        }

        public LoginResultDto GetToken()
        {
            var uid = CurrentUser.Value.Id;
            var entity = Repository.Get(uid);
            return GetToken(entity);
        }

        public LoginResultDto GetToken(UserEntity entity)
        {
            if (entity == null) throw new LogicException(ExceptionCode.AccountNotExist);

            var role = _userRoleRepository.Where(t => t.UserId == entity.Id).ToList();
            var token = _authToken.Create(
                new Claim(ClaimTypes.Name, entity.RealName),
                new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                new Claim(ClaimTypes.Role, string.Join(",", role.Select(t => t.RoleId)))
            );
            return new LoginResultDto { Token = token };
        }
        /// <summary>
        /// 单点登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public LoginResultDto SsoLogin(SSOLoginDto input)
        {
            var token = string.Empty;
            var entity = Repository.Where(t => t.Account == input.Username.Trim()).ToOne();
            if (entity != null) {
                var role = _userRoleRepository.Where(t => t.UserId == entity.Id).ToList();
                token = _authToken.Create(
                    new Claim(ClaimTypes.Name, entity.RealName),
                    new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                    new Claim(ClaimTypes.Role, string.Join(",", role.Select(t => t.RoleId)))
                );

                var last = entity.CurrentLoginTime;
                entity.CurrentLoginTime = DateTime.Now;
                entity.LastLogin = last;
                entity.LoginCount++;
                Repository.Update(entity);
                _eventBus.Trigger(new LoginEventData
                {
                    Result = true,
                    Username = entity.Account,
                    Realname = entity.RealName,
                    LastLogin = entity.LastLogin,
                });
            }
            return new LoginResultDto { Token = token };
        }

        public void Logout()
        {
            //TODO:服务器暂不考虑如何注销，由客户端主动销毁token即可。
        }

        public UserinfoDto GetUserInfo()
        {
            var uid = CurrentUser.Value.Id;

            var entity = Repository.Orm.Select<UserEntity, UserExtenderEntity, OrganizationEntity, PostEntity>()
                .LeftJoin(x => x.t1.Id == x.t2.UserId)
                .LeftJoin(x => x.t1.OrgId == x.t3.Id)
                .LeftJoin(x => x.t1.PostId == x.t4.Id)
                .Where(x => x.t1.Id == uid)
                .ToOne((u, e, o, p) => new { u, e, o, p });
            if (entity == null) throw new LogicException(ExceptionCode.AccountNotExist);

            var roleIds = CurrentUser.Value.RoleId;
            var roleNames = new string[0];
            var userPermission = new string[0];
            if (roleIds != null)
            {
                var lst = Repository.Orm.Select<RoleEntity>().Where(x => roleIds.Contains(x.Id)).ToList(x => x.RoleName);
                roleNames = lst.ToArray();
                //获取角色授权
                 var plist = _roleOperationRepository.GetOperationCodeForRole(CurrentUser.Value.RoleId);
                //获取用户授权
                // var userId = _CurrentUser?.Id ?? 0;
                // var cUserList = _userOptionRepository.Select.Where(u => u.UserId == userId).ToList(u => u.OperationCode);
                // foreach (var citem in cUserList)
                // {
                //     if (citem.NotNullOrEmpty())
                //     {
                //         plist.Add(citem);
                //     }
                // }
                userPermission = plist.ToArray();
            }
            UserinfoDto dto = new UserinfoDto()
            {
                Avatar = entity.e?.Avatar,
                //Roles = roleRepository.Where(x => role.Select(r => r.RoleId).Contains(x.Id)).ToList(x => x.RoleName).ToArray(),
                Permissions = userPermission,
                Roles = roleNames,
                Username = entity.u.Account,
            };



            return dto;
            //output.Roles = roleRepository.Where(x => role.Select(r => r.RoleId).Contains(x.Id)).ToList(x => x.RoleName).ToArray();
            //throw new Exception();
            //return output;

        }

        public List<UserListDto> GetUserListByIds(int[] ids)
        {
            var lst = Repository.Where(x => ids.Contains(x.Id)).ToList().Select(x => Mapper.Map<UserListDto>(x)).ToList();
            return lst;
        }

        public PagedResult<UserListDto> GetPagedList(UserSearchDto input)
        {
            //var orgQuery = Repository.Orm.Select<OrganizationEntity, OrganizationEntity>()
            //                .Where((tnode, tparent) => tnode.ParentId == tparent.Id && (tparent.Id == input.Organization || tparent.ParentId == input.Organization))
            //                .ToList(x => x.t1.Id);

            var list = Repository.Select.From<UserExtenderEntity, OrganizationEntity, PostEntity>(
                    (r, ext, o, post) =>
                        r.LeftJoin(x => x.Id == ext.UserId)
                        .LeftJoin(uo => uo.OrgId == o.Id)
                        .LeftJoin(p => p.PostId == post.Id)
                    )
                        .WhereIf(input.Username.NotNullOrWhiteSpace(), t => t.t1.RealName.Contains(input.Username))
                        .WhereIf(input.Organization != 0, t => t.t1.OrgId == input.Organization ||
                            Repository.Orm.Select<OrganizationEntity, OrganizationEntity>()
                            .Where((tnode, tparent) => tnode.ParentId == tparent.Id && (tparent.Id == input.Organization || tparent.ParentId == input.Organization))
                            .ToList((tnode, tparent) => tnode.Id)
                            .Contains(t.t1.OrgId))
                        //TODO：子查询，筛选所有子部门的用户
                        .OrderBy((r, ext, o, post) => r.Id)
                        .Count(out var total).Page(input.PageNo, input.PageSize)
                        .ToList((r, ext, o, post) => new
                        {
                            Monstd = r,
                            OrgName = o.OrganizationName,
                            Post = post.PostName,
                            Roles = string.Join(',',
                                Repository.Orm.Select<RoleEntity, UserRoleEntity>()
                                .LeftJoin(w => w.t1.Id == w.t2.RoleId)
                                .Where(w => w.t2.UserId == r.Id)
                                .ToList(a => a.t1.RoleName))
                                ,
                            Extender = ext,
                        }).Select(t =>
                        {
                            var userView = Mapper.Map<UserListDto>(t.Monstd);
                            userView.Department = t.OrgName;
                            userView.Post = t.Post;
                            userView.OrgId = t.Monstd.OrgId;
                            userView.Roles = t.Roles?.Split(',');
                            if (t.Extender != null)
                            {
                                userView.Sex = t.Extender.Sex;
                                userView.Telnum = t.Extender.Telnum;
                                userView.Avatar = t.Extender.Avatar;
                                if (t.Extender.Born.HasValue)
                                    userView.Age = (DateTime.Now.Date - t.Extender.Born.Value).Days / 365;
                            }
                            return userView;
                        }).ToList();
            return new PagedResult<UserListDto>(list, total);
        }

        public UserListDto GetUserInfoById(int id)
        {
            var list = Repository.Select.From<OrganizationEntity>((r, o) =>
                    r.LeftJoin(uo => uo.OrgId == o.Id))
                        .Where(t => t.t1.Id == id)
                        .ToList((r, o) => new
                        {
                            Monstd = r,
                            OrgName = o.OrganizationName
                        }).Select(t =>
                        {
                            var userView = Mapper.Map<UserListDto>(t.Monstd);
                            userView.Department = t.OrgName;
                            userView.OrgId = t.Monstd.OrgId;
                            return userView;
                        }).First();
            return list;
        }

        public List<UserAppOutput> GetUserList(int roleId)
        {
            List<UserAppOutput> list = Repository.Select.From<UserRoleEntity>((r, u) => r.InnerJoin(ru => ru.Id == u.UserId))
                .WhereIf(roleId > 0, (r, u) => u.RoleId == roleId)
                .ToList((r, u) => new
                {
                    model = r
                }).Select(t =>
                {
                    UserAppOutput info = new UserAppOutput();
                    info.value = t.model.Id;
                    info.text = t.model.RealName;

                    return info;
                }).ToList();
            return list;
        }
        public bool AccountExist(UserDetailDto input)
        {
            var exist = Repository.Where(t => t.Account == input.Account).ToOne();
            if (exist == null) return false;
            if (input.Id > 0)
            {
                return input.Id != exist.Id;
            }
            return true;
        }

        public UserDetailDto GetModel(int id)
        {
            UserDetailDto view = new UserDetailDto();
            view = Repository.Select
                .Where(r => r.Id == id).ToList(r =>
                    new
                    {
                        user = r,
                    })
                .Select(t =>
                {
                    var UserView = Mapper.Map<UserDetailDto>(t.user);
                    UserView.OrgId = t.user.OrgId;
                    return UserView;
                }).FirstOrDefault();
            return view;
        }

        public bool Insert(UserDetailDto input)
        {
            var entity = Mapper.Map<UserEntity>(input);
            entity.Password = GetEncryptPassword(input.Password);
            var user = base.Insert(entity);
            var ext = Mapper.Map<UserExtenderEntity>(input);
            ext.UserId = user?.Id ?? 0;
            var num = Repository.Orm.Insert(ext).ExecuteIdentity();

            var ids = Repository.Orm.Select<RoleEntity>().Where(x => input.Roles.Contains(x.RoleName) && !x.IsDeleted).ToList(x => x.Id);
            var lst = ids.Select(x => new UserRoleEntity() { UserId = user.Id, RoleId = x }).ToList();
            Repository.Orm.Insert(lst).ExecuteIdentity();

            //_logService.AddOperationLog(true, "添加成功", $"添加了用户[{input.Fullname}]");
            _eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Insert, true, input.Fullname));
            //_eventBus.Trigger();

            return true;
        }

        public bool Update(UserListDto input)
        {
            //Mapper.Map<UserEntity>()
            //1. 更新基本数据 (只能更新实体，同时不能把密码更新了，所以单独写)
            var entity = Repository.Get(input.Id);
            entity.RealName = input.Fullname;
            entity.Account = input.Account;
            entity.OrgId = input.OrgId;
            var n = base.Update(input.Id, input);


            //2. 更新扩展信息
            var extRepo = Repository.Orm.GetRepository<UserExtenderEntity>();
            var ext = extRepo.Where(x => x.UserId == input.Id).First();
            //不存在时，保证有数据
            if (ext == null)
                ext = new UserExtenderEntity();
            ext.Department = input.Department;
            ext.Sex = input.Sex;
            ext.Telnum = input.Telnum;
            ext.Avatar = input.Avatar;
            var en = extRepo.InsertOrUpdate(ext);

            //3. 用户权限重置
            int num = Repository.Orm
                .Delete<UserRoleEntity>()
                .Where(x => x.UserId == input.Id)
                .ExecuteAffrows();

            if (input.Roles != null && input.Roles.Length > 0)
            {
                var roleRepo = Repository.Orm.Select<RoleEntity>();
                var roleIds = roleRepo.Where(x => input.Roles.Contains(x.RoleName)).ToList(x => x.Id);
                var lst = roleIds.Select(x => new UserRoleEntity { UserId = input.Id, RoleId = x }).ToList();
                Repository.Orm.Insert(lst).ExecuteAffrows();
            }
            //_logService.AddOperationLog(true, "更新成功", $"修改了用户[{input.Fullname}]");
            _eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Update, true, input.Fullname));
            return true;
        }

        public bool ResetPassword(ResetPwdDto input)
        {
            var password = "123456";//TODO:可配置的密码，应该从参数或配置读取
            var user = Repository.Where(x => x.Account == input.Username && !x.IsDeleted).First();
            if (user == null) throw new Exception("账号不存在");
            user.Password = GetEncryptPassword(password);
            int num = Repository.Update(user);
            //_logService.AddOperationLog(true, "更新成功", $"重置了用户密码[{user.RealName}]");
            _eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Update, true, user.RealName));
            return num > 0;
        }

        static string GetEncryptPassword(string password)
        {
            return EncryptProvider.Md5(password);
        }
        public PersonalOutputDto GetPersonal()
        {
            var id = CurrentUser.Value.Id;
            //var id = 11;
            var user = Repository.Orm.Select<UserEntity, UserExtenderEntity>()
                .LeftJoin<UserExtenderEntity>((u, e) => u.Id == e.UserId)
                .Where((u, e) => u.Id == id)
                .ToList((u, e) =>
                    new
                    {
                        user = u,
                        extender = e,
                    })
                .Select(t =>
                {
                    PersonalOutputDto view = new PersonalOutputDto();
                    view.Personal = Mapper.Map<PersonalInfoDto>(t.extender);
                    view.UserInfo = Mapper.Map<PersonalUserDto>(t.user);
                    if (view.Personal == null)
                        view.Personal = new PersonalInfoDto();
                    view.Personal.Fullname = t.user.RealName;
                    view.UserInfo.Organization = t.extender.Department;
                    var roles = Repository.Orm.Select<RoleEntity, UserRoleEntity>().Where(x => x.t1.Id == x.t2.RoleId && x.t2.UserId == id).ToList(x => x.t1.RoleName);
                    view.UserInfo.Role = roles.ArrayToString();
                    return view;
                }).FirstOrDefault();
            return user;
        }

        public bool SavePersonal(PersonalInputDto personal)
        {
            var id = CurrentUser.Value.Id;
            //var id = 11;
            var entity = Repository.Get(id);
            entity.RealName = personal.Fullname;
            Repository.Update(entity);

            var extenderRepo = Repository.Orm.GetRepository<UserExtenderEntity>();
            var extender = extenderRepo.Select.Where(x => x.UserId == id).ToOne();
            if (extender == null)
            {
                extender = new UserExtenderEntity();
                extender.UserId = id;
            }
            Mapper.Map(personal, extender);
            var enti = extenderRepo.InsertOrUpdate(extender);
            //_logService.AddOperationLog(true, "更新成功", $"更新了个人资料。");
            _eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Update, true, "个人资料"));

            return true;
        }

        public bool ChangePassword(ChangePwdDto input)
        {
            if (string.IsNullOrEmpty(input.Old))
                throw new LogicException(ExceptionCode.PasswordError, "请输入原始密码。");
            var user = Repository.Where(x => x.Id == CurrentUser.Value.Id && !x.IsDeleted).First();
            if (user == null) throw new Exception("账号不存在");
            if (GetEncryptPassword(input.Old) != user.Password)
                throw new LogicException(ExceptionCode.PasswordError, "原始密码不正确。");
            user.Password = GetEncryptPassword(input.New);
            int num = Repository.Update(user);
            //_logService.AddOperationLog(true, "更新成功", $"修改了个人密码。");
            _eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Update, true, "个人资料"));
            return num > 0;
        }

        public bool UploadAvatar(string base64)
        {
            var uid = CurrentUser.Value.Id;
            var orm = Repository.Orm.Select<UserExtenderEntity>();
            var user = orm.Where(x => x.UserId == uid).First();
            if (user == null) throw new LogicException(ExceptionCode.AccountNotExist);

            var arr = base64.Split(';');
            var buffer = Convert.FromBase64String(arr[1].Split(',')[1]);
            string filename = $"{Guid.NewGuid()}.png";

            //FileInfo f = new FileInfo(filename);
            //if (!f.Directory.Exists) f.Directory.Create();

            //FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            //fs.Write(buffer, 0, buffer.Length);
            //fs.Flush();
            //fs.Close();

            var output = _fileService.Upload(new Contracts.DTO.File.FileInputDto
            {
                Data = buffer,
                Filename = filename,
                ContentType = arr[0],
                RootPath = "avatar",
            });
            user.Avatar = output.Id.ToString();
            var count = orm.ToUpdate().SetSource(user).ExecuteAffrows();
            return count > 0;
        }

        public UserListDto GetUserTelById(int id)
        {
            var list = Repository.Select.From<UserExtenderEntity>((u, ex) =>
                    u.LeftJoin(u => u.Id == ex.UserId))
                        .Where(t => t.t1.Id == id)
                        .ToList((u, ex) => new
                        {
                            Monstd =u,
                            telnum = ex.Telnum
                        }).Select(t =>
                        {
                            var userView = Mapper.Map<UserListDto>(t.Monstd);
                            userView.Telnum = t.telnum;
                            return userView;
                        }).First();
            return list;
        }
        /// <summary>
        /// 通过账号查询用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public UserListDto GetUserTelByAccount(string account)
        {
            var list = Repository.Select.From<UserExtenderEntity>((u, ex) =>
                    u.LeftJoin(u => u.Id == ex.UserId))
                        .Where(t => t.t1.Account == account)
                        .ToList((u, ex) => new
                        {
                            Monstd = u,
                            telnum = ex.Telnum
                        }).Select(t =>
                        {
                            var userView = Mapper.Map<UserListDto>(t.Monstd);
                            userView.Telnum = t.telnum;
                            return userView;
                        }).FirstOrDefault();
            return list;
        }

        public List<UserListDto> GetUserTelByroleId(int roleId)
        {
            var list = Repository.Select.From<UserExtenderEntity,UserRoleEntity>((u, ex, r) =>
                    u.LeftJoin(u => u.Id == ex.UserId)
                    .InnerJoin(u=>u.Id==r.UserId))
                        .Where(t => t.t3.RoleId == roleId)
                        .ToList((u, ex,r) => new
                        {
                            Monstd = u,
                            telnum = ex.Telnum
                        }).Select(t =>
                        {
                            var userView = Mapper.Map<UserListDto>(t.Monstd);
                            userView.Telnum = t.telnum;
                            return userView;
                        }).ToList();
            return list;
        }

        /// <summary>
        /// 查询当前登录人的信息
        /// </summary>
        /// <returns></returns>
        public UserListDto GetUserById()
        {
            var uid = CurrentUser.Value.Id;
            var info = Repository.Where(o => o.Id == uid).ToOne();
            return Mapper.Map<UserListDto>(info);
        }
    }
}