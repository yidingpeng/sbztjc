<template>
  <el-drawer
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    direction="rtl"
    size="50%"
    title="项目动态列表"
  >
    <el-table :data="ProDynamicList" style="width: 100%">
      <el-table-column
        align="center"
        label="类型"
        prop="dyTypeName"
        width="100"
      />
      <el-table-column
        align="center"
        label="名称"
        prop="projectName"
        width="150"
      />
      <el-table-column
        align="center"
        label="操作内容"
        prop="operationContent"
        width="410"
      />
      <el-table-column
        align="center"
        label="操作人"
        prop="createdByName"
        width="100"
      />
      <el-table-column
        align="center"
        label="操作时间"
        prop="createdAt"
        width="170"
      />
    </el-table>
  </el-drawer>
</template>

<script>
  import { defineComponent, reactive, toRefs } from 'vue'
  import { DTgetList } from '@/api/project'

  export default defineComponent({
    name: 'ProDynamicList',
    emits: ['fetch-data'],
    setup() {
      const state = reactive({
        formRef: null,
        ProDynamicList: [],
        form: {
          roles: [],
        },
        rules: {},
        dialogFormVisible: false,
      })

      const showEdit = (row) => {
        state.form = Object.assign({}, row)
        state.dialogFormVisible = true
        ProDynamicDetail(row.id)
      }
      const close = () => {
        state.dialogFormVisible = false
      }
      //项目动态
      const ProDynamicDetail = async (row) => {
        const List = await DTgetList({ Id: row })
        state.ProDynamicList = List.data
      }
      return {
        ...toRefs(state),
        showEdit,
        close,
      }
    },
  })
</script>
