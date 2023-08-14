<template>
<!-- 	<uni-notice-bar show-icon scrollable color="#628CF6" background-color="#E9EFFF" :speed="30" show-close
		:text="TextScrolling" /> -->
	<view class="header">
<!-- 		<view class="quick-box">
			<view class="quick-item">
				<view>
					<uni-badge class="uni-badge-left-margin" text="" absolute="rightTop" size="small">
						<image mode="aspectFit" class="quick-icon" src="/static/icons/task.png"></image>
					</uni-badge>
				</view>
				<view class="qick-title">RFID录入</view>
			</view>
			<view class="quick-item">
				<view>
					<uni-badge class="uni-badge-left-margin" text="" absolute="rightTop" size="small">
						<image mode="aspectFit" class="quick-icon" src="/static/icons/approve.png"></image>
					</uni-badge>
				</view>
				<view class="qick-title">审批</view>
			</view>
			<view class="quick-item">
				<view>
					<uni-badge class="uni-badge-left-margin" text="" absolute="rightTop" size="small">
						<image mode="aspectFit" class="quick-icon" src="/static/icons/notice.png"></image>
					</uni-badge>
				</view>
				<view class="qick-title">通知</view>
			</view>
		</view> -->
	</view>
	<view class="main">
		<view class="warp">
			<!-- <uni-section title="问题反馈" type="line" padding>
				<uni-grid :column="3" :show-border="false" :square="false">
					<uni-grid-item @click="goFeedBack()">
						<view class="grid-item-box">
							<uni-icons type="help" :size="30" color="#333" />
							<text class="text">问题上报</text>
						</view>
					</uni-grid-item>
					<uni-grid-item @click="goInfo()">
						<view class="grid-item-box">
							<uni-icons type="info" :size="30" color="#333" />
							<text class="text">反馈信息</text>
						</view>
					</uni-grid-item>
				</uni-grid>
			</uni-section> -->
			<uni-section title="物料管理" type="line" padding>
				<uni-grid :column="3" :show-border="false" :square="false">
					<uni-grid-item @click="goInspection()">
						<view class="grid-item-box">
							<uni-icons custom-prefix="iconfont" type="icon-wuliaobaojian" :size="30" />
							<text class="text">扫码</text>
						</view>
					</uni-grid-item>
					<uni-grid-item @click="goMaterialquality()">
						<view class="grid-item-box">
							<uni-icons custom-prefix="iconfont" type="icon-zhijiandan_huaban" :size="30" />
							<text class="text">质检</text>
						</view>
					</uni-grid-item>
					<uni-grid-item @click="goFifo()">
						<view class="grid-item-box">
							<uni-icons custom-prefix="iconfont" type="icon-approach" size="30"></uni-icons>
							<!-- <uni-icons type="home" :size="30" color="#777" /> -->
							<text class="text">入库</text>
						</view>
					</uni-grid-item>
					<uni-grid-item @click="goFifo2()">
						<view class="grid-item-box">
							<uni-icons custom-prefix="iconfont" type="icon-exit" size="30"></uni-icons>
							<text class="text">出库</text>
						</view>
					</uni-grid-item>
					<uni-grid-item @click="LinLiao()">
						<view class="grid-item-box">
							<uni-icons custom-prefix="iconfont" type="icon-lingliaoshengchan" :size="30" />
							<text class="text">领料</text>
						</view>
					</uni-grid-item>
					<uni-grid-item @click="LinLiaoSelect()">
						<view class="grid-item-box">
							<uni-icons type="search" :size="30" />
							<text class="text">领料查询</text>
						</view>
					</uni-grid-item>
					<uni-grid-item @click="TuiLiao()">
						<view class="grid-item-box">
							<uni-icons custom-prefix="iconfont" type="icon-tuiliao" :size="30" />
							<text class="text">退料</text>
						</view>
					</uni-grid-item>
					<!-- <uni-grid-item @click="goSupplier()">
						<view class="grid-item-box">
							<uni-icons type="gear" :size="30" color="#777" />
							<text class="text">供应商任务</text>
						</view>
					</uni-grid-item> -->
				</uni-grid>
			</uni-section>
		<!-- 	<uni-section title="工时管理" type="line" padding>
				<uni-grid :column="3" :show-border="false" :square="false">
					<uni-grid-item @click="goWorkHour()">
						<view class="grid-item-box">
							<uni-icons custom-prefix="iconfont" type="icon-renyuangongshi" size="30"></uni-icons>
							<text class="text">工时填报</text>
						</view>
					</uni-grid-item>
				</uni-grid>
			</uni-section> -->
		</view>
	</view>
</template>
<style>
	@import "@/static/iconfont.css";
</style>
<script>
	import {
		GetDeployCode,
	} from '@/api/Purchase.js'
	export default {
		data() {
			return {
				TextScrolling: '',
				QrCode:''
			}
		},
		onLoad() {
			this.GetDeployCode('TextScrolling')
		},
		methods: {
			goInspection() {

				
					 uni.scanCode({
					success: function(res) {
						uni.navigateTo({
							url: '/pages/index/OrderSave?QrCoe='+res.result,
						});
					},
					fail: function() {
						uni.showToast({
							title: '扫码失败',
							icon: 'none'
						})
					}
				});
					
				// uni.navigateTo({
				// 	url: '/pages/index/ScanCode',
				// });
			},
			goFeedBack() {
				uni.navigateTo({
					url: '/pages/feedback/index',
				});
			},
			goInfo() {
				uni.navigateTo({
					url: '/pages/feedback/info',
				});
			},
			goMaterialquality() {
				uni.navigateTo({
					url: '/pages/material/IsQualified',
				});
			},
			//进入入库页面
			goFifo() {
				uni.navigateTo({
					url: '/pages/fifo/FifoManage',
				});
			},
			//进入出库页面
			goFifo2() {
				uni.navigateTo({
					url: '/pages/fifo/FifoManage2',
				});
			},
			LinLiao() {
				uni.navigateTo({
					url: '/pages/material/picking',
				});
			},
			LinLiaoSelect(){
				uni.navigateTo({
					url: '/pages/material/PickingInquiry',
				});
			},
			TuiLiao() {
				uni.navigateTo({
					url: '/pages/material/returnedMaterial',
				});
			},
			goWorkHour() {
				uni.navigateTo({
					url: '/pages/manHour/whfilling',
				});
			},
			//进入供应商任务页面
			// goSupplier() {
			// 	uni.navigateTo({
			// 		url: '/pages/supplier/materialinfo',
			// 	});
			// },
			//goSignature
			// goSignature() {
			// 	uni.navigateTo({
			// 		url: '/pages/supplier/complete',
			// 	});
			// },
			//获取滚动消息内容
			async GetDeployCode() {
				let data = await GetDeployCode({
					code: 'TextScrolling'
				});
				this.TextScrolling = data.data.value
				//console.log(this.TextScrolling)
			}
		}
	}
</script>

<style lang="scss" scoped>
	.uni-noticebar {
		margin-bottom: 0;
	}

	.header {
		height: 6rem;
		background: linear-gradient(to bottom, #337ecc 0, #337ecc 65%, #f5f5f5 0, #f5f5f5);
		padding: 0 0.5rem;
		display: flex;

		.quick-box {
			height: 4.5rem;
			display: flex;
			margin-top: 1.5rem;
			width: 100%;
			background-color: #fff;
			border-radius: 0.5rem;
			box-shadow: 0 0.2rem 1rem 0 #999;

			.quick-item {
				display: flex;
				flex: 1;
				flex-direction: column;
				justify-content: center;
				align-items: center;
			}

			.quick-icon {
				height: 2rem;
				width: 2rem;
			}

			.qick-title {
				font-size: 0.6rem;
			}
		}
	}

	.main {
		margin-top: 1rem;
		padding: 0 0.5rem;
	}

	.text {
		font-size: 12px;
		margin-top: 5px;
	}

	.grid-item-box {
		flex: 1;
		// position: relative;
		/* #ifndef APP-NVUE */
		display: flex;
		/* #endif */
		flex-direction: column;
		align-items: center;
		justify-content: center;
		padding: 15px 0;
	}

	.grid-dot {
		position: absolute;
		top: 5px;
		right: 15px;
	}

	.swiper {
		height: 420px;
	}
</style>
