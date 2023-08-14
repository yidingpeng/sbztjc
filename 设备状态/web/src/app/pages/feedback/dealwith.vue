<template>
	<view class="container">
		<uni-section title="信息处理" type="line">
			<view class="example">
				<!-- 基础用法，不包含校验规则 -->
				<uni-forms ref="formRef" labelWidth="90" labelAlign="center" :modelValue="formData" :rules="rules">
					<uni-forms-item label="项目编号" name="projectCode">
						<text style="line-height: 36px;">{{formData.projectCode}}</text>
					</uni-forms-item>
					<uni-forms-item label="项目名称" name="projectName">
						<text style="line-height: 36px;">{{formData.projectName}}</text>
					</uni-forms-item>
					<uni-forms-item label="发生地址" name="addressId">
						<uni-data-checkbox style="padding-top: 5px;" v-model="formData.addressId" :localdata="address" />
					</uni-forms-item>
					<uni-forms-item label="问题来源" name="source">
						<text style="line-height: 36px;">{{formData.source}}</text>
					</uni-forms-item>
					<uni-forms-item label="问题描述" name="problemDescription">
						<view style="line-height: 36px;max-height: 125px; overflow: auto;">
							{{formData.problemDescription}}
						</view>
					</uni-forms-item>
					<uni-forms-item label="问题类型" name="problemTypeName">
						<text style="line-height: 36px;">{{formData.problemTypeName}}</text>
					</uni-forms-item>
					<uni-forms-item label="接收人员" name="solutionName">
						<text style="line-height: 36px;">{{formData.solutionName}}</text>
					</uni-forms-item>
					<uni-forms-item label="要求完成时间" name="feedbackTime">
						<uni-dateformat style="line-height: 36px;" :date="formData.feedbackTime" format="yyyy-MM-dd">
						</uni-dateformat>
					</uni-forms-item>
					<uni-forms-item label="原因类型" name="reasonType">
						<text style="line-height: 36px;">{{state.reasonTypeText}}</text>
					</uni-forms-item>
					<uni-forms-item label="判定依据" name="pfb_ExceptionDesc">
						<view style="line-height: 36px;max-height: 125px; overflow: auto;">
							{{formData.pfb_ExceptionDesc}}
						</view>
					</uni-forms-item>
					<uni-forms-item label="计划开始时间" name="planStartTime" required>
						<uni-datetime-picker type="date" v-model="formData.planStartTime" placeholder="计划开始时间" />
					</uni-forms-item>
					<uni-forms-item label="计划完成时间" name="planEndTime" required>
						<uni-datetime-picker type="date" v-model="formData.planEndTime" placeholder="计划完成时间" />
					</uni-forms-item>
					<uni-forms-item label="实际开始时间" name="estimatedSettlementDate" required>
						<uni-datetime-picker type="date" v-model="formData.estimatedSettlementDate"
							placeholder="实际开始时间" />
					</uni-forms-item>
					<uni-forms-item label="原因分析" name="causeAnalysis" required>
						<uni-easyinput type="textarea" v-model="formData.causeAnalysis" placeholder="请输入原因分析" />
					</uni-forms-item>
					<uni-forms-item label="改善措施" name="improvement" required>
						<uni-easyinput type="textarea" v-model="formData.improvement" placeholder="请输入改善措施" />
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
		getReasonType,
		getInsertFileUrl,
		GetAllListById,
		GetFeedbackIdByPtid,
		getFeedbackImg,
		doEdit
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
		imgUrls: [],
		reasonTypeText: '',
		Dialog: null,
		imageSrc: '',
	})
	// 校验规则
	const rules = {
		planStartTime: {
			rules: [{
				required: true,
				errorMessage: '请选择计划开始时间'
			}]
		},
		planEndTime: {
			rules: [{
				required: true,
				errorMessage: '请选择计划完成时间'
			}]
		},
		estimatedSettlementDate: {
			rules: [{
				required: true,
				errorMessage: '请选择实际开始时间'
			}]
		},
		causeAnalysis: {
			rules: [{
				required: true,
				errorMessage: '请输入原因分析'
			}]
		},
		improvement: {
			rules: [{
				required: true,
				errorMessage: '请输入改善措施'
			}]
		},
	}
	//根据路由参数获取反馈信息
	const GetFeedbackData = async (param) => {
		let feedbackData = await GetAllListById({
			id: param
		})
		console.log(feedbackData.data)
		state.formData = Object.assign({}, feedbackData.data)
		if (state.formData.planStartTime.indexOf('0001-01-01') != -1) {
			state.formData.planStartTime = null
		}
		if (state.formData.planEndTime.indexOf('0001-01-01') != -1) {
			state.formData.planEndTime = null
		}
		if (state.formData.estimatedSettlementDate.indexOf('0001-01-01') != -1) {
			state.formData.estimatedSettlementDate = null
		}
		//原因类型数据加载
		let Rdata = await getReasonType()
		Rdata.data.forEach((item) => {
			if (item.dictionaryValue == state.formData.reasonType) {
				state.reasonTypeText = item.dictionaryText
			}
		})
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

	//提交
	const FeedBackSubmit = () => {
		state['formRef'].validate().then(async res => {
			try {
				state.running = true
				state.formData.DealWithDynamic = "ProblemFBStatus3"
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
