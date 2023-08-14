import request from '@/utils/request'

const baseURL = '/orders'

//查询所有订单信息
export function getAllList(params) {
  return request({
    url: `${baseURL}/GetAllList`,
    method: 'get',
    params,
  })
}
export function ToExcel(data) {
  return request({
    url: `/ElectronicRecord/ToExcel`,
    method: 'POST',
    data,
    responseType: 'blob',
  })
}
