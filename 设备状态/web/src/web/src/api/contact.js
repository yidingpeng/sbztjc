import request from '@/utils/request'
// const BaseApi = 'http://192.168.0.53:8087/api'
const baseUrl = '/Contact'
const baseUrl2 = 'PersonalCenter'
export function getList(params) {
  return request({
    url: `${baseUrl}/List`,
    method: 'get',
    params,
  })
}

export function doAdd(data) {
  return request({
    url: baseUrl,
    method: 'post',
    data,
  })
}

export function doEdit(data) {
  return request({
    url: baseUrl,
    method: 'put',
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

export function CompanyNameList(params) {
  console.log(123)
  return request({
    url: `${baseUrl}/CompanyFullName`,
    method: 'get',
    params,
  })
}

export function ContactsCategoryList(params) {
  return request({
    url: `${baseUrl}/ContactsType`,
    method: 'get',
    params,
  })
}

// 获取机构表机构部门信息
export function GetOrganizationNameList(params) {
  return request({
    url: `${baseUrl2}/OrganizationName`,
    method: 'get',
    params,
  })
}

export function GetCompanyAllList() {
  return request({
    url: 'ClientCompany/GetAllList',
    method: 'get',
  })
}

//获取市场片区信息
export function GetSalseAreaSelect(params) {
  return request({
    url: '/bd_SalesAreaInfo/GetSalseAreaSelect',
    method: 'get',
    params,
  })
}

//字典获取客户级别
export function GetDicClient() {
  return request({
    url: '/ClientCompany/DictionaryClient',
    method: 'get',
  })
}
