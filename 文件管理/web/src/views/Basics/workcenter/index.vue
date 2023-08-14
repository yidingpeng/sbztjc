<template>
  <div class="system-configuration-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['workcenter:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['workcenter:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button>
      </vab-query-form-left-panel>
      <vab-query-form-right-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.workName"
              clearable
              placeholder="请输入工作中心名称"
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
      border
      :data="list"
      @row-dblclick="handleEdit"
      @selection-change="setSelectRows"
    >
      <el-table-column show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="工作中心编码"
        prop="workCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="工作中心名称"
        prop="workName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="工作中心类型"
        prop="workTypeTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="终端IP"
        prop="gwIP"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="MAC地址"
        prop="gwMac"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="父级工作中心"
        prop="atAreaTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="追溯系统"
        prop="isHasFollow"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <el-tag v-if="row.isHasFollow == 0" effect="dark" type="success">
            是
          </el-tag>
          <el-tag v-else effect="dark" type="danger">否</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="装配管理系统"
        prop="isHasAms"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <el-tag v-if="row.isHasAms == 0" effect="dark" type="success">
            是
          </el-tag>
          <el-tag v-else effect="dark" type="danger">否</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="光影引导系统"
        prop="isHasGuangying"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <el-tag v-if="row.isHasGuangying == 0" effect="dark" type="success">
            是
          </el-tag>
          <el-tag v-else effect="dark" type="danger">否</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="状态"
        prop="gwStatus"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <el-tag v-if="row.gwStatus == 0" effect="dark" type="success">
            启用
          </el-tag>
          <el-tag v-else effect="dark" type="danger">禁用</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="180">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['workcenter:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['workcenter:delete'] }"
            link
            type="primary"
            @click="handleDelete(row)"
          >
            删除
          </el-button>
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
  import { getList, doDelete } from '@/api/basics/workcenter'
  import Edit from './components/BasicsWorkCenterEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'BasicsWorkCenter',
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
          workName: '',
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
