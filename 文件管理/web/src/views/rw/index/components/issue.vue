<template>
  <el-col :lg="24" :md="24" :sm="24">
    <vab-card header="未解决的问题" shadow="hover">
      <template #header>
        <span>未解决的问题(TOP10)</span>
        <el-link href="#/problem/problemfeedback" style="float: right">
          更多
        </el-link>
      </template>
      <div class="issue">
        <el-table border :data="list" :row-class-name="tableRowClassName">
          <el-table-column label="序号" type="index" />
          <el-table-column label="项目">
            <template #default="{ row }">
              {{ row.projectCode }} {{ row.projectName }}
            </template>
          </el-table-column>
          <el-table-column label="问题类型" prop="problemTypeText">
            <template #default="{ row }">
              <el-tag v-if="row.problemTypeText" type="danger">
                {{ row.problemTypeText }}
              </el-tag>
            </template>
          </el-table-column>
          <el-table-column label="反馈时间" prop="feedbackTime" />
          <el-table-column label="截止时间" prop="estimatedSettlementDate" />
          <el-table-column label="问题描述" prop="pfb_ExceptionDesc" />
          <!-- <el-table-column label="处理">
            <el-button link type="primary">急速处理</el-button>
          </el-table-column> -->
        </el-table>
      </div>
    </vab-card>
  </el-col>
</template>

<script>
  import { getIssueTop } from '@/api/stat/index'

  export default defineComponent({
    data() {
      return { list: [] }
    },
    mounted() {
      this.fetchData()
    },
    methods: {
      async fetchData() {
        const { data } = await getIssueTop({ count: 10, deal: 1 })
        this.list = data
      },
      tableRowClassName({ row }) {
        const d = new Date(row.estimatedSettlementDate)
        const day = 24 * 60 * 60 * 1000
        const offset = (d - new Date()) / day
        if (offset < 0) {
          return 'error-row'
        } else if (offset < 3) {
          return 'warning-row'
        } else if (offset < 7) {
          return 'success-row'
        }
        return ''
      },
    },
  })
</script>

<style lang="scss">
  .el-table .warning-row .cell {
    --el-table-tr-bg-color: var(--el-color-warning-light-9);
  }
  .el-table .success-row .cell {
    --el-table-tr-bg-color: var(--el-color-warning-light-3);
  }
  .el-table .error-row .cell {
    color: var(--el-color-danger);
  }
</style>
