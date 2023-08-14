import request from '@/utils/request'
import { baseURL } from '../config'

export function getList() {
  return request({
    url: `${baseURL}/notice`,
    method: 'get',
  })
}
