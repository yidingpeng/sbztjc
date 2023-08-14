<template>
  <el-dialog v-model="dialogFormVisible" :title="title" width="500px" @close="close">
    <el-form ref="formRef" label-width="150px" :model="form" :rules="rules">
      <!-- <el-form-item label="计划节假日时间" prop="holidayTime">
        <el-input v-model="form.holidayTime" :style="{ width: '100px' }" type="number" step="0.1" />小时
      </el-form-item>
      <el-form-item label="计量校准时间" prop="jiaozhunTime">
        <el-input v-model="form.jiaozhunTime" :style="{ width: '100px' }" type="number" step="0.1" />小时
      </el-form-item> -->
      <el-form-item label="制度时间" prop="zhiduTime">
        <el-input v-model="form.zhiduTime" :style="{ width: '100px' }" type="number" step="0.1" />小时

      </el-form-item>


    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>

import { GetDeviceTestRoomAllList, GetDeviceNameConditionList, doAdd, GetRepeat } from '@/api/DeviceStatus/DeviceStatusTag'
import { doEdit } from '@/api/DeviceStatus/DRoomTimeInput'
import { watch } from 'vue';
export default defineComponent({
  name: 'BasicsProductionEdit',
  emits: ['fetch-data'],
  setup(props, { emit }) {
    const $baseMessage = inject('$baseMessage')

    const state = reactive({
      formRef: null,
      list: [],
      devicelist: [],
      isAdd: false,
      form: {
        roles: [],
      },
      value1: '',
      value2: '',
      rules: {
        holidayTime: [
          { required: true, message: '请输入数字', trigger: 'blur' },
          { pattern: /^\d+(\.\d{0,1})?$/, message: '只能输入数字且只能有一位小数', trigger: 'blur' }
        ],
        weibaoTime: [
          // {
          //   required: true,
          //   trigger: 'blur',
          //   validator: async (rule, value, callback) => {
          //     if (value) {
          //       const model = await GetRepeat(state.form)
          //       if (model.data) {
          //         callback(new Error('已存在相同试验编码'))
          //       } else {
          //         callback()
          //       }
          //     } else {
          //       callback(new Error('请输入试验编码'))
          //     }
          //   },
          // },
          // {
          //   min: 3,
          //   trigger: 'blur',
          //   message: '编码长度不能小于3个字符',
          // },
          // {
          //   max: 150,
          //   trigger: 'blur',
          //   message: '长度不能超过150个字符',
          // },
          { required: true, message: '请输入数字', trigger: 'blur' },
          { pattern: /^\d+(\.\d{0,1})?$/, message: '只能输入数字且只能有一位小数', trigger: 'blur' }
        ],
        jiaozhunTime: [
          { required: true, message: '请输入数字', trigger: 'blur' },
          { pattern: /^\d+(\.\d{0,1})?$/, message: '只能输入数字且只能有一位小数', trigger: 'blur' }
        ],
        zhiduTime: [
          { required: true, message: '请输入数字', trigger: 'blur' },
          { pattern: /^\d+(\.\d{0,1})?$/, message: '只能输入数字且只能有一位小数', trigger: 'blur' }
        ],

        deviceName: [
          { required: true, trigger: 'blur', message: '请选择试验设备' },
        ],
        deviceRoom: [
          { required: true, trigger: 'blur', message: '请选择试验间' },
        ],
        remark: [
          { trigger: 'blur', message: '长度不能超过255个字符' },
          {
            max: 255,
            trigger: 'blur',
            message: '长度不能超过255个字符',
          },
        ],
      },
      title: '',
      dialogFormVisible: false,
    })

    const showEdit = (row) => {
      if (!row) {
        state.title = '试验单添加'
        state.isAdd = true
      } else {
        state.title = '时间输入'
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
    const handleChange = async (val) => {

      console.log(val)
      const data = await GetDeviceNameConditionList({ roomName: val })

      console.log(data.data)
      state.devicelist = data.data
      state.listLoading = false
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
    const fetchData = async () => {
      state.listLoading = true
      console.log(GetDeviceTestRoomAllList())
      const {
        data: { list, total },
      } = await GetDeviceTestRoomAllList()
      state.list = list
      state.total = total
      state.listLoading = false
    }

    onMounted(() => {
      fetchData()
    })

    return {
      ...toRefs(state),
      showEdit,
      close,
      save,
      fetchData,
      handleChange,
    }
  },
})
</script>
<style></style>
