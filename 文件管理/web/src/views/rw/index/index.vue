<template>
  <div class="rw-main">
    <el-row :gutter="20">
      <user-info />
      <quick />
      <notice />
      <user-stat />
      <workflow-stat />
      <issue />
      <message />

      <el-col :span="24">
        <update />
      </el-col>
    </el-row>
  </div>
</template>

<script>
  import userInfo from './components/userInfo.vue'
  import message from './components/message'
  import notice from './components/notice'
  import quick from './components/quick'
  import userStat from './components/userStat'
  import workflowStat from './components/workflowStat'
  import update from './components/update'
  import issue from './components/issue'

  export default defineComponent({
    name: 'TemplateIndex',
    components: {
      userInfo,
      message,
      notice,
      quick,
      userStat,
      workflowStat,
      update,
      issue,
    },
    setup() {},
  })
</script>

<style scoped>
  .rw-main {
  }
</style>
