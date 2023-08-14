using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Domain.Entities.Purchase;
using System.Collections.Generic;

namespace RW.PMS.Application.Contracts.Purchase
{
    public interface IPurchaseService : ICrudApplicationService<Mat_OperateEntity, int>
    {
        /// <summary>
        /// 物料处理分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<MaterialOperateDto> GetPagedList(MaterialOperateSearchDto input);
        /// <summary>
        /// 根据BomId查询订单明细集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<MaterialOperateDto> GetOperateList(MaterialOperateSearchDto input);

        /// <summary>
        /// 根据供应商查询订单子表分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<MaterialOperateDto> GetSupplierPagedList(MaterialOperateSearchDto input);

        /// <summary>
        /// 判断是否存在相同bom物料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(MaterialOperateDto input);
        /// <summary>
        /// 物料处理选择供应商
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="supCode"></param>
        /// <returns></returns>
        bool OptSupplierEdit(string ids, string supCode);

        /// <summary>
        /// 发送物料消息给质检人员
        /// </summary>
        /// <param name="bomcode"></param>
        /// <param name="materialcode"></param>
        /// <param name="link"></param>
        /// <param name="esburl"></param>
        /// <returns></returns>
        bool MessagePush(string bomcode, string materialcode, string link, string esburl,string time);

        /// <summary>
        /// 物料处理成本价格
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="costPrice"></param>
        /// <returns></returns>
        bool CostPriceEdit(string ids, decimal costPrice);

        /// <summary>
        /// 批量下单
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        bool batchAdd(List<Mat_OperateEntity> items);

        /// <summary>
        /// 供应商修改预计、实际完成日期及预计入库日期
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="YJFinishTime"></param>
        /// <returns></returns>
        bool OrderDateTimeEdit(int Id, string type, string time,int BomId);

        /// <summary>
        /// 根据ID查询前五条历史价格数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<MaterialOperateDto> GetByIdList(int Id);

        /// <summary>
        /// 根据ID查询历史供应商
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<MaterialOperateDto> GetByIdSupplier(int Id);

        /// <summary>
        /// 采购分配人员消息通知
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        string SendMessagePurchase(string url, string title, params string[] users);
    }
}
