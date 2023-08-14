import request from '@/utils/request'

const baseUrl = '/organization'
//const baseUrl = 'https://localhost:7242/api/organization'

export function getList(params) {
  return request({
    url: baseUrl,
    method: 'get',
    params,
  })
}

export function getTree(params) {
  return request({
    url: `${baseUrl}/tree`,
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
    url: baseUrl,
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

//组织类别
export function GetOrganizationType(params) {
  return request({
    url: '/organization/OrganizationType',
    method: 'get',
    params,
  })
}
