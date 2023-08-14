import request from '@/utils/request'

const baseURL = '/Room'

export function getroomList() {
  return request({
    url: `${baseURL}/GetList`,
    method: 'get',
    
  })
}