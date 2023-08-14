//const { mock } = require('mockjs')
//const List = []
//const count = 50
// for (let i = 0; i < count; i++) {
//   List.push(
//     mock({
//       id: '@id',
//       type: 'type' + (i % 10),
//       code: '@title',
//       vlaue: '@title(2,3)',
//       desc: '',
//     })
//   )
// }
const List = [
  {
    id: '1',
    type: '系统配置',
    code: 'system_color',
    value: '001',
    desc: '描述',
  },
  {
    id: '2',
    type: '系统配置',
    code: 'system_title',
    value: '生产管理系统3.0',
    desc: '描述',
  },
  {
    id: '3',
    type: '系统配置',
    code: 'system_icon',
    value: '',
    desc: '',
  },
  {
    id: '4',
    type: '系统配置',
    code: 'system_login_backgroudImage',
    value: '',
    desc: '',
  },
  {
    id: '5',
    type: '系统配置',
    code: 'system_login_verifycode',
    value: 'false',
    desc: '登录验证码是否开启，为空和false表示关闭，否则表示开启',
  },
]

const TypeList = ['系统配置', '客户端配置', 'API配置', '全部配置']

module.exports = [
  {
    url: '/configuration',
    type: 'get',
    response: (config) => {
      const { code, pageNo = 1, pageSize = 20 } = config.query
      let mockList = List.filter((item) => {
        return !(code && item.code.indexOf(title) < 0)
      })
      const list = mockList.filter(
        (item, index) =>
          index < pageSize * pageNo && index >= pageSize * (pageNo - 1)
      )
      return {
        code: 200,
        msg: 'success',
        data: { list, ...{ total: mockList.length } },
      }
    },
  },
  {
    url: '/configuration/type',
    type: 'get',
    response: () => {
      let mockList = TypeList.filter((item) => {
        return !(title && item.title.indexOf(title) < 0)
      })
      const list = mockList
      return {
        code: 200,
        msg: 'success',
        data: { list },
      }
    },
  },
  {
    url: '/configuration',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟添加成功',
      }
    },
  },
  {
    url: '/configuration',
    type: 'put',
    response: () => {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/configuration',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟添加成功',
      }
    },
  },
  {
    url: '/configuration',
    type: 'delete',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
]
