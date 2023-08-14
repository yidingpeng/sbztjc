const { mock } = require('mockjs')

const List = []
const clientlist = [
  {
    name: '低',
    code: '132',
    provinceLevelCity: true,
  },
  {
    name: '中',
    code: '133',
    provinceLevelCity: true,
  },
  {
    name: '高',
    code: '134',
    provinceLevelCity: true,
  },
]
const cooperalist = [
  {
    name: '产线及信息化',
    code: '139',
    provinceLevelCity: true,
  },
  {
    name: '试验设备',
    code: '137',
    provinceLevelCity: true,
  },
  {
    name: '车载产品',
    code: '138',
    provinceLevelCity: true,
  },
  {
    name: '城轨市场',
    code: '136',
    provinceLevelCity: true,
  },
  {
    name: '维保市场',
    code: '140',
    provinceLevelCity: true,
  },
]
const count = 50
for (let i = 0; i < count; i++) {
  List.push(
    mock({
      uuid: '@uuid',
      id: '@id',
      companyCode: '@integer',
      clientName: '@clast',
      clientFullName: '@name',
      ownerName: '@cname',
      address: '@region',
      clientRankText: '@cword',
      cooperativeBusinessText: '@cword',
      createdAt: '@datetime',
      remark: '@paragraph',
      clientRank: '1',
      cooperativeBusiness: '1',
    })
  )
}
module.exports = [
  {
    url: '/client/GetClientCompany',
    type: 'post',
    response: (config) => {
      const { companyCode, pageNo = 1, pageSize = 20 } = config.query
      let mockList = List.filter((item) => {
        return !(companyCode && item.companyCode.indexOf(companyCode) < 0)
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
  //字典获取客户级别
  {
    url: '/client/GetDicClient',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { clientlist },
      }
    },
  },
  //字典获取合作业务
  {
    url: '/client/GetDicBusiness',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { cooperalist },
      }
    },
  },
  {
    url: '/client/doAdd',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟添加成功',
      }
    },
  },
  {
    url: '/client/doEdit',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟编辑成功',
      }
    },
  },
  {
    url: '/client/doDelete',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
]
