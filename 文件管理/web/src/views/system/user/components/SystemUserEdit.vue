<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="用户名" prop="account">
        <el-input v-model.trim="form.account" />
      </el-form-item>
      <el-form-item v-if="isAdd" label="输入密码" prop="password">
        <el-input
          v-model.trim="form.password"
          auto-complete="false"
          type="password"
        />
      </el-form-item>
      <el-form-item v-if="isAdd" label="确认密码" prop="confirmPassword">
        <el-input
          v-model="form.confirmPassword"
          auto-complete="false"
          type="password"
        />
      </el-form-item>
      <el-form-item label="姓名">
        <el-input v-model="form.fullname" />
      </el-form-item>
      <el-form-item label="性别">
        <el-radio v-model="form.sex" label="男" />
        <el-radio v-model="form.sex" label="女" />
      </el-form-item>
      <el-form-item label="部门">
        <rw-organization v-model="form.orgId" />
      </el-form-item>
      <el-form-item label="头像">
        <el-upload
          :action="network.uploadURL"
          :before-upload="beforeAvatarUpload"
          class="avatar-uploader"
          :on-success="handleAvatarSuccess"
          :show-file-list="false"
        >
          <img v-if="form.profile" class="avatar" :src="form.profile" />
          <el-icon v-else class="avatar-uploader-icon">
            <plus :size="50" />
          </el-icon>
        </el-upload>
      </el-form-item>
      <el-form-item label="角色">
        <rw-role v-model="form.roles" />
      </el-form-item>
      <el-form-item label="联系电话" prop="telnum">
        <el-input v-model="form.telnum" type="number" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  const network = require('@/config/net.config')
  import { doEdit, doAdd } from '@/api/system/user'
  import { Plus } from '@element-plus/icons-vue'
  import RwOrganization from '@/plugins/RwOrganization'
  import RwRole from '@/plugins/RwRole'

  export default defineComponent({
    name: 'SystemUserEdit',
    components: { Plus, RwOrganization, RwRole },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      const validpassword2 = (rule, value, callback) => {
        console.log(`校验二次密码${value}`)
        if (value === '') {
          callback(new Error('请再次输入密码'))
        } else if (value !== state.form.password) {
          callback(new Error('两次输入的密码不一致'))
        } else {
          callback()
        }
      }
      const state = reactive({
        formRef: null,
        isAdd: false,
        form: {
          confirmPassword: '',
          roles: [],
        },
        rules: {
          account: [
            {
              required: true,
              pattern: '[^ \x22]+',
              trigger: 'blur',
              message: '请输入用户名',
            },
            {
              max: 32,
              trigger: 'blur',
              message: '用户名不能大于32个字符',
            },
          ],
          password: [
            {
              required: true,
              pattern: '[^ \x22]+',
              trigger: 'blur',
              message: '请输入密码',
            },
            {
              min: 6,
              max: 32,
              trigger: 'blur',
              message: '密码不能小于6个字符且不能大于32个字符',
            },
          ],
          confirmPassword: [
            {
              required: true,
              validator: validpassword2,
              trigger: 'blur',
            },
          ],
          telnum: [
            {
              max: 11,
              trigger: 'blur',
              message: '联系电话不能超过11个数字',
            },
          ],
        },
        title: '',
        rolelist: [],
        organization: [],
        dialogFormVisible: false,
      })

      const handleAvatarSuccess = () => {}
      const beforeAvatarUpload = () => {}

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          state.isAdd = true
        } else {
          state.isAdd = false
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
            const { msg } = state.isAdd
              ? await doAdd(state.form)
              : await doEdit(state.form)
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
        handleAvatarSuccess,
        beforeAvatarUpload,
        network,
      }
    },
  })
</script>
