import request from '@/utils/request'

const baseUrl = '/PaymentCollection'
//获取项目名称
export function GetProjectPayment() {
  return request({
    url: `${baseUrl}/Pro`,
    method: 'get',
    // header: {
    //   Accept: 'application/json',
    //   'Content-Type': 'application/json', //自定义请求头信息
    // },
  })
}
//获取合同编号
export function GetContractPayment() {
  return request({
    url: `${baseUrl}/Contra`,
    method: 'get',
  })
}
//获取交付信息
export function GetDeliveryPayment() {
  return request({
    url: `${baseUrl}/Deliv`,
    method: 'get',
  })
}
export function getList(params) {
  return request({
    url: baseUrl,
    method: 'get',
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

//获取字典回款类型
export function GetReturnTypeList() {
  return request({
    url: `${baseUrl}/ReturnType`,
    method: 'get',
  })
}

//获取字典回款方式
export function GetReturnWayList() {
  return request({
    url: `${baseUrl}/ReturnWay`,
    method: 'get',
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

export function getProjectCode(params) {
  return request({
    url: 'ProjectBasics/TreeList',
    method: 'get',
    params,
  })
}
