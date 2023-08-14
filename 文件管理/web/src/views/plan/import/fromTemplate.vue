<template>
  <el-table border :data="templates" width="100%">
    <el-table-column label="模板名称" props="name" width="300">
      <template #default="{ row }">{{ row.name }}</template>
    </el-table-column>
    <el-table-column label="计划">
      <template #default="{ row }">
        <el-select
          v-model="selectPlan[row.id]"
          clearable
          value-key="id"
          @change="(value) => planChangeHandler(row.id, value)"
          @click="planSelectHandler(row.id)"
        >
          <el-option
            v-for="item in plans[row.id]"
            :key="item.id"
            :label="item.planName"
            :value="item.id"
          />
        </el-select>
      </template>
    </el-table-column>
    <!-- <el-table-column label="任务">
      <template #default="{ row }">
        <el-select v-model="selectTask[row.id]" clearable>
          <el-option
            v-for="item in tasks[selectPlan[row.id]]"
            :key="item.id"
            :label="item.wbsName"
            :value="item.id"
          />
        </el-select>
      </template>
    </el-table-column> -->
    <el-table-column label="导入" props="id" width="100">
      <template #default="{ row }">
        <el-button type="primary" @click="importDataHandler(row.id)">
          导入
        </el-button>
      </template>
    </el-table-column>
  </el-table>
</template>

<script>
  import { MBgetList } from '@/api/task'
  import { GetTempLateList } from '@/api/projectTemplate/task'
  import { ImportPlanTask } from '@/api/plan/plan'

  export default {
    name: 'ImportFromTemplate',
    props: {
      planId: { type: Number, default: 0 },
    },
    emits: ['fetchData'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        templates: [],
        selectPlan: [],
        //计划列表：数据为二维数组"[{0:[]},{1:[]}]"
        plans: [],
        selectTask: [],
        //任务列表：数据为二维数组
        tasks: [],
      })

      onMounted(async () => {
        const { data } = await MBgetList()
        state.templates = data.list
      })

      const planSelectHandler = async (id) => {
        //查询到后，直接写入缓存，不必每次都查询
        if (state.plans.indexOf(id) == -1) {
          const { data } = await GetTempLateList({ templateId: id })
          console.log(data)
          state.plans[id] = data
        }
      }

      //任务选中后，改变事件
      const planChangeHandler = (dataId, planId) => {
        let plan = {}
        console.log(state.plans)
        for (const p in state.plans[dataId]) {
          const item = state.plans[dataId][p]
          if (item.id == planId) {
            plan = item
            break
          }
        }
        if (plan == null) throw new Error('计划不能为空')
        console.log(plan.wbsPlan)
        state.tasks[planId] = plan.wbsPlan
        state.selectTask[dataId] = null
      }

      //导入计划、任务模板
      const importDataHandler = async (id) => {
        const templatePlanId = state.selectPlan[id]

        if (!(templatePlanId > 0)) {
          $baseMessage(
            '请选择需要导入的任务！',
            'error',
            'vab-hey-message-error'
          )
          return
        }

        const { code, msg } = await ImportPlanTask({
          currentPlanId: props.planId,
          templatePlanId,
        })

        if (code == 0) $baseMessage(msg, 'success', 'vab-hey-message-success')
        else $baseMessage(msg, 'error', 'vab-hey-message-error')
        emit('fetchData')
      }

      return {
        ...toRefs(state),
        planSelectHandler,
        importDataHandler,
        planChangeHandler,
      }
    },
  }
</script>

<style scoped></style>
