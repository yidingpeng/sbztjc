import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/ContractInfo/GetContractPagedList',
    method: 'post',
    params,
  })
}

export function getProjectCode(params) {
  return request({
    url: 'ProjectBasics/TreeList',
    method: 'get',
    params,
  })
}

export function getContractMode() {
  return request({
    url: '/ContractInfo/getContractMode',
    method: 'get',
  })
}

export function doAdd(data) {
  return request({
    url: '/ContractInfo/ContractAdd',
    method: 'post',
    data,
  })
}

export function InsertFile(data) {
  return request({
    url: '/ContractInfo/InsertFile',
    method: 'post',
    Headers: {
      'Content-Type': 'application/json; charset=utf-8',
    },
    data,
  })
}

export function getInsertFileUrl() {
  return request({
    url: '/ContractInfo/GetInsertFilePath',
    method: 'get',
  })
}

export function GetDownloadFilePath() {
  return request({
    url: '/ContractInfo/GetDownloadFilePath',
    method: 'get',
  })
}

export function UpdownFile(data) {
  return request({
    url: '/ContractInfo/UpdownFile',
    method: 'post',
    data,
  })
}

export function doEdit(data) {
  return request({
    url: '/ContractInfo',
    method: 'put',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/ContractInfo',
    method: 'delete',
    data,
  })
}
//GetByIdList
export function ContractDetailList(params) {
  return request({
    url: '/ContractInfo/GetByIdList',
    method: 'get',
    params,
  })
}

export function DeleteFilesByPid(params) {
  return request({
    url: '/ContractInfo/DeleteFilesByPid',
    method: 'get',
    params,
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

//获取最新的内部合同编码
export function GetTheLastCtCode() {
  return request({
    url: '/ContractInfo/GetTheLastCtCode',
    method: 'get',
  })
}

export function ProBasicsTreeList(params) {
  return request({
    url: '/ProjectBasics/TreeListTabel',
    method: 'get',
    params,
  })
}

//根据合同id查询回款计划数据
export function GetPaymentCListByConId(params) {
  return request({
    url: '/PaymentCollectionPlan/List',
    method: 'get',
    params,
  })
}

//回款计划确认按钮
export function DoEditOrAdd(data) {
  return request({
    url: '/PaymentCollectionPlan',
    method: 'put',
    data,
  })
}

export function DoDeleteCtIdZero(params) {
  return request({
    url: 'PaymentCollectionPlan/DoDeleteCtIdZero',
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

//Id集合查询回款信息
export function GetIdsList(params) {
  return request({
    url: '/ContractInfo/GetIdsList',
    method: 'get',
    params,
  })
}
//合同项目信息新增
export function ContractDetailAdd(data) {
  return request({
    url: '/ContractInfo/ContractDetailAdd',
    method: 'post',
    data,
  })
}
//合同项目信息修改
export function ContractDetailEdit(data) {
  return request({
    url: '/ContractInfo/ContractDetailEdit',
    method: 'put',
    data,
  })
}
//合同项目信息删除
export function ContractDetailDelete(data) {
  return request({
    url: '/ContractInfo/ContractDetailDelete',
    method: 'delete',
    data,
  })
}

//回款比例信息保存
export function BiLiEditOrAdd(data) {
  return request({
    url: '/PaymentCollectionPlan/BiLiEditOrAdd',
    method: 'put',
    data,
  })
}
