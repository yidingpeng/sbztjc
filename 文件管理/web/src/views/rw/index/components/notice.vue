<template>
  <el-col :lg="8" :md="12">
    <vab-card class="notice" shadow="hover">
      <template #header>最新公告</template>
      <div class="notice-list">
        <ul>
          <li v-for="item in list" :key="item.id">
            <span class="type">[{{ item.type }}]</span>
            <a :href="'#/inform/' + item.id">{{ item.title }}</a>
            <span class="time">[{{ toTimeString(item.time) }}]</span>
          </li>

          <li v-if="empty">近期暂无公告</li>
        </ul>
      </div>
    </vab-card>
  </el-col>
</template>

<script>
  import { getList } from '@/api/system/inform'
  import { toTimeString } from '@/utils/rwUtils'

  export default defineComponent({
    name: 'IndexNotice',
    setup() {},
    data() {
      return { empty: false, list: [] }
    },
    async mounted() {
      const {
        data: { list },
      } = await getList()
      this.list = list
      this.empty = list.length == 0
    },
    methods: { toTimeString },
  })
</script>

<style lang="scss" scoped>
  .notice-list {
    max-height: 200px;
    overflow: scroll;
    font-size: 15px;
    ul {
      list-style: none;
      margin-left: 0px;
      padding-inline-start: 5px;
      li {
        margin-bottom: 10px;
        a {
          margin-right: 5px;
        }
        .time {
          color: gray;
        }
        .type {
          color: #35bb93;
        }
      }
    }
  }
</style>
