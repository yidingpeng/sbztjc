<template>
	<uni-list>
		<uni-list-item :title="'应用名称：'+name"></uni-list-item>
		<uni-list-item :title="'Version：'+version"></uni-list-item>
	</uni-list>
</template>

<script setup>
	import {
		reactive,
		toRefs,
		onMounted,
	} from 'vue'

	const state = reactive({
		version: '',
		name: ''
	})

	const {
		version,
		name
	} = toRefs(state)

	onMounted(() => {
		SystemInfo()
	})
	const SystemInfo = () => {
		uni.getSystemInfo({
			success: (res) => {
				if (res.platform == "android") {
					plus.runtime.getProperty(plus.runtime.appid, (info) => {
						//console.log(JSON.stringify(info));
						state.version = info.version
						state.name = info.name
					})
				}
			}
		})
	}
</script>
