<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="设备名称" prop="devName">
        <el-input v-model="form.devName" />
      </el-form-item>
      <el-form-item label="设备编码" prop="devNo">
        <el-input v-model="form.devNo" />
      </el-form-item>
      <el-form-item label="设备IP" prop="devIP">
        <el-input v-model="form.devIP" />
      </el-form-item>
      <el-form-item label="IC卡号" prop="devCardno">
        <el-input v-model="form.devCardno" />
      </el-form-item>
      <el-form-item label="状态" prop="devStatus">
        <el-select
          v-model="form.devStatus"
          allow-create13
          placeholder="选择产品状态"
        >
          <el-option key="0" label="启用" :value="0" />
          <el-option key="1" label="禁用" :value="1" />
        </el-select>
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input v-model="form.remark" rows="3" type="textarea" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { doAdd, doEdit, GetRepeat } from '@/api/basics/device'

  export default defineComponent({
    name: 'BasicsDeviceEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        isAdd: false,
        form: {
          roles: [],
        },
        rules: {
          devName: [
            { required: true, trigger: 'blur', message: '请输入设备名称' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          devNo: [
            {
              required: true,
              trigger: 'blur',
              validator: async (rule, value, callback) => {
                if (value) {
                  const model = await GetRepeat(state.form)
                  if (model.data) {
                    callback(new Error('已存在相同设备编码'))
                  } else {
                    callback()
                  }
                } else {
                  callback(new Error('请输入设备编码'))
                }
              },
            },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
            {
              min: 3,
              trigger: 'blur',
              message: '编码长度不能小于3个字符',
            },
          ],
          devStatus: [
            { required: true, trigger: 'blur', message: '请选择设备状态' },
          ],
          devCardno: [
            { trigger: 'blur', message: '长度不能超过50个字符' },
            {
              max: 50,
              trigger: 'blur',
              message: '长度不能超过50个字符',
            },
          ],
          devIP: [
            {
              required: true,
              message: '请输入正确的IP地址',
              validator: (rule, value, callback) => {
                const regexp =
                  /^((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})(\.((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})){3}$/
                if (value !== '' && !regexp.test(value)) {
                  callback(new Error('请输入正确的IP地址'))
                } else {
                  callback()
                }
              },
              trigger: 'blur',
            },
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
      }
    },
  })
</script>
