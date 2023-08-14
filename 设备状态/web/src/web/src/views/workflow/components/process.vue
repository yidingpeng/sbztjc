<template>
  <div class="el-descriptions is-el-descriptions--default">
    <div class="el-descriptions__header">
      <div class="el-descriptions__title">流程处理</div>
      <div class="el-descriptions__extra"></div>
    </div>
    <div class="el-descriptions__body">
      <el-tabs type="card">
        <el-tab-pane v-if="canApproval" label="流程审批">
          <audit />
        </el-tab-pane>
        <el-tab-pane label="审批意见">
          <el-table border :data="detail">
            <el-table-column label="序号" type="index" :width="50" />
            <el-table-column label="处理人" prop="operator" />
            <el-table-column label="意见" prop="comments" />
            <el-table-column label="审批结果" prop="result">
              <template #default="{ row }">
                <el-tag v-if="row.result" type="success">通过</el-tag>
                <el-tag v-else type="error">驳回</el-tag>
              </template>
            </el-table-column>
            <el-table-column label="附件" prop="files">
              <template #default="{ row }">
                <rw-file :files="row.files" show-type="link" />
              </template>
            </el-table-column>
            <el-table-column label="审批时间" prop="time" />
          </el-table>
        </el-tab-pane>
        <el-tab-pane label="流程节点">
          <el-table border :data="nodes">
            <el-table-column label="序号" type="index" :width="50" />
            <el-table-column label="节点" prop="nodeId" />
            <el-table-column label="节点名称" prop="nodeName" />
            <el-table-column label="处理人" prop="handler" />
            <el-table-column label="下一个节点" prop="nextNodeId" />
            <el-table-column label="节点状态" prop="status">
              <template #default="{ row }">
                <el-tag v-if="row.status == 0">未执行</el-tag>
                <el-tag v-else-if="row.status == 2" type="warning">
                  审批中
                </el-tag>
                <el-tag v-else-if="row.status == 1" type="success">
                  已执行
                </el-tag>
                <el-tag v-else-if="row.status == 4" type="info">跳过</el-tag>
                <el-tag v-else type="error">{{ row.status }}</el-tag>
                <el-button
                  v-if="!canApproval && row.status == 2"
                  link
                  style="margin-left: 5px"
                  type="primary"
                  @click="handleUrging(row.id)"
                >
                  催办
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-tab-pane>
        <el-tab-pane label="流程图">
          <sc-workflow :edit="false" :model-value="flowData" />
        </el-tab-pane>
      </el-tabs>
    </div>
  </div>
</template>

<script>
  import scWorkflow from '@/plugins/scWorkflow'
  import rwFile from '@/plugins/RwFile'
  import audit from './audit'
  import { urging } from '@/api/workflow/userFlow'

  let $baseMessage = inject('$baseMessage')

  export default defineComponent({
    name: 'ProcessWorkFlow',
    components: { scWorkflow, audit, rwFile },
    props: {
      detail: { type: Array, default: () => [] },
      nodes: { type: Array, default: () => [] },
      flowData: { type: Object, default: () => {} },
      canApproval: { type: Boolean, default: () => false },
    },
    setup() {
      $baseMessage = inject('$baseMessage')
    },
    methods: {
      async handleUrging(id) {
        const { msg } = await urging(id)
        $baseMessage(msg)
      },
    },
  })
</script>
