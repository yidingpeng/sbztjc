const List = [
  {
    id: 4,
    parentId: 0,
    moduleName: '首页',
    title: '首页',
    icon: 'home-2-fill',
    path: '/',
    component: 'Layout',
    sort: '0',
    moduleCode: null,
    children: [
      {
        id: 5,
        parentId: 4,
        moduleName: 'Index',
        title: '首页',
        icon: 'home-6-line',
        path: 'index',
        component: '@/views/index',
        sort: '0',
        moduleCode: null,
        children: null,
      },
      {
        id: 6,
        parentId: 4,
        moduleName: 'Dashboard',
        title: '看板',
        icon: 'dashboard-line',
        path: 'dashboard',
        component: '@/views/index/dashboard',
        sort: '0',
        moduleCode: null,
        children: null,
      },
      {
        id: 7,
        parentId: 4,
        moduleName: 'Workbench',
        title: '工作台',
        icon: 'tools-fill',
        path: 'workbench',
        component: '@/views/index/workbench',
        sort: '0',
        moduleCode: null,
        children: null,
      },
    ],
  },
  {
    id: 9,
    parentId: 0,
    moduleName: '系统管理Demo',
    title: '系统管理',
    icon: 'settings-3-fill',
    path: '/system',
    component: 'Layout',
    sort: '0',
    moduleCode: null,
    children: [
      {
        id: 10,
        parentId: 9,
        moduleName: 'Role',
        title: '角色管理',
        icon: 'user-settings-line',
        path: '/system/role',
        component: '@/views/system/role',
        sort: '0',
        moduleCode: null,
        children: null,
      },
      {
        id: 11,
        parentId: 9,
        moduleName: 'Module',
        title: '菜单管理',
        icon: 'file-list-2-fill',
        path: '/system/module',
        component: '@/views/system/module',
        sort: '0',
        moduleCode: null,
        children: null,
      },
      {
        id: 12,
        parentId: 9,
        moduleName: 'Log',
        title: '日志管理',
        icon: 'file-copy-2-line',
        path: '/system/log',
        component: '@/views/setting/systemLog',
        sort: '0',
        moduleCode: null,
        children: null,
      },
      {
        id: 13,
        parentId: 9,
        moduleName: 'User',
        title: '用户管理',
        icon: 'user-3-fill',
        path: '/system/user',
        component: '@/views/system/user',
        sort: '0',
        moduleCode: null,
        children: null,
      },
      {
        id: 14,
        parentId: 9,
        moduleName: 'Organization',
        title: '部门管理',
        icon: 'group-fill',
        path: '/system/organization',
        component: '@/views/system/organization',
        sort: '0',
        moduleCode: null,
        children: null,
      },
      {
        id: 15,
        parentId: 9,
        moduleName: 'Configuration',
        title: '系统配置',
        icon: 'settings-2-line',
        path: '/system/configuration',
        component: '@/views/system/configuration',
        sort: '0',
        moduleCode: null,
        children: null,
      },
      {
        id: 16,
        parentId: 9,
        moduleName: 'Dictionary',
        title: '字典管理',
        icon: 'book-3-line',
        path: '/system/dictionary',
        component: '@/views/system/dictionary',
        sort: '0',
        moduleCode: null,
        children: null,
      },
    ],
  },
]

module.exports = [
  {
    url: '/module',
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
        data: list,
      }
    },
  },
  {
    url: '/module',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/module',
    type: 'delete',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
]
