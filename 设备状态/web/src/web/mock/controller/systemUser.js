const { mock } = require('mockjs')
const List = []
const count = 50
for (let i = 0; i < count; i++) {
  List.push(
    mock({
      id: '@id',
      account: '@title(1, 2)',
      fullname: '@title(1,2)',
      sex: '男',
      department: '信息化部',
      telnum: '18600000000',
      roles: ['admin', 'user'],
    })
  )
}
module.exports = [
  {
    url: '/api/user',
    type: 'get',
    response: (config) => {
      const { title, pageNo = 1, pageSize = 20 } = config.query
      let mockList = List.filter((item) => {
        return !(title && item.title.indexOf(title) < 0)
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
    url: '/api/user',
    type: 'put',
    response: () => {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/api/user',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟添加成功',
      }
    },
  },
  {
    url: '/api/user',
    type: 'delete',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
  {
    url: '/api/user/resetpwd',
    type: 'delete',
    response: () => {
      return {
        code: 200,
        msg: '模拟重置密码成功',
      }
    },
  },
]
