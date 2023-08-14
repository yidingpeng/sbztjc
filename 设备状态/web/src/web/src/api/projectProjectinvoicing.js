import request from '@/utils/request'

const baseUrl = '/ProjectInvoicing'
export function getList(params) {
  return request({
    url: `${baseUrl}/getList`,
    method: 'get',
    params,
  })
}

export function doAdd(data) {
  return request({
    url: `${baseUrl}/DoAdd`,
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
//获取项目编号
export function getProjectCode(params) {
  return request({
    url: 'ProjectBasics/TreeList',
    method: 'get',
    params,
  })
}

//获取联系人信息
export function GetContractList(params) {
  return request({
    url: 'ContractInfo/GetByIdList',
    method: 'get',
    params,
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
//根据项目id获取项目信息
export function GetListById(params) {
  return request({
    url: 'ProjectBasics/GetListById',
    method: 'get',
    params,
  })
}
