const List = [
  {
    id: 0,
    type: '系统公告',
    title: '关于系统通知应用指南',
    link: '/inform/1',
    time: '@datetime',
    status: 1,
    readCount: 123,
  },
  {
    id: 1,
    type: '系统公告',
    title: '软件框架发布通知-v1.0',
    link: '/inform/2',
    time: '@datetime',
    status: 1,
    readCount: 124,
  },
  {
    id: 2,
    type: '审批通知',
    title: '问题反馈：GJCS22000 XXXX试验台问题反馈 审批通知',
    link: '/pm/2',
    time: '@datetime',
    status: 1,
    readCount: 133,
  },
  {
    id: 3,
    type: '审批通知',
    title: '问题反馈：GJCS22000 XXXX试验台问题反馈 已审批通过',
    link: '/pm/2',
    time: '@datetime',
    status: 1,
    readCount: 144,
  },
]

module.exports = [
  {
    url: '/inform',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { list: List, total: List.length },
      }
    },
  },
  {
    url: '/inform/top',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { list: List, total: List.length },
      }
    },
  },
  {
    url: '/inform/types',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: {
          list: [
            { name: '通知公告', value: 'notice' },
            { name: '公司新闻', value: 'news' },
            { name: '系统更新', value: 'update' },
          ],
        },
      }
    },
  },
  {
    url: '/inform',
    type: 'post',
    response: () => {
      return { code: 200, msg: '模拟添加成功' }
    },
  },
  {
    url: '/inform',
    type: 'put',
    response: () => {
      return { code: 200, msg: '模拟修改成功' }
    },
  },
  {
    url: '/inform/[0-9]+',
    type: 'delete',
    response: () => {
      return { code: 200, msg: '模拟删除成功' }
    },
  },
]
