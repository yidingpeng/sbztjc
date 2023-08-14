import request from '@/utils/request'

const baseURL = '/DeviceBaseRoom'


export function GetList(params) {
  return request({
    url: `${baseURL}/GetList`,
    method: 'get',
    params,
  })
}
export function GetAllList(params) {
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
export function updateClear (data) {
  return request({
    url: `${baseURL}/updateIsClear`,
    method: 'post',
    data,
  })
}
