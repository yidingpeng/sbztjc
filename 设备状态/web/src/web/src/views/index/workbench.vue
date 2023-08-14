<template>
  <div class="workbench-container">
    <el-row :gutter="24">
      <el-col :lg="18" :md="18" :sm="18" :xl="18" :xs="18">
        <order />
      </el-col>
      <el-col :lg="6" :md="6" :sm="6" :xl="6" :xs="6">
        <target />
      </el-col>
    </el-row>
  </div>
</template>

<script>
  import Order from './components/Order'
  import Target from './components/Target'

  export default defineComponent({
    name: 'Workbench',
    components: {
      Order,
      Target,
    },
  })
</script>

<style lang="scss" scoped>
  .workbench-container {
    padding: 0 !important;
    background: $base-color-background !important;
  }
</style>
