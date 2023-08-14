import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/materials/getList',
    method: 'get',
    params,
  })
}

export function doEdit(data) {
  return request({
    url: '/materials/doEdit',
    method: 'post',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/materials/doDelete',
    method: 'post',
    data,
  })
}
