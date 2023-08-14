//import API from '@/api'
import { getList as roleList } from '@/api/system/role'
import { getList as orgList } from '@/api/system/organization'
import { getList as userList } from '@/api/system/user'

//审批工作流人员/组织选择器配置

export default {
  //配置接口正常返回代码
  successCode: 200,
  //配置组织
  group: {
    //请求接口对象
    apiObj: orgList,
    //接受数据字段映射
    parseData: function (res) {
      return {
        rows: res.data,
        msg: res.message,
        code: res.code,
      }
    },
    //显示数据字段映射
    props: {
      key: 'id',
      label: 'name',
      children: 'children',
    },
  },
  //配置用户
  user: {
    apiObj: userList,
    pageSize: 20,
    parseData: function (res) {
      return {
        rows: res.data.list,
        total: res.data.total,
        msg: res.message,
        code: res.code,
      }
    },
    props: {
      key: 'id',
      label: 'fullname',
    },
    request: {
      page: 'pageNo',
      pageSize: 'pageSize',
      groupId: 'organization',
      keyword: 'username',
    },
  },
  //配置角色
  role: {
    //请求接口对象
    apiObj: roleList,
    //接受数据字段映射
    parseData: function (res) {
      return {
        rows: res.data.list,
        msg: res.message,
        code: res.code,
      }
    },
    //显示数据字段映射
    props: {
      key: 'id',
      label: 'role',
      children: 'children',
    },
  },
}
