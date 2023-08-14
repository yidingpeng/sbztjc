const { mock } = require('mockjs')
const List = []
const ptlist = [
  {
    name: '1项目',
    code: '0',
    provinceLevelCity: true,
  },
  {
    name: '2项目',
    code: '1',
    provinceLevelCity: true,
  },
  {
    name: '3项目',
    code: '2',
    provinceLevelCity: true,
  },
]
const conlist = [
  {
    name: '1001',
    code: '0',
    provinceLevelCity: true,
  },
  {
    name: '1002',
    code: '1',
    provinceLevelCity: true,
  },
  {
    name: '1003',
    code: '2',
    provinceLevelCity: true,
  },
]
const count = 50
for (let i = 0; i < count; i++) {
  List.push(
    mock({
      uuid: '@uuid',
      id: '@id',
      deliver_Code: '@natural',
      pt_Name: '1',
      payment_Time: '@date',
      ct_Cash: '@float',
      non_Payment: '@float',
      payment_Cash: '@float',
      payment_Precent: '@float',
      remark: '@paragraph',
      pt_Id: '1',
    })
  )
}
module.exports = [
  {
    url: '/payment/getList',
    type: 'get',
    response: (config) => {
      const { deliver_Code, pageNo = 1, pageSize = 20 } = config.query
      let mockList = List.filter((item) => {
        return !(deliver_Code && item.deliver_Code.indexOf(deliver_Code) < 0)
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
  //获取项目名称
  {
    url: '/payment/GetProjectPayment',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { ptlist },
      }
    },
  },
  //获取合同编号
  {
    url: '/payment/GetContractPayment',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { conlist },
      }
    },
  },
  {
    url: '/payment/doEdit',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟编辑成功',
      }
    },
  },
  {
    url: '/payment/doAdd',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟添加成功',
      }
    },
  },
  {
    url: '/payment/doDelete',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
]
