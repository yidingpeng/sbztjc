import request from '@/utils/request'

const baseUrl = '/bd_SalesAreaInfo'
export function getList(params) {
  return request({
    url: `${baseUrl}/GetPagedList`,
    method: 'post',
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
    url: `${baseUrl}/doAdd`,
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

export function getPlaceName() {
  return request({
    url: `${baseUrl}/getPlaceName`,
    method: 'get',
  })
}

//获取联系人表格
export function getContactsList(params) {
  return request({
    url: '/Contact/GetPagedListByRol',
    method: 'get',
    params,
  })
}
