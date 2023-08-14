import {
	http
} from '@/utils/http.js'

export function DoAdd(data) {
	return http.request({
		url: '/Order/DoAdd',
		method: 'post',
		data,
	})
}