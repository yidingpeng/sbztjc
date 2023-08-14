<template>
  <div class="message-detail-container">
    <div class="header">
      <div class="title">{{ form.title }}</div>
      <div class="info">
        <!-- <div class="read">
          已读：
          <span>0</span>
        </div> -->
        <el-row align="middle" :gutter="20">
          <el-col :span="12">类型：{{ getType(form.type) }}</el-col>
          <el-col :span="12" style="text-align: right">
            发布时间：{{ form.time }}
          </el-col>
        </el-row>
        <!-- <div>
          <span>类型：</span>
          <span>{{ getType(form.type) }}</span>
        </div>
        <div class="time">发布时间：{{ form.time }}</div> -->
      </div>
    </div>
    <span v-html="form.content"></span>
    <div>
      <el-button link type="primary" @click="getUrl(form.type, form.dataId)">
        阅读详情
      </el-button>
    </div>
  </div>
</template>

<script>
  import { getOne } from '@/api/system/message'
  import router from '@/router'
  import { useDictionaryStore } from '~/src/store/modules/dictionary'

  export default defineComponent({
    name: 'MessageDetail',
    setup() {
      const state = reactive({
        form: { title: '[title]', content: '[content]', time: '[time]' },
      })

      const fetchData = async () => {
        const { id } = router.currentRoute.value.params

        const { data } = await getOne(id)
        state.form = data
      }

      onMounted(async () => {
        console.log('detail/index mounted')
        fetchData()
      })
      //onBeforeMount(() => console.log('message before mouted.'))
      //(() => console.log('message onload'))
      //func()

      const getUrl = function (type, id) {
        //console.log(`跳转到详情页[${type}-${id}]`)
        if (type == 'workflow') router.push(`/workflow/detail/${id}`)
        else if (type == 'inform') router.push(`/inform/detail/${id}`)
      }

      const getType = function (name) {
        const { getValue } = useDictionaryStore()
        return getValue('MessageType', name)
      }

      return { ...toRefs(state), fetchData, getUrl, getType }
    },
  })
</script>

<style type="scss" scoped>
  .message-detail-container {
    padding: 10px;
  }
  .header {
    border-bottom: 1px dashed lightgray;
  }
  .title {
    height: 50px;
    font-size: 20px;
    font-weight: bold;
    line-height: 50px;
    text-align: center;
  }
  .info {
    color: gray;
    text-align: center;
  }
  .read {
    display: inline;
    margin-right: 59px;
  }
  .time {
    display: inline;
    margin-right: 20px;
    text-align: right;
  }
</style>
