<template>
  <el-col :lg="8" :md="12" :sm="24">
    <vab-card class="users" shadow="hover">
      <template #header>用户统计</template>
      <el-row :gutter="20">
        <el-col :span="8">
          <div class="item">
            <div>现有用户数</div>
            <div class="data"><vab-count :end-val="user.total" /></div>
          </div>
        </el-col>
        <el-col :span="8">
          <div class="item">
            <div>今日活跃用户</div>
            <div class="data"><vab-count :end-val="user.activeToday" /></div>
          </div>
        </el-col>
        <el-col :span="8">
          <div class="item">
            <div>本周活跃用户</div>
            <div class="data"><vab-count :end-val="user.activeWeek" /></div>
          </div>
        </el-col>
      </el-row>
    </vab-card>
  </el-col>
</template>

<script>
  import VabCount from '@/plugins/VabCount'
  import { getUserStat } from '@/api/stat/index'

  export default defineComponent({
    name: 'UserStat',
    components: { VabCount },
    setup() {},
    data() {
      return { user: { total: 0, activeToday: 0, activeWeek: 0 } }
    },
    async mounted() {
      const { data } = await getUserStat()
      this.user = data
    },
  })
</script>

<style lang="scss" scoped>
  .users {
    .item {
      width: 100%;
      text-align: center;
      .data {
        width: 100%;
        font-size: 50px;
      }
    }
  }
</style>
