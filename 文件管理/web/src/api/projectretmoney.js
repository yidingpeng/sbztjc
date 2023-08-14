import request from '@/utils/request'

//获取项目名称
export function GetProjectPayment(params) {
  return request({
    url: '/ProjectRetMoney/Pro',
    method: 'get',
    params,
  })
}
//获取合同信息
export function GetDeliveryPayment(params) {
  return request({
    url: '/ProjectBasics/ContractList',
    method: 'get',
    params,
  })
}
export function getList(params) {
  return request({
    url: '/ProjectRetMoney',
    method: 'get',
    params,
  })
}

export function doAdd(data) {
  return request({
    url: '/ProjectRetMoney',
    method: 'post',
    data,
  })
}

export function doEdit(data) {
  return request({
    url: '/ProjectRetMoney',
    method: 'put',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/ProjectRetMoney',
    method: 'delete',
    data,
  })
}

export function getProjectCode(params) {
  return request({
    url: 'ProjectBasics/TreeList',
    method: 'get',
    params,
  })
}
