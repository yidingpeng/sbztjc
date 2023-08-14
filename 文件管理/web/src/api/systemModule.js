import request from '@/utils/request'
const baseUrl = '/Module'
//const baseUrl = 'http://192.168.0.53:8087/api/Module'
export function getList() {
  return request({
    url: baseUrl,
    method: 'get',
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
  console.log(data)
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
