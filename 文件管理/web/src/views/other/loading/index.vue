<template>
  <div class="loading-container">
    <el-row :gutter="20">
      <el-col :span="24">
        element-plus已知bug，无法通过css自定义loading动画，待官方修复后放开此功能！
      </el-col>
      <el-col :lg="14" :md="14" :sm="24" :xl="14" :xs="24">
        <vab-card shadow="hover">
          <template #header>
            <span>默认 Loading</span>
          </template>
          <el-button type="primary" @click="handleLoading()">
            默认效果
          </el-button>
          <el-button type="primary" @click="handleLoading(1)">效果1</el-button>
          <el-button type="primary" @click="handleLoading(2)">效果2</el-button>
          <el-button type="primary" @click="handleLoading(3)">效果3</el-button>
          <el-button type="primary" @click="handleLoading(4)">效果4</el-button>
          <el-button type="primary" @click="handleLoading(5)">效果5</el-button>
          <el-button type="primary" @click="handleLoading(6)">效果6</el-button>
          <el-button type="primary" @click="handleLoading(7)">效果7</el-button>
          <el-button type="primary" @click="handleLoading(8)">效果8</el-button>
          <el-button type="primary" @click="handleLoading(9)">效果9</el-button>
        </vab-card>
      </el-col>
      <el-col :lg="10" :md="10" :sm="24" :xl="10" :xs="24">
        <vab-card shadow="hover">
          <template #header>
            <span>多彩 Loading</span>
          </template>
          <el-button type="primary" @click="handleColorfullLoading(1)">
            效果1
          </el-button>
          <el-button type="primary" @click="handleColorfullLoading(2)">
            效果2
          </el-button>
          <el-button type="primary" @click="handleColorfullLoading(3)">
            效果3
          </el-button>
          <el-button type="primary" @click="handleColorfullLoading(4)">
            效果4
          </el-button>
        </vab-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
  export default defineComponent({
    name: 'Loading',
    setup() {
      const $baseLoading = inject('$baseLoading')
      const $baseColorfullLoading = inject('$baseColorfullLoading')

      const handleLoading = (index) => {
        const Loading = $baseLoading(index)
        setTimeout(() => {
          Loading.close()
        }, 3000)
      }
      const handleColorfullLoading = (index) => {
        const Loading = $baseColorfullLoading(index)
        setTimeout(() => {
          Loading.close()
        }, 3000)
      }
      return {
        handleLoading,
        handleColorfullLoading,
      }
    },
  })
</script>

<style lang="scss" scoped>
  .loading-container {
    padding: 0 !important;
    background: $base-color-background !important;

    :deep() {
      .el-button {
        margin-top: 10px;
        margin-right: 10px;
        margin-left: 0;
      }

      .el-button + .el-button {
        margin-right: 10px;
        margin-left: 0;
      }
    }
  }
</style>
