<template>
	<view class="main">
		<view class="login-box">
			<view class="login-form">
				<view class="login-title">
					<text>欢迎登录</text>
					<text class="note">信息录入管理APP</text>
				</view>
				<uni-forms ref="formRef" :modelValue="formData" :rules="rules">
					<uni-forms-item name="Username">
						<uni-easyinput prefixIcon="person" v-model="formData.Username" :styles="inputStyle"
							placeholder="请输入账号" />
					</uni-forms-item>
					<uni-forms-item name="Password">
						<uni-easyinput prefixIcon="locked" v-model="formData.Password" :styles="inputStyle"
							type="password" placeholder="请输入密码" />
					</uni-forms-item>
				</uni-forms>
				<button class="login-submit" @click="userLogin" :loading="running" :disabled="running">登录</button>
			</view>
			
		</view>
	</view>
</template>

<script setup>
	import {
		reactive,
		toRefs
	} from 'vue'

	import {
		login
	} from '@/api/user.js'

	import {
		useUserStore
	} from '@/store/user.js'

	const userStore = useUserStore()

	const inputStyle = {
		borderColor: '#f1f5f9'
	}

	const rules = {
		Username: {
			rules: [{
					required: true,
					errorMessage: '请输入账号',
				},
				{
					maxLength: 20,
					errorMessage: '账号长度不能超过 {maxLength} 个字符',
				}
			]
		},
		Password: {
			rules: [{
				required: true,
				errorMessage: '请输入密码',
			}]
		}
	}

	const state = reactive({
		formRef: null,
		running: false,
		formData: {
			Username: '',
			Password: ''
		}
	})

	const userLogin = () => {
		state['formRef'].validate().then(async res => {
			try {
				state.running = true
				const {
					data: {
						token
					}
				} = await login(state.formData)
				uni.setStorageSync('token', token)
				console.log(token)
				await userStore.getUserInfo()
				uni.switchTab({
					url: '/pages/index/index'
				});
				console.log(res)
			} finally {
				state.running = false
			}
		}).catch(err => {})
	}

	const {
		formRef,
		running,
		formData
	} = toRefs(state)
</script>

<style lang="scss" scoped>
	.main {
		height: 100%;
		background: url('/static/app_bg.png') center center no-repeat;
		background-size: cover;
		display: flex;
		align-items: center;
		justify-content: center;
		color: #fff;
		flex-direction: column;
		padding: 0 1rem;

		.co-name {
			font-size: 2.5rem;
		}

		.login-box {
			background: rgba(255, 255, 255, 0.9);
			width: 100%;
			display: flex;
			align-items: center;
			flex-direction: column;
			padding: 1.5rem 0;
			border-radius: 0.5rem;
			color: #000;

			.login-form {
				width: 80%;

				.login-title {
					display: flex;
					align-items: center;
					flex-direction: column;
					font-size: 1.5rem;
					font-weight: bold;

					.note {
						font-size: 0.8rem;
						font-weight: normal;
						margin: 1rem 0;
					}
				}

				.uni-easyinput {
					background-color: #f1f5f9;
				}

				.login-submit {
					background-color: #337ecc;
					color: #fff;
				}
			}

			.login-help {
				width: 80%;
				display: flex;
				justify-content: flex-end;
				margin-top: 0.5rem;
				color: #434655;
				font-size: 0.8rem;
			}
		}
	}
</style>
