<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    title="质检"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="项目编码" prop="projectCode">
        <el-input v-model="form.projectCode" disabled />
      </el-form-item>
      <el-form-item label="项目名称" prop="projectName">
        <el-input v-model="form.projectName" disabled />
      </el-form-item>
      <el-form-item label="物料编码" prop="materialCode">
        <el-input v-model="form.materialCode" disabled />
      </el-form-item>
      <el-form-item label="物料名称" prop="materialName">
        <el-input v-model="form.materialName" disabled />
      </el-form-item>
      <el-form-item label="质检数量" prop="count">
        <el-input v-model="form.count" disabled />
      </el-form-item>
      <el-form-item label="合格数量" prop="qcount">
        <el-input-number v-model="form.qcount" :max="999" :min="0" :step="1" />
      </el-form-item>
      <el-form-item label="是否合格" prop="qualified">
        <el-radio-group v-model="form.qualified">
          <el-radio :label="1">是</el-radio>
          <el-radio :label="2">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input
          v-model="form.remark"
          :autosize="{ minRows: 4, maxRows: 4 }"
          :maxlength="500"
          placeholder="请输入备注"
          show-word-limit
          type="textarea"
        />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { DoQCAdd } from '@/api/purchase/purchase'

  export default defineComponent({
    name: 'QCEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          materialCode: [
            { required: true, trigger: 'blur', message: '物料编码为空' },
          ],
          materialName: [
            { required: true, trigger: 'blur', message: '物料名称为空' },
          ],
          projectCode: [
            { required: true, trigger: 'blur', message: '项目号为空' },
          ],
          projectName: [
            { required: true, trigger: 'blur', message: '项目名称为空' },
          ],
          qcount: [
            { required: true, trigger: 'blur', message: '请确认合格数量' },
          ],
          qualified: [
            { required: true, trigger: 'blur', message: '请选择是否合格' },
          ],
        },
        dialogFormVisible: false,
      })

      const showEdit = (row) => {
        state.form = Object.assign({}, row)
        state.form.id = 0
        state.form.qualified = ''
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
            //质检信息
            const qrData = {
              qrCode: state.form.qrCode,
              count: state.form.count,
              qCount: state.form.qcount,
              remark: state.form.remark,
              qualified: state.form.qualified,
            }
            const { msg } = await DoQCAdd(qrData)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      onMounted(() => {
        //GetbomCode()
      })

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
      }
    },
  })
</script>
