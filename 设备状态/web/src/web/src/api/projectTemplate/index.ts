import request from '@/utils/request'
//配置阶段列表
export function getList(params: any) {
  return request({
    url: '/TaskPlan/GetStageList',
    method: 'get',
    params,
  })
}
//新增或修改
export function StageAddOrEdit(data: any) {
  return request({
    url: '/TaskPlan/StageAddOrEdit',
    method: 'post',
    data,
  })
}
//删除
export function StageDelete(data: any) {
  return request({
    url: '/TaskPlan/StageDelete',
    method: 'delete',
    data,
  })
}

// export function editTemplate(data: any) {
//   return request({
//     url: '/projectTemplate',
//     method: 'put',
//     data,
//   })
// }

export function StageSortEdit(data: any) {
  return request({
    url: '/TaskPlan/StageSortEdit',
    method: 'post',
    data,
  })
}
//上移下移
export function MoveEdit(params: any) {
  return request({
    url: '/TaskPlan/MoveEdit',
    method: 'post',
    params,
  })
}

//根据类型获取最大或最小的排序号
export function GetSort(params: any) {
  return request({
    url: '/TaskPlan/GetSort',
    method: 'get',
    params,
  })
}
