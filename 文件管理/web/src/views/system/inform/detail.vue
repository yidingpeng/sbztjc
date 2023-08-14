<template>
  <div class="description-container">
    <vab-card shadow="never">
      <template #header>
        <h2>
          {{ form.title }}
          <span v-if="form.status != 1">(待发布)</span>
        </h2>

        <el-row :gutter="20" style="color: #646464">
          <el-col :span="8">类别：{{ form.type }}</el-col>
          <el-col :span="8">发布人：{{ form.publisher }}</el-col>
          <el-col :span="8">发布时间：{{ form.time }}</el-col>
        </el-row>
        <div v-if="form.canEdit" class="tools">
          <el-button-group>
            <el-button size="small" type="primary" @click="handlePublish">
              立即发布
            </el-button>
            <el-button size="small" type="danger" @click="handleDelete">
              删除
            </el-button>
          </el-button-group>
        </div>
      </template>

      <div v-html="form.content"></div>
      <div v-if="form.content == ''">无内容</div>

      <el-divider border-style="dashed" />
      <h3>附件列表</h3>
      <rw-file :files="form.files" />
    </vab-card>
  </div>
</template>

<script>
  import { getOne, doPublish, doDelete } from '@/api/system/inform'
  import router from '@/router'
  import RwFile from '@/plugins/RwFile'

  export default defineComponent({
    name: 'Preview',
    components: { RwFile },
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
        console.log('router:', router.currentRoute.value)
        const { id } = router.currentRoute.value.params
        const { code, data, msg } = await getOne(id)
        if (code == 0) state.form = data
        else $baseMessage(msg)
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

      .tools {
        position: absolute;
        top: 30px;
        right: 20px;
      }
    }
  }
</style>
