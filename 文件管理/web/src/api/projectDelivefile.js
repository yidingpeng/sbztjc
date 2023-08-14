import request from '@/utils/request'
const baseUrl = '/ProjectDeliveryFile'

export function getList(params) {
  return request({
    url: `${baseUrl}/GetList`,
    method: 'get',
    params,
  })
}

export function doEdit(data) {
  return request({
    url: baseUrl,
    method: 'put',
    data,
  })
}

export function doAdd(data) {
  return request({
    url: `${baseUrl}/DoAdd`,
    method: 'post',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: baseUrl,
    method: 'delete',
    data,
  })
}

export function getProjectCode(params) {
  return request({
    url: 'ProjectBasics/TreeList',
    method: 'get',
    params,
  })
}

export function getProDeliveryCode(params) {
  return request({
    url: 'ProjectDelivery/GetByIdList',
    method: 'get',
    params,
  })
}

export function getProDeviceCode(params) {
  return request({
    url: 'ProjectDeviceDetails/DeviceList',
    method: 'get',
    params,
  })
}

export function getDeliFileTypeCode(params) {
  return request({
    url: `${baseUrl}/GetDeliveryType`,
    method: 'get',
    params,
  })
}
