<template>
  <el-table
    v-loading="listLoading"
    :data="list"
    default-expand-all
    row-key="id"
    style="width: 100%"
  >
    <el-table-column label="导航菜单" prop="title" show-overflow-tooltip>
      <template #default="{ row }">
        {{ row.title }}
      </template>
    </el-table-column>
    <el-table-column label="菜单操作" prop="optionInfo" width="380">
      <template #default="{ row }">
        <el-checkbox-group v-model="row.opChecked">
          <el-checkbox
            v-for="(item, key) in row.operationList"
            :key="key"
            :disabled="IsDisabled(item.operationCode)"
            :label="item.id"
          >
            {{ item.title }}
          </el-checkbox>
        </el-checkbox-group>
      </template>
    </el-table-column>
  </el-table>
  <el-button
    class="mt-4"
    style="width: 100%; margin-top: 10px"
    type="primary"
    @click="SaveOperations()"
  >
    保存
  </el-button>
</template>
<script>
  import { Edit, Plus } from '@element-plus/icons-vue'
  import {
    getModelList,
    GetUserOperation,
    EditUserOperation,
  } from '@/api/system/user'

  export default defineComponent({
    name: 'UserOperation',
    props: {
      user: {
        type: Number,
        default: () => {
          return 0
        },
      },
    },
    setup(props) {
      //const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const checkList = ref([])
      const state = reactive({
        checkListFirLength: 0,
        list: [],
        listLoading: true,
        userId: 0,
      })

      //将该用户所属角色的权限选项禁用
      const IsDisabled = (operationCode) => {
        console.log(operationCode)
        let isC = false
        state.list[0].isChecked.forEach((item) => {
          if (item == operationCode) {
            isC = true
          }
        })
        return isC
      }
      const fetchData = async (user) => {
        checkList.value = []
        state.listLoading = true
        state.userId = user
        const { data } = await getModelList({ userId: user })
        state.list = data

        state.checkListFirLength = checkList.value.length
        const userOperation = await GetUserOperation({ userId: user })
        userOperation.data.forEach((item) => {
          checkList.value.push(item)
        })
        state.listLoading = false
      }
      const SaveOperations = async () => {
        const newCheckList = checkList.value
        checkList.value = checkList.value.splice(0, state.checkListFirLength)
        console.log(newCheckList)
        const { msg } = await EditUserOperation({
          UserId: state.userId,
          OperationCode: newCheckList,
        })
        $baseMessage(msg, 'success', 'vab-hey-message-success')
        fetchData(state.userId)
      }

      watch(
        () => props.user,
        (newVal) => {
          fetchData(newVal)
        },
        { immediate: true }
      )

      return {
        ...toRefs(state),
        fetchData,
        SaveOperations,
        IsDisabled,
        checkList,
        Plus,
        Edit,
      }
    },
  })
</script>
