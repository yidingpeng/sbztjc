﻿using RW.PMS.Domain.Entities.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Purchase
{
    public interface IProductIssuService:ICrudApplicationService<Mat_ProductIssuEntity, int>
    {

    }
}
