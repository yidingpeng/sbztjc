import request from '@/utils/request'

const baseURL = '/DeviceWeiBaoTimeOnOff'



export function DoAddWeibao(data) {
  return request({
    url:  `${baseURL}/DoAdd`,
    method: 'post',
    data,
  })
}