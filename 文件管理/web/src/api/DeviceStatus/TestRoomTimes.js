import request from '@/utils/request'

const baseURL = '/TestRoomTime'


export function DoAddweibao(data) {
  return request({
    url: `${baseURL}/DoAddweibao`,
    method: 'post',
    data,
  })
}

export function DoAddholiday(data) {
  return request({
    url: `${baseURL}/DoAddholiday`,
    method: 'post',
    data,
  })
}

export function DoAddzhidu(data) {
  return request({
    url: `${baseURL}/DoAddzhidu`,
    method: 'post',
    data,
  })
}