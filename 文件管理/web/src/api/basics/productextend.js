import request from '@/utils/request'

const baseURL = '/productextend'

export function getList(params) {
  return request({
    url: baseURL,
    method: 'get',
    params,
  })
}

//查询数据是否重复
export function GetRepeat(params) {
  return request({
    url: `${baseURL}/GetRepeat`,
    method: 'get',
    params,
  })
}

//获取产品型号
export function GetProductionModelList() {
  return request({
    url: `${baseURL}/GetProductionModelList`,
    method: 'get',
  })
}

export function doEdit(data) {
  return request({
    url: baseURL,
    method: 'put',
    data,
  })
}

export function doAdd(data) {
  return request({
    url: baseURL,
    method: 'post',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: baseURL,
    method: 'delete',
    data,
  })
}
