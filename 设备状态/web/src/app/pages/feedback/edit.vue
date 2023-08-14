<template>
	<view class="container">
		<uni-section title="基本信息" type="line">
			<view class="example">
				<!-- 基础用法，不包含校验规则 -->
				<uni-forms ref="formRef" labelWidth="90" labelAlign="center" :modelValue="formData">
					<uni-forms-item label="项目名称" name="pt_ID" required>
						<e-select :modelValue="state.projectName" v-model="formData.pt_ID" :options="list"
							@change="bindChange" placeholder="选择选项" :clear="true"></e-select>
					</uni-forms-item>
					<uni-forms-item label="发生地址" name="addressId" required>
						<uni-data-checkbox v-model="formData.addressId" :localdata="address" />
					</uni-forms-item>
					<uni-forms-item label="问题描述" name="problemDescription" required>
						<uni-easyinput type="textarea" v-model="formData.problemDescription" placeholder="请输入问题描述" />
					</uni-forms-item>
					<!-- <uni-forms-item label="反馈图片">
						<view class="example-body">
							<uni-file-picker :value="fileLiset" :auto-upload="false" ref="UploadRef" limit="3"
								title="最多选择3张图片" @select="FileSelect" @delete="FileDelete" style="line-height: 30px;">
							</uni-file-picker>
						</view>
					</uni-forms-item> -->
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
		getProjectCode,
		getInsertFileUrl,
		doEdit,
		GetAllListById,
		// GetFeedbackIdByPtid,
		// getFeedbackImg,
		// GetFilesByPid,
	} from '@/api/feedback.js'
	import {
		getUserId,
		getUserList,
		GetToken
	} from '@/api/user.js'
	import {
		reactive,
		toRefs,
		onMounted,
	} from 'vue'

	import {
		storeToRefs
	} from "pinia"

	const state = reactive({
		running: false,
		// UploadRef: null,
		formRef: null,
		// popup: null,
		// visible: true,
		// ProVisible: true,
		// 基础表单数据
		formData: {
			pt_ID: null,
			userId: null,
			isQualified: true,
			//imgFilesId: '',
			addressId: null,
			problemDescription: null,
		},
		// isUploading: false,
		// 发生地址数据源
		address: [{
			text: '厂内',
			value: '1'
		}, {
			text: '厂外',
			value: '2'
		}],
		// fileLiset: [],
		// fileUrl: '',
		// imgUrls:[],
		// picId:[],
		list: [],
		projectName: null,
	})
	// 校验规则
	const rules = {
		pt_ID: {
			rules: [{
				required: true,
				errorMessage: '请选择项目'
			}]
		},
		addressId: {
			rules: [{
				required: true,
				errorMessage: '请选择发送地址'
			}]
		},
		problemDescription: {
			rules: [{
				required: true,
				errorMessage: '请输入问题描述'
			}]
		},
	}

	//根据路由参数获取反馈信息
	const GetFeedbackData = async (param) => {
		//表单数据赋值
		let feedbackData = await GetAllListById({
			id: param
		})
		let ProJOptionsData = await getProjectCode()
		if (ProJOptionsData.data.length > 0) {
			ProJOptionsData.data.forEach((item) => {
				item.projectName = item.projectName.length > 10 ? item.projectName
					.substr(0, 10) + '...' : item.projectName
				state.list.push({
					text: item.projectCode + ' ' + item.projectName,
					value: item.id
				})
			})
		}
		state.formData = Object.assign({}, feedbackData.data)
		state.projectName = state.formData.projectCode + ' ' + state.formData.projectName
		// let pathList = await GetFilesByPid({Id:state.formData.id})
		// pathList.data.forEach((item) =>{
		// 	fileLiset.push({url:item.rootPath})
		// })
		//console.log(state.formData)
		// GetPicIds(state.formData.id)
	}
	//根据id获取反馈图片ids
	const GetPicIds = async (param) => {
		let picData = await GetFeedbackIdByPtid({
			pid: param
		})
		state.picId = picData.ids
		//console.log(state.picId)
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
		//console.log(state.imgUrls)
	}

	//项目名称选择事件
	const bindChange = (value) => {
		if (value <= 0) {
			state.formData.pt_ID = ''
		} else {
			state.formData.pt_ID = value.value
			state.projectName = value.text
		}
	}
	//获取项目数据
	const GetProName = async () => {
		let ProJOptionsData = await getProjectCode()
		if (ProJOptionsData.data.length > 0) {
			ProJOptionsData.data.forEach((item) => {
				item.projectName = item.projectName.length > 10 ? item.projectName
					.substr(0, 10) + '...' : item.projectName
				state.list.push({
					label: item.projectCode + ' ' + item.projectName,
					value: item.id
				})
			})
		}
	}
	//提交
	const FeedBackSubmit = () => {
		// if (state.isUploading) {
		// 	uni.showToast({
		// 		title: '文件上传中',
		// 		icon: 'info'
		// 	})
		// 	return
		// }
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
			} finally {}
		}).catch(err => {})
		state['formRef'].setRules(rules)
	}

	// const FileDelete = (file) => {
	// 	//console.log(file)
	// 	for (let i = 0; i < state.fileLiset.length; i++) {
	// 		if (state.fileLiset[i].tempFiles[0].name == file.tempFile.name) {
	// 			state.fileLiset.splice(i, 1);
	// 		}
	// 	}
	// 	state.formData.imgFilesId = state.fileLiset.map((item) => item.response).join()
	// }

	// //获取上传图片地址
	// const GetUploadUrl = async () => {
	// 	let result = await getInsertFileUrl();
	// 	state.fileUrl = result.imgurl
	// }
	// const FileSelect = async (files) => {
	// 	let igmFile = files.tempFilePaths;
	// 	igmFile.forEach((item) => {
	// 		state.isUploading = true;
	// 		uni.uploadFile({
	// 			url: state.fileUrl,
	// 			method: "POST",
	// 			filePath: item,
	// 			name: 'UploadFile',
	// 			header: {
	// 				'Authorization': uni.getStorageSync('token') ? "Bearer " + uni.getStorageSync(
	// 					'token') : '',
	// 			},
	// 			success: (res) => {
	// 				let a = JSON.parse(res.data);
	// 				files.response = a.id
	// 				state.fileLiset.push(files)
	// 				//console.log(state.fileLiset)
	// 				if (state.formData.imgFilesId != '') {
	// 					state.formData.imgFilesId = state.formData.imgFilesId + a.id + ','
	// 				} else {
	// 					state.formData.imgFilesId = a.id + ','
	// 				}
	// 			},
	// 			complete: () => {}
	// 		})
	// 		state.isUploading = false;
	// 	})
	// }
	const {
		running,
		formRef,
		// UploadRef,
		// visible,
		formData,
		// index,
		address,
		// fileLiset,
		// imgUrls,
		// picId,
		list,
		projectName,
	} = toRefs(state)

	onMounted(() => {
		state['formRef'].setRules(rules)
		//GetUploadUrl()
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

	.segmented-control {
		margin-bottom: 15px;
	}

	.button-group {
		margin-top: 15px;
		display: flex;
		justify-content: space-around;
	}

	.form-item {
		display: flex;
		align-items: center;
	}

	.button {
		display: flex;
		align-items: center;
		height: 35px;
		margin-left: 10px;
	}

	.picker-view {
		width: 750rpx;
		height: 450rpx;
		margin-top: 0rpx;
	}

	.ProSelectBtn {
		margin-top: 10px;
		border-style: none;
		font-size: 16px;
		line-height: 55px;
	}

	.scroll-view {
		/* #ifndef APP-NVUE */
		width: 100%;
		height: 100%;
		/* #endif */
		flex: 1
	}

	// 处理抽屉内容滚动
	.scroll-view-box {
		flex: 1;
		position: absolute;
		top: 0;
		right: 0;
		bottom: 0;
		left: 0;
	}

	.info {
		padding: 15px;
		color: #666;
	}

	.info-text {
		font-size: 14px;
		color: #666;
	}

	.info-content {
		padding: 5px 15px;
	}

	.close {
		padding: 10px;
	}

	.uni-list-cell {
		justify-content: flex-start
	}
</style>
