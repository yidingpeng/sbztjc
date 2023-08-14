<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="项目编码" prop="projectCode">
        <rw-project
          v-model="form.projectCode"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
        />
      </el-form-item>
      <el-form-item label="当前版本号" prop="currentVersion">
        <el-input v-model="form.currentVersion" />
      </el-form-item>
      <el-form-item label="BOM编码" prop="bomCode">
        <el-input v-model="form.bomCode" />
      </el-form-item>
      <el-form-item label="申请人" prop="applicant">
        <el-input v-model="form.applicant" />
      </el-form-item>
      <el-form-item label="申请日期" prop="applicationDate">
        <el-input v-model="form.applicationDate" />
      </el-form-item>
      <el-form-item label="图纸代号" prop="drawingCode">
        <el-input v-model="form.drawingCode" />
      </el-form-item>
      <el-form-item label="BomId" prop="bomId">
        <el-input v-model="form.bomId" />
      </el-form-item>
      <el-form-item label="状态" prop="status">
        <el-input v-model="form.status" />
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
  import { IMdoEdit, IMdoAdd } from '@/api/purchase/purchase'
  import RwProject from '@/plugins/RwProjectCode'

  export default defineComponent({
    name: 'IndentEdit',
    components: { RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
          qualified: 0,
        },
        rules: {
          currentVersion: [
            { required: true, trigger: 'blur', message: '请输入版本号' },
          ],
          bomCode: [
            { required: true, trigger: 'blur', message: '请输入BOM编码' },
          ],
          applicant: [
            { required: true, trigger: 'blur', message: '请输入申请人' },
          ],
          applicationDate: [
            { required: true, trigger: 'blur', message: '请选择申请日期' },
          ],
          projectCode: [
            { required: true, trigger: 'blur', message: '请选择项目编码' },
          ],
          drawingCode: [
            { required: true, trigger: 'blur', message: '请输入图纸代号' },
          ],
          bomId: [{ required: true, trigger: 'blur', message: '请输入BOMID' }],
          status: [{ required: true, trigger: 'blur', message: '请输入状态' }],
        },
        title: '',
        dialogFormVisible: false,
        materialCodeOptions: null,
        bomCodeOptions: null,
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
            if (state.form.projectCode.length != undefined) {
              state.form.projectCode =
                state.form.projectCode[state.form.projectCode.length - 1]
            }
            console.log(state.form)
            const { msg } = state.form.id
              ? await IMdoEdit(state.form)
              : await IMdoAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      onMounted(() => {})

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
      }
    },
  })
</script>
