<template>
  <div class="target">
    <vab-card
      class="target-card1"
      shadow="hover"
      skeleton
      :skeleton-rows="7"
      style="height: 530px"
    >
      <template #header>
        <vab-icon />
        日志
      </template>
      <!-- <vab-chart
        class="target-echart1"
        :init-option="initOptions"
        :option="option"
        theme="vab-echarts-theme"
      />-->

      <el-input
        v-model="form.desc"
        class="TextArea"
        rows="20"
        type="textarea"
      />
    </vab-card>
    <vab-card
      class="target-card2"
      shadow="hover"
      skeleton
      :skeleton-rows="5"
      style="height: 155px"
    >
      <template #header>
        <span>操作</span>
      </template>
      <!-- <vab-chart
        class="target-echart2"
        :init-options="initOptions"
        :option="option2"
        theme="vab-echarts-theme"
      />-->

      <el-row>
        <el-button type="success">继续按钮</el-button>
        <el-button type="danger">急停按钮</el-button>
      </el-row>
    </vab-card>
  </div>
</template>

<script>
  // import VabChart from '@/plugins/VabChart'

  export default defineComponent({
    data() {
      return {
        form: {
          desc: 12312312,
        },
      }
    },
  })
</script>

<style lang="scss" scoped>
  .target {
    float: right;
    width: 100%;
    margin-bottom: $base-margin;
  }
</style>
