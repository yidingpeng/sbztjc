//工时管理API
import {
	http
} from '@/utils/http.js'

//新增、修改工时填报
export function WHAddOrEdit(data) {
  return http.request({
    url: '/WH/WHAddOrEdit',
    method: 'post',
    data,
  })
}