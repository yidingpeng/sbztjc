<template>
	<view class="container">
		<uni-section title="基本信息" type="line">
			<view class="example">
				<!-- 基础用法，不包含校验规则 -->
				<uni-forms ref="formRef" labelWidth="90" labelAlign="center" :modelValue="formData">
					<uni-forms-item label="项目编号" name="projectCode">
						<text style="line-height: 36px;">{{formData.projectCode}}</text>
					</uni-forms-item>
					<uni-forms-item label="项目名称" name="projectName">
						<text style="line-height: 36px;">{{formData.projectName}}</text>
					</uni-forms-item>
					<uni-forms-item label="发生地址" name="addressId" required>
						<uni-data-checkbox style="padding-top: 5px;" v-model="formData.addressId" :localdata="address"
							:disabled="true" />
					</uni-forms-item>
					<uni-forms-item label="问题来源" name="source" required>
						<text style="line-height: 36px;">{{formData.source}}</text>
					</uni-forms-item>
					<uni-forms-item label="问题描述" name="problemDescription">
						<view style="line-height: 36px;max-height: 125px; overflow: auto;">
							{{formData.problemDescription}}
						</view>
					</uni-forms-item>
					<uni-forms-item label="问题类型" name="problemTypeCode" required>
						<uni-data-select v-model="formData.problemTypeCode" :localdata="ProblemTypeData" :clear="false"
							placeholder="选择问题类型">
						</uni-data-select>
					</uni-forms-item>
					<uni-forms-item label="接收人员" name="solutionName" required>
						<e-select :modelValue="formData.solutionName" v-model="formData.solutionId"
							:options="ProTeamData" @change="bindJSRYChange" placeholder="选择接收人" :clear="true">
						</e-select>
					</uni-forms-item>
					<uni-forms-item label="原因类型" name="reasonType" required>
						<uni-data-select v-model="formData.reasonType" :localdata="ReasonTypeData" :clear="false"
							placeholder="选择原因类型">
						</uni-data-select>
					</uni-forms-item>
					<uni-forms-item label="要求完成时间" name="feedbackTime" required>
						<uni-datetime-picker type="date" v-model="formData.feedbackTime" placeholder="要求完成时间" />
					</uni-forms-item>
					<uni-forms-item label="判定依据" name="pfb_ExceptionDesc" required>
						<uni-easyinput type="textarea" v-model="formData.pfb_ExceptionDesc" placeholder="请输入判定依据" />
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
		getAll,
	} from '@/api/system/dictionary.js'
	import {
		getReasonType,
		getProjectCode,
		getProblemType,
		GetProTeamById,
		doEdit,
		GetAllListById,
		GetFeedbackIdByPtid,
		getFeedbackImg,
	} from '@/api/feedback.js'
	import {
		getUserList,
		GetByNameList,
		GetAccountInfo,
		GetToken
	} from '@/api/user.js'
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
		ProTeamData: [],
		// 基础表单数据
		formData: {
			isQualified: null,
		},
		ProblemTypeData: [],
		// 发生地址数据源
		address: [{
			text: '厂内',
			value: '1'
		}, {
			text: '厂外',
			value: '2'
		}],
		//原因类型
		ReasonTypeData: [],
		picId: [],
		imgUrls: [],
		solutionName: '',
		Dialog: null,
		imageSrc: '',
		lastTime: 0,
	})
	// 校验规则
	const rules = {
		problemTypeCode: {
			rules: [{
				required: true,
				errorMessage: '请选择问题类型'
			}]
		},
		solutionName: {
			rules: [{
				required: true,
				errorMessage: '请选择接收人员'
			}]
		},
		feedbackTime: {
			rules: [{
				required: true,
				errorMessage: '请选择要求完成时间'
			}]
		},
		reasonType: {
			rules: [{
				required: true,
				errorMessage: '请选择原因类型'
			}]
		},
		pfb_ExceptionDesc: {
			rules: [{
				required: true,
				errorMessage: '请输入判定依据'
			}]
		},
	}

	//接收人员选择事件赋值
	const bindJSRYChange = (value) => {
		if (value.value <= 0) {
			state.formData.solutionId = ''
		} else {
			state.formData.solutionId = value.value
			state.formData.solutionName = value.text
		}
	}
	//接收人输入事件
	// const handleInput = (value) => {
	// 	if (state.lastTime == 0) {
	// 		state.lastTime = setTimeout(async () => {
	// 			await GetUserList(value)
	// 		}, 1000)
	// 	} else {
	// 		clearTimeout(state.lastTime)
	// 		state.lastTime = setTimeout(async () => {
	// 			await GetUserList(value)
	// 		}, 1000)
	// 	}
	// }
	//筛选查询用户集合
	// const GetUserList = async (name) => {
	// 	if (name != '') {
	// 		let data = await GetByNameList({
	// 			Name: name
	// 		})
	// 		state.ProTeamData = data.data.length > 0 ? data.data : []
	// 	}
	// }
	//根据路由参数获取反馈信息
	const GetFeedbackData = async (param) => {
		//接收人员数据加载
		const userData = await getUserList()
		userData.data.forEach((item) => {
			if (item.orgId > 3)
				state.ProTeamData.push({
					value: item.id,
					text: item.realName
				})
		})
		//表单数据赋值
		let feedbackData = await GetAllListById({
			id: param
		})
		state.formData = Object.assign({}, feedbackData.data)
		if (state.formData.feedbackTime.indexOf('0001-01-01') != -1) {
			state.formData.feedbackTime = ''
		}
		GetPicIds(state.formData.id)
	}
	//根据id获取反馈图片ids
	const GetPicIds = async (param) => {
		let picData = await GetFeedbackIdByPtid({
			pid: param
		})
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
				let user = await GetAccountInfo({
					Account: state.formData.solutionName
				})
				if (user != null) {
					state.running = true
					state.formData.DealWithDynamic = "ProblemFBStatus2"
					const {
						msg
					} = await doEdit(state.formData)
					uni.showToast({
						title: msg,
						icon: 'none'
					})
					state.running = false
					if (msg.includes('成功')) {
						//reset()
						uni.navigateTo({
							url: '/pages/feedback/info'
						})
					}
				} else {
					uni.showToast({
						title: '无效的接收人员',
						icon: 'noen'
					})
				}

			} finally {
				// state.formData.pt_ID = 
				// state.formData.ProblemTypeID = state.ProblemTypeData[0].id
				// state.formData.imgFilesId = ''
				// state.formData.addressId = '1'
				// state.formData.estimatedSettlementDate = null
				// state.formData.feedbackTime = new Date().toISOString().slice(0, 10)
				// state.formData.pfb_ExceptionDesc = ''
				// state.formData.reasonType = ''
			}
		}).catch(err => {
			console.log(err)
		})
	}
	const reset = () => {
		state['formRef'].resetFields()
		state.form = {}
	}
	//获取字典数据源
	const GetDicData = async () => {
		//获取所有的字典数据
		let dicList = await getAll()
		//问题类型
		dicList.data['ProblemType'].forEach((item) => {
			state.ProblemTypeData.push({
				value: item.value,
				text: item.key
			})
		})
		//原因类型
		dicList.data['ReasonType'].forEach((item) => {
			state.ReasonTypeData.push({
				value: item.value,
				text: item.key
			})
		})
	}
	const {
		running,
		formRef,
		formData,
		ProblemTypeData,
		address,
		ProTeamData,
		ReasonTypeData,
		picId,
		imgUrls,
		Dialog,
		imageSrc,
	} = toRefs(state)

	onMounted(() => {
		state['formRef'].setRules(rules)
		//GetProblemType()
		//getUserIdFun()
		//GetReasonTypeMed()
		//GetUserList()
		//获取路由中的参数
		let route = getCurrentPages();
		let curParam = route[route.length - 1].$page.options.id;
		//console.log(curParam)
		GetFeedbackData(curParam)
		GetDicData()
	})
</script>

<style lang="scss">
	.example {
		padding: 15px;
		background-color: #fff;
	}

	.button {
		display: flex;
		align-items: center;
		height: 35px;
		margin-left: 10px;
	}

	.prewImg {
		width: 80px;
		height: 80px;
		margin-right: 3px;
		border-radius: 20%;
	}
</style>
