<template>
	<uni-popup :show="showDailog" type="center">
		<view class="example">
			<!-- 自定义表单校验 -->
			<uni-forms ref="formRef" :rules="rules" :modelValue="form">
				<uni-forms-item label="预计完成时间" required name="yjFinishTime">
					<uni-easyinput v-model="form.yjFinishTime" placeholder="请选择预计完成时间" />
				</uni-forms-item>
			</uni-forms>
			<button type="primary" @click="submit('formRef')">提交</button>
		</view>
	</uni-popup>

</template>

<script setup>
	import {
		reactive,
		toRefs,
		onMounted,
	} from 'vue'

	const state = reactive({
		formRef: null,
		// 基础表单数据
		form: {
			yjFinishTime: null,
		},
		rules: {
			yjFinishTime: {
				rules: [{
					required: true,
					errorMessage: '预计完成时间不能为空'
				}]
			},

		},
		showDailog:false,
	})

	//提交
	const Submit = () => {
		state['formRef'].validate().then(async res => {
			try {
				//console.log(state.formData)
				const {
					msg
				} = await doFifoAdd(state.form)
				uni.showToast({
					title: msg,
					icon: 'success'
				})
				if (msg.indexOf("成功") != -1) {
					reset()
				}
			} finally {
				reset()
			}
		}).catch(err => {})
		state['formRef'].setRules(rules)
	}
	const {
		formRef,
		form,
		rules,
	} = toRefs(state)

	onMounted(() => {
		state['formRef'].setRules(rules)
	})
</script>

<style>
</style>
