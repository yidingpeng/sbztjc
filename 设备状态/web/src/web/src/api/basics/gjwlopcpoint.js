import request from '@/utils/request'

const baseURL = '/gjwlopcpoint'

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

//获取Opc点位类型
export function GetGwOPCTypeList() {
  return request({
    url: `${baseURL}/GwOPCType`,
    method: 'get',
  })
}

//获取工作中心信息
export function GetWorkCenterList() {
  return request({
    url: `${baseURL}/WorkCenter`,
    method: 'get',
  })
}

//获取工具信息
export function GetToolList() {
  return request({
    url: `${baseURL}/ToolList`,
    method: 'get',
  })
}

//获取物料信息
export function GetMaterialList() {
  return request({
    url: `${baseURL}/MaterialList`,
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
