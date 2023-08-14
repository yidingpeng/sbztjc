<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="110px" :model="form" :rules="rules">
      <el-form-item label="物料属性" prop="matProperty">
        <el-select
          v-model="form.matProperty"
          filterable
          placeholder="请选择物料属性"
        >
          <el-option label="电气" value="电气" />
          <el-option label="机械" value="机械" />
          <el-option label="培训" value="培训" />
          <el-option label="运营" value="运营" />
          <el-option label="电气、机械" value="电气、机械" />
        </el-select>
      </el-form-item>
      <el-form-item label="供应商编码" prop="supCode">
        <el-input v-model.trim="form.supCode" />
      </el-form-item>
      <el-form-item label="供应商名称" prop="supName">
        <el-input v-model.trim="form.supName" />
      </el-form-item>
      <el-form-item label="主要供货品牌" prop="supplyBrand">
        <el-input v-model.trim="form.supplyBrand" />
      </el-form-item>
      <el-form-item label="主要供货类别" prop="supplyType">
        <el-input v-model.trim="form.supplyType" />
      </el-form-item>
      <el-form-item label="公司性质" prop="companyType">
        <el-select
          v-model="form.companyType"
          filterable
          placeholder="请选择公司性质"
        >
          <el-option label="厂家" value="厂家" />
          <el-option label="平台" value="平台" />
          <el-option label="代理商" value="代理商" />
          <el-option label="经销商" value="经销商" />
          <el-option label="办事处" value="办事处" />
          <el-option label="集成商" value="集成商" />
          <el-option label="代理商、厂家" value="代理商、厂家" />
          <el-option label="厂家（湖南区）" value="厂家（湖南区）" />
          <el-option label="厂家（华南区）" value="厂家（华南区）" />
          <el-option label="厂家（华北区）" value="厂家（华北区）" />
        </el-select>
      </el-form-item>
      <el-form-item label="联系人" prop="supPrincipal">
        <el-input v-model.trim="form.supPrincipal" />
      </el-form-item>
      <el-form-item label="联系方式" prop="contactDetails">
        <el-input v-model.trim="form.contactDetails" />
      </el-form-item>
      <el-form-item label="地址" prop="address">
        <el-input v-model.trim="form.address" type="textarea" />
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input v-model.trim="form.remark" />
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
          matProperty: [
            { required: true, trigger: 'change', message: '请选择物料属性' },
          ],
          companyType: [
            { required: true, trigger: 'change', message: '请选择公司性质' },
          ],
          supCode: [{ required: true, trigger: 'blur', message: '请输入编码' }],
          supName: [{ required: true, trigger: 'blur', message: '请输入名称' }],
          supplyBrand: [
            { required: true, trigger: 'blur', message: '请输入主要供货品牌' },
          ],
          supplyType: [
            { required: true, trigger: 'blur', message: '请输入主要供货类别' },
          ],
          supPrincipal: [
            { required: true, trigger: 'blur', message: '请输入联系人' },
          ],
          contactDetails: [
            { required: true, trigger: 'blur', message: '请输入联系方式' },
          ],
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
