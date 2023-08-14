<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" :model="form" :rules="rules">
      <el-form-item label="要求到货时间" prop="yqArrivalTime">
        <el-date-picker
          v-model="form.yqArrivalTime"
          :disabled-date="disabledDate"
          :style="{ width: '100%' }"
          type="date"
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
  import { doBatchEdit } from '@/api/purchase/purchase'

  export default defineComponent({
    name: 'POYQArrivalTime',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
          yqArrivalTime: null,
        },
        saveForm: {},
        rules: {
          yqArrivalTime: [
            { required: true, trigger: 'blur', message: '请选择要求到货时间' },
          ],
        },
        title: '',
        dialogFormVisible: false,
        rowsLenth: 0,
        disabledDate(time) {
          return time.getTime() <= Date.now()
        },
      })

      const showEdit = (rows) => {
        state.title = '编辑要求到货时间'
        state.saveForm = Object.assign({}, rows)
        state.rowsLenth = rows.length
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form.yqArrivalTime = null
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            const sendData = []
            for (let i = 0; i < state.rowsLenth; i++) {
              state.saveForm[i].yqArrivalTime = filterTime(
                state.form.yqArrivalTime
              )
              sendData.push(state.saveForm[i])
            }
            const { msg } = await doBatchEdit(sendData)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      //中国标准时间转换为年月日
      const filterTime = (time) => {
        const date = new Date(time)
        const y = date.getFullYear()
        let m = date.getMonth() + 1
        m = m < 10 ? `0${m}` : m
        let d = date.getDate()
        d = d < 10 ? `0${d}` : d
        let h = date.getHours()
        h = h < 10 ? `0${h}` : h
        let minute = date.getMinutes()
        minute = minute < 10 ? `0${minute}` : minute
        let s = date.getSeconds()
        s = s < 10 ? `0${s}` : s
        return `${y}-${m}-${d} ${h}:${minute}:${s}`
      }

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        filterTime,
      }
    },
  })
</script>
