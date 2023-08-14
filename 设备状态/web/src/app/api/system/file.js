import {
	http
} from '@/utils/http.js'


//获取上传图片文件地址
export function getUploadFileUrl() {
  return http.request({
    url: '/File/GetFileUrl',
    method: 'get',
  })
}