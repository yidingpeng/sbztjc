import request from '@/utils/request'

const baseURL = '/configuration'
//const baseURL = 'https://localhost:7242/api/configuration'
export function getList(params) {
  return request({
    url: baseURL,
    method: 'get',
    params,
  })
}

export function getTypeList(params) {
  return request({
    url: `${baseURL}/types`,
    method: 'get',
    params,
  })
}

export function doEdit(data) {
  return request({
    url: baseURL,
    method: 'put',
    data,
  })
}

export function doAdd(data) {
  return request({
    url: baseURL,
    method: 'post',
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
