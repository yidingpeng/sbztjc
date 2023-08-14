<template>
  <el-col :lg="8" :md="12" :sm="24">
    <vab-card class="message-container" shadow="hover" skeleton>
      <template #header>未读消息</template>
      <el-row :gutter="20">
        <el-col v-if="emptyShow" :span="24">
          <el-empty description="暂无数据" :image-size="80" />
        </el-col>
        <el-col :span="24">
          <ul v-loading="listLoading">
            <li v-for="item in list" :key="item.id" class="list-item">
              <div class="list-item-meta">
                <div class="list-item-meta-time">
                  [{{ toTimeString(item.time) }}]
                </div>
                <div class="list-item-meta-title">
                  <el-link :underline="false" @click="getMessageUrl(item)">
                    {{ item.title }}
                  </el-link>
                </div>
              </div>
            </li>
          </ul>
        </el-col>
      </el-row>
    </vab-card>
  </el-col>
</template>

<script>
  import { topList } from '@/api/system/message'
  import { toTimeString, getMessageUrl } from '@/utils/rwUtils'

  export default defineComponent({
    name: 'IndexMessage',
    setup() {},
    data() {
      return { list: [], emptyShow: false, listLoading: false }
    },
    async mounted() {
      const { data } = await topList({ count: 10 })
      this.list = data
      this.emptyShow = data.length == 0
    },
    methods: {
      toTimeString,
      getMessageUrl,
    },
  })
</script>

<style lang="scss" scoped>
  .message-container {
    ul {
      padding: 0;
      margin: 0;
      list-style: none;
      outline: none;
      .list-item {
        padding: 5px;

        &-meta {
          display: flex;
          flex: 1 1;
          align-items: flex-start;

          &-time {
            text-align: center;
            min-width: 70px;
            margin-right: 5px;
            color: gray;
          }

          &-title {
            font-size: 14px;
            color: rgba(0, 0, 0, 0.85);
            overflow-x: auto;
            a {
              text-overflow: clip;
            }
          }

          &-item {
            display: inline-block;
            height: 61px;
            margin-left: 40px;
            font-size: 14px;
            color: rgba(0, 0, 0, 0.45);
            vertical-align: middle;

            > span {
              line-height: 30px;
            }

            > p {
              margin-top: 4px;
              margin-bottom: 0;
            }
          }

          :deep() {
            .el-progress {
              margin-top: 21px;
            }
          }
        }
      }
    }
  }
</style>
