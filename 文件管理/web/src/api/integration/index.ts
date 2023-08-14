import request from '@/utils/request'
import { baseAPI } from '@/config/net.config'

const baseUrl = `${baseAPI}/integration`

export function importOrganization(data: any) {
  return request({
    url: `${baseUrl}/import/organization`,
    method: 'post',
    data,
  })
}

export function exportOrganization(data: any) {
  return request({
    url: `${baseUrl}/export/organization`,
    method: 'post',
    data,
  })
}
