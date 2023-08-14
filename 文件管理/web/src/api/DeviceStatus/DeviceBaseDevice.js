import request from '@/utils/request'

const baseURL = '/DeviceBaseDevice'


export function dGetList(params) {
  return request({
    url: `${baseURL}/GetList`,
    method: 'get',
    params,
  })
}
export function ddoEdit(data) {
  console.log(data)
  return request({
    url: baseURL,
    method: 'put',
    data,
  })
}
export function ddoAdd(data) {
  return request({
    url: baseURL,
    method: 'post',
    data,
  })
}
export function ddoDelete(data) {
  return request({
    url: baseURL,
    method: 'delete',
    data,
  })
}
