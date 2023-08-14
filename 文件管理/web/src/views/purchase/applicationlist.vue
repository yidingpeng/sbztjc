<template>
  <div class="applicationlist-container">
    <vab-query-form>
      <vab-query-form-top-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="申请主题">
            <el-input
              v-model.trim="queryForm.title"
              clearable
              placeholder="请输入主题"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="理由/用途">
            <el-input
              v-model.trim="queryForm.reason"
              clearable
              placeholder="请输入理由用途"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="申 请 人">
            <el-input
              v-model.trim="queryForm.CreatedBy"
              clearable
              placeholder="请输入申请人"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-top-panel>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['applicationlist:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table v-loading="listLoading" :data="list">
      <el-table-column
        align="center"
        label="申请单号"
        prop="applicationNo"
        width="200"
      />
      <el-table-column
        align="center"
        label="申请主题"
        prop="title"
        width="450"
      />
      <el-table-column
        align="center"
        label="理由/用途"
        prop="reason"
        show-overflow-tooltip
        width="450"
      />
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="状态" prop="status" width="100">
        <template #default="{ row }">
          <el-tag v-if="row.status == 0">审核中</el-tag>
          <el-tag v-else-if="row.status == 1" type="success">已审核</el-tag>
          <el-tag v-else-if="row.status == 2" type="danger">已驳回</el-tag>
        </template>
      </el-table-column>
      <el-table-column align="center" label="操作" width="120">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['applicationlist:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            审核
          </el-button>
          <!-- <el-button
            v-permissions="{ permission: ['applicationlist:delete'] }"
            link type="primary"
            @click="handleDelete(row)"
          >
            删除
          </el-button> -->
        </template>
      </el-table-column>
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
    <edit ref="editRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import { SQDPageList, SQDDelete } from '@/api/purchase/purchase'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  import Edit from './components/applicationEdit'
  export default defineComponent({
    name: 'ApplicationList',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const router = useRouter()
      const state = reactive({
        editRef: null,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          title: '',
        },
      })

      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          router.push({
            path: '/purchase/purchasequisition',
          })
        }
      }
      const handleDelete = (row) => {
        //console.log(row)
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await SQDDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await SQDDelete({ ids })
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              await fetchData()
            })
          } else {
            $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
          }
        }
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
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await SQDPageList(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
