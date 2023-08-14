<template>
	<view class="container">
		<uni-card v-for="(item, index) in state.listData" :key="index" :title="item.materialName"
			:extra="item.materialCode">
			<view><text class="uni-text">预计完成时间：{{item.yjFinishTime}}</text>&nbsp;&nbsp;
				<text class="uni-text">实际完成时间：{{item.sjFinishTime}}</text>
			</view>
			<view>
				<text class="uni-text">预计发货时间：{{item.yjShipTime}}</text>&nbsp;&nbsp;
				<text class="uni-text">预计入库时间：{{item.gysArrivalTime}}</text>
				<!-- <text class="uni-text">图纸代号：{{item.drawingCode}}</text> -->
			</view>

			<view slot="actions" class="card-actions">
				<view class="card-actions-item" @click="actionsClick(item.id,'接收',item.bomId)"
					v-show="item.yjFinishTime === '' ? true : false">
					<uni-icons type="auth" size="18" color="#999"></uni-icons>
					<text class="card-actions-item-text">接收</text>
				</view>
				<view class="card-actions-item" @click="actionsClick(item.id,'完成',item.bomId)"
					v-show="item.sjFinishTime === '' ? true : false">
					<uni-icons type="checkbox" size="18" color="#999"></uni-icons>
					<text class="card-actions-item-text">完成</text>
				</view>
				<view class="card-actions-item" @click="actionsClick(item.id,'发货',item.bomId)"
					v-show="item.gysArrivalTime === '' ? true : false">
					<uni-icons type="cart" size="18" color="#999"></uni-icons>
					<text class="card-actions-item-text">发货</text>
				</view>
			</view>
		</uni-card>
		<uni-pagination :page-size="state.queryForm.pageSize" :current="state.queryForm.pageNo" :total="state.total"
			@change="changePage" />
		<text
			class="example-info">当前页：{{ state.queryForm.pageNo }}，数据总量：{{ state.total }}条，每页数据：{{ state.queryForm.pageSize }}</text>
		<view>
			<!-- 输入框示例 -->
			<uni-popup ref="inputDialog" background-color="#fff">
				<uni-section title="选择预计完成时间" type="line">
					<view class="example">
						<!-- 基础表单校验 -->
						<uni-forms style="padding: 5px;" labelWidth="90">
							<uni-forms-item label="预计完成时间">
								<uni-datetime-picker type="date" v-model="state.fromData.yjFinishTime" :clear-icon="false" placeholder="请输入预计完成时间" />
							</uni-forms-item>
						</uni-forms>
						<view class="button-group">
							<button type="primary" size="mini" @click="Close('JS')">取消</button>
							<button type="primary" size="mini" @click="dialogInputConfirm('接收')">提交</button>
						</view>
					</view>
				</uni-section>
			</uni-popup>
		</view>
		<view>
			<!-- 输入框示例 -->
			<uni-popup ref="inputDialog3" background-color="#fff">
				<uni-section title="选择预计入库时间" type="line">
					<view class="example">
						<!-- 基础表单校验 -->
						<uni-forms style="padding: 5px;" labelWidth="90">
							<uni-forms-item label="实际完成时间">
								<uni-datetime-picker type="date" v-model="state.fromData.gysArrivalTime"
									:clear-icon="false" placeholder="请输入预计入库时间" />
							</uni-forms-item>
						</uni-forms>
						<view class="button-group">
							<button type="primary" size="mini" @click="Close('FH')">取消</button>
							<button type="primary" size="mini" @click="dialogInputConfirm('发货')">提交</button>
						</view>
					</view>
				</uni-section>
			</uni-popup>
		</view>

		<view>
			<!-- 普通弹窗 -->
			<uni-popup ref="popup" background-color="#fff">
				<uni-section title="选择实际完成、预计发货日期" type="line">
					<view class="example">
						<!-- 基础表单校验 -->
						<uni-forms style="padding: 5px;" labelWidth="90">
							<uni-forms-item label="实际完成时间">
								<uni-datetime-picker type="date" v-model="state.fromData.sjFinishTime"
									:clear-icon="false" placeholder="请输入实际完成时间" />
							</uni-forms-item>
							<uni-forms-item label="预计发货时间">
								<uni-datetime-picker type="date" v-model="state.fromData.yjShipTime" :clear-icon="false"
									placeholder="请选择预计发货时间" />
							</uni-forms-item>
						</uni-forms>
						<view class="button-group">
							<button type="primary" size="mini" @click="Close('WC')">取消</button>
							<button type="primary" size="mini" @click="dialogInputConfirm('完成')">提交</button>
						</view>
					</view>
				</uni-section>
			</uni-popup>
		</view>
	</view>
</template>

<script setup>
	import {
		reactive,
		toRefs,
		onMounted,
	} from 'vue'
	import {
		SupplierGetList,
		OrderDateTimeEdit,
	} from '@/api/Purchase.js'
	// import uniPopup from '@/pages/supplier/receive.vue'

	const state = reactive({
		inputDialog: null,
		inputDialog2: null,
		inputDialog3: null,
		popup: null,
		// 基础表单数据
		queryForm: {
			pageNo: 1,
			pageSize: 5,
			Supplier: 'GYS001'
		},
		fromData: {
			id: 0,
			yjFinishTime: null,
			sjFinishTime: null,
			gysArrivalTime: null, //预计入库时间
			yjShipTime: null, //预计发货时间
			bomId: 0,
		},
		total: 0,
		listData: [],
		tishi: false,
	})

	const getData = async () => {
		const {
			data: {
				list,
				total
			},
		} = await SupplierGetList(state.queryForm)

		state.listData = list
		state.total = total
		state.fromData.id = 0;
		state.fromData.yjFinishTime = null;
		state.fromData.sjFinishTime = null;
		state.fromData.gysArrivalTime = null; //预计入库时间
		state.fromData.yjShipTime = null; //预计发货时间
		state.fromData.bomId = 0;
	}
	const changePage = (e) => {
		state.queryForm.pageNo = e.current
		getData()
	}
	//Close
	const Close = (name) => {
		if(name=="JS"){
			state.inputDialog.close();
		}else if(name=="WC"){
			state.popup.close();
		}else if(name=="FH"){
			state.inputDialog3.close();
		}
	}
	//接收
	const actionsClick = (id, name, bomid) => {
		state.fromData.id = id
		state.fromData.bomId = bomid
		if (name == '接收') {
			state.inputDialog.open();
		} else if (name == '完成') {
			state.popup.open('center');
		} else if (name == '发货') {
			state.inputDialog3.open();
		}
	}
	const dialogInputConfirm = async (name) => {
		let msgs = ''
		if (name == '接收') {
			if (state.fromData.yjFinishTime == null) {
				msgs = '预计完成时间不能为空!'
				uni.showToast({
					title: msgs,
					icon: 'none'
				})
			} else {
				const {
					msg
				} = await OrderDateTimeEdit({
					Id: state.fromData.id,
					type: 'JS',
					time: state.fromData.yjFinishTime,
					BomId: state.fromData.bomId,
				})
				uni.showToast({
					title: msg,
					icon: 'success'
				})
				await getData()
				state.inputDialog.close();
			}
		} else if (name == '完成') {
			msgs = '实际完成、预计发货时间不能为空!'
			if (state.fromData.sjFinishTime == null || state.fromData.yjShipTime == null) {
				uni.showToast({
					title: msgs,
					icon: 'none'
				})
			} else {
				let date1=new Date(state.fromData.sjFinishTime);
				console.log(date1)
				const {
					msg
				} = await OrderDateTimeEdit({
					Id: state.fromData.id,
					type: 'WC',
					time: state.fromData.sjFinishTime + ',' + state.fromData.yjShipTime,
					BomId: state.fromData.bomId,
				})
				uni.showToast({
					title: msg,
					icon: 'success'
				})
				await getData()
				state.popup.close();
			}
		} else if (name == '发货') {
			msgs = '预计入库时间不能为空!'
			if (state.fromData.gysArrivalTime == null) {
				uni.showToast({
					title: msgs,
					icon: 'none'
				})
			} else {
				const {
					msg
				} = await OrderDateTimeEdit({
					Id: state.fromData.id,
					type: 'FH',
					time: state.fromData.gysArrivalTime,
					BomId: state.fromData.bomId,
				})
				uni.showToast({
					title: msg,
					icon: 'success'
				})
				await getData()
				state.inputDialog3.close();
			}
		}
	}

	const {
		queryForm,
		inputDialog,
		inputDialog2,
		inputDialog3,
		popup,
		fromData,
		tishi,
	} = toRefs(state)
	onMounted(() => {
		getData()
	})
</script>

<style lang="scss">
	.example-info {
		padding-left: 60px;
		text-align: center;
		font-size: 14px;
		color: #666;
	}

	.card-actions {
		display: flex;
		// flex-direction: row;
		justify-content: space-around;
		align-items: center;
		height: 20px;
	}

	.card-actions-item {
		padding-top: 13px;
		display: flex;
		// flex-direction: row;
		// align-items: center
	}

	.card-actions-item-text {
		font-size: 12px;
		color: #666;
		//margin-left: 5px
	}

	.uni-text {
		font-size: 12px;
	}

	.button-group {
		margin-bottom: 10px;
		display: flex;
		justify-content: space-around;
	}
</style>
