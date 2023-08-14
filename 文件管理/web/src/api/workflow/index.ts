import request from '@/utils/request'
import { baseAPI } from '@/config/net.config'

const baseUrl = `${baseAPI}/workflow`
//const baseUrl = '/workflow'

export function getList(params: any) {
  return request({ url: baseUrl, method: 'get', params })
}

export function doEdit(id: any, data: any) {
  return request({ url: `${baseUrl}/${id}`, method: 'put', data })
}

export function doDelete(id: any) {
  return request({ url: [baseUrl, '/', id].join(''), method: 'delete' })
}

export function doAdd(data: any) {
  return request({ url: baseUrl, method: 'post', data })
}

export function doGetOne(id: any) {
  return request({ url: `${baseUrl}/${id}`, method: 'get' })
}
