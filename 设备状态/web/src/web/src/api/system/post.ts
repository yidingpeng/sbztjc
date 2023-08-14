import request from '@/utils/request'

//const baseUrl = '/organization'
import { baseAPI } from '@/config/net.config'

const baseUrl = `${baseAPI}/post`

export function getList(params: any) {
  return request({
    url: baseUrl,
    method: 'get',
    params,
  })
}

export function doEdit(data: any) {
  return request({
    url: baseUrl,
    method: 'put',
    data,
  })
}

export function doAdd(data: any) {
  return request({
    url: baseUrl,
    method: 'post',
    data,
  })
}

export function doDelete(data: any) {
  return request({
    url: baseUrl,
    method: 'delete',
    data,
  })
}
