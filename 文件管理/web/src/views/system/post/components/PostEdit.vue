<template>
  <el-dialog
    v-model="dialogFormVisible"
    :before-close="handleClose"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="所属部门" prop="orgId">
        <rw-organization
          v-model="form.orgId"
          placeholder="不输入表示顶级岗位"
          @change="changed = true"
        />
      </el-form-item>
      <el-form-item label="排序号" prop="orderIndex">
        <el-input
          v-model="form.orderIndex"
          type="number"
          @change="changed = true"
        />
      </el-form-item>
      <el-form-item label="岗位名称" prop="postName">
        <el-input v-model="form.postName" @change="changed = true" />
      </el-form-item>
      <el-form-item label="岗位描述" prop="desc">
        <el-input
          v-model="form.desc"
          rows="3"
          type="textarea"
          @change="changed = true"
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
  import { doEdit, doAdd } from '@/api/system/post'
  import RwOrganization from '@/plugins/RwOrganization'

  export default defineComponent({
    name: 'SystemOrganizationEdit',
    components: { RwOrganization },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      const $confirm = inject('$baseConfirm')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        list: [],
        isAdd: false,
        changed: false,
        rules: {
          postName: [
            { required: true, trigger: 'blur', message: '请输入岗位名称' },
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
        // handleClose(() => {
        //   state.dialogFormVisible = false
        // })
        state.dialogFormVisible = false
        state['formRef'].resetFields()
        state.form = {
          orderIndex: 0,
          roles: [],
        }
        //state.dialogFormVisible = false
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            if (!state.form.parentId) state.form.parentId = 0
            if (state.form.leader == '') state.form.leader = null
            const { msg } = state.isAdd
              ? await doAdd(state.form)
              : await doEdit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      const handleClose = function (done) {
        if (state.changed)
          $confirm('有未提交的内容，确定要关闭吗？', null, () => {
            done()
          })
        else done()
      }
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        handleClose,
      }
    },
  })
</script>
