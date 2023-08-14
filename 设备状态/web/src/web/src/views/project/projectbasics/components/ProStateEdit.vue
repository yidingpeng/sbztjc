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
      <el-form-item label="项目状态" prop="projectStatus">
        <el-select
          v-model="form.projectStatus"
          filterable
          placeholder="请选择项目状态"
        >
          <el-option
            v-for="(item, index) in ProjectStatusOption"
            :key="index"
            :disabled="item.disabled"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="阶段" prop="proState">
        <el-select v-model="form.proState" filterable placeholder="请选择阶段">
          <el-option
            v-for="(item, index) in ProStateOption"
            :key="index"
            :disabled="item.disabled"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
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
  import { ProjectStatus, UpdateProState, GetProState } from '@/api/project'

  export default defineComponent({
    name: 'ProStateEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        ProjectStatusOption: [],
        ProStateOption: [],
        formRef: null,
        title: '',
        form: {
          roles: [],
        },
        rules: {
          projectStatus: [
            { required: true, trigger: 'change', message: '请选择状态类型' },
          ],
          proState: [
            { required: true, trigger: 'change', message: '请选择阶段' },
          ],
        },
        dialogFormVisible: false,
      })

      const showEdit = (row) => {
        state.title = '更新状态、阶段'
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
            const { msg } = await UpdateProState({
              Id: state.form.id,
              ProjectStatus: state.form.projectStatus,
              proState: state.form.proState,
            })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      //项目状态
      const GetProjectStatusOption = async () => {
        const ProjectClassList = await ProjectStatus()
        state.ProjectStatusOption = ProjectClassList.data
      }
      //状态
      const GetProStateData = async () => {
        const ProStateList = await GetProState()
        state.ProStateOption = ProStateList.data
      }
      onMounted(() => {
        GetProjectStatusOption()
        GetProStateData()
      })
      return {
        ...toRefs(state),
        showEdit,
        UpdateProState,
        close,
        save,
        GetProjectStatusOption,
        GetProStateData,
      }
    },
  })
</script>
