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
      children: [
        {
          id: '@id',
          name: '@title(1,2)',
          code: '@title(1,2)',
          desc: '二级部门',
        },
      ],
    })
  )
}

const List2 = [
  {
    id: 1,
    label: '所有部门',
    children: [
      { id: 3, label: '信息化部' },
      { id: 4, label: '电气设计部' },
      { id: 4, label: '机械设计部' },
    ],
  },
]

module.exports = [
  {
    url: '/organization/tree',
    type: 'get',
    response(config) {
      const { role, pageNo = 1, pageSize = 20 } = config.query
      const mockList = List2.filter(
        (item) => !(role && item.role.indexOf(role) < 0)
      )
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
    url: '/organization',
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
    url: '/organization',
    type: 'put',
    response: () => {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/organization',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟添加成功',
      }
    },
  },
  {
    url: '/organization',
    type: 'delete',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
]
