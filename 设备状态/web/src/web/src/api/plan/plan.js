import request from '@/utils/request'

export function GetPlanList(params) {
  return request({
    url: '/plan/GetPlanList',
    method: 'get',
    params,
  })
}

export function GetTaskList(params) {
  return request({
    url: '/plan/GetTaskList',
    method: 'get',
    params,
  })
}

export function PlanAddOrEdit(data) {
  return request({
    url: '/plan/PlanAddOrEdit',
    method: 'post',
    data,
  })
}

export function SavePlan(data) {
  return request({
    url: '/plan/SavePlan',
    method: 'post',
    data,
  })
}

export function PlanDelete(data) {
  return request({
    url: '/plan/PlanDelete',
    method: 'delete',
    data,
  })
}

export function MoveTask(data) {
  return request({
    url: '/plan/moveTask',
    method: 'put',
    data,
  })
}

export function ImportPlanTask(data) {
  return request({
    url: '/plan/importPlanTask',
    method: 'post',
    data,
  })
}

export function AddTask(data) {
  return request({
    url: '/plan/addPlanTask',
    method: 'post',
    data,
  })
}

export function DeleteTask(data) {
  return request({
    url: '/plan/deletePlanTask',
    method: 'delete',
    data,
  })
}

export function ClearTask(data) {
  return request({
    url: '/plan/clearPlanTask',
    method: 'delete',
    data,
  })
}

export function EditTaskFiled(data) {
  return request({
    url: '/plan/EditTaskFiled',
    method: 'put',
    data,
  })
}
