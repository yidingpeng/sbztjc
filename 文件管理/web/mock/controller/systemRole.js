const List = [
  {
    id: '@id',
    role: 'admin',
    btnRolesCheckedList: ['read:system', 'write:system', 'delete:system'],
    description: '',
  },
  {
    id: '@id',
    role: 'editor',
    btnRolesCheckedList: ['read:system', 'write:system'],
    description: '',
  },
]

module.exports = [
  {
    url: '/api/role',
    type: 'get',
    response(config) {
      const { role, pageNo = 1, pageSize = 20 } = config.query
      const mockList = List.filter(
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
    url: '/api/role',
    type: 'post',
    response() {
      return {
        code: 200,
        msg: '模拟添加成功',
      }
    },
  },
  {
    url: '/api/role',
    type: 'put',
    response() {
      return {
        code: 200,
        msg: '模拟修改成功',
      }
    },
  },
  {
    url: '/api/role',
    type: 'delete',
    response() {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
]
