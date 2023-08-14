<template>
  <div class="description-container">
    <vab-card shadow="never">
      <el-radio-group v-model="size">
        <el-radio label="large">默认</el-radio>
        <el-radio label="default">中等</el-radio>
        <el-radio label="small">小</el-radio>
      </el-radio-group>

      <el-descriptions border :column="3" :size="size" :title="form.title">
        <template #extra>
          <el-button-group>
            <el-button size="small" type="primary" @click="handlePublish">
              立即发布
            </el-button>
            <el-button size="small" type="danger" @click="handleDelete">
              删除
            </el-button>
          </el-button-group>
        </template>

        <el-descriptions-item>
          <template #label>类别</template>
          {{ form.type }}
        </el-descriptions-item>
        <el-descriptions-item>
          <template #label>发布时间</template>
          <span>{{ form.time }}</span>
        </el-descriptions-item>
      </el-descriptions>
    </vab-card>

    <vab-card shadow="never">
      <div v-html="form.content"></div>
    </vab-card>
  </div>
</template>

<script>
  import { getOne, doPublish, doDelete } from '~/src/api/system/inform'
  import router from '~/src/router'

  export default defineComponent({
    name: 'Preview',
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        size: 'default',
        accordion: false,
        activeName: ['1', '2', '3', '4'],
        form: { title: '' },
      })

      const handleAccordion = (val) => {
        if (val) state.activeName = '1'
        else state.activeName = ['1', '2', '3', '4']
      }

      onMounted(async () => {
        const { id } = router.currentRoute.value.params
        const { data } = await getOne(id)
        state.form = data
      })

      const handlePublish = () => {
        const { form } = state
        $baseConfirm(`确定要发布公告[${form.title}]吗？`, null, async () => {
          const { msg } = await doPublish(form.id)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
        })
      }

      const handleDelete = async () => {
        const { form } = state
        if (form.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete(form.id)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
          })
        }
      }

      return {
        ...toRefs(state),
        handleAccordion,
        handlePublish,
        handleDelete,
      }
    },
  })
</script>

<style lang="scss" scoped>
  .description-container {
    padding: 0 !important;
    background: $base-color-background !important;

    :deep() {
      .el-descriptions {
        padding-top: $base-padding !important;
      }

      .el-collapse {
        margin-top: $base-margin !important;
      }
    }
  }
</style>
