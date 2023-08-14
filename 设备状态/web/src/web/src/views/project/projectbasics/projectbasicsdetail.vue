<template>
  <div class="detail-container">
    <el-page-header :content="'【' + title + '】详情页面'" @back="goBack" />
    <el-descriptions border :column="3" title="详情">
      <template #extra>
        <el-button size="small" type="primary">操作</el-button>
      </template>
      <el-descriptions-item>
        <template #label>标题</template>
      </el-descriptions-item>
      <el-descriptions-item>
        <template #label>作者</template>
      </el-descriptions-item>
      <el-descriptions-item>
        <template #label>时间</template>
      </el-descriptions-item>
      <el-descriptions-item>
        <template #label>评级</template>
      </el-descriptions-item>

      <el-descriptions-item>
        <template #label>备注</template>
      </el-descriptions-item>
      <el-descriptions-item>
        <template #label>
          <i class="el-icon-office-building"></i>
          联系地址
        </template>
        江苏省苏州市吴中区吴中大道 1188 号
      </el-descriptions-item>
    </el-descriptions>

    <vab-json-viewer copyable :expand-depth="5" sort :value="route" />
  </div>
</template>

<script>
  import { useTabsStore } from '@/store/modules/tabs'
  import { handleActivePath } from '@/utils/routes'
  import VabJsonViewer from 'vue-json-viewer'
  import { Refresh } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'Detail',
    components: { VabJsonViewer },
    setup() {
      const route = useRoute()
      const router = useRouter()
      const tabsStore = useTabsStore()
      const { changeTabsMeta, delVisitedRoute } = tabsStore

      const state = reactive({
        route: { query: { title: '加载中' } },
        rate: 0,
        form: {
          text: '',
        },
        title: '加载中',
      })

      const goBack = async () => {
        const detailPath = await handleActivePath(route, true)
        await router.push('/project/projectbasics/index')
        await delVisitedRoute(detailPath)
      }

      onMounted(() => {
        changeTabsMeta({
          title: '详情页',
          meta: {
            title: `${route.query.title} 详情页`,
          },
        })
        state.title = route.query.title
        state.route = {
          path: route.path,
          params: route.params,
          query: { ...route.query, ...{ rate: parseInt(route.query.rate) } },
          name: route.name,
          meta: route.meta,
        }
      })

      return {
        ...toRefs(state),
        goBack,
        Refresh,
      }
    },
  })
</script>
