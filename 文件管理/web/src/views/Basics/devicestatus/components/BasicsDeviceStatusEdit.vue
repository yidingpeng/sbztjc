<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <!-- <el-form-item label="房间号码" prop="roomNumber">
        <el-input v-model="form.roomNumber" />
      </el-form-item>
      <el-form-item label="设备名称" prop="deviceName">
        <el-input v-model="form.deviceName" />
      </el-form-item>
      <el-form-item label="设备IP" prop="ip">
        <el-input v-model="form.ip" />
      </el-form-item> -->
      <el-form-item label="监听路径" prop="path">
        <el-input v-model="form.path" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { doAdd, doEdit, GetRepeat } from '@/api/basics/devicestatus'

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
          // deviceName: [
          //   { required: true, trigger: 'blur', message: '请输入设备名称' },
          //   {
          //     max: 200,
          //     trigger: 'blur',
          //     message: '长度不能超过200个字符',
          //   },
          // ],
          path: [
            {
              required: true,
              trigger: 'blur',
              validator: async (rule, value, callback) => {
                if (value) {
                  const model = await GetRepeat(state.form)
                  if (model.data) {
                    callback(new Error('已存在相同监听路径'))
                  } else {
                    callback()
                  }
                } else {
                  callback(new Error('请输入监听路径'))
                }
              },
            },
            {
              max: 200,
              trigger: 'blur',
              message: '长度不能超过200个字符',
            },
          ],
          // roomNumber: [
          //   {
          //     required: true,
          //     trigger: 'blur',
          //     message: '长度不能超过50个字符',
          //   },
          //   {
          //     max: 50,
          //     trigger: 'blur',
          //     message: '长度不能超过50个字符',
          //   },
          // ],
          // ip: [
          //   {
          //     required: true,
          //     message: '请输入正确的IP地址',
          //     validator: (rule, value, callback) => {
          //       const regexp =
          //         /^((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})(\.((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})){3}$/
          //       if (value !== '' && !regexp.test(value)) {
          //         callback(new Error('请输入正确的IP地址'))
          //       } else {
          //         callback()
          //       }
          //     },
          //     trigger: 'blur',
          //   },
          // ],
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
