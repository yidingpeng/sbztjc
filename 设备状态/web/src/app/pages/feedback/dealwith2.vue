<template>
	<view class="container">
		<uni-collapse ref="collapse" v-model="state.value">
			<uni-collapse-item title="基础信息(提交人员)">
				<view class="content">
					<uni-forms labelWidth="90" labelAlign="right">
						<uni-forms-item label="项目编号:" name="projectCode">
							{{formData.projectCode}}
						</uni-forms-item>
						<uni-forms-item label="项目名称:" name="projectName">
							{{formData.projectName}}
						</uni-forms-item>
						<uni-forms-item label="发生地址:" name="addressId">
							<uni-data-checkbox style="padding-top: 6px;" v-model="formData.addressId"
								:localdata="address" />
						</uni-forms-item>
						<uni-forms-item label="问题来源:" name="source">
							<text style="line-height: 36px;">{{formData.source}}</text>
						</uni-forms-item>
						<uni-forms-item label="问题描述:" name="problemDescription">
							<view style="max-height: 125px; overflow: auto;">
								{{formData.problemDescription}}
							</view>
						</uni-forms-item>
						<uni-forms-item label="问题类型:" name="problemTypeName">
							{{formData.problemTypeName}}
						</uni-forms-item>
						<uni-forms-item label="接收人员:" name="solutionName">
							{{formData.solutionName}}
						</uni-forms-item>
						<uni-forms-item label="要求完成时间:" name="feedbackTime">
							<uni-dateformat :date="formData.feedbackTime" format="yyyy-MM-dd">
							</uni-dateformat>
						</uni-forms-item>
						<uni-forms-item label="原因类型:" name="reasonType">
							<text v-show='false'>{{formData.reasonTypeName}}</text>
							<text>{{formData.reasonTypeName}}</text>
						</uni-forms-item>
						<uni-forms-item label="判定依据:" name="pfb_ExceptionDesc">
							<view style="max-height: 125px; overflow: auto;">
								{{formData.pfb_ExceptionDesc}}
							</view>
						</uni-forms-item>
						<uni-forms-item label="反馈图片:">
							<image v-for="(item,index) in imgUrls" :key="index" :src="item" @click="clickImg(item)"
								class="prewImg">
							</image>
						</uni-forms-item>
					</uni-forms>
				</view>
			</uni-collapse-item>
			<uni-collapse-item title="问题处理(接收人员)">
				<view class="content">
					<uni-forms labelWidth="90" labelAlign="right">
						<uni-forms-item label="计划开始时间:" name="planStartTime">
							<uni-dateformat style="line-height: 36px;" :date="formData.planStartTime"
								format="yyyy-MM-dd">
							</uni-dateformat>
						</uni-forms-item>
						<uni-forms-item label="计划完成时间:" name="planEndTime">
							<uni-dateformat style="line-height: 36px;" :date="formData.planEndTime" format="yyyy-MM-dd">
							</uni-dateformat>
						</uni-forms-item>
						<uni-forms-item label="实际开始时间:" name="estimatedSettlementDate">
							<uni-dateformat :date="formData.estimatedSettlementDate" format="yyyy-MM-dd">
							</uni-dateformat>
						</uni-forms-item>
						<uni-forms-item label="原因分析:" name="causeAnalysis">
							<view style="max-height: 125px; overflow: auto;">
								{{formData.causeAnalysis}}
							</view>
						</uni-forms-item>
						<uni-forms-item label="改善措施:" name="improvement">
							<view style="max-height: 125px; overflow: auto;">
								{{formData.improvement}}
							</view>
						</uni-forms-item>
					</uni-forms>
				</view>
			</uni-collapse-item>
			<uni-collapse-item title="结果处理(提交人员)">
				<view class="content">
					<uni-forms labelWidth="90" labelAlign="right">
						<uni-forms-item label="验证结果:" name="isQualified">
							<text v-if="formData.dealWithDynamic == 'ProblemFBStatus4' 
								||formData.dealWithDynamic == 'ProblemFBStatus5'
								||formData.dealWithDynamic == 'ProblemFBStatus6'">
								{{formData.isQualified == 1 ? '不合格':'合格'}}
							</text>
						</uni-forms-item>
						<uni-forms-item label="不合格原因:" name="unqualifiedReason" v-show="isHG2">
							{{formData.unqualifiedReason}}
						</uni-forms-item>
						<uni-forms-item label="处理动态:" name="dealWithDynamic">
							{{formData.dealWithDynamicTxt}}
						</uni-forms-item>
						<uni-forms-item label="实际完成时间:" name="actualSettlementDate" v-show="isHG">
							<uni-dateformat style="line-height: 36px;" :date="formData.actualSettlementDate"
								format="yyyy-MM-dd">
							</uni-dateformat>
						</uni-forms-item>
					</uni-forms>
				</view>
			</uni-collapse-item>
		</uni-collapse>
		<uni-popup ref="Dialog" type="dialog">
			<image :src="imageSrc"></image>
		</uni-popup>
	</view>
</template>

<script setup>
	import {
		getReasonType,
		getInsertFileUrl,
		GetAllListById,
		GetFeedbackIdByPtid,
		getFeedbackImg,
	} from '@/api/feedback.js'
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
	} = storeToRefs(userStore)

	const state = reactive({
		Dialog: null,
		// 基础表单数据
		formData: {},
		picId: [],
		// 发生地址数据源
		address: [{
			text: '厂内',
			value: '1',
			disable: true
		}, {
			text: '厂外',
			value: '2',
			disable: true
		}],
		fileUrl: '',
		imgUrls: [],
		isHG: false,
		isHG2: false,
		imageSrc: '',
	})



	//根据路由参数获取反馈信息
	const GetFeedbackData = async (param) => {
		let feedbackData = await GetAllListById({
			id: param
		})
		state.formData = Object.assign({}, feedbackData.data)
		//console.log(state.formData)
		if (state.formData.isQualified == 1) {
			state.isHG = false
			state.isHG2 = true
		} else {
			state.isHG = true
			state.isHG2 = false
		}
		if (state.formData.planStartTime.indexOf('0001-01-01') != -1) {
			state.formData.planStartTime = ''
		}
		if (state.formData.planEndTime.indexOf('0001-01-01') != -1) {
			state.formData.planEndTime = ''
		}
		if (state.formData.estimatedSettlementDate.indexOf('0001-01-01') != -1) {
			state.formData.estimatedSettlementDate = null
		}
		if (state.formData.feedbackTime.indexOf('0001-01-01') != -1) {
			state.formData.feedbackTime = ''
		}
		if (state.formData.actualSettlementDate.indexOf('0001-01-01') != -1) {
			state.formData.actualSettlementDate = ''
		}
		GetPicIds(state.formData.id)
	}
	//根据id获取反馈图片ids
	const GetPicIds = async (param) => {
		let picData = await GetFeedbackIdByPtid({
			pid: param
		})
		//console.log(picData.ids)
		state.picId = picData.ids
		state.picId.forEach((item) => {
			GetPic(item)
		})
	}
	//获取图片
	const GetPic = async (param) => {
		let picurl = await getFeedbackImg({
			id: param
		})
		state.imgUrls.push(picurl.url)
	}

	const clickImg = (item) => {
		state.Dialog.open()
		state.imageSrc = item
	}
	const {
		formData,
		address,
		imgUrls,
		dealWithDynamicOp,
		isHG,
		isHG2,
		Dialog,
		imageSrc,
	} = toRefs(state)

	onMounted(() => {
		//获取路由中的参数
		let route = getCurrentPages();
		let curParam = route[route.length - 1].$page.options.id;
		GetFeedbackData(curParam)
	})
</script>

<style lang="scss">
	.example {
		padding: 15px;
		background-color: #fff;
	}

	.example-body {
		padding: 10px;
		padding-top: 0;
	}

	.prewImg {
		width: 80px;
		height: 80px;
		margin-right: 3px;
		border-radius: 20%;
	}

	.uni-forms-item {
		font-size: 12px;
		line-height: 36px;
		color: #666;
	}
</style>
