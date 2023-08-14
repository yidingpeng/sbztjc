/**
 * @description 导出网络配置
 **/
module.exports = {
  // 默认的接口地址，开发环境和生产环境都会走/vab-mock-server
  // 正式项目可以选择自己配置成需要的接口地址，如"https://api.xxx.com"
  // 问号后边代表开发环境，冒号后边代表生产环境
  baseURL:
    process.env.NODE_ENV === 'development'
      ? 'https://localhost:7242/api' //'http://192.168.0.53:9011/api'
      : // '/vab-mock-server'
        //'http://47.96.139.205:8006/api', //205生产环境API
        //https://localhost:7242/api, //本地测试环境API
        //'http://192.168.0.225:8021/api', //本地部署测试环境API
        //'http://192.168.0.53:8087/api', //53测试环境API
        'http://127.0.0.1:8044/api', //100测试环境API
  // 配后端数据的接收方式application/json;charset=UTF-8 或 application/x-www-form-urlencoded;charset=UTF-8
  contentType: 'application/json;charset=UTF-8',
  getUploadUrl(path) {
    return `${this.baseURL}/upload${path ?? ''}`
  },
  uploadURL: '/upload',
  //配置为空时，由baseURL来决定由谁来处理API请求
  baseAPI: '',
  // 最长请求时间
  requestTimeout: 20000,
  // 操作正常code，支持String、Array、int多种类型
  successCode: [200, 0, '200', '0'],
  // 数据状态的字段名称
  statusName: 'code',
  // 状态信息的字段名称
  messageName: 'msg',
}
