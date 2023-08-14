<template>
  <div class="system-configuration-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['productionmodel:add'] }"
          :disabled="btnState"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['productionmodel:delete'] }"
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
              v-model.trim="queryForm.Pname"
              clearable
              placeholder="请输入产品型号名称"
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
        label="产品型号编码"
        prop="pmodelCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="产品型号名称"
        prop="pname"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="图号"
        prop="drawNo"
        show-overflow-tooltip
      />
      <!--  <el-table-column
        align="center"
        label="物料号" 
        prop="materialNo"
        show-overflow-tooltip
      /> -->
      <el-table-column
        align="center"
        label="产品型号类型"
        prop="productionModelTypeTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="产品类型"
        prop="productionTypeTxt"
        show-overflow-tooltip
      />
      <!--  <el-table-column
        align="center"
        label="线路号"
        prop="trainLine"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="车型号"
        prop="traiNModel"
        show-overflow-tooltip
      /> -->
      <!-- <el-table-column
        align="center"
        label="同产品型号排序号"
        prop="orderIndex"
        show-overflow-tooltip
      /> -->
      <el-table-column
        align="center"
        label="状态"
        prop="pstatus"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <el-tag v-if="row.pstatus == 0" effect="dark" type="success">
            启用
          </el-tag>
          <el-tag v-else effect="dark" type="danger">禁用</el-tag>
        </template>
      </el-table-column>
      <!--   <el-table-column
        align="center"
        label="备注" 
        prop="remark"
        show-overflow-tooltip
      /> -->
      <el-table-column align="center" label="操作" width="220">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['productionmodel:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['productionmodel:delete'] }"
            link
            type="primary"
            @click="handleDelete(row)"
          >
            删除
          </el-button>
          <el-button link type="primary" @click="handleDrawer(row)">
            额外字段
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
    <el-dialog v-model="opDrawer" title="额外字段管理" width="70%">
      <PExtend :module="pModelID" />
    </el-dialog>
  </div>
</template>

<script>
  import { getList, doDelete } from '@/api/basics/productionmodel'
  import Edit from './components/BasicsProductionModelEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  import PExtend from '@/views/Basics/productextend/index.vue'

  export default defineComponent({
    name: 'BasicsProductionModel',
    components: { Edit, PExtend },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        list: [],
        listLoading: true,
        opDrawer: false,
        btnState: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        pModelID: 0,
        queryForm: {
          pid: 0,
          pageNo: 1,
          pageSize: 10,
          Pname: '',
        },
      })
      const handleDrawer = (row) => {
        state.opDrawer = true
        state.pModelID = row.id
      }
      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.inquireData(state.queryForm.pid)
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
        if (state.queryForm.pid > 0) {
          state.btnState = false
          const {
            data: { list, total },
          } = await getList(state.queryForm)
          state.list = list
          state.total = total
        }

        state.listLoading = false
      }

      const inquireData = (id) => {
        state.queryForm.pid = id
        fetchData()
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
        inquireData,
        handleDrawer,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
