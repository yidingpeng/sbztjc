<template>
  <el-col :lg="16" :sm="24">
    <vab-card class="workflow" shadow="hover">
      <template #header>待办流程</template>
      <el-table border :data="list">
        <el-table-column label="序号" prop="id" width="70" />
        <el-table-column label="流程名称" prop="title">
          <template #default="{ row }">
            <a :href="'#/workflow/detail/' + row.id">{{ row.title }}</a>
          </template>
        </el-table-column>
        <el-table-column label="发起人" prop="requested" />
        <el-table-column label="发起时间" prop="createTime">
          <template #default="{ row }">
            {{ toTimeString(row.createTime) }}
          </template>
        </el-table-column>
        <el-table-column label="当前节点" prop="currentNode" />
      </el-table>
    </vab-card>
  </el-col>
</template>

<script>
  import { getUserFlowTop } from '@/api/workflow/userFlow'
  import { toTimeString } from '@/utils/rwUtils'

  export default defineComponent({
    name: 'IndexWorkflow',
    setup() {},
    data() {
      return {
        empty: false,
        list: [],
      }
    },
    async mounted() {
      const {
        data: { list },
      } = await getUserFlowTop({ count: 10 })
      this.list = list
    },
    methods: { toTimeString },
  })
</script>

<style scoped>
  .workflow {
  }
</style>
