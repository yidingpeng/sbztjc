const List = [
  {
    id: '@id',
    //项目Id
    pt_id: '30',
    // 项目编号
    pt_code: 'GJ6666666',
    // 项目名称
    pt_Name: '空压机试验台',
    // 合同编号
    ct_code: 'CT88888',
    // 合同金额
    ct_cash: '25152',
    // 合同签订日期
    ct_signingDate: '2021-3-6',
    // 合同交货日期
    ct_deliveryDate: '2022-9-7',
    // 付款方式
    pay_mode: '微信',
    // 回款条款
    payment_collection: 'XXXXXXXXXXXXXX',
    // 合同附件
    ct_attachment: 'XXXXXXXXXX',
    // 备注
    Remark: '无',
  },
  {
    id: '@id',
    pt_id: '30',
    pt_code: 'GJ684521',
    pt_Name: '空压机试验台2',
    ct_code: 'CT8884558',
    ct_cash: '25152',
    ct_signingDate: '2021-3-6',
    ct_deliveryDate: '2022-9-7',
    pay_mode: '现金',
    payment_collection: 'XXXXXXXXXXXXXX',
    ct_attachment: 'XXXXXXXXXX',
    Remark: '无',
  },
  {
    id: '@id',
    pt_id: '30',
    pt_code: 'GJ664512',
    pt_Name: '空压机试验台3',
    ct_code: 'CT84118',
    ct_cash: '25152',
    ct_signingDate: '2021-3-6',
    ct_deliveryDate: '2022-9-7',
    pay_mode: '支付宝',
    payment_collection: 'XXXXXXXXXXXXXX',
    ct_attachment: 'XXXXXXXXXX',
    Remark: '无',
  },
]
module.exports = [
  {
    url: '/contract/getList',
    type: 'get',
    response: (config) => {
      const { pt_Name, ct_code, pageNo = 1, pageSize = 20 } = config.query
      let mockList = List.filter((item) => {
        return !(
          (pt_Name && item.pt_Name.indexOf(pt_Name) < 0) ||
          (ct_code && item.ct_code.indexOf(ct_code) < 0)
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
    url: '/contract/doEdit',
    type: 'post',
    response() {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/contract/doDelete',
    type: 'post',
    response() {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
]
