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
							</uni-forms-item>
						</uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="24">
							<uni-forms-item label="项目编码" name="projectCode">
								<text class="texts">{{formData.projectCode}}</text>
							</uni-forms-item>
						</uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="24">
							<uni-forms-item label="项目名称" name="projectName">
								<text class="texts">{{formData.projectName}}</text>
							</uni-forms-item>
						</uni-col>
					</uni-row>
					<uni-row>
						<uni-col :span="24">
							<uni-forms-item label="数量" name="count">
								<text class="texts">{{formData.count}}</text>
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
		GetByQrCode,
		DoCDAdd,
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
		running: false,
		formData: {
			qrCode: '',
		},
		source: '',
		searchValue: '',
		searchFoucus: true,
	})
	// 校验规则
	const rules = {}
	//提交按钮
	const btnConfirm = () => {
		state['formRef'].validate().then(async res => {
			if (state.formData.materialCode == '' ||
				state.formData.materialCode == undefined ||
				state.formData.materialCode == null) {
				uni.showToast({
					title: '找不到物料信息!',
					icon: 'none'
				})
			} else {
				state.running = true
				state.formData.Id = 0
				const {
					msg
				} = await DoCDAdd(state.formData)
				uni.showToast({
					title: msg,
					icon: 'none'
				})
				reset()
			}
			state.running = false
		}).catch(err => {
			uni.showToast({
				title: err,
				icon: 'none'
			})
		})
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
			//查询报检信息
			const cd = await GetByQrCodeCD({
				QrCode: qrcode
			})
			//未报检
			if (cd.data == null) {
				//查询二维码信息
				const result = await GetByQrCode({
					QrCode: qrcode
				})
				//存在二维码信息
				if (result.data != null) {
					state.formData = Object.assign({}, result.data)
					state.formData.remark = ''
				} else {
					uni.showToast({
						title: '无相关信息!',
						icon: 'none'
					})
				}
			} else { //已报检
				uni.showToast({
					title: '物料已报检!',
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
	const reset = () => {
		state['formRef'].resetFields()
		state.formData = {}
		state.searchValue = ''
		state.searchFoucus = false
		nextTick(() => {
			state.searchFoucus = true
		})
	}
	const {
		formRef,
		running,
		formData,
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

	.texts {
		line-height: 36px;
	}
</style>
