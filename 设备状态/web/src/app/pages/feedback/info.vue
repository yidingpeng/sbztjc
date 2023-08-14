<template>
	<view class="container">
		<uni-search-bar class="SearchMenu" placeholder="请输入项目名称或编号" @confirm="search" @cancel="cancel" @clear="clear">
		</uni-search-bar>
		<uni-card v-for="(item,index) in list" :key="index">
			<uni-row>
				<uni-col>
					<view>
						<uni-title type="h5" color="#666" :title="item.projectName+'('+item.projectCode+')'">
						</uni-title>
					</view>
				</uni-col>
			</uni-row>
			<uni-row>
				<uni-col :span="12">
					<view>反馈人员：{{list[index].userName}}</view>
				</uni-col>
				<uni-col :span="12">
					<view>接收人员：{{list[index].solutionName}}</view>
				</uni-col>
			</uni-row>
			<uni-row>
				<uni-col>处理动态：
					<text style="color: red;"
						v-if="list[index].dealWithDynamic=='ProblemFBStatus1'">{{list[index].dealWithDynamicTxt}}</text>
					<text style="color: #ff571a;"
						v-else-if="list[index].dealWithDynamic=='ProblemFBStatus2'">{{list[index].dealWithDynamicTxt}}</text>
					<text style="color: #ff571a;"
						v-else-if="list[index].dealWithDynamic=='ProblemFBStatus3'">{{list[index].dealWithDynamicTxt}}</text>
					<text style="color: green;"
						v-else-if="list[index].dealWithDynamic=='ProblemFBStatus4'">{{list[index].dealWithDynamicTxt}}</text>
					<text style="color: #7ba428;"
						v-else-if="list[index].dealWithDynamic=='ProblemFBStatus5'">{{list[index].dealWithDynamicTxt}}</text>
					<text style="color: #7ba428;"
						v-else-if="list[index].dealWithDynamic=='ProblemFBStatus6'">{{list[index].dealWithDynamicTxt}}</text>
				</uni-col>
			</uni-row>
			<uni-row>
				<uni-col>
					<view>要求完成时间：<uni-dateformat :date="list[index].feedbackTime" format="yyyy-MM-dd">
						</uni-dateformat>
					</view>
				</uni-col>
			</uni-row>
			<uni-row style="padding-top: 5px;">
				<uni-col :span="5">
					<uni-tag v-if="list[index].userName == state.userid"
						:disabled="list[index].dealWithDynamic == 'ProblemFBStatus1' ? false :true" text="编辑"
						type="primary" @click="Edit(item.id)"
						custom-style="background-color: #2979ff;;border-color: #2979ff;; color: #fff; font-size:14px;" />
				</uni-col>
				<uni-col :span="5">
					<uni-tag v-if="state.scry == true"
						:disabled="list[index].dealWithDynamic== 'ProblemFBStatus1' ? false : true" text="判定"
						type="primary" @click="PanDin(item.id)"
						custom-style="background-color: #2979ff;;border-color: #2979ff;; color: #fff; font-size:14px;" />
				</uni-col>
				<uni-col :span="5">
					<uni-tag v-if="list[index].solutionName == state.userid"
						:disabled="list[index].dealWithDynamic== 'ProblemFBStatus2' ? false : true" text="处理"
						@click="linkTDealwith(item.id)"
						custom-style="background-color: #f3a73f;;border-color: #f3a73f;; color: #fff; font-size:14px;" />
				</uni-col>
				<uni-col :span="5">
					<uni-tag v-if="state.zlry == true"
						:disabled="list[index].dealWithDynamic== 'ProblemFBStatus3' ? false : true" text="验证"
						@click="linkTDealwith1(item.id)"
						custom-style="background-color: #18bc37;;border-color: #18bc37;; color: #fff; font-size:14px;" />
				</uni-col>
				<uni-col :span="4">
					<uni-tag text="详情" @click="linkTDealwith2(item.id)"
						custom-style="background-color: #2979ff;border-color: #2979ff; color: #fff; font-size:14px;" />
				</uni-col>
			</uni-row>
		</uni-card>
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
	const userStore = useUserStore();
	const {
		realName
	} = storeToRefs(userStore);
	import {
		GetFeedbackAllList,
		DealWithdynamic,
		GetRoleNameByUserId,
	} from '@/api/feedback.js'
	import {
		GetDeployCode,
	} from '@/api/Purchase.js'
	import {
		onReachBottom,
		onPullDownRefresh,
	} from '@dcloudio/uni-app';


	//页面触底事件
	onReachBottom(() => {
		state.PageNo++
		GetFeedbakList()
	})
	//页面下拉刷新事件
	onPullDownRefresh(async () => {
		state.searchKey = ''
		state.PageNo = 1
		let ListData = await GetFeedbackAllList({
			key: state.searchKey,
			PageNo: state.PageNo,
			PageSize: state.PageSize
		})
		state.list = ListData.data
		uni.stopPullDownRefresh()
		//await FeedbackFunction()
		let data = await GetDeployCode({
			code: 'FeedbackFunction'
		});
		let subString = data.data.value.split(",");
		subString.forEach((item, index) => {
			if (index == 0) {
				state.PdShow = JSON.parse(item)
			} else if (index == 1) {
				state.ChuLi = JSON.parse(item)
			} else if (index == 2) {
				state.YanZheng = JSON.parse(item)
			}
		})
	})

	const state = reactive({
		list: [],
		PageNo: 1,
		PageSize: 10,
		searchKey: '',
		PdShow: false,
		ChuLi: false,
		YanZheng: false,
		userid: null,
		scry: false, //责任判定按钮控制
		zlry:false, //验证按钮控制
	})
	const linkTDealwith = (e) => {
		uni.navigateTo({
			url: '/pages/feedback/dealwith?id=' + e,
		});
	}
	const linkTDealwith1 = (e) => {
		uni.navigateTo({
			url: '/pages/feedback/dealwith1?id=' + e,
		});
	}
	const linkTDealwith2 = (e) => {
		uni.navigateTo({
			url: '/pages/feedback/dealwith2?id=' + e,
		});
	}
	const PanDin = (e) => {
		uni.navigateTo({
			url: '/pages/feedback/determine?id=' + e,
		});
	}
	const Edit = (e) => {
		uni.navigateTo({
			url: '/pages/feedback/edit?id=' + e,
		});
	}
	//获取问题反馈信息数据
	const GetFeedbakList = async () => {
		if (state.searchKey == '') {
			let ListData = await GetFeedbackAllList({
				PageNo: state.PageNo,
				PageSize: state.PageSize
			})
			ListData.data.forEach(item => {
				state.list.push(item)
			})
		} else {
			let ListData = await GetFeedbackAllList({
				key: state.searchKey,
				PageNo: state.PageNo,
				PageSize: state.PageSize
			})
			ListData.data.forEach(item => {
				state.list.push(item)
			})
		}
		//获取判定人员
		let PDRdata = await GetDeployCode({
			code: 'Judging'
		});
		let UserIdData = PDRdata.data.value.split(',');
		state.userid = realName
		UserIdData.forEach((item) => {
			if (item == state.userid) {
				state.scry = true
			}
		})
		//获取质量验证通知人
		let YZRdata = await GetDeployCode({
			code: 'ExperimentResult'
		});
		let YZData = YZRdata.data.value.split(',');
		YZData.forEach((item) => {
			if (item == state.userid) {
				state.zlry = true
			}
		})
	}
	const FeedbackFunction = async () => {
		let data = await GetDeployCode({
			code: 'FeedbackFunction'
		});
		let subString = data.data.value.split(",");
		subString.forEach((item, index) => {
			if (index == 0) {
				state.PdShow = JSON.parse(item)
			} else if (index == 1) {
				state.ChuLi = JSON.parse(item)
			} else if (index == 2) {
				state.YanZheng = JSON.parse(item)
			}
		})
	}

	//搜索问题反馈信息
	const search = async (key) => {
		state.PageNo = 1
		state.searchKey = key.value
		let ListData = await GetFeedbackAllList({
			key: key.value,
			PageNo: state.PageNo,
			PageSize: state.PageSize
		})
		state.list = ListData.data
	}
	const cancel = async () => {
		state.PageNo = 1
		state.searchKey = ''
		let ListData = await GetFeedbackAllList({
			key: state.searchKey,
			PageNo: state.PageNo,
			PageSize: state.PageSize
		})
		state.list = ListData.data
	}

	const clear = async () => {
		state.PageNo = 1
		state.searchKey = ''
		let ListData = await GetFeedbackAllList({
			key: state.searchKey,
			PageNo: state.PageNo,
			PageSize: state.PageSize
		})
		state.list = ListData.data
	}
	const {
		list,
		PageNo,
		PageSize,
		searchKey,
		PdShow,
		ChuLi,
		YanZheng,
		userid,
		scry,
		zlry,
	} = toRefs(state)

	onMounted(() => {
		GetFeedbakList()
	})
</script>

<style>
	/*将搜索栏固定在顶部*/
	.SearchMenu {
		position: sticky;
		/* #ifdef H5 */
		top: 44px;
		/* #endif */
		/* #ifndef H5 */
		/* top: 0; */
		/* #endif */
		z-index: 999;
		/* flex: 1; */
		/* flex-direction: column; */
		overflow: hidden;
		background-color: #ffffff;
	}
</style>
