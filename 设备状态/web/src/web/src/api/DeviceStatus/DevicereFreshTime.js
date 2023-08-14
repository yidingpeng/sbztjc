import request from '@/utils/request'

const baseURL = '/DevicveReFresh'



export function doReFresh() {
  return request({
    url:  `${baseURL}/deviceReFresh`,
    method: 'get',
  })
}