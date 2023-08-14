import request from '@/utils/request'

const baseURL = '/fileInformation'

export function getList(params) {
  return request({
    url: baseURL,
    method: 'get',
    params,
  })
}
export function doAdd(data) {
  return request({
    url: `${baseURL}/doAdd`,
    method: 'post',
    data,
  })
}