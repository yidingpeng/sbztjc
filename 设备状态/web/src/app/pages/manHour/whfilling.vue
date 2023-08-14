<template>
	<uni-section title="基本信息" type="line">
		<view class="example">
			<!-- 基础表单校验 -->
			<uni-forms ref="formRef" :rules="rules" :modelValue="formData" labelWidth="80">
				<uni-forms-item label="填报日期" required name="whDate">
					<uni-datetime-picker type="date" :clear-icon="false" v-model="formData.whDate" />
				</uni-forms-item>
				<uni-forms-item label="项目名称" required name="projectCode">
					<e-select :modelValue="projectName" v-model="formData.projectCode" :options="ProList"
						@change="handleChange" placeholder="选择项目"></e-select>
				</uni-forms-item>
				<uni-forms-item label="计划内容" required name="taskContent">
					<uni-easyinput type="textarea" v-model="formData.taskContent" placeholder="当天计划内容" />
				</uni-forms-item>
				<uni-forms-item label="实际内容" required name="jobContent">
					<uni-easyinput type="textarea" v-model="formData.jobContent" placeholder="实际工作内容" />
				</uni-forms-item>
				<uni-forms-item label="开始时间" required name="jobStartTime">
					<xp-picker mode="hi" v-model="formData.jobStartTime" />
				</uni-forms-item>
				<uni-forms-item label="结束时间" required name="jobEndTime">
					<xp-picker mode="hi" v-model="formData.jobEndTime" />
				</uni-forms-item>
				<uni-forms-item label="工种类型" required name="job">
					<uni-data-checkbox style="padding-top: 5px;" v-model="formData.job" :localdata="jobType" />
				</uni-forms-item>
				<uni-forms-item label="工作地点" required name="location">
					<uni-data-checkbox style="padding-top: 5px;" v-model="formData.location" :localdata="address"
						@change="handleLocationChange" />
				</uni-forms-item>
				<uni-forms-item v-if="offSite" label="厂外地点" :required="offSite" name="offsiteLocation"
					:rules="offSite == false ?[]:[{'required': true,errorMessage: '请输入厂外地点'}]">
					<uni-easyinput v-model="formData.offsiteLocation" placeholder="请输入厂外地点" />
				</uni-forms-item>
				<uni-forms-item label="完成情况" required name="completeStatus">
					<uni-data-checkbox style="padding-top: 5px;" v-model="formData.completeStatus"
						:localdata="tatusList" @change="handleCompleteChange" />
				</uni-forms-item>
				<uni-forms-item v-if="Incomplete" label="未完成原因" :required="Incomplete" name="incompleteReason"
					:rules="Incomplete == false ?[]:[{'required': true,errorMessage: '请输入未完成原因'}]">
					<uni-easyinput type="textarea" v-model="formData.incompleteReason" placeholder="未完成原因" />
				</uni-forms-item>
				<uni-forms-item label="备注" name="remark">
					<uni-easyinput type="textarea" v-model="formData.remark" placeholder="备注信息" />
				</uni-forms-item>
			</uni-forms>
			<button type="primary" @click="btnConfirm">提交</button>
		</view>
	</uni-section>
</template>

<script setup>
	import {
		getProjectCode
	} from '@/api/feedback.js'
	import {
		WHAddOrEdit
	} from '@/api/workhour/workhour.js'

	import {
		reactive,
		toRefs,
		onMounted,
	} from 'vue'

	const state = reactive({
		formRef: null,
		running: false,
		formData: {
			whDate: new Date().toISOString().slice(0, 10),
			jobStartTime: '08:30',
			jobEndTime: '17:30',
			location: '厂内',
			offsiteLocation: '',
			completeStatus: '已完成',
			projectCode: '',
			incompleteReason: '',
			job:'',
			taskContent:'',
			jobContent:'',
		},
		ProList: [], //项目集合
		projectName: '',
		jobType: [{
			text: '电工',
			value: '电工'
		}, {
			text: '钳工',
			value: '钳工'
		}, {
			text: '调试',
			value: '调试'
		}],
		// 发生地址数据源
		address: [{
			text: '厂内',
			value: '厂内'
		}, {
			text: '厂外',
			value: '厂外'
		}],
		tatusList: [{
			text: '已完成',
			value: '已完成'
		}, {
			text: '未完成',
			value: '未完成'
		}],
		offSite: false,
		Incomplete: false,
	})
	// 校验规则
	const rules = {
		whDate: {
			rules: [{
				required: true,
				errorMessage: '请选择填报日期'
			}]
		},
		projectCode: {
			rules: [{
				required: true,
				errorMessage: '请选择项目名称'
			}]
		},
		taskContent: {
			rules: [{
				required: true,
				errorMessage: '请填写计划内容'
			}]
		},
		jobContent: {
			rules: [{
				required: true,
				errorMessage: '请填写实际工作内容'
			}]
		},
		location: {
			rules: [{
				required: true,
				errorMessage: '请选择工作地点'
			}]
		},
		job: {
			rules: [{
				required: true,
				errorMessage: '请选择工种类型'
			}]
		},
		completeStatus: {
			rules: [{
				required: true,
				errorMessage: '请选择完成情况'
			}]
		},
	}

	//获取项目数据
	const GetProName = async () => {
		let ProJOptionsData = await getProjectCode()
		if (ProJOptionsData.data.length > 0) {
			ProJOptionsData.data.forEach((item) => {
				item.projectName = item.projectName.length > 9 ? item.projectName
					.substr(0, 9) + '...' : item.projectName
				state.ProList.push({
					text: item.projectCode.trim() + ' ' + item.projectName.trim(),
					value: item.projectCode
				})
			})
		}
	}

	//项目名称选择事件
	const handleChange = (value) => {
		if (value.value.length < 0) {
			state.formData.projectCode = ''
		} else {
			state.formData.projectCode = value.value
			state.projectName = value.text
		}
	}

	//工作地点选择事件
	const handleLocationChange = (e) => {
		if (e.detail.value == '厂内') {
			state.offSite = false
			state.formData.offsiteLocation = ''
		} else {
			state.offSite = true
		}
	}
	//
	const handleCompleteChange = (e) => {
		if (e.detail.value == '未完成') {
			state.Incomplete = true
		} else {
			state.formData.incompleteReason = ''
			state.Incomplete = false
		}
	}
	//提交按钮
	const btnConfirm = () => {
		state['formRef'].validate().then(async res => {
			state.running = true
			//console.log(state.formData)
			const {
				msg
			} = await WHAddOrEdit(state.formData)
			uni.showToast({
				title: msg,
				icon: 'none'
			})
			reset()
			state.running = false
		}).catch(err => {
			console.log(err)
		})
	}

	const reset = () => {
		state['formRef'].resetFields()
		state.form = {
			whDate: new Date().toISOString().slice(0, 10),
			jobStartTime: '08:30',
			jobEndTime: '17:30',
			location: '厂内',
			offsiteLocation: '',
			completeStatus: '已完成',
			projectCode: '',
			incompleteReason: '',
			job:'',
			taskContent:'',
			jobContent:'',
		}
		state.projectName = ''
	}
	const {
		formRef,
		running,
		formData,
		ProList,
		projectName,
		address,
		offSite,
		tatusList,
		Incomplete,
		jobType,
	} = toRefs(state)

	onMounted(() => {
		state['formRef'].setRules(rules)
		GetProName()
	})
</script>

<style lang="scss">
	.example {
		padding: 15px;
		background-color: #fff;
	}
</style>
