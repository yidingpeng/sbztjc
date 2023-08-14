<template>
	<view class="container">
		<uni-section title="订单信息" style="padding-right:10px ;" type="line">
			<view class="example">
				<view>
					<uni-forms ref="formRef" :modelValue="formData" :rules="rules"  >
						<uni-forms-item label="RFID:" name="RFID">
                             
							<input class="uni-input" disabled v-model="formData.rFID" />
							<!-- <uni-easyinput  disabled  v-model="formData.rFID"   /> -->

						</uni-forms-item>
						<uni-forms-item label="车轴编号:" name="AxleNumber">


							<input type="text" class="uni-input" v-model="formData.axleNumber" placeholder="请输入车轴编号" placeholder-style="font-size:2px" />
							<!-- 	<uni-easyinput  type="text"   v-model="formData.axleNumber" :styles="inputstyle.styles" placeholder="请输入车轴编号" /> -->
						</uni-forms-item>
						<uni-forms-item label="车轴型号:" name="AxleModel">

							<input class="uni-input" v-model="formData.axleModel" placeholder="请输入车轴型号" placeholder-style="font-size:2px"  />
							<!-- <uni-easyinput type="text"   v-model="formData.axleModel" :styles="inputstyle.styles" placeholder="请输入车轴型号"/> -->
						</uni-forms-item>
						<uni-forms-item label="操作人:" name="Operator">

							<input class="uni-input" disabled v-model="formData.operator" />
							<!-- <uni-easyinput  disabled  v-model="formData.operator"  placeholder=""  /> -->
						</uni-forms-item>
					</uni-forms>

				</view>
				<view class="buttoncenter">
					<button class="login-submit" size="mini" @click="userLogin"><p class="buttoncenter">提交</p></button>
					
				</view>


			</view>
		</uni-section>
	</view>
</template>

<script setup>
	import {
		DoAdd
	} from '@/api/order.js'

	import {
		reactive,
		toRefs,
		onMounted,
		nextTick,
	} from 'vue'
	import {
		useUserStore
	} from '@/store/user.js'

	import {
		storeToRefs
	} from "pinia"

	const userStore = useUserStore();
	const inputstyle = reactive({
		placeholderStyle: "color:#2979FF;font-size:14px",
		styles: {
			color: '#2979FF',
			borderColor: '#2979FF',

		},
	})
	const {
		realName,
		avatar,
		deptName
	} = storeToRefs(userStore)
	const state = reactive({
		formRef: null,
		formData: {
			id: 0,
			axleNumber: '',
			axleModel: '',
			rFID: '1001',
			currentState: '',
			creationTime: new Date().toISOString().slice(0, 10),
			operator: '',
			isDeleted: ''
		},
	})
	const inputStyle = {
		borderColor: '#f1f5f9',

	}
	// const rules = {
	// 	RFID: {
	// 		rules: [{
	// 			required: true,
	// 			errorMessage: '请输入RFID',
	// 		}]
	// 	},
	// 	AxleNumber: {
	// 		rules: [{
	// 			required: true,
	// 			errorMessage: '请输入车轴编号',
	// 		}]
	// 	},

	// 	Operator: {
	// 		rules: [{
	// 			required: true,
	// 			errorMessage: '请输入操作人',
	// 		}]
	// 	},
	// 	AxleModel: {
	// 		rules: [{
	// 			required: true,
	// 			errorMessage: '请输入车轴型号',
	// 		}]
	// 	}
	// }
	const userLogin = () => {
		if (!state.formData.axleNumber.trim() || state.formData.axleNumber == null) {
			uni.showToast({
				title: '请输入车轴编号',
				icon: 'none',
			})
		} else if (!state.formData.axleModel.trim() || state.formData.axleModel == null) {
			uni.showToast({
				title: '请输入车轴型号',
				icon: 'none',
			})
		} else {
			state['formRef'].validate().then(async res => {
				const {
					msg
				} = await DoAdd(state.formData)
				uni.showToast({
					title: msg,
					icon: 'none',
				})

				if (msg.indexOf("添加成功") != -1) {
					uni.switchTab({
						url: '/pages/index/index',
					});
				}

			}).catch(err => {
				console.log('表单错误信息：', err);
			})
		}

	}
	const {
		formData,
		formRef,
	} = toRefs(state)
	const {
		placeholderStyle,
		styles,
	} = toRefs(inputstyle)
	onMounted(() => {
		//获取路由中的参数
		let route = getCurrentPages();
		let curParam = route[route.length - 1].$page.options.QrCoe;
		// console.log(JSON.stringify(route))
		//state.formData.RFID = curParam;
		state.formData.operator = realName;
	})
</script>
<style>
	.login-submit {
		display: flex;	
		height: 45px;
		font-size: 17px;
		margin-left: 10px;
		background-color: #337ecc;
		color: #fff;
        margin-top: 240px;
		margin-bottom: 20px;
	}
	
   .buttoncenter{
	   width: 100%;
	   text-align: center;
	   line-height: 45px;
   }
	 

	.container {
		background-color: #ffffff;
		
	}

	.uni-input {
		color: #000 !important;
		height: 35px;
		line-height: 20px;
		margin-left: -5px;
	}
#selectForm >>> .el-form-item__label {
  text-align: center;
}
	.uni-forms-item {
		margin-left: 20px;
		margin-top: 20px;
	}
	
</style>>
