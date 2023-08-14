<template>
  <div class="system-configuration-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['productextend:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['productextend:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button>
      </vab-query-form-left-panel>
      <!--  <vab-query-form-right-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.devName"
              clearable
              placeholder="请输入设备名称"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-right-panel> -->
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
        label="产品型号"
        prop="pModelTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="字段名称"
        prop="colName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="界面列名称"
        prop="textName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="界面列说明"
        prop="textMemo"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="字段值"
        prop="extendValue"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="180">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['productextend:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['productextend:delete'] }"
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
  import { getList, doDelete } from '@/api/basics/productextend'
  import Edit from './components/BasicsProductExtendEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'BasicsProductExtend',
    components: { Edit },
    props: {
      module: {
        type: Number,
        default: () => {
          return 0
        },
      },
    },
    setup(props) {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        pModelID: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          pModelID: '',
        },
      })

      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.inquireData(state.queryForm.pModelID)
          state.editRef.showEdit()
        }
      }
      const handleDelete = (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData(state.pModelID)
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await doDelete({ ids })
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              await fetchData(state.pModelID)
            })
          } else {
            $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
          }
        }
      }
      const handleSizeChange = (val) => {
        state.queryForm.pageSize = val
        fetchData(state.pModelID)
      }
      const handleCurrentChange = (val) => {
        state.queryForm.pageNo = val
        fetchData(state.pModelID)
      }
      const queryData = () => {
        state.queryForm.pageNo = 1
        fetchData(state.pModelID)
      }
      const fetchData = async (module) => {
        module > 0 ? (state.queryForm.pModelID = module) : ''

        state.listLoading = true
        if (state.queryForm.pModelID > 0) {
          const {
            data: { list, total },
          } = await getList(state.queryForm)
          state.list = list
          state.total = total
        }
        state.listLoading = false
      }

      watch(
        () => props.module,
        (newVal) => {
          fetchData(newVal)
        },
        { immediate: true }
      )
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
