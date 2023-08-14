<template>
  <el-dialog v-model="dialogFormVisible" :title="title" width="500px" @close="close">
    <el-form ref="formRef" label-width="150px" :model="form" :rules="rules">
      <el-form-item label="试验间" prop="deviceRoom">
        <el-select :data="list" v-model="form.deviceRoom" allow-create13 placeholder="选择试验间" @change="handleChange">
          <el-option v-for="item in list" :key="item" :label="item.roomName" :value="item.roomName" />

        </el-select>
      </el-form-item>
      <el-form-item label="试验设备" prop="deviceName">
        <el-select :data="devicelist" v-model="form.deviceName" allow-create13 placeholder="选择试验设备">
          <el-option v-for="item in devicelist" :key="item" :label="item.deviceName" :value="item.deviceName" />

        </el-select>
      </el-form-item>
      <el-form-item label="试验项目名称" prop="projectName">
        <el-input v-model="form.projectName" />
      </el-form-item>
      <el-form-item label="实验员工号" prop="employeeId">
        <el-input v-model="form.employeeId" />
      </el-form-item>
      <el-form-item label="试验工程师" prop="testEngineer">
        <el-input v-model="form.testEngineer" />
      </el-form-item>
      <el-form-item label="项目开始时间" prop="startTime">
        <el-date-picker v-model="form.startTime" type="datetime" placeholder="选择日期时间" />

      </el-form-item>

      <el-form-item label="项目结束时间" v-if="!isAdd">
        <el-date-picker v-model="form.endTime" type="datetime" placeholder="选择日期时间" />
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

import { GetDeviceTestRoomAllList, GetDeviceNameConditionList, doAdd, GetRepeat, doEdit } from '@/api/DeviceStatus/DeviceStatusTag'
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
        projectName: [
          { required: true, trigger: 'blur', message: '请输入项目试验名称' },
          {
            max: 150,
            trigger: 'blur',
            message: '长度不能超过150个字符',
          },
        ],
        employeeId: [
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
          { required: true, trigger: 'blur', message: '请输入实验员工号' },
          {
            max: 150,
            trigger: 'blur',
            message: '长度不能超过150个字符',
          },
        ],
        testEngineer: [
          { required: true, trigger: 'blur', message: '请实验工程师' },
        ],
        startTime: [
          { required: true, trigger: 'blur', message: '输入开始时间' },
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
