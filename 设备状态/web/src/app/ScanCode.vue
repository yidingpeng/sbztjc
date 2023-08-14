<template>
    <view class="content">
        <image class="logo" src="/static/logo.png"></image>
        <view class="bu">
            <button @tap="startcode()" type="primary">开启连续扫码</button>
        </view>
        <view class="bu">
            <button @tap="getqrcode()" type="primary">获取扫码数据</button>
        </view>
        <view class="bu">
            <button @tap="clearqrcode()" type="primary">清空扫码数据</button>
        </view>

    </view>
</template>

<script>
    const modal = uni.requireNativePlugin('yy-scanbarcode'); 
    export default {
        data() {
            return {

            }
        },

        methods: {
            startcode(){
                 modal.startcode(); 
            },
            getqrcode(){
                modal.getqrcode({

                },result=>{
                uni.showToast({
                icon:'none',
                title: result,
                duration: 2000
                                     });
                }); 
            },
            clearqrcode(){
                 modal.clearqrcode({
                //message:'清除缓存成功'
            //message可以传你想要弹出的内容,传空则不会显示弹窗toast
                     message:''
                 });
            },

        }
    }
</script>

<style>
    .content {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }
    .logo {
        height: 200rpx;
        width: 200rpx;
        margin-top: 200rpx;
        margin-left: auto;
        margin-right: auto;
        margin-bottom: 50rpx;
    }
    .text-area {
        display: flex;
        justify-content: center;
    }
    .title {
        font-size: 36rpx;
        color: #8f8f94;
    }
    .bu {
        margin-top: 20upx;
    }
</style>