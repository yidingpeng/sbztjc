import request from '@/utils/request'

const BaseUrl = 'User'
const BaseUrl2 = 'PersonalCenter'
//个人中心基本信息
export function getUserData(params) {
  return request({
    url: BaseUrl,
    method: 'get',
    params,
  })
}

//个人中心修改密码
export function DoUpdatePwd(data) {
  return request({
    url: `${BaseUrl2}/Password`,
    method: 'put',
    data,
  })
}

//个人中心修改基本信息
export function DoUpdateMsg(data) {
  return request({
    url: `${BaseUrl2}/PersonalMsg`,
    method: 'put',
    data,
  })
}
// 获取机构表机构部门信息
export function GetOrganizationNameList(params) {
  return request({
    url: `${BaseUrl2}/OrganizationName`,
    method: 'get',
    params,
  })
}
