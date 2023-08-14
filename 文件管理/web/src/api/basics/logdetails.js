import request from '@/utils/request'

const baseURL = '/logdetails'

export function getList(params) {
  return request({
    url: baseURL,
    method: 'get',
    params,
  })
}

export function Download() {
  return request({
    url: `${baseURL}/Download`,
    method: 'post',
  })
}
