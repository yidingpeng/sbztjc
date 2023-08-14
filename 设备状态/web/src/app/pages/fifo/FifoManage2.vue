<template>
	<view class="container">
		<uni-row>
			<uni-col :span="21">
				<uni-search-bar radius="5" :focus="searchFoucus" v-model="searchValue" placeholder="输入二维码号" clearButton="auto" cancelButton="none"
					@confirm="handleSearch" />
			</uni-col>
			<uni-col :span="3">
				<uni-icons style="float:right;padding-top: 12px;padding-right: 8px;" @click="scanClick()" type="scan"
					:size="30" color="#666" />
			</uni-col>
		</uni-row>
		<uni-section title="出库信息" type="line">
			<view class="example">
				<!-- 基础表单校验 -->
				<uni-forms ref="formRef" :modelValue="formData">
					<uni-forms-item label="领料人员" name="deliverySignature" required>
						<e-select :modelValue="formData.fifoPersonnel" v-model="formData.deliverySignature"
							:options="PersonnelList" @change="bindChange" placeholder="请选择领料人员" :clear="true">
						</e-select>
					</uni-forms-item>
					<uni-forms-item label="物料编号" name="materialCode">
						<text class="fifotext">{{formData.materialCode}}</text>
					</uni-forms-item>
					<uni-row>
						<uni-col :span="18">
							<uni-forms-item label="物料名称" name="materialName">
								<text class="fifotext">{{formData.materialName}}</text>
							</uni-forms-item>
						</uni-col>
						<uni-col :span="6"><text class="fifotext">库存：{{formData.kuCun}}</text></uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="14">
							<uni-forms-item label="出库数量" name="count">
								<uni-number-box style="padding-top: 5px;" v-model="formData.count" :min="1"
									:disabled="slState" />
							</uni-forms-item>
						</uni-col>
						<uni-col :span="7">
							<text class="fifotext">历史已出：{{yrksl}}</text>
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
				<button type="primary" :loading="running" class="mini-btn" @click="FifoSubmit()">出库</button>
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
		getUserList,
	} from '@/api/user.js'
	import {
		GetByQrCode,
		doFifoAdd,
		GetByQrCodeFifo,
		GetByQrCodeYRK,
	} from '@/api/Purchase.js'

	const userStore = useUserStore();
	const {
		realName
	} = storeToRefs(userStore)
	const state = reactive({
		formRef: null,
		// 基础表单数据
		formData: {
			type: 2,
			qrCode: '',
			bomCode: '',
			bomName: '',
			materialCode: '',
			materialName: '',
			projectCode: '',
			projectName: '',
			count: 1,
			fifoDateTime: new Date().toISOString().slice(0, 10),
			fifoPersonnel: '',
			remark: '',
			deliverySignature: '',
			kuCun: 0,
		},
		PersonnelList: [],
		slState: true,
		yrksl: 0,
		running: false,
		searchValue: '',
		searchFoucus: true,
	})
	// 校验规则
	const rules = {
		deliverySignature: {
			rules: [{
				required: true,
				errorMessage: '请选择领料人员'
			}]
		},
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
	//搜索回车
	const handleSearch = async (res) => {
		if (res.value.length >= 10 && res.value.length < 20) {
			await QrCodeInfoLoad(res.value)
		}
	}

	//获取二维码号是否有效并加载
	const QrCodeInfoLoad = async (qrcode) => {
		if (qrcode.length > 10 && qrcode.length < 20) {
			const QrCode = await GetByQrCodeFifo({
				QrCode: qrcode,
				Type: 1,
			})
			if (QrCode.data != null) {
				state.formData = Object.assign({}, QrCode.data)
				state.formData.deliverySignature = ''
				//历史已出库数量
				const yrkCount = await GetByQrCodeYRK({
					QrCode: qrcode,
					Type: 2,
				})
				state.yrksl = yrkCount.data
			} else {
				uni.showToast({
					title: '物料未入库!',
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
				if (state.formData.fifoPersonnel == '') {
					uni.showToast({
						title: '请选择领料人!',
						icon: 'none'
					})
				} else if (state.formData.materialCode == '') {
					uni.showToast({
						title: '找不到物料信息!',
						icon: 'none'
					})
				} else if (state.formData.kuCun - state.formData.count < 0 || state.formData.kuCun == 0) {
					uni.showToast({
						title: '库存不足!',
						icon: 'none'
					})
				} else {
					state.running = true
					state.formData.Id = 0
					state.formData.type = 2
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
	}

	//获取用户数据
	const GetPersonnel = async () => {
		let per = await getUserList()
		//console.log(per.data)
		if (per.data.length > 0) {
			per.data.forEach((item) => {
				if (item.orgId > 3) {
					state.PersonnelList.push({
						text: item.account,
						value: item.id
					})
				}
			})
		}
	}

	//领料人员选择事件
	const bindChange = (value) => {
		if (value.value <= 0) {
			state.formData.deliverySignature = ''
			state.formData.fifoPersonnel = ''
		} else {
			state.formData.deliverySignature = value.value
			state.formData.fifoPersonnel = value.text
		}
	}
	//编辑数量
	const btnCount = () => {
		state.slState = false
	}

	const reset = () => {
		state.formData.type = 2
		state.formData.qrCode = ''
		state.formData.bomCode = ''
		state.formData.bomName = ''
		state.formData.materialCode = ''
		state.formData.materialName = ''
		state.formData.count = 1
		state.formData.projectCode = ''
		state.formData.projectName = ''
		state.formData.remark = ''
		state.formData.fifoPersonnel = ''
		state.formData.deliverySignature = ''
		state.formData.fifoDateTime = new Date().toISOString().slice(0, 10)
		state.formData.kuCun = 0
		state.yrksl = 0
		state.slState = true
		state.searchValue = ''
		state.searchFoucus = false
		nextTick(() => {
			state.searchFoucus = true
		})
	}
	const {
		formRef,
		formData,
		PersonnelList,
		slState,
		running,
		yrksl,
		searchValue,
		searchFoucus,
	} = toRefs(state)
	onMounted(() => {
		GetPersonnel()
		state['formRef'].setRules(rules)
	})
</script>
<style lang="scss" scoped>
	.example {
		padding: 15px;
		background-color: #fff;
	}

	.container {
		background-color: #ffffff;
	}

	.fifotext {
		line-height: 36px;
	}
</style>
