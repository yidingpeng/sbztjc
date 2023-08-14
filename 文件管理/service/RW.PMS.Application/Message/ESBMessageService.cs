using AutoMapper;
using Microsoft.Extensions.Configuration;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Message
{
    public class ESBMessageService : ApplicationService, IESBMessageService
    {
        IRepository<UserEntity, int> userRepo;
        private readonly IConfiguration _configuration;
        ThirdPartService thirdPartService;
        public ESBMessageService(IDataValidatorProvider dataValidator,
                                 IMapper mapper,
                                 Lazy<ICurrentUser> currentUser,
                                 IConfiguration configuration,
                                 IRepository<UserEntity, int> userRepo,
                                 ThirdPartService thirdPartService
                                 ) : base(dataValidator, mapper, currentUser)
        {
            this.userRepo = userRepo;
            this.thirdPartService = thirdPartService;
            this._configuration = configuration;
        }

        public string SendToUser(string returnUrl, string title, params string[] users)
        {
            if (users == null || users.Length == 0) throw new LogicException("发送的用户不能为空。");

            var tel = userRepo.Orm.Select<UserEntity, UserExtenderEntity>()
                 .Where((a, b) => a.Id == b.UserId)
                 .Where((a, b) => users.Contains(a.RealName))
                 .ToList((a, b) => b.Telnum);

            var myArrayList = tel.Select(x => new { type = "user", id = x }).ToArray();

            string esburl = _configuration["ESBUrl:key"] + "/sendMessage";
            var data = new ESBSendMessageInput
            {
                Title = title,
                Type = 1,
                Desc = "",
                Link = returnUrl,
                From = "PLM",
                TargetPlantform = "OA",
                Targets = myArrayList,
                SendMode = 0,
                SendModeValue = ""
            };

            var result = thirdPartService.PostRequestAsync(esburl, data);
            return result.Result;
        }
    }
}
