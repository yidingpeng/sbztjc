import request from '@/utils/request'
import { encryptedData } from '@/utils/encrypt'
import { loginRSA } from '@/config'
import { baseAPI } from '@/config/net.config'

const baseUrl = `${baseAPI}/login`
//const baseUrl = '/login'

export async function login(data: any) {
  if (loginRSA) {
    data = await encryptedData(data)
    console.log('data is :', data)
  }
  return request({
    url: baseUrl,
    method: 'post',
    data,
  })
}

export async function ssologin(data: any) {
  if (loginRSA) {
    data = await encryptedData(data)
    console.log('data is :', data)
  }
  return request({
    url: 'Login/SSO',
    method: 'post',
    data,
  })
}

export async function socialLogin(data: any) {
  if (loginRSA) {
    data = await encryptedData(data)
  }
  return request({
    url: '/socialLogin',
    method: 'post',
    data,
  })
}

export function getUserInfo() {
  return request({
    url: `${baseUrl}/userInfo`,
    method: 'get',
  })
}

export function logout() {
  return request({
    url: '/logout',
    method: 'get',
  })
}

export function register(data: any) {
  return request({
    url: '/register',
    method: 'post',
    data,
  })
}
