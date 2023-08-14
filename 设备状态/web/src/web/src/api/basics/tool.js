import request from '@/utils/request'

const baseURL = '/tool'

export function getList(params) {
  return request({
    url: baseURL,
    method: 'get',
    params,
  })
}

export function getInsertFileUrl() {
  return request({
    url: '/Tool/GetInsertFilePath',
    method: 'get',
  })
}

export function GetFilesByPid(params) {
  return request({
    url: `${baseURL}/GetFilesByPid`,
    method: 'get',
    params,
  })
}

export function GetDownloadFilePath() {
  return request({
    url: `${baseURL}/GetFilePreviewPath`,
    method: 'get',
  })
}

export function GetPictureData(params) {
  return request({
    url: `${baseURL}/GetPictureData`,
    method: 'get',
    params,
  })
}

export function DeleteFilesByPid(params) {
  return request({
    url: `${baseURL}/UpdateImgPidtoZero`,
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

//根据id获取工具图片信息ids
export function GetImageByPtid(params) {
  return request({
    url: '/Tool/GetImageByPtid',
    method: 'get',
    params,
  })
}

//获取工具图片
export function getToolImg(params) {
  return request({
    url: '/Tool/Get',
    method: 'get',
    params,
  })
}

//获取工具类别
export function GetToolClassList() {
  return request({
    url: `${baseURL}/ToolClass`,
    method: 'get',
  })
}

//获取工具类型
export function GetToolTypeList() {
  return request({
    url: `${baseURL}/ToolType`,
    method: 'get',
  })
}

export function doEdit(data) {
  return request({
    url: baseURL,
    method: 'put',
    data,
  })
}

export function doAdd(data) {
  return request({
    url: baseURL,
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
