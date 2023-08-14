import {
	defineStore
} from 'pinia'

import {
	userInfo
} from '@/api/user.js'

export const useUserStore = defineStore('user', {
	
	state: () => {
		return {
			realName: '访客',
			avatar: '/static/avatar.png',
			deptName: '湖南润伟智能机器有限公司'
		}
	},
	getters: {
		getRealName: (state) => state.realName,
		getAvatar: (state) => state.avatar,
		getDeptName: (state) => state.deptName
	},
	actions: {
		async getUserInfo() {
			var {
				data: {
					avatar,
					username
				}
			} = await userInfo()
			this.realName = username
		}
	}
})
