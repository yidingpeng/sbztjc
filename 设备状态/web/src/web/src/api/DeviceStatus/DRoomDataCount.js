import request from '@/utils/request'

const baseURL = '/DRoomDataCount'
export function getweek(params) {
  return request({
    url: `${baseURL}/getweek`,
    method: 'get',
    params,
  })
}

export function getmonth(params) {
  return request({
    url: `${baseURL}/getmonth`,
    method: 'get',
    params,
  })
}

export function getyear(params) {
  return request({
    url: `${baseURL}/getyear`,
    method: 'get',
    params,
  })
}

export function importweekcount(params) {
  return request({
    url: `${baseURL}/importweekcount`,
    method: 'get',
    params,
    responseType: 'blob',
  })
}
export function importmonthcount(params) {
  return request({
    url: `${baseURL}/importmonthcount`,
    method: 'get',
    params,
    responseType: 'blob',
  })
}
export function importyearcount(params) {
  return request({
    url: `${baseURL}/importyearcount`,
    method: 'get',
    params,
    responseType: 'blob',
  })
}