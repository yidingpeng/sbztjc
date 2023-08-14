import request from '@/utils/request'

const baseURL = '/TestSheet'



export function doAdd(data) {
  return request({
    url: `${baseURL}/doAdd`,
    method: 'post',
    data,
  })
}
export function Docountdataone(data) {
  return request({
    url: `${baseURL}/Docountdataone`,
    method: 'post',
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
export function doEdit(data) {
  console.log(data)
  return request({
    url:  `${baseURL}/DoEdit`,
    method: 'post',
    data,
  })
}
export function GetRepeat(params) {
  return request({
    url: `${baseURL}/GetRepeat`,
    method: 'get',
    params,
  })
}