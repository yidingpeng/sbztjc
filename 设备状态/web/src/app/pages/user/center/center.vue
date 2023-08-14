<template>
	<view class="header">
		<view class="user-box">
			<view class="avatar">
				<image mode="aspectFit" class="avatar-img" :src="avatar"></image>
			</view>
			<view class="login-info">
				<text>{{realName}}</text>
				<text class="note">{{deptName}}</text>
			</view>
		</view>
	</view>
	<view>
		<uni-list>
			<uni-list-item link to="/pages/user/setting/setting" :show-extra-icon="true" showArrow
				:extra-icon="gearIcon" title="设置" />
			<uni-list-item link to="/pages/user/about/index" :show-extra-icon="true" showArrow :extra-icon="infoIcon" title="关于" />
		</uni-list>
	</view>
</template>

<script setup>
	import {
		useUserStore
	} from '@/store/user.js'

	import {
		storeToRefs
	} from "pinia"

	const userStore = useUserStore();

	const {
		realName,
		avatar,
		deptName
	} = storeToRefs(userStore)

	const gearIcon = {
		color: '#337ecc',
		size: '22',
		type: 'gear-filled'
	}
	const infoIcon = {
		color: '#337ecc',
		size: '22',
		type: 'info-filled'
	}
</script>

<style lang="scss" scoped>
	.header {
		background-color: #337ecc;
		padding: 0 1rem 1rem 1rem;
		color: #fff;
		overflow: hidden;

		.user-box {
			display: flex;
			align-items: center;
		}

		.avatar {
			background-color: #fff;
			width: 3rem;
			height: 3rem;
			border-radius: 50%;
			overflow: hidden;

			.avatar-img {
				width: 100%;
				height: 100%;
			}
		}

		.login-info {
			margin-left: 1rem;
			display: flex;
			flex-direction: column;

			.note {
				color: #DFF1FF;
				font-size: 0.8rem;
				margin-top: 0.2rem;
			}
		}

		.quick-box {
			margin-top: 2rem;
			height: 4.5rem;
			border-radius: 0.5rem;
			box-shadow: 0 0.2rem 1rem 0 #999;
			background: rgba(255, 255, 255, 0.8);
			color: #000;
			display: flex;

			.quick-item {
				display: flex;
				flex: 1 1 0;
				position: relative;
				justify-content: center;
				align-items: center;
			}

			.quick-item:not(:last-child)::after {
				content: "";
				position: absolute;
				height: 50%;
				width: 1px;
				background-color: #c0c4cc;
				top: 25%;
				right: 0;
			}
		}
	}
</style>
