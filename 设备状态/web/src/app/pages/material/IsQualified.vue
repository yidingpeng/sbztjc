<template>
	<view class="container">
		<uni-row>
			<uni-col :span="21">
				<uni-search-bar radius="5" :focus="searchFoucus" v-model="searchValue" placeholder="输入二维码号"
					clearButton="auto" cancelButton="none" @confirm="handleSearch" @clear="handleClear" />
			</uni-col>
			<uni-col :span="3">
				<uni-icons style="float:right;padding-top: 12px;padding-right: 8px;" @click="scanClick()" type="scan"
					:size="30" color="#666" />
			</uni-col>
		</uni-row>
		<uni-section title="物料信息" style="padding-right:10px ;" type="line">
			<view class="example">
				<uni-forms ref="formRef" :rules="rules" labelWidth="90" labelAlign="center" :modelValue="formData">
					<uni-row>
						<uni-col :span="20">
							<uni-forms-item label="物料编号" name="materialCode">
								<text class="texts">{{formData.materialCode}}</text>
							</uni-forms-item>
						</uni-col>
						<uni-col :span="4">
							<text class="texts">{{source}}</text>
						</uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="24">
							<uni-forms-item label="物料名称" name="materialName">
								<text class="texts">{{formData.materialName}}</text>
								<!-- <uni-easyinput v-model="formData.materialName" disabled /> -->
							</uni-forms-item>
						</uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="24">
							<uni-forms-item label="项目编码" name="projectCode">
								<text class="texts">{{formData.projectCode}}</text>
								<!-- <uni-easyinput v-model="formData.projectName" disabled /> -->
							</uni-forms-item>
						</uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="24">
							<uni-forms-item label="项目名称" name="projectName">
								<text class="texts">{{formData.projectName}}</text>
								<!-- <uni-easyinput v-model="formData.projectName" disabled /> -->
							</uni-forms-item>
						</uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="16">
							<uni-forms-item label="合格数量" required name="qcount">
								<uni-number-box style="padding-top: 5px;" @change="changeValue"
									v-model="formData.qcount" :min="0" />
							</uni-forms-item>
						</uni-col>
						<uni-col :span="8">
							<uni-forms-item label="质检数量" name="count">
								<text class="texts">{{formData.count}}</text>
							</uni-forms-item>
						</uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="24">
							<uni-forms-item label="是否合格" required name="qualified">
								<uni-data-checkbox v-model="formData.qualified" :localdata="items"
									style="padding-top: 4px;" />
							</uni-forms-item>
						</uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="24">
							<uni-forms-item label="备注" name="remark">
								<uni-easyinput type="textarea" v-model="formData.remark" placeholder="请输入备注信息" />
							</uni-forms-item>
						</uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="24">
							<uni-forms-item label="质检照片">
								<view class="example-body">
									<uni-file-picker :value="fileLiset" :auto-upload="false" ref="UploadRef" limit="9"
										title="最多选择9张图片" @select="FileSelect" @delete="FileDelete"
										style="line-height: 30px;">
									</uni-file-picker>
								</view>
							</uni-forms-item>
						</uni-col>
					</uni-row>
				</uni-forms>
				<view style="text-align: center;">
					<button :loading="running" size="mini" @click="btnConfirm" type="primary">提交</button>
				</view>
			</view>
		</uni-section>
	</view>
</template>

<script setup>
	import {
		UpdateMQualified,
		getInsertFileUrl,
		GetByQrCode,
		MatByCodeInfo,
		doQCAdd,
		GetByQrCodeQC,
		GetByQrCodeCD,
	} from '@/api/Purchase.js'
	import {
		reactive,
		toRefs,
		onMounted,
		nextTick,
	} from 'vue'


	const state = reactive({
		formRef: null,
		items: [{
				value: 1,
				text: '是'
			},
			{
				value: 2,
				text: '否'
			},
		],
		running: false,
		UploadRef: null,
		fileLiset: [],
		formData: {
			qrCode: '',
			qualified: '',
			imgFilesId: '',
		},
		isUploading: false,
		source: '',
		searchValue: '',
		searchFoucus: true,
	})
	// 校验规则
	const rules = {
		materialCode: {
			rules: [{
				required: true,
				errorMessage: '物料信息为空!'
			}]
		},
		qualified: {
			rules: [{
				required: true,
				errorMessage: '请选择是否合格!'
			}]
		}
	}
	//提交按钮
	const btnConfirm = () => {
		state['formRef'].validate().then(async res => {
			console.log(state.formData.materialCode)
			if (state.formData.materialCode == '' || state.formData.materialCode == undefined) {
				uni.showToast({
					title: '找不到物料信息!',
					icon: 'none'
				})
			} else if (state.formData.qualified == '') {
				uni.showToast({
					title: '请选择是否合格!',
					icon: 'none'
				})
			} else {
				state.running = true
				state.formData.Id = 0
				const {
					msg
				} = await doQCAdd(state.formData)
				uni.showToast({
					title: msg,
					icon: 'none'
				})
			}
			state.running = false
			reset()
		}).catch(err => {})
	}
	//搜索框回车事件
	const handleSearch = async (res) => {
		await QrCodeInfoLoad(res.value)
	}
	const scanClick = () => {
		//允许从相机和相册扫码
		uni.scanCode({
			success: async function(res) {
				await QrCodeInfoLoad(res.result)
			},
			fail: function() {
				uni.showToast({
					title: '扫码失败',
					icon: 'none'
				})
			}
		});
	}


	//获取二维码号是否有效并加载
	const QrCodeInfoLoad = async (qrcode) => {
		if (qrcode.length > 10 && qrcode.length < 20) {
			const result = await GetByQrCode({
				QrCode: qrcode
			})
			if (result.data != null) {
				//报检
				const cd = await GetByQrCodeCD({
					QrCode: qrcode
				})
				if (cd.data != null) {
					//未质检
					if (result.data.qualified == 0) {
						//查询是否外协件，外协件需要质检，标准件不需要质检
						const matInfo = await MatByCodeInfo({
							Code: result.data.materialCode
						})
						if (matInfo.data != null) {
							state.source = matInfo.data.source
						}
						state.formData = Object.assign({}, result.data)
						state.formData.qcount = state.formData.count
						state.formData.qualified = ''
						//已质检合格
					} else if (result.data.qualified == 1) {
						uni.showToast({
							title: '物料已质检!',
							icon: 'none'
						})
						//部分质检(有不合格的存在)
					} else {
						const qcinfo = await GetByQrCodeQC({
							QrCode: result.data.qrCode
						})
						if (qcinfo.data != null) {
							if (qcinfo.data.qCount < result.data.count) {
								state.formData = Object.assign({}, result.data)
								state.formData.qcount = state.formData.count
								state.formData.qualified = ''
							} else {
								uni.showToast({
									title: '物料已质检!',
									icon: 'none'
								})
							}
						}
					}
				} else {
					uni.showToast({
						title: '物料未报检!',
						icon: 'none'
					})
				}
			} else {
				uni.showToast({
					title: '无相关信息!',
					icon: 'none'
				})
			}
		} else {
			uni.showToast({
				title: '无效二维码!',
				icon: 'none'
			})
		}
	}

	//获取上传图片地址
	const GetUploadUrl = async () => {
		let result = await getInsertFileUrl();
		state.fileUrl = result.imgurl
	}
	//删除图片
	const FileDelete = (file) => {
		//console.log(file)
		for (let i = 0; i < state.fileLiset.length; i++) {
			if (state.fileLiset[i].name == file.tempFile.name) {
				state.fileLiset.splice(i, 1);
			}
		}
		state.formData.imgFilesId = state.fileLiset.map((item) => item.response).join() + ','
	}
	//上传图片
	const FileSelect = async (files) => {
		let igmFile = files.tempFilePaths;
		let i = 0;
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
					//files.response = a.id
					state.fileLiset.push(files.tempFiles[i++])
					state.fileLiset[state.fileLiset.length - 1].response = a.id;
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
	const changeValue = (value) => {
		if (value < state.formData.count) {
			state.formData.qualified = 2
		} else if (value == state.formData.count) {
			state.formData.qualified = 1
		} else if (value > state.formData.count) {
			state.formData.qcount = state.formData.count
			state.formData.qualified = 1
			uni.showToast({
				title: '合格数已超质检数!',
				icon: 'none'
			})
		}
	}
	const reset = () => {
		state['formRef'].resetFields()
		state.formData = {}
		state.fileLiset = []
		state['UploadRef'].clearFiles()
		state.searchValue = ''
		state.searchFoucus = false
		nextTick(() => {
			state.searchFoucus = true
		})
		// state.formData.qrCode = ''
		// state.formData.materialCode = ''
		// state.formData.materialName = ''
		// state.formData.count = 0
		// state.formData.qcount = 1
		// state.formData.projectCode = ''
		// state.formData.projectName = ''
		// state.formData.remark = ''
		// state.formData.qualified = ''
		// state.source = ''
	}
	const {
		formRef,
		items,
		running,
		UploadRef,
		fileLiset,
		formData,
		source,
		searchValue,
		searchFoucus,
	} = toRefs(state)

	onMounted(() => {
		state['formRef'].setRules(rules)
		GetUploadUrl();
	})
</script>
<style lang="scss" scoped>
	.container {
		background-color: #ffffff;
	}

	.texts {
		line-height: 36px;
	}
</style>
