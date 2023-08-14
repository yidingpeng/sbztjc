const { mock } = require('mockjs')
const List = []
const ProjectList = [
  { label: '测试项目1', value: '110001' },
  { label: '测试项目2', value: '110002' },
  { label: '测试项目3', value: '110003' },
  { label: '测试项目4', value: '110004' },
  { label: '测试项目5', value: '110005' },
  { label: '测试项目6', value: '110006' },
  { label: '测试项目7', value: '110007' },
  { label: '测试项目8', value: '110008' },
  { label: '测试项目9', value: '110009' },
  { label: '测试项目10', value: '1100010' },
]
// const projectContacts = [
//   { label: '张三', value: '1' },
//   { label: '李四', value: '2' },
//   { label: '王五', value: '3' },
//   { label: '赵六', value: '4' },
// ]
// const contactsTypes = [
//   { label: '内部', value: '1' },
//   { label: '客户', value: '2' },
// ]
// //负责板块
// const respPlate = [
//   { label: '项目', value: '142' },
//   { label: '商务', value: '143' },
//   { label: '技术', value: '144' },
//   { label: '采购', value: '145' },
//   { label: '团队', value: '146' },
// ]
const count = 50
for (let i = 0; i < count; i++) {
  List.push(
    mock({
      id: '@id',
      pid: '@guid',
      pName: '@region' + '@ctitle',
      contactsName: '@cname',
      contactsTypeName: '@ctitle',
      fzbkName: '@ctitle',
    })
  )
}
module.exports = [
  {
    url: '/projectcontacts/getList',
    type: 'get',
    response: (config) => {
      const { projectCode, pageNo = 1, pageSize = 20 } = config.query
      let mockList = List.filter((item) => {
        return !(projectCode && item.projectCode.indexOf(projectCode) < 0)
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
    url: '/project/projectcontacts/doEdit',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/projectcontacts/doDelete',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
  //项目名称
  {
    url: '/projectcontacts/projectList',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { ProjectList },
      }
    },
  },
  //项目联系人
  {
    url: 'https://localhost:7242/api/projectcontacts/Contact',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
      }
    },
  },
  //项目联系人类别
  {
    url: '/projectcontacts/projectContactsTypes',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { contactsTypes },
      }
    },
  },
  //负责板块
  {
    url: '/projectcontacts/respPlates',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { respPlate },
      }
    },
  },
]
