<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="980px"
    @close="close"
  >
    <el-form ref="formRef" label-width="120px" :model="form" :rules="rules">
      <el-tabs v-model="activeName" type="card">
        <el-tab-pane label="内容" name="content">
          <el-row>
            <el-col :span="12">
              <el-form-item label="申请主题" prop="title">
                <el-input v-model="form.title" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="申请单号" prop="applicationNo">
                <el-input v-model="form.applicationNo" disabled />
              </el-form-item>
            </el-col>
          </el-row>
          <el-form-item label="理由/用途" prop="reason">
            <el-input v-model="form.reason" disabled type="textarea" />
          </el-form-item>
          <el-form-item label="备注" prop="remark">
            <el-input v-model="form.remark" disabled type="textarea" />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane label="明细" name="detail">
          <el-table v-loading="listLoading" :data="tableData" height="500px">
            <el-table-column
              align="center"
              label="物料名称"
              prop="name"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="物料编码"
              prop="code"
              show-overflow-tooltip
            />
            <el-table-column align="center" label="规格型号" prop="model" />
            <el-table-column
              align="center"
              label="品牌"
              prop="brand"
              show-overflow-tooltip
            />
            <el-table-column align="center" label="数量" prop="count" />
            <el-table-column
              align="center"
              label="单位"
              prop="basicUnit"
              show-overflow-tooltip
            />
          </el-table>
        </el-tab-pane>
      </el-tabs>
    </el-form>
    <template #footer>
      <el-button @click="close">驳 回</el-button>
      <el-button type="primary" @click="save">同 意</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { PurchaseDetailList, OrderSH } from '@/api/purchase/purchase'
  import { ref } from 'vue'

  export default defineComponent({
    name: 'SupplierEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      const activeName = ref('content')
      const state = reactive({
        tableData: [],
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          title: [{ required: true, trigger: 'blur', message: '请输入主题' }],
          reason: [
            { required: true, trigger: 'blur', message: '请输入理由/用途' },
          ],
          applicationNo: [
            { required: true, trigger: 'blur', message: '请输入申请单号' },
          ],
        },
        title: '',
        dialogFormVisible: false,
        listLoading: true,
      })

      const showEdit = async (row) => {
        if (!row) {
          state.title = '添加'
        } else {
          state.title = '审核'
          state.form = Object.assign({}, row)
          if (row.applicationNo != '' && row.applicationNo != null) {
            state.listLoading = true
            const data = await PurchaseDetailList({
              ApplicationNo: row.applicationNo,
            })
            if (data.data.length > 0) {
              state.tableData = data.data
            }
            state.listLoading = false
          }
        }
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            // const { msg } = await SupplierEdit(state.form)
            const { msg } = await OrderSH(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      // const handleClick = async (TabsPaneContext) => {
      //   if (TabsPaneContext.props.name == 'detail') {
      //     console.log(TabsPaneContext)
      //   }
      // }
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        //handleClick,
        activeName,
      }
    },
  })
</script>
