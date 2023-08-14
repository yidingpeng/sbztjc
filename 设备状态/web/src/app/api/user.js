import {
	http
} from '@/utils/http.js'

export function login(data) {
	return http.request({
		url: '/Login',
		method: 'POST',
		data
	})
}

export function userInfo() {
	return http.request({
		url: '/Login/UserInfo',
		method: 'GET',
	})
}

export function getUserId(){
	return http.request({
		url:'/User/GetUserId',
		method:'GET'
	})
}
export function getUserList() {
	return http.request({
		url:'/User/UserList',
		method: 'GET',
	})
}

export function GetToken(){
	return http.request({
		url:'/User/GetToken',
		method:'GET'
	})
}


//账号查询用户信息
export function GetAccountInfo(params) {
	return http.request({
		url: '/User/GetAccountInfo',
		method: 'GET',
		params,
	})
}
//模糊查询用户集合
export function GetByNameList(params) {
	return http.request({
		url: '/User/GetByNameList',
		method: 'GET',
		params,
	})
}