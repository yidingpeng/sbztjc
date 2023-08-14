const { mock } = require('mockjs')
const List = []
const count = 50
for (let i = 0; i < count; i++) {
  List.push(
    mock({
      id: '@id',
      name: '@title(1, 2)',
      code: '@title(2,3)',
      desc: '部门描述',
    })
  )
}
module.exports = [
  {
    url: '/api/organization',
    type: 'get',
    response: (config) => {
      const { key, pageNo = 1, pageSize = 20 } = config.query
      let mockList = List.filter((item) => {
        return !(
          key &&
          item.name.indexOf(key) < 0 &&
          item.code.indexOf(key) < 0
        )
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
    url: '/api/organization',
    type: 'put',
    response: () => {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/api/organization',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟添加成功',
      }
    },
  },
  {
    url: '/api/organization',
    type: 'delete',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
]
