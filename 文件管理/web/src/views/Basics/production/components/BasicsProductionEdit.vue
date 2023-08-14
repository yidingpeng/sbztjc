<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="产品名称" prop="pname">
        <el-input v-model="form.pname" />
      </el-form-item>
      <el-form-item label="产品编码" prop="pCode">
        <el-input v-model="form.pCode" />
      </el-form-item>
      <el-form-item label="规格" prop="size">
        <el-input v-model="form.size" />
      </el-form-item>
      <el-form-item label="产品种类" prop="category">
        <el-input v-model="form.category" />
      </el-form-item>
      <el-form-item label="状态" prop="pstatus">
        <el-select
          v-model="form.pstatus"
          allow-create13
          placeholder="选择产品状态"
        >
          <el-option key="0" label="启用" :value="0" />
          <el-option key="1" label="禁用" :value="1" />
        </el-select>
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input v-model="form.remark" rows="3" type="textarea" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { doAdd, doEdit, GetRepeat } from '@/api/basics/production'

  export default defineComponent({
    name: 'BasicsProductionEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        isAdd: false,
        form: {
          roles: [],
        },
        rules: {
          pname: [
            { required: true, trigger: 'blur', message: '请输入产品名称' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          pCode: [
            {
              required: true,
              trigger: 'blur',
              validator: async (rule, value, callback) => {
                if (value) {
                  const model = await GetRepeat(state.form)
                  if (model.data) {
                    callback(new Error('已存在相同产品编码'))
                  } else {
                    callback()
                  }
                } else {
                  callback(new Error('请输入产品编码'))
                }
              },
            },
            {
              min: 3,
              trigger: 'blur',
              message: '编码长度不能小于3个字符',
            },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          size: [
            { trigger: 'blur', message: '长度不能超过150个字符' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          category: [
            { trigger: 'blur', message: '长度不能超过150个字符' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          pstatus: [
            { required: true, trigger: 'blur', message: '请选择产品状态' },
          ],
          remark: [
            { trigger: 'blur', message: '长度不能超过255个字符' },
            {
              max: 255,
              trigger: 'blur',
              message: '长度不能超过255个字符',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
      })

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          state.isAdd = true
        } else {
          state.title = '编辑'
          state.isAdd = false
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
            const { msg } = state.isAdd
              ? await doAdd(state.form)
              : await doEdit(state.form)
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
