<template>
  <el-table
    v-loading="listLoading"
    :data="list"
    style="width: 100%"
    table-layout="auto"
  >
    <el-table-column type="index" />
    <el-table-column label="功能编码">
      <template #default="{ row }">
        <template v-if="row.edit">
          <el-input v-model="row.operationCode" style="width: 100px" />
        </template>
        <span v-else>{{ row.operationCode }}</span>
      </template>
    </el-table-column>
    <el-table-column label="功能标题" prop="title">
      <template #default="{ row }">
        <template v-if="row.edit">
          <el-input v-model="row.title" style="width: 100px" />
        </template>
        <span v-else>{{ row.title }}</span>
      </template>
    </el-table-column>
    <el-table-column>
      <template #default="{ row }">
        <el-button
          v-if="row.edit"
          size="small"
          type="primary"
          @click="confirmEdit(row)"
        >
          保存
        </el-button>
        <el-button v-else link type="primary" @click="row.edit = !row.edit">
          编辑
        </el-button>
        <el-button
          v-if="row.edit == false"
          link
          type="primary"
          @click="HandDelete(row)"
        >
          删除
        </el-button>
        <el-button
          v-if="row.edit"
          size="small"
          style="margin-left: 10px"
          @click="cancelEdit(row)"
        >
          取消
        </el-button>
      </template>
    </el-table-column>
  </el-table>
  <div v-if="AddNewOperation" style="width: 100%; margin-top: 10px">
    <el-input
      v-model="NewIndex"
      disabled="false"
      style="width: 6%; margin-left: 1%"
    />
    <el-input v-model="newOperationCode" style="width: 20%; margin-left: 3%" />
    <el-input v-model="newTitle" style="width: 20%; margin-left: 8%" />
    <el-button
      link
      style="margin-left: 10%"
      type="primary"
      @click="saveNewInfo()"
    >
      保存
    </el-button>
    <el-button
      link
      style="margin-left: 10px"
      type="primary"
      @click="AddNewOperation = false"
    >
      取消
    </el-button>
  </div>
  <el-button
    class="mt-4"
    style="width: 100%; margin-top: 10px"
    @click="onAddItem()"
  >
    添加权限
  </el-button>
</template>
<script>
  import { Edit, Plus } from '@element-plus/icons-vue'
  import {
    getOperation,
    DoOperationEdit,
    DoOperationAdd,
    DoOperationDelete,
  } from '@/api/system/module'

  export default defineComponent({
    name: 'ModuleOperation',
    props: {
      module: {
        type: Number,
        default: () => {
          return 0
        },
      },
    },
    setup(props) {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        NewIndex: '',
        AddNewOperation: false,
        newOperationCode: '',
        newTitle: '',
        list: [],
        listLoading: true,
        moduleId: 0,
      })
      const fetchData = async (module) => {
        state.AddNewOperation = false
        state.newOperationCode = ''
        state.newTitle = ''
        state.listLoading = true
        state.moduleId = module
        const { data } = await getOperation(module)
        state.list = data.map((v) => {
          v.edit = false
          v.operationCodes = v.operationCode
          v.operationTitle = v.title
          v.moduleId = module
          return v
        })
        state.listLoading = false
      }

      const HandDelete = async (row) => {
        $baseConfirm('你确定要删除当前项吗', null, async () => {
          const { msg } = await DoOperationDelete({
            OperationCode: row.operationCode,
          })
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          await fetchData(state.moduleId)
        })
      }
      const cancelEdit = (row) => {
        row.operationCode = row.operationCodes
        row.title = row.operationTitle
        row.edit = false
      }
      const confirmEdit = (row) => {
        if (row.operationCode == '' || row.title == '') {
          $baseMessage('请输入数据', 'error', 'vab-hey-message-error')
          return
        }
        const isTrue = /^\w+:\w+$/.test(row.operationCode)
        if (isTrue == false) {
          $baseMessage(
            '请输入正确的功能编码格式',
            'error',
            'vab-hey-message-error'
          )
          return
        }
        editOperation(row)
      }

      const editOperation = async (data) => {
        const { msg } = await DoOperationEdit(data)
        fetchData(state.moduleId)
        $baseMessage(msg, 'success', 'vab-hey-message-success')
        row.edit = false
        row.operationCodes = row.operationCode
        row.operationTitle = row.title
      }

      const onAddItem = async () => {
        state.NewIndex = state.list.length + 1
        state.AddNewOperation = true
      }

      const saveNewInfo = async () => {
        if (state.newOperationCode == '' || state.newTitle == '') {
          $baseMessage('请输入数据', 'error', 'vab-hey-message-error')
          return
        }
        const isTrue = /^\w+:\w+$/.test(state.newOperationCode)
        if (isTrue == false) {
          $baseMessage(
            '请输入正确的功能编码格式',
            'error',
            'vab-hey-message-error'
          )
          return
        }
        const { msg } = await DoOperationAdd({
          moduleId: state.moduleId,
          operationCode: state.newOperationCode,
          title: state.newTitle,
        })
        $baseMessage(msg, 'success', 'vab-hey-message-success')
        await fetchData(state.moduleId)
      }
      watch(
        () => props.module,
        (newVal) => {
          fetchData(newVal)
        },
        { immediate: true }
      )

      return {
        ...toRefs(state),
        fetchData,
        cancelEdit,
        confirmEdit,
        editOperation,
        onAddItem,
        HandDelete,
        saveNewInfo,
        Plus,
        Edit,
      }
    },
  })
</script>
