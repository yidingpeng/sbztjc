import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/ProjectDelivery/GetDeliveryPagedList',
    method: 'post',
    params,
  })
}

export function AcceptancePhase() {
  return request({
    url: '/ProjectDelivery/GetAcceptancePhase',
    method: 'get',
  })
}

export function doAdd(data) {
  return request({
    url: '/ProjectDelivery/ProDeliveryAdd',
    method: 'post',
    data,
  })
}

export function doEdit(data) {
  return request({
    url: '/ProjectDelivery',
    method: 'put',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/ProjectDelivery',
    method: 'delete',
    data,
  })
}

// export function getProjectCode() {
//   return request({
//     url: '/ProjectDelivery/GetprojectList',
//     method: 'get',
//   })
// }

//根据ID获取List
export function GetByDeliveryList(params) {
  return request({
    url: '/ProjectDelivery/GetByIdList',
    method: 'get',
    params,
  })
}
//获取项目编码
export function getProjectCode(params) {
  return request({
    url: 'ProjectBasics/TreeList',
    method: 'get',
    params,
  })
}
//根据项目id获取项目信息
export function GetListById(params) {
  return request({
    url: 'ProjectBasics/GetListById',
    method: 'get',
    params,
  })
}

//下载文件
export function doDownload(url, fileurl, id, filename) {
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
