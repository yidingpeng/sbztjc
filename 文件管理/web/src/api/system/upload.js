import request from '@/utils/request'

import { baseAPI } from '@/config/net.config'
const baseURL = `${baseAPI}/upload`
export function getImage(id) {
  return request({
    url: `${baseURL}/image/${id}`,
    method: 'get',
    id,
  })
}

export function getImageBase64(id) {
  return request({ url: `${baseURL}/base64/${id}`, method: 'get', id })
}

export function getList(params) {
  return request({ url: baseURL, method: 'get', params })
}

export function doUpload(type, data) {
  return request({ url: `${baseURL}/${type}`, method: 'post', data })
}

export function doDelete(id) {
  return request({ url: `${baseURL}/${id}`, method: 'delete', id })
}

export function doUploadAvatar(params) {
  console.log('上传参数为：', params)
  return request({
    url: `${baseURL}/avatar`,
    method: 'post',
    //contentType: 'multipart/form-data',
    headers: {
      'Content-Type': 'multipart/form-data',
    },
    data: params,
    // transformRequest: function () {
    //   return base64ToFile(file)
    // },
  })
}

export function doDownload(id, filename) {
  request({
    url: `${baseURL}/download/${id}`,
    method: 'get',
    responseType: 'blob',
  }).then((res) => {
    const fileURL = window.URL.createObjectURL(res)
    const fileLink = document.createElement('a')

    fileLink.href = fileURL
    fileLink.setAttribute('download', filename)
    document.body.appendChild(fileLink)

    fileLink.click()
  })
}

// function base64ToFile(urlData, fileName) {
//   let arr = urlData.split(',')
//   let mime = arr[0].match(/:(.*?);/)[1]
//   let bytes = atob(arr[1]) // 解码base64
//   let n = bytes.length
//   let ia = new Uint8Array(n)
//   while (n--) {
//     ia[n] = bytes.charCodeAt(n)
//   }
//   return new File([ia], fileName, { type: mime })
// }
