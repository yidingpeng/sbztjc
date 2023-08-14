import request from '@/utils/request'
// export function getCurrent(params: any) {
//   return request({
//     url: '/projectTemplate/tasks/current',
//     method: 'get',
//     params,
//   })
// }

// export function getList(params: any) {
//   return request({
//     url: '/projectTemplate/tasks',
//     method: 'get',
//     params,
//   })
// }

export function WBSTabsDoAddOrEdit(data: any) {
  return request({
    url: '/TaskPlan/WBSTabsDoAddOrEdit',
    method: 'post',
    data,
  })
}

export function WBSDoAddOrEdit(data: any) {
  return request({
    url: '/TaskPlan/WBSDoAddOrEdit',
    method: 'post',
    data,
  })
}

// export function editTaskFiled(data: any) {
//   return request({
//     url: '/projectTemplate/tasks',
//     method: 'put',
//     data,
//   })
// }

// export function setTaskAction(data: any) {
//   return request({
//     url: '/projectTemplate/taskAction',
//     method: 'post',
//     data,
//   })
// }

// export function removeTask(data: any) {
//   return request({
//     url: '/projectTemplate/tasks',
//     method: 'delete',
//     data,
//   })
// }

//GetTempLateList
export function GetTempLateList(params: any) {
  return request({
    url: '/TaskPlan/GetTempLateList',
    method: 'get',
    params,
  })
}

//WBSPlanGetSort
export function WBSPlanGetSort(params: any) {
  return request({
    url: '/TaskPlan/WBSPlanGetSort',
    method: 'get',
    params,
  })
}

//WBS计划列表上移下移
export function WBSMoveEdit(params: any) {
  return request({
    url: '/TaskPlan/WBSMoveEdit',
    method: 'post',
    params,
  })
}

//WBSTabsDelete
//WBS计划名称删除
export function WBSTabsDelete(params: any) {
  return request({
    url: '/TaskPlan/WBSTabsDelete',
    method: 'post',
    params,
  })
}

//WBSPlanDelete
//WBS计划列表删除
export function WBSPlanDelete(params: any) {
  return request({
    url: '/TaskPlan/WBSPlanDelete',
    method: 'post',
    params,
  })
}
