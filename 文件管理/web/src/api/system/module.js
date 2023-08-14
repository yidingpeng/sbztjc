import request from '@/utils/request'
import { baseAPI } from '@/config/net.config'

const baseurl = `${baseAPI}/module`
//var baseurl = '/module'
export function getList() {
  return request({
    url: baseurl,
    method: 'get',
  })
}

export function getOperation(moduleId) {
  return request({
    url: `${baseurl}/${moduleId}/Operation`,
    method: 'get',
  })
}

export function doAdd(data) {
  return request({
    url: baseurl,
    method: 'post',
    data,
  })
}

export function doEdit(data) {
  console.log(data)
  return request({
    url: baseurl,
    method: 'put',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: baseurl,
    method: 'delete',
    data,
  })
}

export function DoOperationEdit(data) {
  return request({
    url: `${baseurl}/DoOperationEdit`,
    method: 'put',
    data,
  })
}

export function DoOperationAdd(data) {
  return request({
    url: `${baseurl}/DoOperationAdd`,
    method: 'post',
    data,
  })
}

export function DoOperationDelete(params) {
  return request({
    url: `${baseurl}/DoOperationDelete`,
    method: 'delete',
    params,
  })
}

export function doSort(data) {
  return request({ url: `${baseurl}/sort`, method: 'put', data })
}
