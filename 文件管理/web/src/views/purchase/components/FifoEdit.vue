<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="供应商编码" prop="supCode">
        <el-input v-model="form.supCode" />
      </el-form-item>
      <el-form-item label="供应商名称" prop="supName">
        <el-input v-model="form.supName" />
      </el-form-item>
      <el-form-item label="负责人" prop="supPrincipal">
        <el-input v-model="form.supPrincipal" />
      </el-form-item>
      <el-form-item label="联系方式" prop="contactDetails">
        <el-input v-model="form.contactDetails" />
      </el-form-item>
      <el-form-item label="地址" prop="address">
        <el-input v-model="form.address" />
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input v-model="form.remark" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { SupplierEdit, SupplierAdd } from '@/api/purchase/purchase'

  export default defineComponent({
    name: 'SupplierEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          supCode: [{ required: true, trigger: 'blur', message: '请输入编码' }],
          supName: [{ required: true, trigger: 'blur', message: '请输入名称' }],
          supPrincipal: [
            { required: true, trigger: 'blur', message: '请输入负责人' },
          ],
          contactDetails: [
            { required: true, trigger: 'blur', message: '请输入联系方式' },
          ],
          address: [{ required: true, trigger: 'blur', message: '请输入地址' }],
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
            // const { msg } = await SupplierEdit(state.form)
            const { msg } = state.form.id
              ? await SupplierEdit(state.form)
              : await SupplierAdd(state.form)
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
