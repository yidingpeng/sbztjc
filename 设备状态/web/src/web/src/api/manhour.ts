import request from '@/utils/request'

export function getList(params: any) {
  return request({
    url: '/wh/GetPageList',
    method: 'get',
    params,
  })
}

export function doEdit(data: any) {
  return request({
    url: '/wh/WHAddOrEdit',
    method: 'post',
    data,
  })
}

export function doDelete(data: any) {
  return request({
    url: '/wh/WHDelete',
    method: 'post',
    data,
  })
}
