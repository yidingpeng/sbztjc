<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="类型" prop="type">
        <el-input v-model="form.type" />
        <!-- <el-select
          v-model="form.type"
          allow-create13
          placeholder="选择或输入类型"
        >
          <el-option
            v-for="item in typelist"
            :key="item"
            :label="item"
            :value="item"
          />
        </el-select> -->
      </el-form-item>
      <el-form-item label="参数标识" prop="code">
        <el-input v-model="form.code" />
      </el-form-item>
      <el-form-item label="参数值" prop="value">
        <el-input v-model="form.value" />
      </el-form-item>
      <el-form-item label="描述" prop="desc">
        <el-input v-model="form.desc" rows="3" type="textarea" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { doAdd, doEdit } from '@/api/system/configuration'

  export default defineComponent({
    name: 'SystemConfigurationEdit',
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
          type: [
            { required: true, trigger: 'blur', message: '请输入或选择类型' },
          ],
          code: [
            { required: true, trigger: 'blur', message: '请输入参数编码' },
            {
              min: 3,
              trigger: 'blur',
              message: '编码长度不能小于3个字符',
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
