<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-input v-show="false" v-model="form.id" link type="primary" />
      <el-form-item label="备注" prop="remark">
        <el-input
          v-model="form.remark"
          :autosize="{ minRows: 4, maxRows: 4 }"
          :maxlength="500"
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
  import { defineComponent, reactive, toRefs } from 'vue'
  import { UpdateProRemark } from '@/api/project'

  export default defineComponent({
    name: 'ProRemarkEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        formRef: null,
        title: '',
        form: {
          roles: [],
        },
        rules: {
          remark: [
            { required: true, trigger: 'blur', message: '请输入备注信息' },
          ],
        },
        dialogFormVisible: false,
      })

      const showEdit = (row) => {
        state.title = '更新备注信息'
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
            //console.log(state.form)
            const { msg } = await UpdateProRemark({
              Id: state.form.id,
              Remark: state.form.remark,
            })
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
        UpdateProRemark,
      }
    },
  })
</script>
