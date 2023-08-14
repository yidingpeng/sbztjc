<template>
  <el-descriptions border :column="1">
    <el-descriptions-item label="审批意见">
      <el-input
        v-model="form.comments"
        maxlength="2000"
        placeholder="请输入审批意见"
        rows="4"
        show-word-limit
        type="textarea"
      />
    </el-descriptions-item>
    <el-descriptions-item label="审批附件">
      <rw-upload
        ref="uploader"
        :files="form.files"
        @remove="handleRemove"
        @upload="handleUpload"
      />
    </el-descriptions-item>
    <el-descriptions-item label="审批结果">
      <el-radio v-model="form.result" :label="true">通过</el-radio>
      <el-radio v-model="form.result" :label="false">驳回</el-radio>
      <span v-if="!form.result">
        驳回至
        <el-select v-model="form.back" label="top" style="width: 130px">
          <el-option label="发起人" value="0" />
          <el-option label="上一级" value="1" />
        </el-select>
      </span>
    </el-descriptions-item>
    <el-descriptions-item label="处理">
      <el-button type="primary" @click="submit">提交</el-button>
    </el-descriptions-item>
  </el-descriptions>
</template>

<script>
  import { doAudit } from '@/api/workflow/userFlow'
  import rwUpload from '@/plugins/RwUpload'
  import router from '@/router'

  export default defineComponent({
    name: 'WorkflowAudit',
    components: { rwUpload },
    inject: ['$baseMessage', 'reload'],
    props: { id: { type: Number, default: 0 } },
    setup() {
      //const $baseMessage = inject('$baseMessage')
      //const reload = inject('reload')
      //const uploader = this.$refs('uploader')

      //console.log('uploader object :', uploader)

      const state = reactive({
        form: {
          flowId: 0,
          comments: '同意',
          result: true,
          back: '0',
          files: [],
        },
      })

      // const submit = async function () {
      //   const { id } = router.currentRoute.value.params
      //   state.form.flowId = id
      //   state.form.files = uploader.listToFile()
      //   const { msg } = await doAudit(state.form)
      //   $baseMessage(msg, 'success', 'vab-hey-message-success')
      //   reload()
      // }

      return { ...toRefs(state) }
    },
    methods: {
      handleRemove() {},
      handleUpload() {},
      async submit() {
        //const $baseMessage = this.inject('$baseMessage')

        const { id } = router.currentRoute.value.params
        this.form.flowId = id
        this.form.files = this.$refs.uploader.listToFile()
        const { msg } = await doAudit(this.form)
        this.$baseMessage(msg, 'success', 'vab-hey-message-success')
        this.reload()
      },
    },
  })
</script>
