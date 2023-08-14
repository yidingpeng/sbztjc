<template>
  <div class="workflow-publish-container">
    <el-form :model="form" :rules="rules">
      <el-descriptions border :column="3" size="default" title="流程基本信息">
        <template #extra>
          <el-button type="default" @click="submitHandle(false)">
            暂存
          </el-button>
          <el-button type="primary" @click="submitHandle(true)">提交</el-button>
          <el-button type="danger">删除</el-button>
        </template>
        <el-descriptions-item class="aa" :span="3">
          <template #label>标题</template>
          <el-form-item prop="title">
            <el-input v-model="form.title" />
          </el-form-item>
        </el-descriptions-item>
        <el-descriptions-item span="2">
          <template #label>流程编号</template>
          <span>保存后自动生成</span>
        </el-descriptions-item>
        <el-descriptions-item label="流程类型" :span="2">
          <span>
            {{ useDictionaryStore().getValue('WorkflowType', form.type) }}
          </span>
        </el-descriptions-item>
        <el-descriptions-item v-if="form.allowUserSend" label="抄送" :span="3">
          <send-select v-model="form.sendUsers" />
        </el-descriptions-item>
        <el-descriptions-item label="附件" :span="3">
          <!-- <el-button @click="test">测试</el-button> -->
          <rw-upload
            ref="uploader"
            :files="form.files"
            type="workflow"
            @on-change-files="handleUploadChange"
          />
        </el-descriptions-item>
      </el-descriptions>
      <type-select edit :type="form.type" />
    </el-form>
    <use-select
      v-if="selectVisible"
      ref="useselect"
      @closed="selectVisible = false"
    />
  </div>
</template>

<script>
  import { doGetOne } from '@/api/workflow/index'
  import { getUserFlowOne } from '@/api/workflow/userFlow'
  import { doAddUserFlow, doEditUserFlow } from '@/api/workflow/userFlow'
  import typeSelect from './components/typeSelect'
  import router from '@/router'
  import { useUserStore } from '@/store/modules/user'
  import { useDictionaryStore } from '@/store/modules/dictionary'
  import sendSelect from './components/send'
  import useSelect from '@/plugins/scWorkflow/select'
  import RwUpload from '@/plugins/RwUpload'
  import { useTabsStore } from '@/store/modules/tabs'

  export default defineComponent({
    name: 'WorkflowDetail',
    components: { typeSelect, sendSelect, useSelect, RwUpload },
    provide() {
      return {
        getDetail: async () => {
          await this.fetchData()
          return this.form.detail
        },
        select: this.selectHandle,
      }
    },
    setup() {
      //const $alert = inject('$baseAlert')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        selectVisible: false,
        form: {
          workFlowId: 0,
          title: '',
          type: 'issue',
          createTime: '',
          desc: '',
          detail: {},
          autoPublish: true,
          sendUsers: [],
          allowUserSend: true,
          files: [],
        },
        rules: {
          title: [
            { required: true, message: '请输入流程标题', trigger: 'blur' },
          ],
        },
      })
      const fetchData = async function () {
        console.log('page route:', router.currentRoute.value)
        const { id } = router.currentRoute.value.params

        //编辑流程
        if (router.currentRoute.value.name == 'EditMyWorkflow') {
          const { data: userData } = await getUserFlowOne(id)
          console.log('getUserFlowOne', userData)
          state.form = userData
          //state.form.files = userData.fiels
        } else {
          //发起流程
          //state.form.type = type
          state.form.workFlowId = id

          const { getUsername } = useUserStore()
          const username = getUsername
          const { data } = await doGetOne(id)
          state.form.type = data.type
          state.form.title = `${username}的${data.title}`
          state.form.allowUserSend = data.allowUserSend
        }
      }

      onMounted(() => {
        console.log('publish mouented')
      })

      const submitHandle = async function (autoPublish) {
        state.form.autoPublish = autoPublish
        state.form.files = this.$refs.uploader.listToFile()

        if (state.form.id > 0) {
          const { msg } = await doEditUserFlow(state.form.id, state.form)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
        } else {
          const { msg } = await doAddUserFlow(state.form)

          $baseMessage(msg, 'success', 'vab-hey-message-success')
        }

        const tabStore = useTabsStore()
        const { delVisitedRoute } = tabStore
        const route = this.$route
        //console.log('route', route)
        //console.log('router', this.$router)
        //先清空已有的详情页，再重新打开一个详情页
        await delVisitedRoute('/workflow/my/:id')
        console.log(
          'route to :',
          autoPublish,
          `/workflow/detail/${route.params.id}`
        )
        //如果点击“提交”按钮，则跳转到详情页，否则跳转到列表页
        if (autoPublish) router.push(`/workflow/detail/${route.params.id}`)
        else router.push('/workflow/my')

        //if (isActive(route.path)) toLastTab()
      }

      return { ...toRefs(state), fetchData, submitHandle, useDictionaryStore }
    },
    methods: {
      selectHandle(type, data) {
        this.selectVisible = true
        this.$nextTick(() => {
          //console.log(this.$refs.useselect)
          this.$refs.useselect.open(type, data)
        })
      },
      test() {
        //console.log(this.form.sendUsers)
        console.log('test method:', this.form)
      },
      handleUpload(data) {
        console.log('handleUpload:', data)
        //this.form.files.push(data)
      },
      handleRemove(data) {
        console.log('handleRemove', data)
        //const index = this.form.files.indexOf(data)
        //this.form.files.splice(index, 1)
      },
      handleUploadChange(value) {
        this.form.files = value
      },
    },
  })
</script>

<style lang="scss" scoped>
  .workflow-publish-container {
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
