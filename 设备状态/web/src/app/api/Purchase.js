import {
	http
} from '@/utils/http.js'

export function UpdateMQualified(data) {
	return http.request({
		url: '/Purchase/UpdateMQualified',
		method: 'POST',
		data,
	})
}
//根据二维码号查询二维码信息
export function GetByQrCode(params) {
	return http.request({
		url: '/Purchase/GetByIdQrCode',
		method: 'GET',
		params,
	})
}
//根据二维码号查询出入库信息
export function GetByQrCodeFifo(params) {
	return http.request({
		url: '/Purchase/GetByIdQrCodeFifo',
		method: 'GET',
		params,
	})
}

//出入库新增
export function doFifoAdd(data) {
	return http.request({
		url: '/Purchase/FifoAdd',
		method: 'post',
		data,
	})
}

//出入库编辑
export function doFifoEdit(data) {
	return http.request({
		url: '/Purchase/FifoEdit',
		method: 'put',
		data,
	})
}

//供应子表分页
export function SupplierGetList(params) {
	return http.request({
		url: '/Purchase/SupplierGetList',
		method: 'GET',
		params,
	})
}

//
export function OrderDateTimeEdit(params) {
	return http.request({
		url: '/purchase/OrderDateTimeEdit',
		method: 'POST',
		params,
	})
}
//通过参数类型获取系统配置值
export function GetDeployType(params) {
	return http.request({
		url: '/Purchase/GetDeployType',
		method: 'GET',
		params,
	})
}
//通过参数编码查询系统配置值
export function GetDeployCode(params) {
	return http.request({
		url: '/Purchase/GetDeployCode',
		method: 'GET',
		params,
	})
}

//获取上传图片文件地址
export function getInsertFileUrl() {
	return http.request({
		url: '/purchase/GetInsertFilePath',
		method: 'get',
	})
}

//根据物料编码获取物料信息
export function MatByCodeInfo(params) {
	return http.request({
		url: '/Purchase/MatByCodeInfo',
		method: 'GET',
		params,
	})
}
//二维码查询已入库数量
export function GetByQrCodeYRK(params) {
	return http.request({
		url: '/Purchase/GetByQrCodeYRK',
		method: 'GET',
		params,
	})
}
//根据二维码获取质检信息
export function GetByQrCodeQC(params) {
	return http.request({
		url: '/Purchase/GetByQrCodeQC',
		method: 'GET',
		params,
	})
}
//根据二维码获取报检信息
export function GetByQrCodeCD(params) {
	return http.request({
		url: '/Purchase/GetByQrCodeCD',
		method: 'GET',
		params,
	})
}

//质检新增
export function doQCAdd(data) {
	return http.request({
		url: '/Purchase/DoQCAdd',
		method: 'post',
		data,
	})
}

//报检新增
export function DoCDAdd(data) {
	return http.request({
		url: '/Purchase/DoCDAdd',
		method: 'post',
		data,
	})
}

//质检编辑
export function doQCEdit(data) {
	return http.request({
		url: '/Purchase/DoQCEdit',
		method: 'put',
		data,
	})
}

//根据用户查询待确认的物料列表
export function IssulList(params) {
	return http.request({
		url: '/Purchase/IssulList',
		method: 'GET',
		params,
	})
}
//批量领料
export function BatchPick(data) {
	return http.request({
		url: '/Purchase/BatchPick',
		method: 'post',
		data,
	})
}

//退料
export function ReturnedMaterial(data) {
	return http.request({
		url: '/Purchase/ReturnedMaterial',
		method: 'post',
		data,
	})
}
