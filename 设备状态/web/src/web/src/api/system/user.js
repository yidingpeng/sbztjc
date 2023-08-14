import request from '@/utils/request'

const baseUrl = '/user'
//const baseUrl = 'https://localhost:7242/api/user'

export function getList(params) {
  return request({
    url: baseUrl,
    method: 'get',
    params,
  })
}

export function getPersonal(params) {
  return request({
    url: `${baseUrl}/personal`,
    method: 'get',
    params,
  })
}

export function doEditPersonal(data) {
  return request({
    url: `${baseUrl}/personal`,
    //url: '/user/info',
    method: 'put',
    data,
  })
}

export function doChangePwd(data) {
  return request({
    url: `${baseUrl}/password`,
    method: 'put',
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

export function doResetPwd(data) {
  return request({
    url: `${baseUrl}/resetpwd`,
    method: 'put',
    data,
  })
}

export function getModelList(params) {
  return request({
    url: '/module/GetOperationListByUserId',
    method: 'get',
    params,
  })
}

export function GetUserOperation(params) {
  return request({
    url: `${baseUrl}/GetUserOperation`,
    method: 'get',
    params,
  })
}

export function EditUserOperation(data) {
  return request({
    url: `${baseUrl}/DoOperationEdit`,
    method: 'post',
    data,
  })
}

export function GetUserId() {
  return request({
    url: `${baseUrl}/GetUserId`,
    method: 'get',
  })
}

export function getPermission(data) {
  return request({
    url: `${baseUrl}/permission`,
    method: 'get',
    params: data,
  })
}

export function GetUserByAccount(params) {
  return request({
    url: `${baseUrl}/UserListByAccount`,
    method: 'get',
    params,
  })
}
export function GetUserList(params) {
  return request({
    url: `${baseUrl}/UserList`,
    method: 'get',
    params,
  })
}
//通过登录人获取信息
export function GetUserById(params) {
  return request({
    url: `${baseUrl}/GetUserById`,
    method: 'get',
    params,
  })
}
