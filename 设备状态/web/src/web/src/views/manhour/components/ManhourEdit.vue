<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="工作日期" prop="whDate">
        <el-date-picker
          v-model="form.whDate"
          :disabled-date="disabledDate"
          placeholder="选择日期"
          type="date"
        />
      </el-form-item>
      <el-form-item label="操作人员" prop="userId">
        <el-select
          v-model="form.userId"
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
      <el-form-item label="项目编号" prop="projectCode">
        <el-input v-model="form.projectCode" />
      </el-form-item>
      <el-form-item label="项目名称" prop="projectName">
        <el-input v-model="form.projectName" />
      </el-form-item>
      <el-form-item label="工作需求" prop="taskContent">
        <el-input v-model="form.taskContent" />
      </el-form-item>
      <el-form-item label="工作内容" prop="jobContent">
        <el-input v-model="form.jobContent" />
      </el-form-item>
      <el-form-item label="工作时间" prop="jobStartTime">
        <el-time-select
          v-model="form.jobStartTime"
          end="24:00"
          placeholder="开始时间"
          start="8:30"
          step="00:15"
          style="width: 150px; margin-right: 10px"
        />
        <el-time-picker
          v-model="form.jobEndTime"
          format="HH:mm"
          placeholder="结束时间"
          style="width: 150px"
          value-format="HH:mm"
        />
      </el-form-item>
      <el-form-item label="工作地点" prop="location">
        <el-input v-model="form.location" />
      </el-form-item>
      <el-form-item label="完成情况" prop="completeStatus">
        <el-input v-model="form.completeStatus" />
      </el-form-item>
      <el-form-item label="工作时长" prop="duration">
        <el-input v-model="form.duration" />
      </el-form-item>
      <el-form-item label="加班时长" prop="workOvertimeDuration">
        <el-input v-model="form.workOvertimeDuration" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { doEdit } from '@/api/manhour'
  import { GetUserList } from '@/api/system/user'

  export default defineComponent({
    name: 'ManhourEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          whDate: '',
          jobStartTime: '',
          jobEndTime: '',
          roles: [],
        },
        rules: {
          title: [{ required: true, trigger: 'blur', message: '请输入标题' }],
        },
        title: '',
        list: [],
        disabledDate(time) {
          return time.getTime() > Date.now()
        },
        dialogFormVisible: false,
      })
      const makeRange = (start, end) => {
        const result = []
        for (let i = start; i <= end; i++) {
          result.push(i)
        }
        return result
      }
      const disabledHours = () => {
        return makeRange(0, 16).concat(makeRange(19, 23))
      }
      const disabledMinutes = (hour) => {
        if (hour === 17) {
          return makeRange(0, 29)
        }
        if (hour === 18) {
          return makeRange(31, 59)
        }
      }
      const disabledSeconds = (hour, minute) => {
        if (hour === 18 && minute === 30) {
          return makeRange(1, 59)
        }
      }
      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
        } else {
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
            const { msg } = await doEdit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
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
      onMounted(() => {
        fetchData()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        fetchData,
        disabledHours,
        disabledMinutes,
        disabledSeconds,
      }
    },
  })
</script>
