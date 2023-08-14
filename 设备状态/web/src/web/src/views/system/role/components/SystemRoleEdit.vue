<template>
  <el-dialog v-model="dialogFormVisible" :title="title" width="500px" @close="close">
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="角色名" prop="role">
        <el-input v-model="form.role" maxlength="10" show-word-limit />
      </el-form-item>
      <el-form-item v-show="false" label="菜单">
        <div class="vab-tree-border">
          <el-tree ref="treeRef" :data="list" :default-checked-keys="form.checkedKeys" :default-expanded-keys="[]"
            node-key="path" show-checkbox>
            <template #default="{ data }">
              <span>{{ data.meta.title }}</span>
            </template>
          </el-tree>
        </div>
      </el-form-item>
      <el-form-item v-show="false" label="按钮权限">
        <el-checkbox-group v-model="form.btnRolesCheckedList">
          <el-checkbox v-for="item in btnRoles" :key="item.value" :label="item.value">
            {{ item.lable }}
          </el-checkbox>
        </el-checkbox-group>
      </el-form-item>
      <el-form-item label="描述">
        <el-input v-model="form.description" type="textarea" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
import { doAdd, doEdit, getAuthList } from '@/api/system/role'
import { getList } from '@/api/system/router'

export default defineComponent({
  name: 'RoleManagementEdit',
  emits: ['fetch-data', 'value'],
  setup(props, { emit }) {
    const $baseMessage = inject('$baseMessage')
    let isAdd = false

    const state = reactive({
      formRef: null,
      treeRef: null,
      form: {
        checkedKeys: [],
        btnRolesCheckedList: [],
      },
      rules: {
        role: [
          {
            required: true,
            pattern: '[^ \x22]+',
            trigger: 'blur',
            message: '请输入角色名',
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
        isAdd = true
      } else {
        isAdd = false
        state.title = '编辑'
        state.form = JSON.parse(JSON.stringify(row))
        const {
          data: { list },
        } = await getAuthList({ roleId: row.id })
        state.form.checkedKeys = list
      }
      state.dialogFormVisible = true
    }
    const close = () => {
      state['formRef'].resetFields()
      state.form = {
        checkedKeys: [],
        btnRolesCheckedList: [],
      }
      state['treeRef'].setCheckedKeys([])
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
          const tree = state['treeRef'].getCheckedKeys()
          const treeObject = { treeArray: tree }
          const submit = isAdd ? doAdd : doEdit
          const { msg } = await submit({
            ...state.form,
            ...treeObject,
          })
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          emit('fetch-data')
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
