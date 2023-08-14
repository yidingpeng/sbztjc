import request from '@/utils/request'

const baseURL = '/DRoomWeiBaoTimeOnOff'



export function DoAddWeibao(data) {
  return request({
    url:  `${baseURL}/DoAdd`,
    method: 'post',
    data,
  })
}