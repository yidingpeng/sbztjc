import request from '@/utils/request'

const baseUrl = '/user'
//const baseUrl = 'http://192.168.0.53:8087/api/user'

export function getList(params) {
  return request({
    url: baseUrl,
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

export function doResetPwd(data) {
  return request({
    url: baseUrl + '/resetpwd',
    method: 'put',
    data,
  })
}
