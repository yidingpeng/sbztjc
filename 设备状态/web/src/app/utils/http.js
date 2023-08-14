import Request from '@/utils/luch-request/luch-request/index.js'

import netConfig from '@/config/net.config.js'

const http = new Request()

const handleData = async (response) => {
	let data = response.data
	let code = data && netConfig.statusName in data ? data[netConfig.statusName] : response.statusCode
	if (netConfig.successCode.indexOf(code) + 1) code = 200
	switch (code) {
		case 200:
			return response.data
			break
	}
	let msg = `${data && netConfig.messageName in data ? data[netConfig.messageName] : data.Msg}`
	uni.showToast({
		title: `错误代码：${msg}`,
		icon: 'none'
	});
	return Promise.reject(response)
}

//全局配置
http.setConfig((config) => {
	config.baseURL = netConfig.baseURL
	return config
})

http.interceptors.request.use((config) => {
	const token = uni.getStorageSync('token')
	if (token) {
		config.header = {
			...config.header,
			Authorization: `Bearer ${token}`
		}
	}
	return config
})

http.interceptors.response.use((response) => {
	return handleData(response)
}, (response) => {
	return handleData(response)
})

export {
	http
}
