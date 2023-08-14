<template>
  <div class="user-workflow-container">
    <vab-query-form>
      <vab-query-form-top-panel>
        <el-form
          :inline="true"
          label-width="70px"
          :model="queryForm"
          @submit.prevent
        >
          <el-form-item label="标题">
            <el-input
              v-model.trim="queryForm.title"
              clearable
              placeholder="请输入标题关键字"
            />
          </el-form-item>
          <el-form-item label="类型">
            <rw-dictionary
              v-model="queryForm.type"
              default-value="所有流程"
              type="WorkflowType"
            />
          </el-form-item>
          <el-form-item label="创建时间">
            <el-date-picker
              v-model="queryForm.searchDate"
              end-placeholder="结束日期"
              start-placeholder="开始日期"
              type="daterange"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-top-panel>
    </vab-query-form>

    <el-table v-loading="listLoading" border :data="list">
      <el-table-column label="序号" width="50">
        <template #default="{ $index }">
          <span>
            {{ $index + 1 + queryForm.pageSize * (queryForm.pageNo - 1) }}
          </span>
        </template>
      </el-table-column>
      <el-table-column label="流程名称" prop="title" show-overflow-tooltip>
        <template #default="{ row }">
          <a :href="'#/workflow/detail/' + row.id" target="_self">
            {{ row.title }}
          </a>
        </template>
      </el-table-column>
      <el-table-column label="流程类型" prop="type" show-overflow-tooltip>
        <template #default="{ row }">
          {{ row.type }}
        </template>
      </el-table-column>
      <el-table-column
        label="流程号"
        prop="sn"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column label="发起时间" prop="createTime" />
      <el-table-column label="结束时间" prop="overTime" />
      <el-table-column label="发起人" prop="requested" width="80" />
      <el-table-column
        label="状态"
        prop="enabled"
        show-overflow-tooltip
        width="90"
      >
        <template #default="{ row }">
          <el-tag v-if="row.status == 'Saved'" effect="dark">暂存</el-tag>
          <el-tag
            v-else-if="row.status == 'Completed'"
            effect="dark"
            type="success"
          >
            已完成
          </el-tag>
          <el-tag
            v-else-if="row.status == 'Approving'"
            effect="dark"
            type="warning"
          >
            审核中
          </el-tag>
          <el-tag v-else-if="row.status == 'Countersigning'" effect="dark">
            会签中
          </el-tag>
          <el-tag v-else effect="dark">{{ row.status }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="当前节点" prop="currentNode" />
      <el-table-column label="操作">
        <template #default="{ row }">
          <el-button
            v-if="row.status == 'Saved'"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-if="row.status == 'Saved'"
            link
            type="primary"
            @click="handleDelete(row)"
          >
            删除
          </el-button>
          <el-button link type="primary" @click="handleDetail(row)">
            查看详情
          </el-button>
          <el-button
            v-if="row.status != 'Saved' && row.status != 'Completed'"
            link
            type="primary"
            @click="handleCancel(row)"
          >
            撤销
          </el-button>
        </template>
      </el-table-column>
      <!-- <el-table-column label="操作">
        <template #default="{ row }">
          <el-button link type="primary" @click="handleEdit(row)">编辑</el-button>
          <el-button link type="primary" @click="handleDelete(row)">删除</el-button>
        </template>
      </el-table-column> -->
      <template #empty>
        <el-empty class="vab-data-empty" description="暂无数据" />
      </template>
    </el-table>
    <el-pagination
      background
      :current-page="queryForm.pageNo"
      :layout="layout"
      :page-size="queryForm.pageSize"
      :total="total"
      @current-change="handleCurrentChange"
      @size-change="handleSizeChange"
    />
  </div>
</template>

<script>
  import {
    getUserFlowList,
    doDeleteUserFlow,
    doCancel,
  } from '@/api/workflow/userFlow'
  import { Search } from '@element-plus/icons-vue'
  import router from '@/router'
  import rwDictionary from '@/plugins/RwDictionary'

  export default defineComponent({
    name: 'MyWorkflow',
    components: { rwDictionary },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        queryForm: {
          type: '',
          title: '',
          pageNo: 1,
          pageSize: 20,
        },
      })

      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getUserFlowList(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
      }
      const handleCancel = async (row) => {
        const { msg } = await doCancel(row.id)
        $baseMessage(msg, 'success', 'vab-hey-message-success')
        fetchData()
      }
      const handleSizeChange = (val) => {
        state.queryForm.pageSize = val
        fetchData()
      }
      const handleCurrentChange = (val) => {
        state.queryForm.pageNo = val
        fetchData()
      }
      const queryData = () => {
        state.queryForm.pageNo = 1
        fetchData()
      }
      onMounted(async () => {
        fetchData()
      })

      const handleDetail = (row) => {
        router.push(`/workflow/detail/${row.id}`)
      }
      const handleEdit = (row) => {
        router.push(`/workflow/my/${row.id}`)
      }
      const handleDelete = (row) => {
        $baseConfirm(`确定要删除${row.title}吗？`, null, async () => {
          const { msg } = await doDeleteUserFlow(row.id)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          fetchData()
        })
      }

      return {
        ...toRefs(state),
        fetchData,
        handleDetail,
        handleCancel,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        Search,
      }
    },
  })
</script>
