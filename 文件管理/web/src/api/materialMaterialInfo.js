import request from '@/utils/request'

export function getList(params) {
  return request({
    url: '/Material/MaterialPageList',
    method: 'get',
    params,
  })
}

export function doEdit(data) {
  return request({
    url: '/Material',
    method: 'put',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/Material',
    method: 'delete',
    data,
  })
}

export function doAdd(data) {
  return request({
    url: '/Material',
    method: 'post',
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

export function GetDicMaterialG(params) {
  return request({
    url: 'Material/GetDicMaterialG',
    method: 'get',
    params,
  })
}

export function GetDicMaterialU(params) {
  return request({
    url: 'Material/GetDicMaterialU',
    method: 'get',
    params,
  })
}

export function GetDicMaterialC(params) {
  return request({
    url: 'Material/GetDicMaterialC',
    method: 'get',
    params,
  })
}
//物料分类数据表
export function GetMaterialSortList(params) {
  return request({
    url: 'Material/GetMaterialSortList',
    method: 'get',
    params,
  })
}

//物料换算基础数据分页
export function GetHsPageList(params) {
  return request({
    url: 'Material/GetHsPageList',
    method: 'get',
    params,
  })
}
//物料换算基础数据新增
export function MatHsDoAdd(data) {
  return request({
    url: '/Material/MatHsDoAdd',
    method: 'post',
    data,
  })
}
//物料换算基础数据修改
export function MatHsDoEdit(data) {
  return request({
    url: '/Material/MatHsDoEdit',
    method: 'put',
    data,
  })
}

//物料换算基础数据删除
export function MatHsDoDelete(data) {
  return request({
    url: '/Material/MatHsDoDelete',
    method: 'delete',
    data,
  })
}
//MatListByCode
//编码查信息
export function MatListByCode(params) {
  return request({
    url: 'Material/MatListByCode',
    method: 'get',
    params,
  })
}

//编码查换算信息
export function GetByCode(params) {
  return request({
    url: 'Material/GetByCode',
    method: 'get',
    params,
  })
}
