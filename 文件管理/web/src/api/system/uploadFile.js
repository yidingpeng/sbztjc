import request from '@/utils/request'
//上传文件
export function InsertFile(data) {
  return request({
    url: '/File/InsertFile',
    method: 'post',
    Headers: {
      'Content-Type': 'application/json; charset=utf-8',
    },
    data,
  })
}

export function getUrl() {
  return request({
    url: '/File/GetFileUrl',
    method: 'get',
  })
}

//查看文件
export function doDownload(url, fileurl, id) {
  request({
    url: `${url}?fileUrl=${fileurl}&id=${id}`,
    method: 'get',
    responseType: 'blob',
  }).then((res) => {
    const fileURL = window.URL.createObjectURL(res)
    const fileLink = document.createElement('a')

    fileLink.href = fileURL
    fileLink.setAttribute('target', '_blank')
    document.body.appendChild(fileLink)

    fileLink.click()
  })
}

//根据文件完整路径查看文件
export async function GetFileByPath(url, fullPath) {
  return request({
    url: `${url}?fullPath=${fullPath}`,
    method: 'get',
    responseType: 'blob',
  }) //.then((res) => {
  // var binaryData = []
  // binaryData.push(res)
  // const fileURL = window.URL.createObjectURL(
  //   new Blob(binaryData, { type: 'application/pdf' })
  // )
  //const fileLink = document.createElement('a')
  //fileLink.href = fileURL
  //fileLink.setAttribute('target', '_blank')
  //document.body.appendChild(fileLink)

  //fileLink.click()
}

//下载文件
export function doDownload2(url, fileurl, id, filename) {
  request({
    url: `${url}?fileUrl=${fileurl}&id=${id}`,
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
