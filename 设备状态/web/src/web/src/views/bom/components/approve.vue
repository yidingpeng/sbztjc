<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="项目">
        <span>{{ data.projectCode }} {{ data.projectName }}</span>
      </el-form-item>
      <el-form-item label="BOM编码">
        <span>{{ data.bomCode }}</span>
      </el-form-item>
      <el-form-item label="版本号">
        <span>{{ data.version }}</span>
      </el-form-item>
      <el-form-item label="状态">
        <span>{{ data.statusText }}</span>
      </el-form-item>
      <el-form-item label="审核内容" prop="comment">
        <el-input
          v-model="form.comment"
          maxlength="2000"
          rows="5"
          type="textarea"
        />
      </el-form-item>
      <el-form-item label="审核" prop="">
        <el-radio-group v-model="form.result">
          <el-radio :label="true">通过</el-radio>
          <el-radio :label="false">驳回</el-radio>
        </el-radio-group>
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { approveBOM } from '@/api/bom'

  export default defineComponent({
    name: 'BOMApprove',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        title: '审核',
        dialogFormVisible: false,
        data: {},
        form: {
          bomId: 0,
          comment: '',
          result: null,
          node: '',
        },
        rules: {
          title: [{ required: true, trigger: 'blur', message: '请输入标题' }],
        },
      })

      const showApprove = (data) => {
        state.data = data
        state.dialogFormVisible = true
        state.form.bomId = data.id
        state.form.node = data.status
      }

      const save = async () => {
        const { code, msg } = await approveBOM(state.form)
        $baseMessage(msg, 'success', 'vab-hey-message-success')
        if (code == 0) {
          state.dialogFormVisible = false
          emit('fetch-data')
        }
      }
      const close = () => {
        state.dialogFormVisible = false
      }

      return { ...toRefs(state), showApprove, save, close }
    },
  })
</script>
