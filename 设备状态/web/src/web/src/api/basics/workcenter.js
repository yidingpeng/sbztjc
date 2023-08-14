import request from '@/utils/request'

const baseURL = '/workCenter'

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

//获取工作中心类型
export function GetWorkCenterTypeList() {
  return request({
    url: `${baseURL}/WorkCenterType`,
    method: 'get',
  })
}

//获取父级工作中心
export function GetGongWeiAreaList() {
  return request({
    url: `${baseURL}/GongWeiArea`,
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
