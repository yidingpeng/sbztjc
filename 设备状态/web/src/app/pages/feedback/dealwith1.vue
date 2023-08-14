<template>
	<view class="container">
		<uni-section title="结果验证" type="line">
			<view class="example">
				<!-- 基础用法，不包含校验规则 -->
				<uni-forms ref="formRef" labelWidth="90" labelAlign="center" :modelValue="formData" :rules="rules">
					<uni-forms-item label="项目编号" name="projectCode" required>
						<text style="line-height: 36px;">{{formData.projectCode}}</text>
					</uni-forms-item>
					<uni-forms-item label="项目名称" name="projectName" required>
						<text style="line-height: 36px;">{{formData.projectName}}</text>
					</uni-forms-item>
					<uni-forms-item label="发生地址" name="addressId" required>
						<uni-data-checkbox style="padding-top: 6px;" v-model="formData.addressId" :localdata="address" />
					</uni-forms-item>
					<uni-forms-item label="问题来源" name="source" required>
						<text style="line-height: 36px;">{{formData.source}}</text>
					</uni-forms-item>
					<uni-forms-item label="问题描述" name="problemDescription" required>
						<view style="line-height: 36px;max-height: 125px; overflow: auto;">
							{{formData.problemDescription}}
						</view>
					</uni-forms-item>
					<uni-forms-item label="问题类型" name="problemTypeName" required>
						<text style="line-height: 36px;">{{formData.problemTypeName}}</text>
					</uni-forms-item>
					<uni-forms-item label="接收人员" name="solutionName" required>
						<text style="line-height: 36px;">{{formData.solutionName}}</text>
					</uni-forms-item>
					<uni-forms-item label="要求完成时间" name="feedbackTime" required>
						<uni-dateformat style="line-height: 36px;" :date="formData.feedbackTime" format="yyyy-MM-dd">
						</uni-dateformat>
					</uni-forms-item>
					<uni-forms-item label="原因类型" name="reasonTypeName" required>
						<text style="line-height: 36px;">{{formData.reasonTypeName}}</text>
					</uni-forms-item>
					<uni-forms-item label="判定依据" name="pfb_ExceptionDesc">
						<view style="line-height: 36px;max-height: 125px; overflow: auto;">
							{{formData.pfb_ExceptionDesc}}
						</view>
					</uni-forms-item>
					<uni-forms-item label="计划开始时间" name="planStartTime" required>
						<uni-dateformat style="line-height: 36px;" :date="formData.planStartTime" format="yyyy-MM-dd">
						</uni-dateformat>
					</uni-forms-item>
					<uni-forms-item label="计划完成时间" name="planEndTime" required>
						<uni-dateformat style="line-height: 36px;" :date="formData.planEndTime" format="yyyy-MM-dd">
						</uni-dateformat>
					</uni-forms-item>
					<uni-forms-item label="实际开始时间" name="estimatedSettlementDate" required>
						<uni-dateformat style="line-height: 36px;" :date="formData.estimatedSettlementDate"
							format="yyyy-MM-dd"></uni-dateformat>
					</uni-forms-item>
					<uni-forms-item label="原因分析" name="causeAnalysis" required>
						<view style="line-height: 36px;max-height: 125px; overflow: auto;">
							{{formData.causeAnalysis}}
						</view>
					</uni-forms-item>
					<uni-forms-item label="改善措施" name="improvement" required>
						<view style="line-height: 36px;max-height: 125px; overflow: auto;">
							{{formData.improvement}}
						</view>
					</uni-forms-item>
					<uni-forms-item label="验证结果" name="isQualified" required>
						<uni-data-checkbox style="padding-top: 6px;" v-model="formData.isQualified" :localdata="YZJGData"
							@change="JieGuoYanZheng" />
					</uni-forms-item>
					<uni-forms-item label="不合格原因" name="unqualifiedReason" required v-if="formData.isQualified == 1"
						:rules="formData.isQualified == 0 ? [] : [{'required': true,errorMessage: '请输入不合格原因'}]">
						<uni-easyinput type="textarea" v-model="formData.unqualifiedReason" placeholder="请输入不合格原因" />
					</uni-forms-item>
					<uni-forms-item label="处理动态" name="dealWithDynamic" required>
						<uni-data-select v-model="formData.dealWithDynamic" :localdata="dealWithDynamicOp"
							:clear="false">
						</uni-data-select>
					</uni-forms-item>
					<uni-forms-item label="实际完成时间" required v-if="formData.isQualified == 0"
						:rules="formData.isQualified == 1 ?[]:[{'required': true,errorMessage: '请选择实际完成时间'}]">
						<uni-datetime-picker type="date" v-model="formData.actualSettlementDate"
							:start="formData.estimatedSettlementDate" placeholder="实际完成时间" />
					</uni-forms-item>
					<uni-forms-item label="反馈图片">
						<image v-for="(item,index) in imgUrls" :key="index" :src="item" @click="clickImg(item)"
							class="prewImg">
						</image>
					</uni-forms-item>
				</uni-forms>
				<view style="text-align: center;">
					<button class="mini-btn" type="primary" @click="FeedBackSubmit()" size="mini"
						:loading="running">提交</button>
				</view>
			</view>
		</uni-section>
		<uni-popup ref="Dialog" type="dialog">
			<image :src="imageSrc"></image>
		</uni-popup>
	</view>
</template>

<script setup>
	import {
		//getReasonType,
		GetAllListById,
		GetFeedbackIdByPtid,
		getFeedbackImg,
		doEdit,
		DealWithdynamic,
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
		running: false,
		formRef: null,
		// 基础表单数据
		formData: {
			isQualified: 0,
			unqualifiedReason: null,
			dealWithDynamic: null,
			actualSettlementDate: null,
		},
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
		// 验证结果数据源
		YZJGData: [{
			text: '合格',
			value: 0,
		}, {
			text: '不合格',
			value: 1,
		}],
		//处理动态
		dealWithDynamicOp: [],
		imgUrls: [],
		reasonTypeText: '',
		Dialog: null,
		imageSrc: '',
	})
	// 校验规则
	const rules = {
		dealWithDynamic: {
			rules: [{
				required: true,
				errorMessage: '请选择处理动态'
			}]
		},
		isQualified: {
			rules: [{
				required: true,
				errorMessage: '请选择验证结果'
			}]
		},
	}
	//处理动态change事件
	// const DealwithChange = (choose) => {
	// 	if (choose == "") {
	// 		state.formData.actualSettlementDate = null
	// 	}
	// }
	//结果验证change事件
	const JieGuoYanZheng = (e) => {
		console.log(e.detail.value)
		//不合格
		if (e.detail.value == 1) {
			state.formData.unqualifiedReason = ''
			state.formData.actualSettlementDate = '0001-01-01'
		} else { //合格
			state.formData.dealWithDynamic = "ProblemFBStatus4"
			state.formData.actualSettlementDate = new Date().toISOString().slice(0, 10)
		}
	}

	//根据路由参数获取反馈信息
	const GetFeedbackData = async (param) => {
		let cldt = await DealWithdynamic()
		//console.log(cldt.data)
		cldt.data.forEach((item) => {
			state.dealWithDynamicOp.push({
				value: item.dictionaryValue,
				text: item.dictionaryText
			})
		})
		let feedbackData = await GetAllListById({
			id: param
		})
		//console.log(feedbackData.data)
		state.formData = Object.assign({}, feedbackData.data)
		if (state.formData.planStartTime.indexOf('0001-01-01') != -1) {
			state.formData.planStartTime = null
		}
		if (state.formData.planEndTime.indexOf('0001-01-01') != -1) {
			state.formData.planEndTime = null
		}
		if (state.formData.estimatedSettlementDate == null) {
			state.formData.estimatedSettlementDate = null
		}
		if (state.formData.actualSettlementDate.indexOf('0001-01-01') != -1) {
			state.formData.actualSettlementDate = new Date().toISOString().slice(0, 10)
		}
		// let wtlxData = await getReasonType()
		// if (state.formData.reasonType != '') {
		// 	wtlxData.data.forEach((item) => {
		// 		if (item.dictionaryValue == state.formData.reasonType) {
		// 			state.reasonTypeText = item.dictionaryText
		// 		}
		// 	})
		// }
		await GetPicIds(state.formData.id)
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

	//提交
	const FeedBackSubmit = () => {
		//console.log(state.formData)
		state['formRef'].validate().then(async res => {
			try {
				//console.log(state.formData)
				state.running = true
				const {
					msg
				} = await doEdit(state.formData)
				uni.showToast({
					title: msg,
					icon: 'success'
				})
				uni.navigateTo({
					url: '/pages/feedback/info'
				});
			} finally {
				state.running = false
			}
		}).catch(err => {})
	}
	const {
		running,
		formRef,
		formData,
		address,
		imgUrls,
		YZJGData,
		dealWithDynamicOp,
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
</style>
