import request from '@/utils/request'

const baseURL = '/DeviceTimeInput'



export function doEdit(data) {
  return request({
    url:  `${baseURL}/DoAdd`,
    method: 'post',
    data,
  })
}