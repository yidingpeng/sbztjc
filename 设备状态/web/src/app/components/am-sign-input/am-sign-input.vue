<template>
	<view class="sign">
		<view class="imgBox">
			<view class="nom_img" v-if="!showImg">
				<image v-if="!showImg" src="/static/other/sign_icon.png" style="width: 140px;height: 80px;"
					@click="signModShow=true">
				</image>
			</view>

			<view class="across_img" v-if="showImg">
				<view v-if="showImg" class="delete_icon" @click.stop="deleteImg">
					x
				</view>
				<image v-if="showImg" :src="showImg" style="width: 140px;height: 80px;" @click="signModShow=true">
				</image>
			</view>

		</view>

		<umask :show="signModShow" @click="signModShow=false" :duration="0">
			<view class="warp">
				<view class="signBox" @tap.stop>
					<view class="wrapper">
						<view class="handBtn">
							<image @click="selectColorEvent('black','#1A1A1A')"
								:src="selectColor === 'black' ? '/static/other/color_black_selected.png' : '/static/other/color_black.png'"
								class="black-select"></image>
							<image @click="selectColorEvent('red','#ca262a')"
								:src="selectColor === 'red' ? '/static/other/color_red_selected.png' : '/static/other/color_red.png'"
								class="red-select"></image>
							<button @click="retDraw" class="delBtn">重写</button>
							<button @click="saveCanvasAsImg" class="saveBtn">保存</button>
							<button @click="previewCanvasImg" class="previewBtn">预览</button>
							<button @click="subCanvas" class="subBtn">完成</button>
						</view>
						<view class="handCenter">
							<canvas v-show="signModShow" :disable-scroll="true" @touchstart="uploadScaleStart"
								@touchmove="uploadScaleMove" :style="{width:'100%',height:'calc(85vh - 8rpx)'}"
								:canvas-id="canvasId"></canvas>
						</view>

						<view class="handCenters">
							<canvas :canvas-id="canvasIds" :style="{width:100+'vh',height:85 +'vw'}"></canvas>
						</view>

						<view class="handRight">
							<view class="handTitle">请签名</view>
						</view>
					</view>
				</view>
			</view>
		</umask>
	</view>

</template>

<script>
	import umask from "./u-mask/u-mask.vue"
	export default {
		components: {
			umask
		},
		data() {
			return {
				signModShow: false,
				ctx: '',
				ctxs: '',
				startX: null,
				startY: null,
				canvasWidth: 0,
				canvasHeight: 0,
				selectColor: 'black',
				lineColor: '#1A1A1A', // 颜色
				lineSize: 5, // 笔记倍数
				showImg: ""
			};
		},

		props: ["action", "canvasId", "canvasIds"],
		mounted() {
			if (!this.ctx) {
				this.ctx = uni.createCanvasContext(this.canvasId, this);
			}
			if (!this.ctxs) {
				this.ctxs = uni.createCanvasContext(this.canvasIds, this);
			}
			setTimeout(() => {
				uni.createSelectorQuery().in(this).select('.handCenter').boundingClientRect(rect => {
						this.canvasWidth = rect.width;
						this.canvasHeight = rect.height;
						/* 将canvas背景设置为 白底，不设置  导出的canvas的背景为透明 */
						// this.setCanvasBg('#fff');
					})
					.exec();
			}, 300);
		},
		methods: {
			// 笔迹开始
			uploadScaleStart(e) {
				this.startX = e.changedTouches[0].x
				this.startY = e.changedTouches[0].y
				//设置画笔参数
				//画笔颜色
				this.ctx.setStrokeStyle(this.lineColor)
				//设置线条粗细
				this.ctx.setLineWidth(this.lineSize)
				//设置线条的结束端点样式
				this.ctx.setLineCap("round") //'butt'、'round'、'square'
				//开始画笔
				this.ctx.beginPath()
			},
			// 笔迹移动
			uploadScaleMove(e) {
				//取点
				let temX = e.changedTouches[0].x
				let temY = e.changedTouches[0].y
				//画线条
				this.ctx.moveTo(this.startX, this.startY)
				this.ctx.lineTo(temX, temY)
				this.ctx.stroke()
				this.startX = temX
				this.startY = temY
				this.ctx.draw(true)
			},
			/**
			 * 重写
			 */
			retDraw() {
				this.ctx.clearRect(0, 0, 700, 730);
				this.ctx.draw();
				//设置canvas背景
				// this.setCanvasBg('#fff');
			},
			/**
			 * @param {Object} str
			 * @param {Object} color
			 * 选择颜色
			 */
			selectColorEvent(str, color) {
				this.selectColor = str;
				this.lineColor = color;
			},

			//完成
			subCanvas() {
				let that = this
				uni.canvasToTempFilePath({
					canvasId: that.canvasId,
					fileType: 'png',
					quality: 1, //图片质量
					success(res) {
						that.ctxs.translate(0, that.canvasWidth);
						that.ctxs.rotate(-90 * Math.PI / 180)
						that.ctxs.drawImage(res.tempFilePath, 0, 0, that.canvasWidth, that
							.canvasHeight)
						that.ctxs.draw()
						setTimeout(() => {
							uni.canvasToTempFilePath({
								canvasId: that.canvasIds,
								success: function(res1) {
									if (that.action) {
										uni.showLoading()
										uni.uploadFile({
											url: that.action, //图片上传post请求的地址
											filePath: res1.tempFilePath,
											name: "UploadFile",
											header: {
												'Authorization': uni.getStorageSync(
														'token') ? "Bearer " + uni
													.getStorageSync('token') : '',
											},
											success: (uploadFileRes) => {
												uni.hideLoading()
												that.showImg = res1.tempFilePath
												that.$emit('signToUrl',
													uploadFileRes)
												that.signModShow = false
												that.retDraw()
											},
											fail: (error) => {
												uni.hideLoading()
											}
										});
									} else {
										that.showImg = res1.tempFilePath
										that.$emit('signToUrl', {
											error_code: "201",
											msg: "请配置上传文件接口参数action"
										})
										that.signModShow = false
										that.retDraw()
									}
								},
								fail: (err) => {}
							}, that)
						}, 200);
					}
				}, this);
			},
			//保存到相册
			saveCanvasAsImg() {
				uni.canvasToTempFilePath({
					canvasId: this.canvasId,
					fileType: 'png',
					quality: 1, //图片质量
					success(res) {
						uni.saveImageToPhotosAlbum({
							filePath: res.tempFilePath,
							success(res) {
								uni.showToast({
									title: '已保存到相册',
									duration: 2000
								});
							}
						});
					}
				}, this);
			},
			//预览
			previewCanvasImg() {
				uni.canvasToTempFilePath({
					canvasId: this.canvasId,
					fileType: 'jpg',
					quality: 1, //图片质量
					success(res) {
						uni.previewImage({
							urls: [res.tempFilePath] //预览图片 数组
						});
					}
				}, this);
			},
			//设置canvas背景色  不设置  导出的canvas的背景为透明
			//@params：字符串  color
			setCanvasBg(color) {
				/* 将canvas背景设置为 白底，不设置  导出的canvas的背景为透明 */
				//rect() 参数说明  矩形路径左上角的横坐标，左上角的纵坐标, 矩形路径的宽度, 矩形路径的高度
				//这里是 canvasHeight - 4 是因为下边盖住边框了，所以手动减了写
				this.ctx.rect(0, 0, this.canvasWidth, this.canvasHeight - 4);
				// ctx.setFillStyle('red')
				this.ctx.setFillStyle(color);
				this.ctx.fill(); //设置填充
				this.ctx.draw(); //开画
			},

			deleteImg() {
				this.showImg = ""
			}
		}
	};
</script>

<style lang="scss">
	page {
		background: #d9d9d9;
		height: auto;
		overflow: hidden;
	}

	.imgBox {
		width: 160px;
		height: 100px;
		position: relative;

		.nom_img {
			border-radius: 8px;
			overflow: hidden;
			height: 100px;
		}

		.across_img {
			border: 2px solid #868282;
			border-radius: 8px;
			height: 80px;
			width: 140px;

			.delete_icon {
				position: absolute;
				top: -13px;
				right: 8px;
				width: 26px;
				height: 26px;
				overflow: hidden;
				color: #ffffff;
				font-size: 26px;
				text-align: center;
				line-height: 20px;
				background: red;
				border-radius: 25px;
				z-index: 1;
			}
		}
	}

	.warp {
		width: 100%;
		height: 100vh;
		display: flex;
		justify-content: center;
		align-items: center;

		.signBox {
			width: 85vw;
			height: 85vh;
			background: #ffffff;
			border-radius: 8px;
		}
	}

	.wrapper {
		width: 85vw;
		height: 85vh;
		overflow: hidden;
		display: flex;
		align-content: center;
		flex-direction: row;
		justify-content: center;
		font-size: 28rpx;
	}

	.handRight {
		display: inline-flex;
		align-items: center;
	}

	.handCenter {
		border: 4rpx dashed #e9e9e9;
		flex: 5;
		overflow: hidden;
		box-sizing: border-box;
	}

	.handCenters {
		position: fixed;
		top: 0;
		left: 10000rpx;
		flex: 5;
		overflow: hidden;
		box-sizing: border-box;
	}


	.handTitle {
		transform: rotate(90deg);
		flex: 1;
		color: #666;
	}

	.handBtn button {
		font-size: 28rpx;
	}

	.handBtn {
		height: 85vh;
		display: inline-flex;
		flex-direction: column;
		justify-content: space-between;
		align-content: space-between;
		flex: 1;
	}

	.delBtn {
		position: absolute;
		top: 380rpx;
		left: 46rpx;
		transform: rotate(90deg);
		color: #666;
	}

	.subBtn {
		position: absolute;
		bottom: 158rpx;
		left: 46rpx;
		display: inline-flex;
		transform: rotate(90deg);
		background: #008ef6;
		color: #fff;
		text-align: center;
		justify-content: center;
	}

	/*Peach - 新增 - 保存*/

	.saveBtn {
		position: absolute;
		top: 650rpx;
		left: 46rpx;
		transform: rotate(90deg);
		color: #666;
	}

	.previewBtn {
		position: absolute;
		top: 516rpx;
		left: 46rpx;
		transform: rotate(90deg);
		color: #666;
	}

	/*Peach - 新增 - 保存*/

	.black-select {
		width: 60rpx;
		height: 60rpx;
		position: absolute;
		top: 150rpx;
		left: 70rpx;
	}

	.red-select {
		width: 60rpx;
		height: 60rpx;
		position: absolute;
		top: 260rpx;
		left: 70rpx;
	}
</style>
