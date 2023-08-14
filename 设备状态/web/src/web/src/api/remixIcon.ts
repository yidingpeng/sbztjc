import request from '@/utils/request'

export function getIconList(params: any) {
  return request({
    url: '/orders/GetAllList',
    method: 'get',
    params,
  })
}
