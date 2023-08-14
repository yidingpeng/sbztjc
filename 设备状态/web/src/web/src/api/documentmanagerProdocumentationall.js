import request from '@/utils/request'

//更新文档管理数据库
export function UpdateDocManageAll(params) {
  return request({
    url: '/File/UpdateDocManageAll',
    method: 'get',
    params,
    timeout: 60000 * 60,
  })
}

export function GetAllFolders(params) {
  return request({
    url: 'File/GetAllFolders',
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

export function GetGLFolders() {
  return request({
    url: 'File/GetGLFolders',
    method: 'get',
  })
}
