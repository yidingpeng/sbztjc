import request from '@/utils/request'
const baseUrl = '/ProblemFeedback'

export function getList(params) {
  return request({
    url: `${baseUrl}/GetList`,
    method: 'get',
    params,
  })
}

export function doEdit(data) {
  return request({
    url: `${baseUrl}/DoEdit`,
    method: 'put',
    data,
  })
}

export function DoDealWith(data) {
  return request({
    url: `${baseUrl}/DoDealWith`,
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

export function getProjectCode(params) {
  return request({
    url: 'ProjectBasics/TreeList',
    method: 'get',
    params,
  })
}

export function getProblemType(params) {
  return request({
    url: `${baseUrl}/getProblemType`,
    method: 'get',
    params,
  })
}

export function getInsertFileUrl() {
  return request({
    url: '/ProblemFeedback/GetInsertFilePath',
    method: 'get',
  })
}

export function GetFilesByPid(params) {
  return request({
    url: `${baseUrl}/GetFilesByPid`,
    method: 'get',
    params,
  })
}

export function GetDownloadFilePath() {
  return request({
    url: `${baseUrl}/GetFilePreviewPath`,
    method: 'get',
  })
}

export function GetPictureData(params) {
  return request({
    url: `${baseUrl}/GetPictureData`,
    method: 'get',
    params,
  })
}

export function DeleteFilesByPid(params) {
  return request({
    url: `${baseUrl}/UpdateImgPidtoZero`,
    method: 'get',
    params,
  })
}

//根据id获取反馈图片信息ids
export function GetFeedbackIdByPtid(params) {
  return request({
    url: '/ProblemFeedback/GetFeedbackIdByPtid',
    method: 'get',
    params,
  })
}

//获取反馈图片
export function getFeedbackImg(params) {
  return request({
    url: '/ProblemFeedback/Get',
    method: 'get',
    params,
  })
}

//根据id获取反馈信息
export function GetFeedbackById(params) {
  return request({
    url: '/ProblemFeedback/GetAllListById',
    method: 'get',
    params,
  })
}

//获取处理动态
export function dealWithdynamic() {
  return request({
    url: '/ProblemFeedback/dealWithdynamic',
    method: 'get',
  })
}
//根据查询条件查询所有的问题反馈信息
//GetProblemAllList
export function GetProblemAllList(params) {
  return request({
    url: '/ProblemFeedback/GetProblemAllList',
    method: 'get',
    params,
  })
}

//发送消息给接收人员
export function problemMagSend(params) {
  return request({
    url: '/ProblemFeedback/problemMagSend',
    method: 'post',
    params,
  })
}

//问题反馈超时提醒
export function FateMagSend(params) {
  return request({
    url: '/ProblemFeedback/FateMagSend',
    method: 'post',
    params,
  })
}
