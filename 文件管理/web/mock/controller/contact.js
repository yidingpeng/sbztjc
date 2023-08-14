const List = []

// const CompanyIdList = [
//   { value: '53', label: '53' },
//   { value: '2', label: '2' },
//   { value: '47', label: '47' },
// ]

// const ContactsCategoryList = [
//   { value: '1', label: '150' },
//   { value: '2', label: '149' },
// ]

const SexList = [
  { value: '男', label: '男' },
  { value: '女', label: '女' },
  { value: '保密', label: '保密' },
]

// const PostList = [
//   { value: '主管', label: '主管' },
//   { value: '设计师', label: '设计师' },
//   { value: '技术主管', label: '技术主管' },
// ]

module.exports = [
  {
    url: '/contact/getList',
    type: 'get',
    response: (config) => {
      const { contactsName, pageNo = 1, pageSize = 20 } = config.query
      const mockList = List.filter(
        (item) => !(contactsName && item.contactsName.indexOf(contactsName) < 0)
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
    url: '/contact/doEdit',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/contact/doDelete',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
  {
    url: '/contact/ContactsCategory',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { ContactsCategoryList },
      }
    },
  },
  {
    url: '/contact/Sex',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { SexList },
      }
    },
  },
  {
    url: 'https://localhost:7242/api/Contact/Company',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
      }
    },
  },
  {
    url: '/contact/Post',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { PostList },
      }
    },
  },
]
