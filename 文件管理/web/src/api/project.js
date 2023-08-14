import request from '@/utils/request'
//const baseUrl = 'https://localhost:7242/api/ProjectBasics'
const baseUrl = '/ProjectBasics'
//const baseUrl = 'http://47.96.139.205:8006/api/ProjectBasics'

export function getList(params) {
  return request({
    url: '/ProjectBasics/List',
    method: 'get',
    params,
  })
}

export function GetDataById(params) {
  return request({
    url: '/ProjectBasics/GetListById',
    method: 'get',
    params,
  })
}

export function getByParentIdList(params) {
  return request({
    url: '/ProjectBasics/ParentIdList',
    method: 'get',
    params,
  })
}
//营销销售数据
export function GetSellBasicsList(params) {
  return request({
    url: '/ProjectBasics/GetSellBasicsList',
    method: 'get',
    params,
  })
}

//营销销售数据子项目
export function GetParentSellBasicsList(params) {
  return request({
    url: '/ProjectBasics/GetParentSellBasicsList',
    method: 'get',
    params,
  })
}

export function doEdit(data) {
  return request({
    url: baseUrl,
    method: 'put',
    data,
  })
}

export function doAdd(data) {
  return request({
    url: baseUrl,
    method: 'post',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: baseUrl,
    method: 'delete',
    data,
  })
}

//项目分类
export function GetProjectClass(params) {
  return request({
    url: '/ProjectBasics/Class',
    method: 'get',
    params,
  })
}
//业务类型
export function GetBusinesstype(params) {
  return request({
    url: '/ProjectBasics/Type',
    method: 'get',
    params,
  })
}
//重要等级
export function GetUrgentGrade(params) {
  return request({
    url: '/ProjectBasics/Grade',
    method: 'get',
    params,
  })
}
//管理方式
export function GetProManagementStyle(params) {
  return request({
    url: '/ProjectBasics/Style',
    method: 'get',
    params,
  })
}

//项目状态下拉框
export function ProjectStatus() {
  return request({
    url: '/ProjectBasics/ProjectStatus',
    method: 'get',
  })
}
//客户公司
export function GetClientCompany(params) {
  return request({
    url: '/ProjectBasics/Client',
    method: 'get',
    params,
  })
}
//状态
export function GetProState(params) {
  return request({
    url: '/ProjectBasics/State',
    method: 'get',
    params,
  })
}
//项目信息
export function GetProBasicsTreeList(params) {
  return request({
    url: '/ProjectBasics/TreeList',
    method: 'get',
    params,
  })
}
//根据ID查询客户信息
export function GetByClientList(params) {
  return request({
    url: '/ClientCompany/GetByIdList',
    method: 'get',
    params,
  })
}
//根据ID查询项目联系人信息
export function GetByContactsList(params) {
  return request({
    url: '/projectcontacts/GetByIdList',
    method: 'get',
    params,
  })
}
//根据ID获取交付信息
export function GetByDeliveryList(params) {
  return request({
    url: '/ProjectDelivery/GetByIdList',
    method: 'get',
    params,
  })
}
//根据ID查询项目交付文件信息
export function GetBydeliveryFileList(params) {
  return request({
    url: '/ProjectDeliveryFile/GetByIdList',
    method: 'get',
    params,
  })
}
//根据ID查询项目项目验收信息
export function GetByAcceptanceList(params) {
  return request({
    url: '/ProjectAcceptance/GetByIdList',
    method: 'get',
    params,
  })
}
//合同信息
export function ContractDetailList(params) {
  return request({
    url: '/ContractInfo/GetByIdList',
    method: 'get',
    params,
  })
}
//回款信息
export function PaymentDetailList(params) {
  return request({
    url: '/PaymentCollection/GetByIdList',
    method: 'get',
    params,
  })
}
//质保金信息
export function MoneyDetailList(params) {
  return request({
    url: '/ProjectRetMoney/GetByIdList',
    method: 'get',
    params,
  })
}
//合同信息
export function GetContractList(params) {
  return request({
    url: '/ProjectBasics/ContractList',
    method: 'get',
    params,
  })
}
//设备信息
export function GetDeviceList(params) {
  return request({
    url: '/ProjectDeviceDetails/DeviceList',
    method: 'get',
    params,
  })
}
//获取文件下载路径 ContractInfo
export function GetDownloadFilePath() {
  return request({
    url: '/ContractInfo/GetDownloadFilePath',
    method: 'get',
  })
}
//获取文件下载路径ProjectDelivery
export function GetDownloadFilePath2() {
  return request({
    url: '/ProjectDelivery/GetDownloadFilePath',
    method: 'get',
  })
}

//设备信息
export function GetProInvoicingList(params) {
  return request({
    url: '/ProjectInvoicing/GetByIdList',
    method: 'get',
    params,
  })
}
//获取市场片区信息
export function GetSalseAreaSelect(params) {
  return request({
    url: '/bd_SalesAreaInfo/GetSalseAreaSelect',
    method: 'get',
    params,
  })
}

// 项目动态分页
export function DTgetPagedList(params) {
  return request({
    url: '/ProjectDynamic/PagedList',
    method: 'get',
    params,
  })
}
// 项目动态list集合（ProjectID查询）
export function DTgetList(params) {
  return request({
    url: '/ProjectDynamic/List',
    method: 'get',
    params,
  })
}
//项目动编辑
export function DTdoEdit(data) {
  return request({
    url: '/ProjectDynamic/doEdit',
    method: 'put',
    data,
  })
}
//项目动态新增
export function DTdoAdd(data) {
  return request({
    url: '/ProjectDynamic/doAdd',
    method: 'post',
    data,
  })
}
//项目动态删除
export function DTdoDelete(data) {
  return request({
    url: '/ProjectDynamic/doDelete',
    method: 'delete',
    data,
  })
}
//项目动态类型
export function GetProjectDyType(params) {
  return request({
    url: '/ProjectDynamic/DyType',
    method: 'get',
    params,
  })
}

//更新状态
export function UpdateProState(params) {
  return request({
    url: '/ProjectBasics/UpdateProState',
    method: 'post',
    params,
  })
}

//更新备注
export function UpdateProRemark(params) {
  return request({
    url: '/ProjectBasics/UpdateProRemark',
    method: 'post',
    params,
  })
}
//消息提醒
export function TeamNewsFeed(data) {
  return request({
    url: '/ProjectBasics/TeamNewsFeed',
    method: 'post',
    data,
  })
}

//获取上传文件地址
export function GetInsertFilePath() {
  return request({
    url: '/ProjectBasics/GetInsertFilePath',
    method: 'get',
  })
}

//获取文件
export function GetFile(params) {
  return request({
    url: '/ProjectBasics/GetFile',
    method: 'get',
    responseType: 'arraybuffer',
    params,
  })
}
//导出项目数据列表
export function ExportProList(params) {
  return request({
    url: '/ProjectBasics/ExportProList',
    method: 'get',
    params,
  })
}
