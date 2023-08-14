<template>
  <div class="fifomanage-container">
    <vab-query-form>
      <vab-query-form-top-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="出入库类型">
            <el-select
              v-model="queryForm.type"
              clearable
              disabled
              placeholder="请选择出入库类型"
            >
              <el-option label="入库" value="1" />
              <el-option label="出库" value="2" />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item label="物料编码">
            <el-input
              v-model.trim="queryForm.materialCode"
              clearable
              placeholder="请输入物料编码或名称"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="项目编码">
            <el-input
              v-model.trim="queryForm.projectCode"
              clearable
              placeholder="请输入项目编号或名称"
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
          v-permissions="{ permission: ['fifomanage:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <!-- <el-button
          v-permissions="{ permission: ['fifomanage:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button> -->
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table v-loading="listLoading" :data="list">
      <!-- <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="出入库类型"
        prop="type"
        show-overflow-tooltip
      >
        <template #default="scope">
          <span v-if="scope.row.type == 1">入库</span>
          <span v-if="scope.row.type != 1">出库</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="BOM编码"
        prop="bomCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="BOM名称"
        prop="bomName"
        show-overflow-tooltip
      /> -->
      <el-table-column
        align="center"
        label="项目名称"
        prop="projectName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目编号"
        prop="projectCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料编码"
        prop="materialCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料名称"
        prop="materialName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="二维码编号"
        prop="qrCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="数量"
        prop="count"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="领料人员"
        prop="fifoPersonnel"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="出库时间"
        prop="fifoDateTime"
        width="160"
      />
      <el-table-column align="center" label="库存" prop="kuCun" />
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="120">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['fifomanage:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['fifomanage:delete'] }"
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
  import { FifoPageList, FifoDelete } from '@/api/purchase/purchase'
  import Edit from './components/fifomanageEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'Fifomanage',
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
          title: '',
          type: '2',
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
            const { msg } = await FifoDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await FifoDelete({ ids })
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
        //state.queryForm.type = '2'
        fetchData()
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await FifoPageList(state.queryForm)
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
