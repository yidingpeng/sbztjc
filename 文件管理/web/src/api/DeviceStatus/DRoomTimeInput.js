import request from '@/utils/request'

const baseURL = '/DRoomTimeInput'



export function doEdit(data) {
  return request({
    url:  `${baseURL}/DoAdd`,
    method: 'post',
    data,
  })
}