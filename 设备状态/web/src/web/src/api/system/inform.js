import request from '@/utils/request'

const baseUrl = '/inform'
//const baseUrl = 'https://localhost:7242/api/inform'
export function getList(params) {
  return request({
    url: baseUrl,
    method: 'get',
    params,
  })
}

export function getOne(id) {
  return request({ url: `${baseUrl}/${id}`, method: 'get', id })
}

export function topList(params) {
  return request({
    url: `${baseUrl}/top`,
    method: 'get',
    params,
  })
}

export function getTypeList(params) {
  return request({ url: `${baseUrl}/types`, method: 'get', params })
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

export function doDelete(id) {
  return request({
    url: `${baseUrl}/${id}`,
    method: 'delete',
    id,
  })
}

export function doPublish(id) {
  return request({
    url: `${baseUrl}/publish`,
    method: 'post',
    params: { id: id },
  })
}
