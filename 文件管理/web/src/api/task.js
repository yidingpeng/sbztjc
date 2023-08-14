import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/TaskProcessInfo/PageList',
    method: 'get',
    params,
  })
}
export function getParentList(params) {
  return request({
    url: '/TaskProcessInfo/ParentList',
    method: 'get',
    params,
  })
}
//GetByParentList
export function GetByParentList(params) {
  return request({
    url: '/TaskProcessInfo/GetByParentList',
    method: 'get',
    params,
  })
}
//上移下移
export function UpdateSeqNo(params) {
  return request({
    url: '/TaskProcessInfo/UpdateSeqNo',
    method: 'post',
    params,
  })
}
//MaxSeqNo
export function MaxSeqNo(params) {
  return request({
    url: '/TaskProcessInfo/MaxSeqNo',
    method: 'get',
    params,
  })
}
//MinSeqNo
export function MinSeqNo(params) {
  return request({
    url: '/TaskProcessInfo/MinSeqNo',
    method: 'get',
    params,
  })
}
export function doAdd(data) {
  return request({
    url: '/TaskProcessInfo/doAdd',
    method: 'post',
    data,
  })
}
export function doEdit(data) {
  return request({
    url: '/TaskProcessInfo',
    method: 'put',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/TaskProcessInfo',
    method: 'delete',
    data,
  })
}
//项目模板分页查询
export function MBgetList(params) {
  return request({
    url: '/TaskPlan/ProTemplatePagedList',
    method: 'get',
    params,
  })
}
//项目模板新增
export function MBdoAdd(data) {
  return request({
    url: '/TaskPlan/MBdoAdd',
    method: 'post',
    data,
  })
}
//项目模板修改
export function MBdoEdit(data) {
  return request({
    url: '/TaskPlan/MBdoEdit',
    method: 'put',
    data,
  })
}
//项目模板删除
export function MBdoDelete(data) {
  return request({
    url: '/TaskPlan/MBdoDelete',
    method: 'delete',
    data,
  })
}

//MBdoEditState
//项目模板修改
export function DoEditState(params) {
  return request({
    url: '/TaskPlan/DoEditState',
    method: 'post',
    params,
  })
}

//查看文件
export function doDownload(url, fileurl, id) {
  request({
    url: `${url}?fileUrl=${fileurl}&id=${id}`,
    method: 'get',
    responseType: 'blob',
  }).then((res) => {
    const fileURL = window.URL.createObjectURL(res)
    const fileLink = document.createElement('a')

    fileLink.href = fileURL
    fileLink.setAttribute('target', '_blank')
    document.body.appendChild(fileLink)

    fileLink.click()
  })
}

//下载文件
export function doDownload2(url, fileurl, id, filename) {
  request({
    url: `${url}?fileUrl=${fileurl}&id=${id}`,
    method: 'get',
    responseType: 'blob',
  }).then((res) => {
    const fileURL = window.URL.createObjectURL(res)
    const fileLink = document.createElement('a')

    fileLink.href = fileURL
    fileLink.setAttribute('download', filename)
    document.body.appendChild(fileLink)

    fileLink.click()
  })
}
