<template>
  <el-dialog v-model="dialogFormVisible" :title="title" width="500px" @close="close">
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">

      <el-form-item label="试验间" prop="roomName">
        <el-input v-model="form.roomName" show-word-limit />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
import { getAuthList } from '@/api/system/role'
import { getList } from '@/api/system/router'
import { doEdit, doAdd } from '@/api/DeviceStatus/DeviceBaseRoom'
export default defineComponent({
  name: 'RoleManagementEdit',
  emits: ['fetch-data', 'value', 'dfetch-data'],
  setup(props, { emit }) {
    const $baseMessage = inject('$baseMessage')
    let isAdd = false

    const state = reactive({
      formRef: null,
      treeRef: null,
      disabledvalue: false,
      form: {
        checkedKeys: [],
        btnRolesCheckedList: [],
      },
      rules: {
        ids: [
          {
            required: true,
            pattern: '[^ \x22]+',
            trigger: 'blur',
            message: '请输入车轴型号1',
          },
        ],
        roomName: [
          {
            required: true,
            pattern: '[^ \x22]+',
            trigger: 'blur',
            message: '请输入实验间',
          },
        ],
      },
      title: '',
      dialogFormVisible: false,
      list: [],
      /* btnRoles demo */
      btnRoles: [
        {
          lable: '读',
          value: 'read:system',
        },
        {
          lable: '写',
          value: 'write:system',
        },
        {
          lable: '删',
          value: 'delete:system',
        },
      ],
    })

    const showEdit = async (row) => {
      if (!row) {
        state.title = '添加'
        state.disabledvalue = false
        isAdd = true
      } else {
        isAdd = false
        state.title = '编辑'
        state.disabledvalue = true
        state.form = JSON.parse(JSON.stringify(row))
      }
      state.dialogFormVisible = true
    }
    const close = () => {
      ///  state['formRef'].resetFields()
      state.form = {
        checkedKeys: [],
        btnRolesCheckedList: [],
      }
      // state['treeRef'].setCheckedKeys([])

      state.dialogFormVisible = false
    }
    const fetchData = async () => {
      const {
        data: { list },
      } = await getList()
      state.list = list
    }
    const save = () => {
      state['formRef'].validate(async (valid) => {
        if (valid) {
          const submit = isAdd ? doAdd : doEdit
          const { msg } = await submit(state.form)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          emit('fetch-data')
          emit('dfetch-data')
          close()
        }
      })
    }
    onMounted(async () => {
      await fetchData()
    })
    return {
      ...toRefs(state),
      showEdit,
      close,
      fetchData,
      save,
    }
  },
})
</script>

<style lang="scss" scoped>
.vab-tree-border {
  width: 100%;
  height: 250px;
  padding: $base-padding;
  overflow-y: auto;
  border: 1px solid #dcdfe6;
  border-radius: $base-border-radius;
}
</style>
