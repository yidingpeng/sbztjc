<template>
  <div class="workflow-list-container">
    <h3>可发起流程列表页</h3>
    <el-row :gutter="15">
      <el-col
        v-for="item in list"
        :key="item.id"
        :lg="6"
        :md="8"
        :sm="12"
        :xl="6"
        :xs="24"
      >
        <vab-card class="task-item" shadow="hover">
          <template #header>
            <vab-icon icon="task-line" />
            {{ getType(item.type) }}
            <el-tag v-if="item.enabled" class="card-header-tag" type="success">
              启用
            </el-tag>
            <el-tag v-else class="card-header-tag" type="danger">停用</el-tag>
          </template>
          <!-- <el-image
            class="task-item-image"
            :src="require('@/assets/task_image/task.png')"
          /> -->
          <div style="height: 110px">
            <span>流程</span>
            <span
              style="
                display: block;
                width: 100%;
                font-size: 20px;
                line-height: 50px;
                text-align: center;
              "
            >
              {{ item.title }}
            </span>
          </div>
          <div class="task-item-bottom">
            <!-- <el-button link type="primary">查看流程图</el-button> -->
            <el-link
              v-if="item.enabled"
              :href="`#/workflow/publish/${item.id}`"
              style="float: right"
              type="default"
            >
              发起流程
            </el-link>
          </div>
        </vab-card>
      </el-col>
      <!-- <el-col :lg="6" :md="8" :sm="12" :xl="6" :xs="24">
        <vab-card class="task-add" shadow="hover" @click="handleAdd">
          <vab-icon icon="add-circle-line" />
          <p>添加任务</p>
        </vab-card>
      </el-col> -->
    </el-row>
  </div>
</template>

<script>
  import { getList } from '@/api/workflow/index'
  import { useDictionaryStore } from '@/store/modules/dictionary'

  export default {
    name: 'WorkflowList',
    setup() {},
    data() {
      const state = reactive({ list: [] })

      onMounted(async () => {
        const {
          data: { list },
        } = await getList({ enabled: true })
        state.list = list
      })

      return { ...toRefs(state) }
    },
    methods: {
      getType(key) {
        const { getValue } = useDictionaryStore()
        return getValue('WorkflowType', key)
      },
    },
  }
</script>

<style lang="scss" scoped>
  .workflow-list-container {
    padding: 0 !important;
    background: $base-color-background !important;

    .page-header {
      display: flex;
      align-items: center;
      padding: $base-padding $base-padding 0 $base-padding;
      margin-bottom: $base-margin;
      background: var(--el-color-white);
      border: 1px solid #ebeef5;

      :deep() {
        .el-form-item__content {
          width: 221px !important;

          .el-select,
          .el-input,
          .el-date-editor,
          .el-checkbox-group {
            width: 100%;
          }
        }
      }
    }

    :deep() {
      .el-card {
        .el-card__header {
          position: relative;

          .card-header-tag {
            position: absolute;
            top: 15px;
            right: $base-margin;
          }

          > div > span {
            display: flex;
            align-items: center;

            i {
              margin-right: 3px;
            }
          }
        }

        .el-card__body {
          position: relative;

          .card-footer-tag {
            position: absolute;
            right: $base-margin;
            bottom: 15px;
          }
        }
      }
    }

    .task-item {
      min-height: 255px;
      max-height: 255px;

      &-image {
        display: block;
        width: 150px;
        margin: 0 auto 20px auto;
      }

      &-bottom {
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding-top: 20px;
        text-align: right;
        border-top: 1px solid #ebeef5;

        .el-button--mini.is-circle {
          i {
            margin-right: 0 !important;
            font-size: 14px;
          }
        }
      }
    }

    .task-add {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      min-height: 255px;
      max-height: 255px;
      color: var(--el-color-primary);
      text-align: center;
      cursor: pointer;

      i {
        font-size: 30px;
      }

      p {
        margin-top: 20px;
        font-size: 12px;
      }
    }
  }
</style>
