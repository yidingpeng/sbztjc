<template>
  <div class="workflow-edit-container">
    <el-descriptions border :column="2" size="default" title="流程基本信息">
      <template #extra>
        <el-button size="small" type="default">暂存</el-button>
        <el-button size="small" type="primary">提交</el-button>
        <el-button size="small" type="danger">删除</el-button>
      </template>
      <el-descriptions-item span="2">
        <template #label>标题</template>
        <el-input v-model="form.title" />
      </el-descriptions-item>
      <el-descriptions-item>
        <template #label>流程编号</template>
        <el-input v-model="form.sn" />
      </el-descriptions-item>
      <el-descriptions-item>
        <template #label>发起人</template>
        {{ form.requested }}
      </el-descriptions-item>
      <el-descriptions-item>
        <template #label>流程类型</template>
        {{ form.type }}
      </el-descriptions-item>
      <el-descriptions-item>
        <template #label>发起时间</template>
        {{ form.createTime }}
      </el-descriptions-item>
      <el-descriptions-item :span="2">
        <template #label>描述</template>
        {{ form.desc }}
      </el-descriptions-item>
    </el-descriptions>
    <type-select :detail="form.detail" :type="form.type" />
    <workflow-process :detail="form.records" :flow-data="form.flowData" />
  </div>
</template>

<script>
  import { getUserFlowOne } from '@/api/workflow/userFlow'
  import workflowProcess from './components/process'
  import typeSelect from './components/typeSelect'
  import router from '@/router'

  export default defineComponent({
    name: 'WorkflowDetail',
    components: { workflowProcess, typeSelect },
    setup() {
      const state = reactive({ form: {} })
      const fetchData = async () => {
        const { id } = router.currentRoute.value.params
        const { data } = await getUserFlowOne(id)
        data.records.unshift({
          id: 0,
          userWorkflowId: 0,
          comments: '提交审批',
          result: true,
          operator: data.requested,
          time: data.createTime,
        })
        state.form = data
        //alert(state.form.detail)
      }

      onMounted(() => {
        fetchData()
      })

      return { ...toRefs(state) }
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
