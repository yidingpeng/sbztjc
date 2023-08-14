import request from '@/utils/request'

const baseURL = '/productionmodel'

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

//获取额外字段信息
export function GetProductExtendList(params) {
  return request({
    url: `${baseURL}/ProductExtendList`,
    method: 'get',
    params,
  })
}

//获取产品信息
export function GetProductionList() {
  return request({
    url: `${baseURL}/ProductionList`,
    method: 'get',
  })
}

//获取产品型号类型
export function GetProductionModelTypeList() {
  return request({
    url: `${baseURL}/ProductionModelType`,
    method: 'get',
  })
}

//获取产品类型
export function GetProductionTypeList() {
  return request({
    url: `${baseURL}/ProductionType`,
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
