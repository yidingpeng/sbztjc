<template>
  <el-col :span="8">
    <vab-card shadow="hover">
      <template #header>工作流统计</template>
      <el-row :gutter="20">
        <el-col :span="8">
          <div class="item">
            <div class="title">总数</div>
            <vab-count class="num" :end-val="data.total" />
          </div>
        </el-col>
        <el-col :span="8">
          <div class="item">
            <div class="title">发起数</div>
            <vab-count class="num" :end-val="data.added" />
          </div>
        </el-col>
        <el-col :span="8">
          <div class="item">
            <div class="title">处理次数</div>
            <vab-count class="num" :end-val="data.executed" />
          </div>
        </el-col>
      </el-row>
    </vab-card>
  </el-col>
</template>

<script>
  import VabCount from '@/plugins/VabCount'
  import { getWorkflowStat } from '@/api/stat'

  export default defineComponent({
    name: 'WorkflowStat',
    components: { VabCount },
    setup() {},
    data() {
      return { data: { total: 2 } }
    },
    mounted() {
      this.fetchData()
    },
    methods: {
      async fetchData() {
        const { data } = await getWorkflowStat()
        console.log('workflow data is :', data)
        this.data = data
      },
    },
  })
</script>

<style lang="scss" scoped>
  .item {
    text-align: center;
    .num {
      font-size: 50px;
    }
  }
</style>
