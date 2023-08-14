import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/purchase/getList',
    method: 'get',
    params,
  })
}
//BomId订单明细
export function GetOperateList(params) {
  return request({
    url: '/purchase/GetOperateList',
    method: 'get',
    params,
  })
}
//供应商分页
export function supplierPageList(params) {
  return request({
    url: '/purchase/SupplierPageList',
    method: 'get',
    params,
  })
}

export function doEdit(data) {
  return request({
    url: '/purchase',
    method: 'put',
    data,
  })
}
export function doAdd(data) {
  return request({
    url: '/purchase',
    method: 'post',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/purchase/doDelete',
    method: 'delete',
    data,
  })
}
export function SupplierAdd(data) {
  return request({
    url: '/purchase/SupplierAdd',
    method: 'post',
    data,
  })
}
export function doBatchEdit(data) {
  return request({
    url: '/purchase/doBatchEdit',
    method: 'post',
    data,
  })
}
export function SupplierEdit(data) {
  return request({
    url: '/purchase/SupplierEdit',
    method: 'put',
    data,
  })
}

export function SupplierDel(data) {
  return request({
    url: '/purchase/SupplierDel',
    method: 'delete',
    data,
  })
}
//
export function supplierAllList(params) {
  return request({
    url: '/purchase/AllSupplierList',
    method: 'get',
    params,
  })
}
//OptSupplierEdit
export function supplierOpt(params) {
  return request({
    url: '/purchase/OptSupplierEdit',
    method: 'post',
    params,
  })
}
//成本价格
export function CostPrice(params) {
  return request({
    url: '/purchase/CostPriceEdit',
    method: 'post',
    params,
  })
}
//BOM分页
export function ByBomIdList(params) {
  return request({
    url: '/Material/ByBomIdList',
    method: 'get',
    params,
  })
}
//BOMset
export function BOMSetList(params) {
  return request({
    url: '/Material/BomSet',
    method: 'get',
    params,
  })
}
//BOMPage
export function BOMPage(params) {
  return request({
    url: '/Material/BomPage',
    method: 'get',
    params,
  })
}
//ByBomToTreeList
export function ByBomToTreeList(params) {
  return request({
    url: '/Material/ByBomToTreeList',
    method: 'get',
    params,
  })
}
//订单批量新增
export function doShop(params) {
  return request({
    url: '/purchase/doShop',
    method: 'post',
    params,
  })
}

//给质检人员发送消息
export function assignStaffFeed(params) {
  return request({
    url: '/purchase/assignStaffFeed',
    method: 'post',
    params,
  })
}

//二维码信息分页
//FifoPageList
export function QrPageList(params) {
  return request({
    url: '/purchase/QrCodePageList',
    method: 'get',
    params,
  })
}

export function QrDelete(data) {
  return request({
    url: '/purchase/QrCodeDelete',
    method: 'delete',
    data,
  })
}

//二维码新增
export function QrCodeAdd(data) {
  return request({
    url: '/purchase/QrCodeAdd',
    method: 'post',
    data,
  })
}

//二维码新增
export function QrCodeAddPL(data) {
  return request({
    url: '/purchase/QrCodeAddPL',
    method: 'post',
    data,
  })
}

//二维码修改
export function QrCodeEdit(data) {
  return request({
    url: '/purchase/QrCodeEdit',
    method: 'put',
    data,
  })
}

//物料下拉框
export function Materialdropdown() {
  return request({
    url: '/Material/Materialdropdown',
    method: 'get',
  })
}

//物料下拉框
export function BOMdropdown() {
  return request({
    url: '/Material/BOMdropdown',
    method: 'get',
  })
}

//出入库信息分页
export function FifoPageList(params) {
  return request({
    url: '/purchase/FifoPageList',
    method: 'get',
    params,
  })
}
//出入库删除
export function FifoDelete(data) {
  return request({
    url: '/purchase/FifoDelete',
    method: 'delete',
    data,
  })
}

//出入库新增
export function FifoAdd(data) {
  return request({
    url: '/purchase/FifoAdd',
    method: 'post',
    data,
  })
}

//出入库修改
export function FifoEdit(data) {
  return request({
    url: '/purchase/FifoEdit',
    method: 'put',
    data,
  })
}

//查询所有二维码信息
export function GetQrAllList() {
  return request({
    url: '/purchase/GetQrAllList',
    method: 'get',
  })
}
//导出二维码信息
export function PrintQrCodeList(params) {
  return request({
    url: '/purchase/PrintQrCodeList',
    method: 'get',
    params,
  })
}

//根据二维码查询信息
export function GetByIdQrCode(params) {
  return request({
    url: '/purchase/GetByIdQrCode',
    method: 'get',
    params,
  })
}

//库存信息分页
export function InventoryPageList(params) {
  return request({
    url: '/purchase/InventoryPageList',
    method: 'get',
    params,
  })
}
//库存信息删除
export function InventoryDelete(data) {
  return request({
    url: '/purchase/InventoryDelete',
    method: 'delete',
    data,
  })
}

//库存信息新增
export function InventoryAdd(data) {
  return request({
    url: '/purchase/InventoryAdd',
    method: 'post',
    data,
  })
}

//库存信息修改
export function InventoryEdit(data) {
  return request({
    url: '/purchase/InventoryEdit',
    method: 'put',
    data,
  })
}

//BOM订单主表分页
export function IMPageList(params) {
  return request({
    url: '/purchase/IMPageList',
    method: 'get',
    params,
  })
}
//BOM订单主表删除
export function IMdoDelete(data) {
  return request({
    url: '/purchase/IMdoDelete',
    method: 'delete',
    data,
  })
}

//BOM订单主表新增
export function IMdoAdd(data) {
  return request({
    url: '/purchase/IMdoAdd',
    method: 'post',
    data,
  })
}

//BOM订单主表修改
export function IMdoEdit(data) {
  return request({
    url: '/purchase/IMdoEdit',
    method: 'put',
    data,
  })
}
//历史价格数据
export function GetByIdList(params) {
  return request({
    url: '/purchase/GetByIdList',
    method: 'get',
    params,
  })
}
//GetByIdSupplier
//历史最近供应商数据
export function GetByIdSupplier(params) {
  return request({
    url: '/purchase/GetByIdSupplier',
    method: 'get',
    params,
  })
}

//根据项目编号获取项目名称
export function GetProDataByCode(params) {
  return request({
    url: '/ProjectBasics/getDataByCode',
    method: 'get',
    params,
  })
}

//根据bom编号获取bom名称
export function GetBomDataByCode(params) {
  return request({
    url: '/Material/PDM_BOMListByCode',
    method: 'get',
    params,
  })
}

//根据cids获取物料信息
export function getDataByCid(data) {
  return request({
    url: '/Material/getDataByCid',
    method: 'post',
    data,
  })
}

//新增采购订单
export function doPurOrderAdd(data) {
  return request({
    url: '/purchase/doPurOrderAdd',
    method: 'post',
    data,
  })
}

//新增采购明细信息
export function doPurDetailAdd(data) {
  return request({
    url: '/purchase/doPurDetailAdd',
    method: 'post',
    data,
  })
}

export function BatchCK(data) {
  return request({
    url: '/purchase/BatchCK',
    method: 'post',
    data,
  })
}

export function FenLiangCK(data) {
  return request({
    url: '/purchase/FenLiangCK',
    method: 'post',
    data,
  })
}

//质检分页
export function QCPageList(params) {
  return request({
    url: '/purchase/QCPageList',
    method: 'get',
    params,
  })
}
//质检新增
export function DoQCAdd(data) {
  return request({
    url: '/purchase/DoQCAdd',
    method: 'post',
    data,
  })
}
//质检删除
export function QCDelete(data) {
  return request({
    url: '/purchase/QCDelete',
    method: 'delete',
    data,
  })
}
//质检修改
export function QCEdit(data) {
  return request({
    url: '/purchase/QCEdit',
    method: 'put',
    data,
  })
}

//GetByQrCodeQC
//根据二维码号查询质检信息
export function GetByQrCodeQC(params) {
  return request({
    url: '/purchase/GetByQrCodeQC',
    method: 'get',
    params,
  })
}

//根据二维码号查询报检信息
export function GetByQrCodeCD(params) {
  return request({
    url: '/purchase/GetByQrCodeCD',
    method: 'get',
    params,
  })
}

//报检新增
export function DoCDAdd(data) {
  return request({
    url: '/purchase/DoCDAdd',
    method: 'post',
    data,
  })
}

//批量报检
export function PLCDAdd(data) {
  return request({
    url: '/purchase/PLCDAdd',
    method: 'post',
    data,
  })
}

//采购申请删除
export function SQDDelete(data) {
  return request({
    url: '/purchase/SQDDelete',
    method: 'delete',
    data,
  })
}

//采购订单审核
export function OrderSH(data) {
  return request({
    url: '/purchase/OrderSH',
    method: 'post',
    data,
  })
}

//采购申请列表明细数据
export function PurchaseDetailList(params) {
  return request({
    url: '/purchase/PurchaseDetailList',
    method: 'get',
    params,
  })
}

//采购申请列表
export function SQDPageList(params) {
  return request({
    url: '/purchase/SQDPageList',
    method: 'get',
    params,
  })
}

//分配人员消息通知
export function SendMessagePurchase(params) {
  return request({
    url: '/purchase/SendMessagePurchase',
    method: 'post',
    params,
  })
}
