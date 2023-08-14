import request from '@/utils/request'

const baseUrl = '/Dictionary'
//const baseUrl = 'https://localhost:7242/api/Dictionary'

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

export function getParentList(params) {
  return request({
    url: `${baseUrl}/GetParentList`,
    method: 'get',
    params,
  })
}
export function getKeyValue(params) {
  return request({ url: `${baseUrl}/KeyValue`, method: 'get', params })
}

export function getAll() {
  return request({ url: `${baseUrl}/all`, method: 'get' })
}
