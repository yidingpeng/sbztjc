import request from '@/utils/request'

const baseUrl = '/Dictionary'

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
    url: baseUrl + '/GetParentList',
    method: 'get',
    params,
  })
}
