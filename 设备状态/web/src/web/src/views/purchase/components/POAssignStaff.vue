<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" :model="form" :rules="rules">
      <el-form-item label="分配人员" prop="assignStaff">
        <!-- <rw-user
          v-model="form.assignStaff"
          filterable
          :style="{ width: '100%' }"
        /> -->
        <el-select
          v-model="form.assignStaff"
          clearable
          filterable
          placeholder="请选择用户"
        >
          <el-option
            v-for="item in list"
            :key="item.value"
            :label="item.text"
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
  //import RwUser from '@/plugins/RwUser'
  import { doBatchEdit, SendMessagePurchase } from '@/api/purchase/purchase'
  import { GetUserList } from '@/api/system/user'

  export default defineComponent({
    name: 'POAssignStaff',
    //components: { RwUser },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      //const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
          assignStaff: null,
        },
        saveForm: {},
        rules: {
          assignStaff: [
            { required: true, trigger: 'blur', message: '请选择分配人员' },
          ],
        },
        title: '',
        list: [],
        dialogFormVisible: false,
        rowsLenth: 0,
        disabledDate(time) {
          return time.getTime() <= Date.now()
        },
        proCode: '',
        proName: '',
      })

      const showEdit = (rows, proCode, proName) => {
        state.proCode = proCode
        state.proName = proName
        state.title = '分配人员'
        state.saveForm = Object.assign({}, rows)
        state.rowsLenth = rows.length
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form.assignStaff = null
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }

      const fetchData = async () => {
        const per = await GetUserList()
        if (per.data.length > 0) {
          per.data.forEach((item) => {
            if (item.orgId > 3) {
              state.list.push({
                text: item.account,
                value: item.id,
              })
            }
          })
        }
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            //console.log(state.saveForm)
            const sendData = []
            for (let i = 0; i < state.rowsLenth; i++) {
              state.saveForm[i].assignStaff = state.form.assignStaff.toString()
              sendData.push(state.saveForm[i])
            }
            const { msg } = await doBatchEdit(sendData)
            ElMessage({
              type: msg.includes('成功') ? 'success' : 'error',
              message: msg,
            })
            //发送通知到分配人员
            if (msg.includes('成功')) {
              await SendMessagePurchase({
                ProjectCode: state.proCode,
                ProjectName: state.proName,
                UserID:
                  state.saveForm[0].assignStaff > 0
                    ? state.saveForm[0].assignStaff
                    : 0,
              })
            }
            //$baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        fetchData,
        showEdit,
        close,
        save,
      }
    },
  })
</script>
