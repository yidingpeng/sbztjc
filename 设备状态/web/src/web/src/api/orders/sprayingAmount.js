import request from '@/utils/request'

const baseURL = '/SprayingAmount'

//查询所有订单信息
export function getAllList(params) {
  return request({
    url: `${baseURL}/GetAllList`,
    method: 'get',
    params,
  })
}
export function doEdit(data) {
  console.log(data)
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
