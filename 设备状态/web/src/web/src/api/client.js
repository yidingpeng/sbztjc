import request from '@/utils/request'
const baseUrl = '/ClientCompany'
//const baseUrl = 'http://192.168.0.53:8087/api'
//字典获取客户级别
export function GetDicClient() {
  return request({
    url: `${baseUrl}/DictionaryClient`,
    method: 'get',
  })
}
//根据ID查询客户信息
export function GetByIdList(params) {
  return request({
    url: `${baseUrl}/GetByIdList`,
    method: 'get',
    params,
  })
}
//字典获取合作业务
export function GetDicBusiness() {
  return request({
    url: `${baseUrl}/DictionaryBusiness`,
    method: 'get',
  })
}
//获取联系人表格
export function getContactsList(params) {
  return request({
    url: '/Contact/GetPagedListByRol',
    method: 'get',
    params,
  })
}
export function GetClientCompany(params) {
  return request({
    url: baseUrl,
    method: 'get',
    //datatype: 'json',
    params,
  })
}
export function doAdd(data) {
  return request({
    url: baseUrl,
    method: 'post',
    data,
  })
}
export function doEdit(data) {
  return request({
    url: baseUrl,
    method: 'put',
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

//获取市场片区信息
export function GetSalseAreaSelect(params) {
  return request({
    url: '/bd_SalesAreaInfo/GetSalseAreaSelect',
    method: 'get',
    params,
  })
}

//根据客户公司和项目类别查询项目信息
export function GetListByClientIdAndClass(params) {
  return request({
    url: '/ProjectBasics/GetListByClientIdAndClass',
    method: 'get',
    params,
  })
}

//根据客户名称获取开票数据
export function GetInvoicingListByClientId(params) {
  return request({
    url: '/ProjectInvoicing/GetListByClientId',
    method: 'get',
    params,
  })
}
//根据客户名称获取合同数据
export function GetContractsListByClientId(params) {
  return request({
    url: '/ContractInfo/GetListByClientId',
    method: 'get',
    params,
  })
}
//根据客户名称获取回款数据
export function GetPaymentListByClientId(params) {
  return request({
    url: '/PaymentCollection/GetListByClientId',
    method: 'get',
    params,
  })
}

//根据客户名称获取联系人数据
export function GetListByClientId(params) {
  return request({
    url: '/Contact/GetListByClientId',
    method: 'get',
    params,
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
