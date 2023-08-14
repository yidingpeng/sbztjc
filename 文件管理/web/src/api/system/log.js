import request from '@/utils/request'

const baseUrl = '/systemLog'
//const baseUrl = 'https://localhost:7242/api/systemLog'
export function getList(params) {
  return request({
    url: baseUrl,
    method: 'get',
    params,
  })
}
