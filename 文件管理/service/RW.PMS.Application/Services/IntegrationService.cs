using AutoMapper;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.DTO.Integration;
using RW.PMS.Application.Contracts.Service;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Excel;
using RW.PMS.CrossCutting.Extender;
using RW.PMS.CrossCutting.Security.Encrypt;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Integration;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Services
{
    public class IntegrationService : CrudApplicationService<IntegrationRecordEntity, int>, IIntegrationService
    {
        IRepository<UserEntity, int> userRepo;
        IRepository<OrganizationEntity, int> orgRepo;
        IRepository<PostEntity, int> postRepo;
        IFileService fileService;

        public IntegrationService(
            IDataValidatorProvider dataValidator,
            IRepository<IntegrationRecordEntity, int> repository,
            IRepository<OrganizationEntity, int> orgRepo,
            IRepository<UserEntity, int> userRepo,
            IRepository<PostEntity, int> postRepo,
            IFileService fileService,
            IMapper mapper,
            Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            this.orgRepo = orgRepo;
            this.userRepo = userRepo;
            this.fileService = fileService;
            this.postRepo = postRepo;
        }
        //导出组织架构
        public ExportOrganizationDto ExportOrganization()
        {
            ExportOrganizationDto dto = new ExportOrganizationDto();

            var tempfile = Environment.CurrentDirectory + "\\template\\导出模板.xlsx";
            var fullname = Environment.CurrentDirectory + "\\导出\\导出" + (DateTime.Now.ToString("yyyyMMddHHmmss")) + ".xlsx";
            FileInfo f = new FileInfo(fullname);
            if (!f.Directory.Exists) f.Directory.Create();
            File.Copy(tempfile, fullname);

            var excel = ExcelFactory.CreateExcel(fullname);
            DataSet ds = excel.ToDataSet();
            //TODO:填充组织机构、岗位、用户 数据
            //组织机构
            {
                var lst = orgRepo.Select
                   .From<OrganizationEntity>((t1, t2) => t1.LeftJoin(a => a.ParentId == t2.Id))
                   .ToList(x => new { x.t1, x.t2 }
               );
                foreach (var item in lst)
                {
                    ds.Tables[0].Rows.Add(
                        item.t1.Id,
                        item.t1.OrganizationName,
                        item.t2?.OrganizationName,
                        item.t1.OrderIndex,
                        item.t1.IsDeleted ? 1 : 0
                        );
                }
            }
            //岗位
            {
                var lst = postRepo.Select
                    .From<OrganizationEntity>((t1, t2) => t1.LeftJoin(a => a.OrgId == t2.Id))
                    .ToList(x => new { x.t1, x.t2 });
                foreach (var item in lst)
                {
                    ds.Tables[1].Rows.Add(
                        item.t1.Id,
                        item.t1.PostName,
                        item.t2?.OrganizationName,
                        item.t1.OrderIndex,
                        item.t1.IsDeleted ? 1 : 0
                        );
                }
            }
            //用户
            {
                var lst = userRepo.Select
                    .From<OrganizationEntity, PostEntity, UserExtenderEntity>(
                    (t1, t2, t3, t4) =>
                        t1.LeftJoin(a => a.OrgId == t2.Id)
                            .LeftJoin(a => a.PostId == t3.Id)
                            .LeftJoin(a => a.Id == t4.UserId)
                                            )
                    .ToList(x => new { x.t1, x.t2, x.t3, x.t4 });
                foreach (var item in lst)
                {
                    ds.Tables[2].Rows.Add(
                        item.t1.Id,
                        item.t1.RealName,
                        item.t1.Account,
                        item.t1.IsDeleted ? 1 : 0,
                        item.t2?.OrganizationName,
                        item.t3?.PostName,
                        item.t4?.Telnum,
                        item.t4?.Sex
                        );
                }
            }
            var data = excel.FillDataSet(ds);

            dto.Data = data;
            dto.Filename = f.Name;
            return dto;

        }

        //导入组织架构
        public int ImportOrganization(ImportOrganizationDto dto)
        {
            int sum = 0;
            var fileId = dto.File.Id;
            var file = this.fileService.GetFile(fileId);

            MemoryStream ms = new MemoryStream(file.Data);

            IExcelDataReader reader = ExcelReaderFactory.CreateReader(ms);
            ExcelDataSetConfiguration conf = new ExcelDataSetConfiguration();
            conf.ConfigureDataTable = (_) => new ExcelDataTableConfiguration { UseHeaderRow = true };
            var ds = reader.AsDataSet(conf);

            //清空组织机构表数据 FIXME 如果表名发生变化呢？
            if (dto.TruncateOrganization)
                Repository.Orm.Ado.ExecuteNonQuery("truncate table sys_organization");

            var orgs = GetOrganizations(ds.Tables["部门信息"]);
            sum += Repository.Orm.InsertOrUpdate<OrganizationEntity>().SetSource(orgs).ExecuteAffrows();

            //sum += Repository.Orm.Insert(orgs).ExecuteAffrows();

            //清空岗位表数据 FIXME 如果表名发生变化呢？
            if (dto.TruncatePost)
                Repository.Orm.Ado.ExecuteNonQuery("truncate table sys_post");
            var posts = GetPosts(ds.Tables["岗位信息"]);
            //添加成功后，自动关联ID
            foreach (var post in posts)
                post.OrgId = orgs.FirstOrDefault(x => x.OrganizationName == post.OrgName)?.Id ?? 0;
            sum += Repository.Orm.InsertOrUpdate<PostEntity>().SetSource(posts).ExecuteAffrows();
            //sum += Repository.Orm.Insert(posts).ExecuteAffrows();
            posts = postRepo.Select.ToList();

            //清空用户表数据 FIXME 如果表名发生变化呢？
            if (dto.TruncateUser)
            {
                Repository.Orm.Ado.ExecuteNonQuery("truncate table sys_users");
            }
            //扩展表直接删除
            Repository.Orm.Ado.ExecuteNonQuery("truncate table sys_user_extender");
            var userInfos = GetUsers(ds.Tables["人员信息"], dto.Password);
            var users = userInfos.Item1;
            var extenders = userInfos.Item2;
            foreach (var user in users)
            {
                user.PostId = posts.FirstOrDefault(x => x.PostName == user.PostName)?.Id ?? 0;
                user.OrgId = orgs.FirstOrDefault(x => x.OrganizationName == user.OrgName)?.Id ?? 0;
            }

            //sum += Repository.Orm.Insert(users).ExecuteAffrows();
            //Repository.Orm.Insert(extenders).ExecuteAffrows();
            sum+= Repository.Orm.InsertOrUpdate<UserEntity>().SetSource(users).ExecuteAffrows();
            Repository.Orm.InsertOrUpdate<UserExtenderEntity>().SetSource(extenders).ExecuteAffrows();

            //更新部门负责人
            orgRepo.Attach(orgs);
            foreach (var org in orgs)
            {
                var leaderId = users.FirstOrDefault(x => x.RealName == org.LeaderName)?.Id;
                if (leaderId > 0)
                    org.Leader = leaderId;
            }
            orgRepo.Update(orgs);

            return sum;

        }

        static List<OrganizationEntity> GetOrganizations(DataTable table)
        {
            List<OrganizationEntity> orgs = new List<OrganizationEntity>();

            foreach (DataRow row in table.Rows)
            {
                var orgName = GetRowLikeName(row, "上级部门");
                orgs.Add(new OrganizationEntity
                {
                    Id = row[0].ToInt32(),
                    OrganizationName = GetRowLikeName(row, "部门名称"),
                    IsDeleted = GetRowLikeName(row, "禁用状态").ToBool(),
                    OrderIndex = GetRowLikeName(row, "部门排序号").ToInt32(),
                    LeaderName = GetRowLikeName(row, "部门负责人"),
                    //ParentId = orgs.FirstOrDefault(x => x.OrganizationName == )?.Id ?? 0,
                    ParentName = orgName,
                    CreatedAt = DateTime.Now,
                    ParentId = orgs.FirstOrDefault(x => x.OrganizationName == orgName)?.Id ?? 0,
                });
            }

            return orgs;
        }

        static List<PostEntity> GetPosts(DataTable table)
        {
            List<PostEntity> post = new List<PostEntity>();

            foreach (DataRow row in table.Rows)
            {
                post.Add(new PostEntity
                {
                    Id = Convert.ToInt32(row[0]),
                    PostName = Convert.ToString(row[1]),
                    OrgName = Convert.ToString(row[2]),
                    OrderIndex = row[3].ToInt32(),
                    IsDeleted = row[4].ToBool(),
                    CreatedAt = DateTime.Now,
                });
            }
            return post;
        }

        static (List<UserEntity>, List<UserExtenderEntity>) GetUsers(DataTable table, string defaultPassword)
        {
            List<UserEntity> users = new List<UserEntity>();
            List<UserExtenderEntity> extenders = new List<UserExtenderEntity>();
            //var defaultPassword = "123456";
            foreach (DataRow row in table.Rows)
            {
                int id = row[0].ToInt32();
                users.Add(new UserEntity
                {
                    Id = id,
                    RealName = GetRowLikeName(row, "人员名称"),
                    Account = GetRowLikeName(row, "登录名"),
                    Password = EncryptProvider.Md5(defaultPassword),
                    IsDeleted = GetRowLikeName(row, "是否禁止").ToBool(),
                    CreatedAt = DateTime.Now,
                    OrgName = Convert.ToString(GetRowLikeName(row, "部门名称")),
                    PostName = Convert.ToString(GetRowLikeName(row, "岗位名称")),
                });
                extenders.Add(new UserExtenderEntity
                {
                    UserId = id,
                    Sex = Convert.ToString(GetRowLikeName(row, "性别")),
                    Telnum = Convert.ToString(GetRowLikeName(row, "手机号")),
                });
            }

            return (users, extenders);
        }

        static string GetRowLikeName(DataRow row, string name)
        {
            var cols = row.Table.Columns;
            for (int i = 0; i < cols.Count; i++)
            {
                if (cols[i].ColumnName.Contains(name))
                    return Convert.ToString(row[i]);
            }
            return String.Empty;
        }
    }
}
