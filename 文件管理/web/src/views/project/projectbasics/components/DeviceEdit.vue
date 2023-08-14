<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="类型" prop="dyType">
        <el-input
          v-show="false"
          v-model="form.dyType"
          disabled="true"
          link
          type="primary"
        />
        <el-input
          v-model="form.dyTypeName"
          disabled="true"
          link
          type="primary"
        />
      </el-form-item>
      <el-form-item label="所属项目" prop="projectName">
        <el-input
          v-show="false"
          v-model="form.projectID"
          disabled="true"
          link
          type="primary"
        />
        <el-input
          v-model="form.projectName"
          disabled="true"
          link
          type="primary"
        />
      </el-form-item>
      <el-form-item label="操作内容" prop="operationContent">
        <el-input
          v-model="form.operationContent"
          :autosize="{ minRows: 4, maxRows: 4 }"
          :maxlength="800"
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
  import { DTdoAdd, DTdoEdit } from '@/api/project'

  export default defineComponent({
    name: 'DyEdit',
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
          dyType: [
            { required: true, trigger: 'blur', message: '请项目动态类型' },
          ],
          projectName: [
            { required: true, trigger: 'blur', message: '请项目动态类型' },
          ],
          operationContent: [
            { required: true, trigger: 'blur', message: '请输入操作内容' },
          ],
        },
        dialogFormVisible: false,
      })

      const showEdit = (row) => {
        state.form.dyType = 219
        state.form.dyTypeName = '项目'
        state.form.projectID = row.id
        state.form.projectName = `${row.projectCode}-${row.projectName}`
        state.title = '添加项目动态'
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
            const { msg } = await DTdoAdd(state.form)
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
        DTdoAdd,
        DTdoEdit,
      }
    },
  })
</script>
