<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :close-on-press-escape="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="模板名称" prop="name">
        <el-input v-model="form.name" />
      </el-form-item>
      <el-form-item label="项目类型" prop="projectType">
        <el-select
          v-model="form.projectType"
          filterable
          placeholder="请选择项目类型"
          style="width: 100%"
        >
          <el-option
            v-for="(item, index) in ProjectClassOption"
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
  import { MBdoAdd, MBdoEdit } from '@/api/task'
  import { GetProjectClass } from '@/api/project'

  export default defineComponent({
    name: 'TemplateEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          name: [
            {
              required: true,
              pattern: '[^ \x22]+',
              trigger: 'blur',
              message: '请输入模板名称',
            },
          ],
          projectType: [
            {
              required: true,
              trigger: 'change',
              message: '请选择项目类型',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
        ProjectClassOption: [],
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
            const { msg } = state.form.id
              ? await MBdoEdit(state.form)
              : await MBdoAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      //项目分类
      const GetProjectClassData = async () => {
        const ProjectClassList = await GetProjectClass()
        state.ProjectClassOption = ProjectClassList.data
      }
      onMounted(() => {
        GetProjectClassData()
      })

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        GetProjectClassData,
      }
    },
  })
</script>
