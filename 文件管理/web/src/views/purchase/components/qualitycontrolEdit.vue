<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="150px" :model="form" :rules="rules">
      <el-form-item label="项目名称" prop="projectName">
        {{ form.projectName }}
      </el-form-item>
      <el-form-item label="项目编码" prop="projectCode">
        {{ form.projectCode }}
      </el-form-item>
      <el-form-item label="物料编码" prop="materialCode">
        {{ form.materialCode }}
      </el-form-item>
      <el-form-item label="物料名称" prop="materialName">
        {{ form.materialName }}
      </el-form-item>
      <el-form-item label="供应商" prop="supplier">
        {{ form.supplier }}
      </el-form-item>
      <el-form-item label="合格数量" prop="qCount">
        <el-input-number v-model="form.qCount" :min="1" />
      </el-form-item>
      <el-form-item label="是否合格" prop="qualified">
        <el-radio-group v-model="form.qualified">
          <el-radio :label="1" size="large">合格</el-radio>
          <el-radio :label="2" size="large">不合格</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input v-model="form.remark" type="textarea" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { QCEdit } from '@/api/purchase/purchase'

  export default defineComponent({
    name: 'QualitycontrolEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          projectName: [
            { required: true, trigger: 'blur', message: '项目名称为空!' },
          ],
          projectCode: [
            { required: true, trigger: 'blur', message: '项目编码为空!' },
          ],
          materialCode: [
            { required: true, trigger: 'blur', message: '物料编码为空!' },
          ],
          materialName: [
            { required: true, trigger: 'blur', message: '物料名称为空!' },
          ],
          supplier: [
            { required: true, trigger: 'blur', message: '供应商为空!' },
          ],
          qCount: [
            {
              required: true,
              message: '请输入数量',
              trigger: 'blur',
            },
            {
              validator: function (rule, value, callback) {
                if (value) {
                  callback()
                } else {
                  let tip = ''
                  if (value * 1 === 0) {
                    tip = '数量应大于0'
                  }
                  if (tip) {
                    callback(new Error(tip))
                  } else {
                    callback()
                  }
                }
              },
              trigger: 'blur',
            },
          ],
          qualified: [
            { required: true, trigger: 'change', message: '请选择是否合格!' },
          ],
        },
        title: '',
        dialogFormVisible: false,
      })
      const showEdit = (row) => {
        state.title = '质检信息编辑'
        state.form = Object.assign({}, row)
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
            const { msg } = await QCEdit(state.form)
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
