import request from '@/utils/request'

//const baseURL = '/quick'
import { baseAPI } from '@/config/net.config'

const baseURL = `${baseAPI}/quick`

export function getList(params: any) {
  return request({ url: baseURL, method: 'get', params: params })
}

export function doEdit(id: any, data: any) {
  return request({ url: `${baseURL}/${id}`, method: 'put', data })
}

export function doDelete(id: any) {
  return request({ url: `${baseURL}/${id}`, method: 'delete' })
}

export function doAdd(data: any) {
  return request({ url: baseURL, method: 'post', data })
}
