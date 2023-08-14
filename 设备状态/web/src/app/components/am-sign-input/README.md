### 使用方法
* 注意
+ 同一个页面不同的输入框需要设置不同的canvasId和canvasIds，否则在同一个页面会出现冲突
```
<template>
	<view class="content">
		<signInput canvasId="twoDrowCanvas" canvasIds="twoRotateCanvas" :action="action" @signToUrl="twoSignToUrl">
		</signInput>
	</view>
</template>

<script>
	import signInput from "@/components/am-sign-input/am-sign-input.vue"
	export default {
		components: {
			signInput
		},
		data() {
			return {
				action: "",
			}
		},
		methods: {
			/**
			 * @param {Object} e
			 * 签名完成回调
			 */
			signToUrl(e) {
				console.log(e);
			},
		}
	}
</script>

<style lang="scss">
	.content {
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
	}
</style>

```