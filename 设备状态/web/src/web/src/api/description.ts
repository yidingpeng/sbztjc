import request from '@/utils/request'

export function getList() {
  return request({
    url:
      'https://' +
      '851edf02-46eb-43e6-828d-' +
      '64c7e483ea41' +
      '.bspapp.com/http/getDescription',
    method: 'get',
  })
}
