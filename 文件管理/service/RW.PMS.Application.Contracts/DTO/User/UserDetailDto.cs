﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.User
{
    public class UserDetailDto : UserListDto
    {
        public string Password { get; set; }
    }
}
