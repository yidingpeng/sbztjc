import request from '@/utils/request'

const baseURL = '/material'

export function getList(params) {
  return request({
    url: baseURL,
    method: 'get',
    params,
  })
}

export function getInsertFileUrl() {
  return request({
    url: '/Material/GetInsertFilePath',
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
    url: '/Material/GetImageByPtid',
    method: 'get',
    params,
  })
}

//获取物料图片
export function getMaterialImg(params) {
  return request({
    url: '/Material/Get',
    method: 'get',
    params,
  })
}

//获取物料分类
export function GetMaterialClassList() {
  return request({
    url: `${baseURL}/MaterialClass`,
    method: 'get',
  })
}

//获取物料类型
export function GetMaterialTypeList() {
  return request({
    url: `${baseURL}/MaterialType`,
    method: 'get',
  })
}

//获取基本单位
export function GetMaterialUnitList() {
  return request({
    url: `${baseURL}/MaterialUnit`,
    method: 'get',
  })
}

//获取重要度
export function GetMeasuringimportanceList() {
  return request({
    url: `${baseURL}/Measuringimportance`,
    method: 'get',
  })
}

//获取材质
export function GetMeasuringTextureList() {
  return request({
    url: `${baseURL}/MeasuringTexture`,
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
