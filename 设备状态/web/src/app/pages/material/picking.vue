<template>
	<view>
		<view class="uni-container">
			<text v-show="button == true ? false : true">暂无数据</text>
			<uni-card v-for="(item, index) in tableData" :key="index" :title="item.projectName"
				:extra="item.projectCode">
				<view><text class="uni-text">物料：{{item.materialCode}} - {{item.materialName}}</text>
				</view>
				<view>
					<text class="uni-text">数量：{{item.count}}</text>
				</view>
			</uni-card>
			<button v-show="button == true ? true : false" type="primary" @click="LinLiao()" :loading="loading">确认领料</button>
		</view>
	</view>
</template>
<script setup>
	import {
		reactive,
		toRefs,
		onMounted,
	} from 'vue'
	import {
		useUserStore
	} from '@/store/user.js'

	import {
		storeToRefs
	} from "pinia"
	import {
		IssulList,
		BatchPick,
	} from '@/api/Purchase.js'

	const userStore = useUserStore()
	const {
		realName
	} = storeToRefs(userStore)
	const state = reactive({
		tableData: [],
		userName: realName,
		loading: false,
		button: true,
	})

	const getData = async () => {
		const list = await IssulList({
			LLMan: state.userName
		})
		state.button = list.data.length > 0 ? true : false
		state.tableData = list.data
	}
	const LinLiao = async () => {
		if (state.tableData.length > 0) {
			state.loading = true
			const {
				msg
			} = await BatchPick(state.tableData)
			uni.showToast({
				title: msg,
				icon: 'none',
			});
			state.loading = false
			await getData()
		} else {
			uni.showToast({
				title: '无数据!',
				icon: 'none',
			});
		}
	}
	const {
		tableData,
		userName,
		loading,
		button,
	} = toRefs(state)

	onMounted(() => {
		getData()
	})
</script>
