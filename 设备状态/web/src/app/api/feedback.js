import {
	http
} from '@/utils/http.js'

//获取项目信息
export function getProjectCode(params) {
	return http.request({
		url: '/ProjectBasics/GetAllList',
		method: 'get',
		params,
	})
}

//获取问题反馈所有信息
export function GetFeedbackAllList(params) {
	return http.request({
		url: '/ProblemFeedback/GetAllList',
		method: 'get',
		params,
	})
}

//根据id获取问题反馈信息
export function GetAllListById(params){
	return http.request({
		url:'/ProblemFeedback/GetAllListById',
		method:'get',
		params,
	})
}

//根据id获取反馈图片信息ids
export function GetFeedbackIdByPtid(params){
	return http.request({
		url:'/ProblemFeedback/GetFeedbackIdByPtid',
		method:'get',
		params,
	})
}

//获取问题类型
export function getProblemType() {
	return http.request({
		url: '/ProblemFeedback/getProblemType',
		method: 'get',
	})
}

//获取原因类型
export function getReasonType() {
	return http.request({
		url: '/ProblemFeedback/GetReasonType',
		method: 'get',
	})
}

//获取上传图片文件地址
export function getInsertFileUrl() {
  return http.request({
    url: '/ProblemFeedback/GetInsertFilePath2',
    method: 'get',
  })
}

//添加问题反馈
export function doAdd(data) {
  return http.request({
    url: '/ProblemFeedback/DoAdd',
    method: 'post',
    data,
  })
}

//问题处理（编辑）
export function doEdit(data){
	return http.request({
	  url: '/ProblemFeedback/DoDealWith',
	  method: 'put',
	  data,
	})
}

//获取反馈图片
export function getFeedbackImg(params){
	return http.request({
		url:'/ProblemFeedback/Get',
		method:'get',
		params,
	})
}
//根据项目id获取项目团队成员
export function GetProTeamById(params){
	return http.request({
		url:'/projectcontacts/GetByIdList',
		method:'get',
		params,
	})
}

//获取处理动态字典
export function DealWithdynamic(params){
	return http.request({
		url:'/ProblemFeedback/DealWithdynamic',
		method:'get',
		params,
	})
}

//获取反馈图片
export function GetFilesByPid(params){
	return http.request({
		url:'/ProblemFeedback/GetFilesByPid',
		method:'get',
		params,
	})
}
//通过角色名查找下面的用户ID
export function GetRoleNameByUserId(params){
	return http.request({
		url:'/ProblemFeedback/GetRoleNameByUserId',
		method:'get',
		params,
	})
}

//模糊查询项目集合
export function GetByOddsProMain(params){
	return http.request({
		url:'/Project/GetByOddsProMain',
		method:'get',
		params,
	})
}