<script setup>
	import {
		reactive,
		toRefs,
		onMounted,
	} from 'vue'
	import {
		onLaunch,
		onShow,
		onHide
	} from '@dcloudio/uni-app';

	import {
		useUserStore
	} from '@/store/user.js'
	import netConfig from '@/config/weburl.js'
	import {
		GetDeployCode,
	} from '@/api/Purchase.js'

	const userStore = useUserStore()
	const state = reactive({
		version: '',
		newversion: ''
	})

	onLaunch(async () => {
			//#ifdef APP-PLUS
			getClientId()
			//#endif

			const token = uni.getStorageSync('token')
			if (token) {
				try {
					await userStore.getUserInfo()
				} catch (e) {
					toLogin()
					return
				}
				//#ifdef APP-PLUS
				plus.navigator.closeSplashscreen()
				//#endif
			} else {
				toLogin()
			}
			await AndroidCheckUpdate()
		}),

		onShow(() => {
			//console.log('App Show')
		}),

		onHide(() => {
			//console.log('App Hide')
		})

	const AndroidCheckUpdate = async () => {
		uni.getSystemInfo({
			success: (res) => {
				//console.log(res.platform)
				//检测当前平台，如果是安卓则启动安卓更新  
				if (res.platform == "android") {
					plus.runtime.getProperty(plus.runtime.appid, (info) => {
						//console.log(JSON.stringify(info));
						state.version = info.versionCode
					})
				}
			}
		})
		//获取最新版本号
		let data = await GetDeployCode({
			code: 'Version'
		})
		state.newversion = data.data.value
		//console.log(data.data)
		//console.log(state.version + '-' + state.newversion)
		if (data.data.value != null || data.data.value != '' || data.data.value != undefined) {
			if (data.data.value > state.version) {
				uni.showToast({
					title: '有新的版本发布，程序已启动自动更新。新版本下载完成后将自动弹出安装程序。',
					mask: false,
					duration: 5000,
					icon: "none"
				});
				var dtask = plus.downloader.createDownload(
					netConfig.baseURL + "/app/RWPMS.apk", {},
					function(d, status) {
						//下载完成  
						if (status == 200) {
							plus.runtime.install(plus.io.convertLocalFileSystemURL(d
								.filename), {}, {}, function(error) {
								uni.showToast({
									title: '安装失败',
									mask: false,
									duration: 1500
								});
							})
						} else {
							uni.showToast({
								title: '更新失败',
								mask: false,
								duration: 1500
							});
						}
					});
				try {
					dtask.start(); // 开启下载的任务
					var prg = 0;
					var showLoading = plus.nativeUI.showWaiting(
						"正在下载"); //创建一个showWaiting对象 
					dtask.addEventListener('statechanged', function(
						task,
						status
					) {
						// 给下载任务设置一个监听 并根据状态  做操作
						switch (task.state) {
							case 1:
								showLoading.setTitle("正在下载");
								break;
							case 2:
								showLoading.setTitle("已连接到服务器");
								break;
							case 3:
								prg = parseInt(
									(parseFloat(task.downloadedSize) /
										parseFloat(task.totalSize)) *
									100
								);
								showLoading.setTitle("  正在下载" + prg + "%  ");
								break;
							case 4:
								plus.nativeUI.closeWaiting();
								//下载完成
								break;
						}
					});
				} catch (err) {
					plus.nativeUI.closeWaiting();
					uni.showToast({
						title: '更新失败-03',
						mask: false,
						duration: 1500
					});
				}
			}
		}
	}
	const {
		version,
		newversion
	} = toRefs(state)

	onMounted(() => {
		AndroidCheckUpdate()
	})
</script>
<script>
	const toLogin = () => {
		uni.reLaunch({
			url: '/pages/user/login/login',
			success: () => {
				//#ifdef APP-PLUS
				plus.navigator.closeSplashscreen()
				//#endif
			}
		})
	}

	//获取客户端标识
	const getClientId = () => {
		let clientId = uni.getStorageSync('clientId')
		if (!clientId) {
			plus.push.getClientInfoAsync(function(info) {
				uni.setStorageSync('clientId', info.clientid)
			}, function(e) {
				console.log(e)
			})
		}
	}
</script>
<style lang="scss">
	/*每个页面公共css */
	@import '@/uni_modules/uni-scss/index.scss';
	/* #ifndef APP-NVUE */
	@import '@/static/customicons.css';

	// 设置整个项目的背景色
	page {
		background-color: #f5f5f5;
		height: 100%;
	}

	#app {
		height: 100%;
	}

	/* #endif */
</style>
