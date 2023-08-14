import request from '@/utils/request'
const baseUrl = '/router'
//const baseUrl = 'https://localhost:7242/api/Router'
//const baseUrl = 'http://localhost:16000/api/router'

export function getList(params) {
  return request({
    url: baseUrl,
    //url: '/router',
    //url: 'https://localhost:7242/api/Router',
    method: 'get',
    params,
  })
}
