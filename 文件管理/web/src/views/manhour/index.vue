<template>
  <div class="manhour-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-button :icon="Plus" type="primary" @click="handleEdit">
          添加
        </el-button>
        <el-button :icon="Delete" type="danger" @click="handleDelete">
          批量删除
        </el-button>
      </vab-query-form-left-panel>
      <vab-query-form-right-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.projectCode"
              clearable
              placeholder="请输入项目号或项目名"
            />
          </el-form-item>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.whStartDate"
              clearable
              placeholder="请选择开始日期"
            />
          </el-form-item>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.whEndDate"
              clearable
              placeholder="请选择结束日期"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-right-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="日期"
        prop="whDate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="操作人员"
        prop="userName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目号"
        prop="projectCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目名称"
        prop="projectName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="工作需求"
        prop="taskContent"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="工作内容"
        prop="jobContent"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="工作时间">
        <template #default="scope">
          <span>{{ scope.row.jobStartTime }}-{{ scope.row.jobEndTime }}</span>
        </template>
      </el-table-column>
      <el-table-column align="center" label="工作地点">
        <template #default="scope">
          <span v-if="scope.row.offsiteLocation == ''">
            {{ scope.row.location }}
          </span>
          <span v-else>
            {{ scope.row.location }}({{ scope.row.scope.row.offsiteLocation }})
          </span>
        </template>
      </el-table-column>
      <el-table-column align="center" label="完成情况" prop="completeStatus" />
      <el-table-column align="center" label="工作时长" prop="duration" />
      <el-table-column
        align="center"
        label="加班时长"
        prop="workOvertimeDuration"
      />
      <el-table-column align="center" label="操作" width="200">
        <template #default="{ row }">
          <el-button text @click="handleEdit(row)">编辑</el-button>
          <el-button text @click="handleDelete(row)">删除</el-button>
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
  import { getList, doDelete } from '@/api/manhour'
  import Edit from './components/ManhourEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'Manhour',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

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
          whStartDate: '',
          whEndDate: '',
          projectCode: '',
        },
      })

      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.showEdit()
        }
      }
      const handleDelete = (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await doDelete({ ids })
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
        } = await getList(state.queryForm)
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
