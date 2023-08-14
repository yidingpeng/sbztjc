import request from '@/utils/request'
import { baseAPI } from '@/config/net.config'

//const baseUrl = '/message'
const baseUrl = `${baseAPI}/message`
export function getList(params) {
  return request({
    url: baseUrl,
    method: 'get',
    params,
  })
}

export function getOne(id) {
  return request({ url: `${baseUrl}/${id}`, method: 'get', id })
}

export function topList(params) {
  return request({
    url: `${baseUrl}/top`,
    method: 'get',
    params,
  })
}

export function doDelete(id) {
  return request({ url: `${baseUrl}/${id}`, method: 'delete' })
}

export function clearMsg() {
  return request({ url: baseUrl, method: 'delete' })
}
