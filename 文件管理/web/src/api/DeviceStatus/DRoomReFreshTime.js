import request from '@/utils/request'

const baseURL = '/DRoomReFresh'



export function doReFresh() {
  return request({
    url:  `${baseURL}/droomReFresh`,
    method: 'get',
  })
}