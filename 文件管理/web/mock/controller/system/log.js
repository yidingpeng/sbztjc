const { mock } = require('mockjs')
const List = []
const count = 50
for (let i = 0; i < count; i++) {
  List.push(
    mock({
      id: '@id',
      account: '@account(1, 2)',
      'type|1': [1, 2, 3, 4],
      'account|1': ['admin', 'editor', 'test'],
      'result|1': [true, false],
      'executeResult|1': [
        '登录成功',
        '登录成功',
        '登录失败',
        '接口异常',
        'dos攻击',
      ],
      desc: '描述信息',
      ip: '@ip',
      datetime: '@datetime',
    })
  )
}

module.exports = [
  {
    url: '/systemLog',
    type: 'get',
    response: (config) => {
      const { account, pageNo = 1, pageSize = 20, type } = config.query
      const mockList = List.filter(
        (item) =>
          !(account && item.account.indexOf(account) < 0) &&
          (type == 0 || item.type == type)
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
]
