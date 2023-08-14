<template>
  <div class="workflow-view-container">
    <el-descriptions border :column="3" title="流程基本信息">
      <el-descriptions-item :min-width="100" :span="3">
        <template #label>标题</template>
        <span style="font-size: 20px; font-weight: bold; text-align: center">
          {{ form.title }}
        </span>
      </el-descriptions-item>
      <el-descriptions-item>
        <template #label>流程编号</template>
        {{ form.sn }}
      </el-descriptions-item>
      <el-descriptions-item :min-width="100">
        <template #label>发起人</template>
        {{ form.requested }}
      </el-descriptions-item>
      <el-descriptions-item :min-width="100">
        <template #label>流程类型</template>
        {{ getType(form.type) }}
      </el-descriptions-item>
      <el-descriptions-item>
        <template #label>发起时间</template>
        {{ form.createTime }}
      </el-descriptions-item>
      <el-descriptions-item v-if="form.overTime">
        <template #label>结束时间</template>
        {{ form.overTime }}
      </el-descriptions-item>
      <el-descriptions-item label="抄送" :span="2">
        <!-- <el-button @click="test">测试</el-button> -->
        <span v-if="!form.sendUsers || form.sendUsers.length == 0">无</span>
        <el-tag v-for="user in form.sendUsers" :key="user.id">
          {{ user.name }}
        </el-tag>
      </el-descriptions-item>
      <el-descriptions-item label="附件" :span="2">
        <rw-file :files="form.files" />
      </el-descriptions-item>
    </el-descriptions>
    <type-select :type="form.type" />
    <workflow-process
      :can-approval="form.canApproval"
      :detail="form.records"
      :flow-data="form.flowData"
      :nodes="form.nodes"
    />
  </div>
</template>

<script>
  import { getUserFlowOne } from '@/api/workflow/userFlow'
  import workflowProcess from './components/process'
  import typeSelect from './components/typeSelect'
  import router from '@/router'
  import { useDictionaryStore } from '@/store/modules/dictionary'
  import rwFile from '@/plugins/RwFile'

  export default defineComponent({
    name: 'WorkflowDetail',
    components: { workflowProcess, typeSelect, rwFile },
    provide() {
      return {
        getDetail: () => this.form.detail,
        reload: async () => await this.fetchData(),
      }
    },
    setup() {
      const state = reactive({
        form: { detail: { projectNo: '123' }, type: '' },
      })

      const { getValue } = useDictionaryStore()
      const getType = (key) => getValue('WorkflowType', key)

      const fetchData = async () => {
        const { id } = router.currentRoute.value.params
        const { data } = await getUserFlowOne(id)
        state.form = data
      }
      onMounted(async () => {
        fetchData()
      })

      const submit = () => {
        alert(JSON.stringify(state.form.detail))
      }

      return { ...toRefs(state), submit, getType, fetchData }
    },
  })
</script>

<style lang="scss" scoped>
  .workflow-view-container {
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
