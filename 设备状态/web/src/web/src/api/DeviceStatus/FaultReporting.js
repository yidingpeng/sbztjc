import request from '@/utils/request'

const baseURL = '/FaultReporting'


export function GetList(params) {
  return request({
    url: `${baseURL}/GetList`,
    method: 'get',
    params,
  })
}
export function doEdit(data) {
  return request({
    url: baseURL,
    method: 'put',
    data,
  })
}