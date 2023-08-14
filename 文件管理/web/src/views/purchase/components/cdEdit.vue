<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    title="报检"
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
      <el-form-item label="数量" prop="count">
        <el-input v-model="form.count" disabled />
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
  import { DoCDAdd } from '@/api/purchase/purchase'

  export default defineComponent({
    name: 'CDEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      //const $baseMessage = inject('$baseMessage')

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
            { required: true, trigger: 'blur', message: '请确认报检数量' },
          ],
        },
        dialogFormVisible: false,
      })

      const showEdit = (row) => {
        state.form = Object.assign({}, row)
        state.form.id = 0
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
              remark: state.form.remark,
            }
            const { msg } = await DoCDAdd(qrData)
            ElMessage({
              type: msg.includes('成功') ? 'success' : 'error',
              message: msg,
            })
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
