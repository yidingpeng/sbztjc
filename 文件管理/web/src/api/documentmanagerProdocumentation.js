import request from '@/utils/request'

export function GetAllFolders(params) {
  return request({
    url: 'File/GetAllFolders',
    method: 'get',
    params,
  })
}

export function GetDocPagedList(params) {
  return request({
    url: 'File/GetDocPagedList',
    method: 'get',
    params,
  })
}

export function getProjectList(params) {
  return request({
    url: '/ProjectBasics/GetAllProList',
    method: 'get',
    params,
  })
}

export function GetFileInfo(params) {
  return request({
    url: 'File/GetFileInfo',
    method: 'get',
    params,
  })
}

export function GetFolderNames() {
  return request({
    url: 'File/GetFolders',
    method: 'get',
  })
}

export function GetFileByFolderName(params) {
  return request({
    url: 'File/GetFileByFolderName',
    method: 'get',
    timeout: 15000,
    params,
  })
}

export function doEdit(data) {
  return request({
    url: '/documentmanager/prodocumentation/doEdit',
    method: 'post',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/documentmanager/prodocumentation/doDelete',
    method: 'post',
    data,
  })
}

//更新文档管理数据库
export function UpdateDocManage() {
  return request({
    url: '/File/UpdateDocManage',
    method: 'get',
    //请求时间1小时
    timeout: 60000 * 60,
  })
}
