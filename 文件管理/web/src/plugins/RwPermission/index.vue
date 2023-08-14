<template>
  <el-table
    v-loading="state.loading"
    :data="state.treeList"
    default-expand-all
    row-key="id"
  >
    <el-table-column type width="50">
      <template #default="scope">
        <el-checkbox
          v-model="state.list[scope.$index].selfChecked"
          :indeterminate="state.list[scope.$index].isHalf"
          @change="(value) => handleCheckAll(value, scope.$index)"
        />
      </template>
    </el-table-column>
    <el-table-column
      label="菜单"
      prop="title"
      show-overflow-tooltip
      width="200px"
    />
    <el-table-column label="操作">
      <template #default="scope">
        <el-checkbox-group
          v-model="state.list[scope.$index].opChecked"
          @change="(value) => handleOpCheck(value, scope.$index)"
        >
          <el-checkbox
            v-for="(item, key) in scope.row.operation"
            :key="key"
            :disabled="setChkStatue(item.id, scope.row)"
            :label="item.id"
          >
            {{ item.title }}
          </el-checkbox>
        </el-checkbox-group>
      </template>
    </el-table-column>
  </el-table>
</template>
<script setup>
  import { getPermission as getPermissionForRole } from '@/api/system/role'
  import { getPermission as getPermissionForUser } from '@/api/system/user'

  const props = defineProps({
    mode: {
      type: String,
      require: true,
      default: 'role',
      validator(value) {
        return ['role', 'user'].includes(value)
      },
    },
  })

  const state = reactive({
    loading: false,
    treeList: [],
    list: [],
  })

  const load = async (id) => {
    state.loading = true
    const { data } =
      props.mode === 'role'
        ? await getPermissionForRole({ roleId: id })
        : await getPermissionForUser({ userId: id })
    state.list = []
    treeToList(data)
    initModuleCheckStatus()
    state.treeList = data
    state.loading = false
  }

  const clear = () => {
    state.treeList = []
    state.list = []
  }

  const setChkStatue = (id, row) => {
    if (props.mode === 'user') {
      return row.roleChecked?.includes(id)
    }
    return false
  }

  const getCheckedOperation = () => {
    const checkedOperation = []
    state.list.forEach((item) => {
      checkedOperation.push(...item.opChecked)
    })
    return checkedOperation
  }

  const getCheckedModule = () => {
    const checkedModule = []
    state.list.forEach((item) => {
      if (item.selfChecked || item.isHalf) {
        checkedModule.push(item.id)
      }
    })
    return checkedModule
  }

  const getCheckedAll = () => {
    return { module: getCheckedModule(), operation: getCheckedOperation() }
  }

  const handleCheckAll = (value, index) => {
    const data = state.list[index]
    data.opChecked = value ? data.operation : []
    data.isHalf = false
    handleChildCheck(value, data)
    updateModuleCheckStatus(data)
  }

  const handleOpCheck = (value, index) => {
    const data = state.list[index]
    const checkedCount = value.length
    const opCount = data.operation.length
    data.selfChecked = checkedCount === opCount
    data.isHalf = checkedCount > 0 && checkedCount < opCount
    updateModuleCheckStatus(data)
  }

  // 菜单checkbox变化时，子菜单及操作全选或取消全选
  const handleChildCheck = (value, data) => {
    const children = state.list.filter((item) => item.parentId === data.id)
    if (children.length === 0) return
    children.forEach((item) => {
      item.selfChecked = value
      item.isHalf = false
      item.opChecked = value ? item.operation : []
      handleChildCheck(value, item)
    })
  }

  const initModuleCheckStatus = () => {
    const exist = []
    state.list.forEach((item) => {
      if (item.isLast && !exist.includes(item.id)) {
        exist.push(item.id)
        updateModuleCheckStatus(item)
      }
    })
  }

  // 更新模块checkbox状态
  const updateModuleCheckStatus = (data) => {
    const parent = state.list.find((item) => item.id === data.parentId)
    if (!parent) return
    const children = state.list.filter((item) => item.parentId === parent.id)
    if (
      // 如果子菜单且操作全部选中，则父菜单选中
      children.every((item) => item.selfChecked) &&
      parent.opChecked.length == parent.operation.length
    ) {
      parent.selfChecked = true
      parent.isHalf = false
    } else if (
      // 如果子菜单没有全选和半选，则父菜单取消选中
      !children.some((item) => item.isHalf) &&
      !children.some((item) => item.selfChecked)
    ) {
      parent.selfChecked = false
      parent.isHalf = false
    } else {
      parent.selfChecked = false
      parent.isHalf = true
    }
    updateModuleCheckStatus(parent)
  }

  // 将树结构转换为列表结构
  const treeToList = (treeData, parent) => {
    treeData.forEach((item) => {
      const data = {
        title: item.title, // 菜单名称
        id: item.id, // 菜单id
        parentId: parent?.id ?? 0, // 菜单父级id
        operation: item.operation?.map((item) => item.id) ?? [], // 操作id数组
        opChecked: [], // 操作选中id数组
        isLast: (item.children?.length ?? 0) === 0, // 是否最后一级
      }
      if (item.roleChecked != null) {
        data.opChecked.push(...item.roleChecked)
      }
      const opCheckedLength = data.opChecked.length
      data.selfChecked =
        opCheckedLength > 0 && opCheckedLength === data.operation.length //菜单checkbox全选状态
      data.isHalf = opCheckedLength > 0 && !data.selfChecked //菜单checkbox半选状态
      state.list.push(data)
      if (Array.isArray(item.children) && item.children.length > 0) {
        treeToList(item.children, item)
      }
    })
  }

  defineExpose({
    load,
    clear,
    getCheckedOperation,
    getCheckedModule,
    getCheckedAll,
  })
</script>
