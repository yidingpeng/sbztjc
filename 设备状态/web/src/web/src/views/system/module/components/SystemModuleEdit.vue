<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="上级菜单">
        <el-cascader
          clearable
          :model-value="form.parentId"
          :options="optParent"
          :props="propParent"
          @change="handleParent"
        />
      </el-form-item>
      <el-form-item label="排序号" prop="orderIndex">
        <el-input-number v-model="form.orderIndex" />
      </el-form-item>
      <el-form-item label="标题" prop="title">
        <el-input v-model="form.title" />
      </el-form-item>
      <el-form-item label="模块名称" prop="moduleName">
        <el-input v-model="form.moduleName" />
      </el-form-item>
      <el-form-item label="图标">
        <el-popover
          ref="popoverRef"
          placement="bottom-start"
          popper-class="icon-selector-popper"
          trigger="click"
          :width="300"
        >
          <template #reference>
            <el-input v-model="form.icon" readonly>
              <template v-if="form.icon" #prefix>
                <vab-icon :icon="form.icon" />
              </template>
            </el-input>
          </template>
          <vab-icon-selector @handle-icon="handleIcon" />
        </el-popover>
      </el-form-item>
      <el-form-item label="路径" prop="path">
        <el-input v-model="form.path" />
      </el-form-item>
      <el-form-item label="组件" prop="component">
        <el-input v-model="form.component" />
      </el-form-item>
      <el-form-item label="状态" prop="hidden">
        <el-radio-group v-model="form.hidden">
          <el-radio :label="false">显示</el-radio>
          <el-radio :label="true">隐藏</el-radio>
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
  import VabIconSelector from '@/plugins/VabIconSelector'
  import { doAdd, doEdit } from '@/api/system/module'

  export default defineComponent({
    name: 'SystemModuleEdit',
    components: { VabIconSelector },
    props: {
      optParent: {
        type: Array,
        default: () => {
          return []
        },
      },
    },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const propParent = { value: 'id', label: 'title', checkStrictly: true }

      const state = reactive({
        formRef: null,
        popoverRef: null,
        form: { hidden: false },
        rules: {
          title: [{ required: true, trigger: 'blur', message: '请输入标题' }],
          moduleName: [
            { required: true, trigger: 'blur', message: '请输入名称' },
          ],
          path: [{ required: true, trigger: 'blur', message: '请输入路径' }],
          component: [
            { required: true, trigger: 'blur', message: '请输入组件' },
          ],
        },
        title: '',
        dialogFormVisible: false,
        loading: true,
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
        state.form = {}
        state.dialogFormVisible = false
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            const { msg } = state.form.id
              ? await doEdit(state.form)
              : await doAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      const handleIcon = (item) => {
        state.form.icon = item
        state['popoverRef'].hide()
      }

      const handleParent = (value) => {
        state.form.parentId = value ? value[value.length - 1] : 0
      }

      return {
        ...toRefs(state),
        propParent,
        showEdit,
        close,
        save,
        handleIcon,
        handleParent,
      }
    },
  })
</script>
