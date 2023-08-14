<template>
  <el-button type="primary" @click="onAddItem">添加</el-button>
  <el-table border :data="tableData">
    <el-table-column align="center" label="日期">
      <template #default="{ row }">
        <el-date-picker
          v-model="row.whDate"
          :disabled-date="disabledDate"
          placeholder="选择日期"
          type="date"
        />
      </template>
    </el-table-column>
    <el-table-column align="center" label="操作人员">
      <template #default="{ row }">
        <el-select
          v-model="row.userId"
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
      </template>
    </el-table-column>
    <el-table-column align="center" label="项目号" prop="projectCode">
      <template #default="{ row }">
        <el-input v-model="row.projectCode" />
      </template>
    </el-table-column>
    <el-table-column align="center" label="项目名称" prop="projectName">
      <template #default="{ row }">
        <el-input v-model="row.projectName" />
      </template>
    </el-table-column>
    <el-table-column
      align="center"
      label="工作需求"
      prop="taskContent"
      show-overflow-tooltip
    >
      <template #default="{ row }">
        <el-input v-model="row.taskContent" />
      </template>
    </el-table-column>
    <el-table-column
      align="center"
      label="工作内容"
      prop="jobContent"
      show-overflow-tooltip
    >
      <template #default="{ row }">
        <el-input v-model="row.jobContent" />
      </template>
    </el-table-column>
    <el-table-column align="center" label="工作时间" width="260">
      <template #default="{ row }">
        <el-time-select
          v-model="row.jobStartTime"
          end="24:00"
          placeholder="开始"
          start="8:30"
          step="00:15"
          style="width: 110px"
        />
        -
        <el-time-picker
          v-model="row.jobEndTime"
          format="HH:mm"
          placeholder="结束"
          style="width: 110px"
          value-format="HH:mm"
        />
      </template>
    </el-table-column>
    <el-table-column align="center" label="工作地点">
      <template #default="{ row }">
        <el-input v-model="row.location" />
      </template>
    </el-table-column>
    <el-table-column align="center" label="完成情况" prop="completeStatus">
      <template #default="{ row }">
        <el-input v-model="row.location" />
      </template>
    </el-table-column>
    <el-table-column align="center" label="工作时长" prop="duration">
      <template #default="{ row }">
        <el-input v-model="row.duration" />
      </template>
    </el-table-column>
    <el-table-column
      align="center"
      label="加班时长"
      prop="workOvertimeDuration"
    >
      <template #default="{ row }">
        <el-input v-model="row.workOvertimeDuration" />
      </template>
    </el-table-column>
    <el-table-column align="center" label="操作" width="80">
      <template #default="{ row }">
        <el-button text type="danger" @click="handleDelete(row)">删</el-button>
      </template>
    </el-table-column>
  </el-table>
  <el-button type="primary" @click="save">提交</el-button>
</template>

<script>
  import { doEdit } from '@/api/manhour'
  import { GetUserList } from '@/api/system/user'
  import moment from 'moment'

  export default defineComponent({
    name: 'ManhourEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        tableData: [],
        rules: {
          whDate: [
            { required: true, trigger: 'change', message: '请选择工作日期' },
          ],
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
      const onAddItem = () => {
        state.tableData.push({
          whDate: moment().format('YYYY-MM-DD'),
        })
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
      // onMounted(() => {
      //   fetchData()
      // })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        fetchData,
        disabledHours,
        disabledMinutes,
        disabledSeconds,
        onAddItem,
      }
    },
  })
</script>
