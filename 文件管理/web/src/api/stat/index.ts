import request from '@/utils/request'
import { baseAPI } from '@/config/net.config'

//const baseURL = '/quick'
const baseURL = `${baseAPI}/stat`

export function getUserStat(params: any) {
  return request({ url: `${baseURL}/user`, method: 'get', params: params })
}

export function getWorkflowStat(params: any) {
  return request({ url: `${baseURL}/workflow`, method: 'get', params: params })
}

export function getIssueTop(params: any) {
  return request({
    url: `${baseURL}/issueTop`,
    method: 'get',
    params,
  })
}
