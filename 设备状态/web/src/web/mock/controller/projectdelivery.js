const { mock } = require('mockjs')
const List = []
const count = 50
for (let i = 0; i < count; i++) {
  List.push(
    mock({
      deliveryCode: '@uuid',
      id: '@id',
      projectID: '@uuid',
      projectName: '@province' + '@csentence',
      jhjhDate: '@date',
      sjjhDate: '@date',
      jhysDate: '@date',
      sjysDate: '@date',
      acceptancePhaseName: '@cword',
      AcceptanceCertificate: '@url',
      isZYS: '否',
      remark: '@csentence',
    })
  )
}
const List2 = []
for (let i = 0; i < 10; i++) {
  List2.push(mock({ label: '@uuid', value: '@id' }))
}

module.exports = [
  {
    url: '/project/projectdelivery/getList',
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
    url: '/project/projectdelivery/getList2',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { List2 },
      }
    },
  },
  {
    url: '/project/projectdelivery/doEdit',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/project/projectdelivery/doDelete',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
]
