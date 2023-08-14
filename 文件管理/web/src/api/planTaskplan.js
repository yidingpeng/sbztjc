import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/plan\taskplan/getList',
    method: 'get',
    params,
  })
}

export function doEdit(data) {
  return request({
    url: '/plan\taskplan/doEdit',
    method: 'post',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/plan\taskplan/doDelete',
    method: 'post',
    data,
  })
}
