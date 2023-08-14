import request from '@/utils/request'

export function getList(params: any) {
  return request({
    url: '/bom',
    method: 'get',
    params,
  })
}

export function getMyBOM(params: any) {
  return request({
    url: '/bom/my',
    method: 'get',
    params,
  })
}

export function approveBOM(data: any) {
  return request({
    url: '/bom/approve',
    method: 'post',
    data,
  })
}

export function getDetail(id: any) {
  return request({
    url: `/bom/${id}`,
    method: 'get',
  })
}

export function undoBOM(id: any) {
  return request({
    url: `/bom/undo/${id}`,
    method: 'post',
  })
}
