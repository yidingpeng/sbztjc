import request from '@/utils/request'
const baseUrl = '/Role'
//const baseUrl = 'http://192.168.0.53:8087/api/Role'
export function getList(params) {
  return request({
    //url: '/api/role',
    url: baseUrl,
    method: 'get',
    params,
  })
}

export function getAuthList(params) {
  return request({
    //url: '/api/role',
    url: baseUrl + '/Modules',
    method: 'get',
    params,
  })
}

export function doAdd(data) {
  return request({
    //url: '/api/role',
    url: baseUrl,
    method: 'post',
    data,
  })
}

export function doEdit(data) {
  return request({
    //url: '/api/role',
    url: baseUrl,
    method: 'put',
    data,
  })
}

export function doDelete(data) {
  return request({
    //url: '/api/role',
    url: baseUrl,
    method: 'delete',
    data,
  })
}
