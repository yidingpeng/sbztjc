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
    public class PaymentCollectionPlanService : CrudApplicationService<PaymentCollectionPlanEntity, int>, IPaymentCollectionPlanService
    {
        public PaymentCollectionPlanService(IDataValidatorProvider dataValidator,
           IRepository<PaymentCollectionPlanEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {
        }
        
        public List<PaymentCollectionPlanDto> GetListByConId(int contractid)
        {
            var list = Repository.Select
                .Where(t => t.Ct_ID == contractid && t.IsDeleted==false)
                .ToList((t) => new
                {
                    entity = t
                }).Select(t =>
                {
                    var output = Mapper.Map<PaymentCollectionPlanDto>(t.entity);
                    return output;
                }).ToList();
            return list;
        }

        public bool HasHhisItem(PaymentCollectionPlanDto item)
        {
            var list = Repository.Select
                .Where(t => t.Ct_ID == item.Ct_ID&& t.PaymentCTypeID.Equals(item.PaymentCTypeID))
                .ToList((t) => new
                {
                    entity = t
                }).Select(t =>
                {
                    var output = Mapper.Map<PaymentCollectionPlanDto>(t.entity);
                    return output;
                }).ToList();
            return list.Any();
        }

        public void UpdatePaymentCPlan(string newidstr,int CtId)
        {
            List<PaymentCollectionPlanDto> list = new List<PaymentCollectionPlanDto>();
            if (newidstr != null)
            {
                string[] newIds = newidstr.Split(',');
                foreach (var item in newIds)
                {
                    if (item!="")
                    {
                        var itemData = Repository.Select
                       .Where(t => t.Id == Convert.ToInt32(item))
                       .ToList((t) => new
                       {
                           entity = t
                       }).Select(t =>
                       {
                           var output = Mapper.Map<PaymentCollectionPlanDto>(t.entity);
                           return output;
                       }).First();
                        if (itemData != null)
                        {
                            list.Add(itemData);
                        }
                    }
                }
            }
            foreach (var item in list)
            {
                Update(item.Id.Value, new PaymentCollectionPlanDto
                { Id=item.Id, Ct_ID = CtId, PaymentCTypeID = item.PaymentCTypeID, PaymentCProportion = item.PaymentCProportion, ConAmountCPlan = item.ConAmountCPlan, Edit = item.Edit });
            }
        }

        public void DeleteAllCtIdZero(string newidstr)
        {
            List<PaymentCollectionPlanDto> list = new List<PaymentCollectionPlanDto>();
            if (newidstr!=null)
            {
                string[] newIds = newidstr.Split(',');
                foreach (var item in newIds)
                {
                    if (item != "")
                    {
                        var itemData = Repository.Select
                       .Where(t => t.Id == Convert.ToInt32(item))
                       .ToList((t) => new
                       {
                           entity = t
                       }).Select(t =>
                       {
                           var output = Mapper.Map<PaymentCollectionPlanDto>(t.entity);
                           return output;
                       }).First();
                        if (itemData != null)
                        {
                            list.Add(itemData);
                        }
                    }
                }
            }            
            foreach (var item in list)
            {
                Delete(item.Id.Value);
            }
        }
    }
}
