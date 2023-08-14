import request from '@/utils/request'
//分页查询
export function getList(params) {
  return request({
    url: '/ProjectDeviceDetails/PageList',
    method: 'get',
    params,
  })
}
//新增
export function doAdd(data) {
  return request({
    url: '/ProjectDeviceDetails',
    method: 'post',
    data,
  })
}
//修改
export function doEdit(data) {
  console.log(data)
  return request({
    url: '/ProjectDeviceDetails',
    method: 'put',
    data,
  })
}
//删除
export function doDelete(data) {
  return request({
    url: '/ProjectDeviceDetails',
    method: 'delete',
    data,
  })
}
//产品系列
export function ProductLine(params) {
  return request({
    url: '/ProjectDeviceDetails/ProductLine',
    method: 'get',
    params,
  })
}
export function getProjectCode(params) {
  return request({
    url: 'ProjectBasics/TreeList',
    method: 'get',
    params,
  })
}
