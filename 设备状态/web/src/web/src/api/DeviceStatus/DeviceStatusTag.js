import request from '@/utils/request'

const baseURL = '/DeviceStatus'


export function GetDeviceStatusNameAllList() {
  return request({
    url: `${baseURL}/GetDeviceStatusNameAllList`,
    method: 'get',
    
  })
}
export function GetDeviceNameAllList() {
  return request({
    url: `${baseURL}/GetDeviceNameAllList`,
    method: 'get',
    
  })
}
export function GetDeviceTestRoomAllList() {
  return request({
    url: `${baseURL}/GetDeviceTestRoomAllList`,
    method: 'get',
    
  })
}
export function GetDeviceNameConditionList(params) {
  return request({
    url: `${baseURL}/GetDeviceNameConditionList`,
    method: 'get',
    params,
  })
}
export function doEdit(data) {
  console.log(data)
  return request({
    url:  `${baseURL}/DoEdit`,
    method: 'post',
    data,
  })
}
export function doAdd(data) {
  return request({
    url: `${baseURL}/doAdd`,
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

//查询所有试验单信息
export function getTestSheetAllList(params) {
  return request({
    url: `${baseURL}/getTestSheetAllList`,
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

export function updateClear (data) {
  return request({
    url: `${baseURL}/updateIsClear`,
    method: 'post',
    data,
  })
}

export function devicestauscount() {
  return request({
    url: `${baseURL}/devicestauscount`,
    method: 'get',
    
  })
}