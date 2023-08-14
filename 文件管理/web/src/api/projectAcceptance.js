import request from '@/utils/request'
const baseUrl = '/ProjectAcceptance'

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

export function GetAcceptCategory(params) {
  return request({
    url: `${baseUrl}/GetAcceptCategory`,
    method: 'get',
    params,
  })
}
//CheckAcceptCategory
export function GetCheckAcceptCategory(params) {
  return request({
    url: `${baseUrl}/GetCheckAcceptCategory`,
    method: 'get',
    params,
  })
}
export function getInsertFileUrl() {
  return request({
    url: '/File/GetFileUrl',
    method: 'get',
  })
}

export function getProjectCode(params) {
  return request({
    url: 'ProjectBasics/TreeList',
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

//获取联系人表格
export function getContactsList(params) {
  return request({
    url: '/Contact/GetPagedListByRol',
    method: 'get',
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
