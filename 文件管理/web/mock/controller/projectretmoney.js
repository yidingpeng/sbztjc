const { mock } = require('mockjs')
const List = []
const count = 50
for (let i = 0; i < count; i++) {
  List.push(
    mock({
      deliveryCode: '@uuid',
      id: '@id',
      projectCode: '@uuid',
      retMoneyProportion: '@integer',
      retMoneyAmount: '@integer',
      projectID: '@ctitle',
      projectName: '@ctitle',
      warrantyPeriod: '@date',
      expirationDate: '@date',
      alrExpirationMoney: '@integer',
      remark: '@csentence',
    })
  )
}
module.exports = [
  {
    url: '/project/projectretmoney/getList',
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
    url: '/project/projectretmoney/doEdit',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/project/projectretmoney/doDelete',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
]
