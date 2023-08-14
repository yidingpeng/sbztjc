import request from '@/utils/request'
const baseUrl = '/projectcontacts'
const baseUrl2 = '/Contact'
export function getList(params) {
  return request({
    url: `${baseUrl}/List`,
    method: 'get',
    params,
  })
}
export function projectList(params) {
  return request({
    url: `${baseUrl}/Project`,
    method: 'get',
    params,
  })
}
//根据项目ID查询项目联系人信息
export function GetByIdList(params) {
  return request({
    url: `${baseUrl}/GetByIdList`,
    method: 'get',
    params,
  })
}

//根据id查询项目联系人
export function GetConListById(params) {
  return request({
    url: 'User/GetUserInfoById',
    method: 'get',
    params,
  })
}

export function projectContactsList(params) {
  return request({
    url: `${baseUrl}/Contact`,
    method: 'get',
    params,
  })
}
export function projectContactsTypesList(params) {
  return request({
    url: `${baseUrl2}/ContactsType`,
    method: 'get',
    params,
  })
}
export function respPlatesList(params) {
  return request({
    url: `${baseUrl}/Plate`,
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
export function getProjectCode(params) {
  return request({
    url: 'ProjectBasics/TreeList',
    method: 'get',
    params,
  })
}
