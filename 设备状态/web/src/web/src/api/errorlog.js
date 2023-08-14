import request from '@/utils/request'

export function addLog(params) {
  return request({
    url: '/api/system/errorlog',
    method: 'post',
    params,
  })
}
