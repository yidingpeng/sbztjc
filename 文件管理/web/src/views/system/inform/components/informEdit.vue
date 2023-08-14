<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="字典类型" prop="dictionaryText">
        <el-input v-model.trim="form.dictionaryText" />
      </el-form-item>
      <el-form-item label="备注" prop="description">
        <el-input v-model.trim="form.description" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { doAdd, doEdit } from '@/api/system/inform'

  export default defineComponent({
    name: 'InformEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        isAdd: true,
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          dictionaryText: [
            { required: true, trigger: 'blur', message: '请输入字典名称' },
          ],
          description: [],
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
          state.form = JSON.parse(JSON.stringify(row))
          state.isAdd = false
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
