<template>
  <el-dialog v-model="dialogFormVisible" :title="title" width="500px" @close="close">
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="故障试验间" prop="testRoom" label-width="120px">
        <el-input v-model="form.testRoom" readonly="true" oninput="handledevice()" />
      </el-form-item>
      <el-form-item label="故障时间" prop="faultDateTime" label-width="120px">
        <el-input v-model="form.faultDateTime" readonly="true" />
      </el-form-item>

      <el-form-item label="故障设备名称" prop="deviceName" label-width="120px">
        <el-select :data="devicelist" v-model="form.deviceName" allow-create13 placeholder="选择试验设备">

          <el-option v-for="item in devicelist" :key="item" :label="item.deviceName" :value="item.deviceName" />

        </el-select>
      </el-form-item>


      <el-form-item label="故障原因" prop="faultReason" label-width="120px">
        <el-input v-model="form.faultReason" rows="3" type="textarea" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>
<style>
::v-deep .el-form-item__label {
  width: 100px;
}
</style>
<script>
import { doEdit } from '@/api/DeviceStatus/FaultReporting'
import { GetDeviceTestRoomAllList, GetDeviceNameConditionList, doAdd, GetRepeat } from '@/api/DeviceStatus/DeviceStatusTag'
import { Edit } from '@element-plus/icons-vue';
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
      isAdds: false,
      form: {
        roles: [],
      },

      rules: {
        testRoom: [
          { trigger: 'blur', message: '请输入试验名称' },
          {
            max: 150,
            trigger: 'blur',
            message: '长度不能超过150个字符',
          },
        ],
        faultDateTime: [
          {

            trigger: 'blur',
          },
          {
            min: 3,
            trigger: 'blur',
            message: '编码长度不能小于3个字符',
          },
          {
            max: 150,
            trigger: 'blur',
            message: '长度不能超过150个字符',
          },
        ],

        deviceName: [
          { required: true, trigger: 'blur', message: '请选择试验设备' },
        ],
        deviceRoom: [
          { required: true, trigger: 'blur', message: '请选择试验间' },
        ],
        faultReason: [
          { required: true, trigger: 'blur', message: '请入故障原因！' },
          {

            trigger: 'blur',
            message: '请入故障原因！',
          },
        ],
      },
      title: '',
      dialogFormVisible: false,
    })

    const showadd = (row, addoredit) => {
      if (addoredit.toString().includes('adds')) {
        state.title = '添加'
        console.log(row)
        state.isAdds = true
      } else {
        state.title = '故障填报'
        state.isAdds = false
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
    const handledevice = async () => {

      console.log(state.form.testRoom)
      const data = await GetDeviceNameConditionList({ roomName: state.form.testRoom })

      console.log(data.data)
      state.devicelist = data.data
    }

    const save = () => {
      state['formRef'].validate(async (valid) => {
        if (valid) {
          const { msg } = state.isAdds
            ? await doAdd(state.form)
            : await doEdit(state.form)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          emit('fetch-data')
          close()
        }
      })
    }
    const fetchData = async () => {

      console.log(GetDeviceTestRoomAllList())
      const {
        data: { list, total },
      } = await GetDeviceTestRoomAllList()
      state.list = list
      state.total = total

    }

    onMounted(() => {
      fetchData()

    })
    watch(() => state.form.testRoom, (newVal, oldVal) => {
      // 在这里编写你想要执行的代码
      if (newVal) {
        handledevice()
      }
    });
    return {
      ...toRefs(state),
      showadd,
      close,
      save,
      fetchData,
      handledevice,
    }
  },
})
</script>
