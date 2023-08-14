import request from '@/utils/request'

const baseURL = '/Device'

export function getdeviceList() {
  return request({
    url: `${baseURL}/GetList`,
    method: 'get',
   
  })
}