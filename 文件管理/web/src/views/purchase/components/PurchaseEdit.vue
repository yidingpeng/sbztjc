<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="800px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-divider content-position="left">BOM清单信息</el-divider>
      <el-row>
        <el-col :span="8">
          <el-form-item label="BOM编码" prop="bomCode">
            <el-input v-model="form.bomCode" />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="物料编码" prop="materialCode">
            <el-input v-model="form.materialCode" />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="物料名称" prop="materialName">
            <el-input v-model="form.materialName" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-divider content-position="left">项目经理填写</el-divider>
      <el-row>
        <el-col :span="12">
          <el-form-item label="要求到货时间" prop="yqArrivalTime">
            <el-input v-model="form.yqArrivalTime" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-divider content-position="left">采购主管分配</el-divider>
      <el-row>
        <el-col :span="12">
          <el-form-item label="分配人员" prop="assignStaff">
            <el-input v-model="form.assignStaff" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-divider content-position="left">采购人员填写</el-divider>
      <el-row>
        <el-col :span="12">
          <el-form-item label="预计到货时间" prop="yjArrivalTime">
            <el-input v-model="form.yjArrivalTime" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="实际到货时间" prop="sjArrivalTime">
            <el-input v-model="form.sjArrivalTime" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-divider content-position="left">供应商选择</el-divider>
      <el-row>
        <el-col :span="12">
          <el-form-item label="供应商" prop="supplier">
            <el-input v-model="form.supplier" />
          </el-form-item>
        </el-col>
      </el-row>

      <el-form-item label="成本价格" prop="costPrice">
        <el-input v-model="form.costPrice" />
      </el-form-item>
      <el-form-item label="图纸代号" prop="drawingCode">
        <el-input v-model="form.drawingCode" />
      </el-form-item>
      <el-form-item label="预计完成时间" prop="yjFinishTime">
        <el-input v-model="form.yjFinishTime" />
      </el-form-item>
      <el-form-item label="实际完成时间" prop="sjFinishTime">
        <el-input v-model="form.sjFinishTime" />
      </el-form-item>
      <el-form-item label="追溯码" prop="traceabilityCode">
        <el-input v-model="form.traceabilityCode" />
      </el-form-item>
      <el-form-item label="预计到货时间" prop="gysArrivalTime">
        <el-input v-model="form.gysArrivalTime" />
      </el-form-item>
      <el-form-item label="状态" prop="state">
        <el-input v-model="form.state" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { doEdit } from '@/api/purchase/purchase'

  export default defineComponent({
    name: 'PurchaseEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          title: [{ required: true, trigger: 'blur', message: '请输入标题' }],
        },
        title: '',
        dialogFormVisible: false,
      })

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
        } else {
          state.title = '编辑'
          state.form = Object.assign({}, row)
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
            const { msg } = await doEdit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
      }
    },
  })
</script>
