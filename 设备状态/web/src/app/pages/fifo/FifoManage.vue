<template>
	<view class="container">
		<uni-row>
			<uni-col :span="21">
				<uni-search-bar radius="5" :focus="searchFoucus" v-model="searchValue" placeholder="输入二维码号"
					clearButton="auto" cancelButton="none" @confirm="handleSearch" />
			</uni-col>
			<uni-col :span="3">
				<uni-icons style="float:right;padding-top: 12px;padding-right: 8px;" @click="scanClick()" type="scan"
					:size="30" color="#666" />
			</uni-col>
		</uni-row>
		<uni-section title="入库信息" type="line">
			<view class="example">
				<!-- 基础表单校验 -->
				<uni-forms ref="formRef" :modelValue="formData">
					<uni-row>
						<uni-col :span="20">
							<uni-forms-item label="物料编号" name="materialCode">
								<text class="fifotext">{{formData.materialCode}}</text>
							</uni-forms-item>
						</uni-col>
						<uni-col :span="4">
							<text class="fifotext">{{source}}</text>
						</uni-col>
					</uni-row>
					<uni-forms-item label="物料名称" name="materialName">
						<text class="fifotext">{{formData.materialName}}</text>
					</uni-forms-item>
					<uni-forms-item label="物料型号" name="model">
						<text class="fifotext">{{formData.model}}</text>
					</uni-forms-item>
					<uni-row>
						<uni-col :span="14">
							<uni-forms-item label="数&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;量" name="count">
								<uni-number-box style="padding-top: 5px;" v-model="formData.count" :min="1"
									:disabled="slState" />
							</uni-forms-item>
						</uni-col>
						<uni-col :span="7">
							<text class="fifotext">历史已入：{{yrksl}}</text>
						</uni-col>
						<uni-col :span="3"><button style="margin-top:3px;" size="mini" @click="btnCount"
								type="primary">启</button></uni-col>
					</uni-row>
					<uni-forms-item label="项目编码" name="projectCode">
						<text class="fifotext">{{formData.projectCode}}</text>
					</uni-forms-item>
					<uni-forms-item label="项目名称" name="projectName">
						<text class="fifotext">{{formData.projectName}}</text>
					</uni-forms-item>
					<uni-forms-item label="备注" name="remark">
						<uni-easyinput type="textarea" maxlength="255" v-model="formData.remark"
							placeholder="请简要输入备注信息,最多255字符" />
					</uni-forms-item>
				</uni-forms>
				<button type="primary" :loading="running" class="mini-btn" @click="FifoSubmit()">入库</button>
			</view>
		</uni-section>
	</view>
</template>
<script setup>
	import {
		reactive,
		toRefs,
		onMounted,
		defineComponent,
		nextTick,
	} from 'vue'
	import {
		useUserStore
	} from '@/store/user.js'
	import {
		storeToRefs
	} from "pinia"
	import {
		GetAccountInfo,
	} from '@/api/user.js'
	import {
		GetByQrCode,
		doFifoAdd,
		MatByCodeInfo,
		GetByQrCodeQC,
		GetByQrCodeYRK,
	} from '@/api/Purchase.js'
	import {
		getUploadFileUrl
	} from '@/api/system/file.js'

	const userStore = useUserStore();
	const {
		realName
	} = storeToRefs(userStore)
	const state = reactive({
		formRef: null,
		running: false,
		// 基础表单数据
		formData: {
			type: 1,
			qrCode: '',
			bomCode: '',
			bomName: '',
			materialCode: '',
			materialName: '',
			projectCode: '',
			projectName: '',
			count: '1',
			fifoDateTime: new Date().toISOString().slice(0, 10),
			fifoPersonnel: '',
			remark: '',
			deliverySignature: 0,
			model: '',
		},
		pickingPeople: false,
		actionUrl: '',
		slState: true,
		yrksl: 0,
		source: '',
		searchValue: '',
		searchFoucus: true,
	})
	// 校验规则
	const rules = {
		materialCode: {
			rules: [{
				required: true,
				errorMessage: '物料编码为空'
			}]
		},
	}
	//扫码
	const scanClick = () => {
		//允许从相机和相册扫码
		uni.scanCode({
			success: async function(res) {
				await QrCodeInfoLoad(res.result)
			},
			fail: function() {
				uni.showToast({
					title: '扫码失败',
					icon: 'error'
				})
			}
		});
	}
	//搜索框查询
	const handleSearch = async (res) => {
		if (res.value.length >= 10 && res.value.length < 20) {
			await QrCodeInfoLoad(res.value)
		}
	}

	//获取二维码号是否有效并加载
	const QrCodeInfoLoad = async (qrcode) => {
		if (qrcode.length > 10 && qrcode.length < 20) {
			//二维码信息
			const QrCode = await GetByQrCode({
				QrCode: qrcode
			})
			if (QrCode.data != null) {
				//查询物料信息
				const matInfo = await MatByCodeInfo({
					Code: QrCode.data.materialCode
				})
				state.source = matInfo.data.source
				//未质检的物料信息
				if (QrCode.data.qualified == 0) {
					//如果是外协件需要先质检在入库
					if (matInfo.data.source == '外协件') {
						uni.showToast({
							title: '物料未质检!',
							icon: 'none',
						});
					} else { //标准件可直接入库
						state.formData = Object.assign({}, QrCode.data)
					}
				} else {
					//console.log('1')
					//质检信息
					const zjInfo = await GetByQrCodeQC({
						QrCode: qrcode
					})
					//console.log(zjInfo.data)
					state.formData = Object.assign({}, zjInfo.data)
				}
				const yrkCount = await GetByQrCodeYRK({
					QrCode: qrcode,
					Type: 1,
				})
				state.yrksl = yrkCount.data
			} else {
				uni.showToast({
					title: '没有找到二维码信息!',
					icon: 'none',
				});
			}
		} else {
			uni.showToast({
				title: '无效二维码!',
				icon: 'none'
			})
		}
	}

	//提交
	const FifoSubmit = () => {
		state['formRef'].validate().then(async res => {
			try {
				if (state.formData.materialCode == '') {
					uni.showToast({
						title: '找不到物料信息!',
						icon: 'none'
					})
				} else {
					state.running = true
					state.formData.Id = 0
					state.formData.type = 1
					const {
						msg
					} = await doFifoAdd(state.formData)
					uni.showToast({
						title: msg,
						icon: 'none'
					})
					if (msg.indexOf("成功") != -1) {
						reset()
					}
				}

			} finally {
				state.running = false
				//reset()
			}
		}).catch(err => {})
		state['formRef'].setRules(rules)
	}

	//编辑数量
	const btnCount = () => {
		state.slState = false
	}

	const reset = () => {
		state.formData.type = 1
		state.formData.qrCode = ''
		state.formData.bomCode = ''
		state.formData.bomName = ''
		state.formData.materialCode = ''
		state.formData.materialName = ''
		state.formData.count = '1'
		state.formData.projectCode = ''
		state.formData.projectName = ''
		state.formData.remark = ''
		state.formData.fifoPersonnel = ''
		state.formData.model = ''
		state.yrksl = 0
		state.source = ''
		state.formData.fifoDateTime = new Date().toISOString().slice(0, 10)
		state.searchValue = ''
		state.searchFoucus = false
		nextTick(() => {
			state.searchFoucus = true
		})
	}
	const {
		fifotype,
		formRef,
		formData,
		pickingPeople,
		slState,
		running,
		yrksl,
		source,
		searchValue,
		searchFoucus,
	} = toRefs(state)

	onMounted(() => {
		state['formRef'].setRules(rules)
	})
</script>
<style lang="scss" scoped>
	.container {
		background-color: #ffffff;
	}

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

	.fifotext {
		line-height: 36px;
	}

	.fifonumber {
		padding-top: 5px;
	}
</style>
