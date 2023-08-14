<template>
  <div class="system-log-container">
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
            <el-select
              v-model="queryForm.type"
              clearable
              placeholder="流程类型筛选"
            >
              <el-option
                v-for="type in types"
                :key="type.key"
                :label="type.value"
                :value="type.key"
              />
            </el-select>
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

    <el-table v-loading="listLoading" :data="list">
      <el-table-column label="序号">
        <template #default="{ $index }">
          {{ $index + 1 }}
        </template>
      </el-table-column>
      <el-table-column label="流程名称" prop="title" show-overflow-tooltip>
        <template #default="{ row }">
          {{ row.title }}
        </template>
      </el-table-column>
      <el-table-column label="流程类型" prop="type" show-overflow-tooltip />
      <el-table-column label="创建时间" prop="createTime" />
      <el-table-column label="修改时间" prop="updateTime" />

      <el-table-column label="流程数" prop="count" show-overflow-tooltip />
      <el-table-column label="状态" prop="enabled" show-overflow-tooltip>
        <template #default="{ row }">
          <el-tag v-if="row.enabled" effect="dark" type="success">启用</el-tag>
          <el-tag v-else effect="dark" type="danger">禁用</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template #default="{ row }">
          <el-button link type="primary" @click="handleEdit(row)">
            编辑
          </el-button>
          <el-button link type="primary" @click="handleDelete(row)">
            删除
          </el-button>
        </template>
      </el-table-column>
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
  import { getList, doDelete } from '@/api/workflow/index'
  import { Search } from '@element-plus/icons-vue'
  import router from '~/src/router'

  export default defineComponent({
    name: 'WorkflowList',
    provide() {
      return { getTypes: () => types }
    },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        list: [],
        types: [
          { key: '', value: '所有流程' },
          { key: 'issue', value: '问题反馈' },
        ],
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
        } = await getList(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false

        //console.log('types:', state.types)
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

      const handleEdit = (row) => {
        router.push(`/workflow/edit/${row.id}`)
      }
      const handleDelete = (row) => {
        $baseConfirm(`确定要删除${row.title}吗？`, null, async () => {
          const { msg } = await doDelete(row.id)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          fetchData()
        })
      }

      return {
        ...toRefs(state),
        fetchData,
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
