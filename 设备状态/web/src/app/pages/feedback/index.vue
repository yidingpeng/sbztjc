<template>
	<view class="container">
		<uni-section title="基本信息" type="line">
			<view class="example">
				<!-- 基础用法，不包含校验规则 -->
				<uni-forms ref="formRef" labelWidth="90" labelAlign="center" :modelValue="formData">
					<uni-forms-item label="项目名称" name="projectCode" required>
						<uni-combox :candidates="candidates" v-model="formData.projectCode" placeholder="搜索后选择项目"
							@input="handleInput"></uni-combox>
					</uni-forms-item>
					<uni-forms-item label="发生地址" name="addressId" required>
						<uni-data-checkbox style="padding-top: 5px;" v-model="formData.addressId"
							:localdata="address" />
					</uni-forms-item>
					<uni-forms-item label="问题来源" name="source" required>
						<uni-data-select v-model="formData.source" :localdata="sourceData" placeholder="请选择问题来源">
						</uni-data-select>
					</uni-forms-item>
					<uni-forms-item label="问题描述" name="problemDescription" required>
						<uni-easyinput type="textarea" v-model="formData.problemDescription" placeholder="请输入问题描述" />
					</uni-forms-item>
					<uni-forms-item label="反馈图片">
						<view class="example-body">
							<uni-file-picker :value="fileLiset" :auto-upload="false" ref="UploadRef" limit="3"
								title="最多选择3张图片" @select="FileSelect" @delete="FileDelete" style="line-height: 30px;">
							</uni-file-picker>
						</view>
					</uni-forms-item>
				</uni-forms>
				<view style="text-align: center;">
					<button class="mini-btn" type="primary" @click="FeedBackSubmit()" size="mini"
						:loading="running">提交</button>
				</view>
			</view>
		</uni-section>
	</view>
</template>

<script setup>
	import {
		doAdd,
		getInsertFileUrl,
		GetByOddsProMain,
	} from '@/api/feedback.js'
	import {
		getAll,
	} from '@/api/system/dictionary.js'
	import {
		toRefs,
		reactive,
		onMounted,
	} from 'vue'

	const state = reactive({
		running: false,
		UploadRef: null,
		formRef: null,
		// 基础表单数据
		formData: {
			projectCode: '',
			imgFilesId: '',
			addressId: '1',
			source: '',
			problemDescription: '',
		},
		isUploading: false,
		// 发生地址数据源
		address: [{
			text: '厂内',
			value: '1'
		}, {
			text: '厂外',
			value: '2'
		}],
		fileLiset: [],
		fileUrl: '',
		list: [],
		sourceData: [],
		candidates: [],
		lastTime: 0,
	})
	// 校验规则
	const rules = {
		projectCode: {
			rules: [{
				required: true,
				errorMessage: '请选择项目名称'
			}]
		},
		addressId: {
			rules: [{
				required: true,
				errorMessage: '请选择发送地址'
			}]
		},
		source: {
			rules: [{
				required: true,
				errorMessage: '请选择问题来源'
			}]
		},
		problemDescription: {
			rules: [{
				required: true,
				errorMessage: '请输入问题描述'
			}]
		},
	}

	//项目名称输入事件
	const handleInput = (value) => {
		if (state.lastTime == 0) {
			state.lastTime = setTimeout(async () => {
				await GetProjectList(value)
			}, 1500)
		} else {
			clearTimeout(state.lastTime)
			state.lastTime = setTimeout(async () => {
				await GetProjectList(value)
			}, 1500)
		}
	}
	//筛选查询项目集合
	const GetProjectList = async (name) => {
		if (name == '' || name == null || name == undefined) {
			uni.showToast({
				title: '筛选值为空!',
				icon: 'none'
			})
		} else {
			let data = await GetByOddsProMain({
				Name: name
			})
			state.candidates = data.data.length > 0 ? data.data : []
		}
	}

	//提交
	const FeedBackSubmit = () => {
		if (state.isUploading) {
			uni.showToast({
				title: '文件上传中',
				icon: 'info'
			})
			return
		}
		state['formRef'].validate().then(async res => {
			try {
				if (state.formData.projectCode.includes('|')) {
					state.running = true
					state.formData.isQualified = 0
					let proCode = state.formData.projectCode.split('|')
					state.formData.projectCode = proCode[0]
					const {
						msg
					} = await doAdd(state.formData)
					uni.showToast({
						title: msg,
						icon: 'none'
					})
					state.running = false
					if (msg.includes('成功')) {
						reset()
						uni.navigateTo({
							url: '/pages/feedback/info'
						});
					}
				} else {
					uni.showToast({
						title: '无效项目编号!',
						icon: 'none'
					})
				}

			} finally {}
		}).catch(err => {
			console.log(err)
		})
	}
	const FileDelete = (file) => {
		for (let i = 0; i < state.fileLiset.length; i++) {
			if (state.fileLiset[i].tempFiles[0].name == file.tempFile.name) {
				state.fileLiset.splice(i, 1);
			}
		}
		state.formData.imgFilesId = state.fileLiset.map((item) => item.response).join()
	}

	//获取上传图片地址
	const GetUploadUrl = async () => {
		let result = await getInsertFileUrl();
		state.fileUrl = result.imgurl
	}
	const FileSelect = async (files) => {
		let igmFile = files.tempFilePaths;
		igmFile.forEach((item) => {
			state.isUploading = true;
			uni.uploadFile({
				url: state.fileUrl,
				method: "POST",
				filePath: item,
				name: 'UploadFile',
				header: {
					'Authorization': uni.getStorageSync('token') ? "Bearer " + uni.getStorageSync(
						'token') : '',
				},
				success: (res) => {
					let a = JSON.parse(res.data);
					files.response = a.id
					state.fileLiset.push(files)
					//console.log(state.fileLiset)
					if (state.formData.imgFilesId != '') {
						state.formData.imgFilesId = state.formData.imgFilesId + a.id + ','
					} else {
						state.formData.imgFilesId = a.id + ','
					}
				},
				complete: () => {}
			})
			state.isUploading = false;
		})
	}
	//获取字典数据源
	const GetDicData = async () => {
		//获取所有的字典数据
		let dicList = await getAll()
		//问题来源
		dicList.data['ProblemSource'].forEach((item) => {
			state.sourceData.push({
				value: item.value,
				text: item.key
			})
		})
	}

	const reset = () => {
		state.formData = {
			projectCode: '',
			imgFilesId: '',
			addressId: '1',
			source: '',
			problemDescription: '',
		}
	}
	const {
		running,
		formRef,
		UploadRef,
		formData,
		address,
		fileLiset,
		list,
		isUploading,
		sourceData,
		candidates,
		lastTime,
	} = toRefs(state)

	onMounted(() => {
		state['formRef'].setRules(rules)
		GetUploadUrl()
		GetDicData()
	})
</script>

<style lang="scss">
	.example {
		padding: 15px;
		background-color: #fff;
	}

	.form-item {
		display: flex;
		align-items: center;
	}
</style>
