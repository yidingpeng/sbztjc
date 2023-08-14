<template>
	<view>
		<uni-list>
			<uni-list-item showArrow title="个人资料" />
			<uni-list-item showArrow title="账户安全" />
		</uni-list>
	</view>
	<view class="quit-box">
		<button @click="userQuit" class="quit-submit" type="warn">退出账号</button>
	</view>
</template>

<script setup>
	const userQuit = () => {
		uni.removeStorageSync('token');
		uni.reLaunch({
			url: '/pages/user/login/login'
		})
	}
</script>

<style lang="scss" scoped>
	.quit-box {
		margin-top: 1rem;
		padding: 0 1rem;

		.quit-submit {
			background-color: #F47A6B;
		}
	}
</style>
