import request from '@/utils/request'

const baseURL = '/role'
//let baseURL = 'https://localhost:7242/api/role'

export function getList(params) {
  return request({
    url: baseURL,
    method: 'get',
    params,
  })
}
//查询所有角色信息
export function getAllList(params) {
  return request({
    url: `${baseURL}/GetAllList`,
    method: 'get',
    params,
  })
}

export function getTree(params) {
  return request({
    url: `${baseURL}/tree`,
    method: 'get',
    params,
  })
}

export function getAuthList(params) {
  return request({
    url: `${baseURL}/modules`,
    method: 'get',
    params,
  })
}

export function doAdd(data) {
  return request({
    url: baseURL,
    method: 'post',
    data,
  })
}

export function doEdit(data) {
  return request({
    url: baseURL,
    method: 'put',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: baseURL,
    method: 'delete',
    data,
  })
}

export function getPermission(data) {
  return request({
    url: `${baseURL}/permission`,
    method: 'get',
    params: data,
  })
}

export function updatePermission(data) {
  return request({
    url: `${baseURL}/permission`,
    method: 'put',
    data,
  })
}
