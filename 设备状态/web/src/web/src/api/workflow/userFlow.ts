import request from '@/utils/request'

import { baseAPI } from '@/config/net.config'
const baseUrl = `${baseAPI}/workflow`

export function getList(params: any) {
  return request({
    url: '/area/getList',
    method: 'get',
    params,
  })
}

export function getUserFlowOne(id: any) {
  return request({ url: `${baseUrl}/my/${id}`, method: 'get' })
}

export function getUserFlowList(params: any) {
  return request({ url: `${baseUrl}/my`, method: 'get', params })
}
export function getUserFlowTop(params: any) {
  return request({ url: `${baseUrl}/top`, method: 'get', params })
}

export function doAddUserFlow(data: any) {
  return request({ url: `${baseUrl}/my`, method: 'post', data })
}

export function doEditUserFlow(id: any, data: any) {
  return request({ url: `${baseUrl}/my/${id}`, method: 'put', data })
}

export function doDeleteUserFlow(id: any) {
  return request({ url: [baseUrl, '/my/', id].join(''), method: 'delete' })
}

export function doAudit(data: any) {
  return request({ url: [baseUrl, '/audit'].join(''), method: 'post', data })
}

/*
  @description: 自动生成编号
 */
export function doCreateNo(params: any) {
  return request({ url: `${baseUrl}/no`, method: 'get', params })
}

export function doCancel(id: any) {
  return request({ url: `${baseUrl}/cancel/${id}`, method: 'put' })
}

/*
  @description:获取所有文件
 */
export function getFiles(params: any) {
  return request({ url: `${baseUrl}/files`, method: 'get', params })
}

/*
 * 流程催办
 */
export function urging(id: any) {
  return request({ url: `${baseUrl}/urging/${id}`, method: 'post' })
}
