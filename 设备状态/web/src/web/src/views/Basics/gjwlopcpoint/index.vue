<template>
  <div class="system-configuration-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['gjwlopcpoint:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['gjwlopcpoint:delete'] }"
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
              v-model.trim="queryForm.gwname"
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
        label="工作中心名称"
        prop="gwname"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="工具名称"
        prop="gjname"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料名称"
        prop="wlname"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料名称"
        prop="opcDeviceName"
        show-overflow-tooltip
      />
      <el-table-column
        v-for="(item, index) in header"
        :key="index"
        align="center"
        :label="item.label"
        :prop="item.name"
      >
        <template #default="{ row }">
          {{ opcvalue(row.opclist, item.name) }}
        </template>
      </el-table-column>
      <el-table-column align="center" label="操作" width="180">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['gjwlopcpoint:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['gjwlopcpoint:delete'] }"
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
  import {
    getList,
    doDelete,
    GetGwOPCTypeList,
  } from '@/api/basics/gjwlopcpoint'
  import Edit from './components/BasicsGjwlOpcPointEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'BasicsGjwlOpcPoint',
    components: { Edit },

    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        header: [],
        editRef: null,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          gwname: '',
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

      //Opc点位类型
      const GetGwOPCTypeData = async () => {
        const GwOPCType = await GetGwOPCTypeList()
        state.header = GwOPCType.data
      }

      const opcvalue = (row, code) => {
        let opc
        row.forEach((item, index) => {
          if (item.code == code) {
            opc = row[index].value
          }
        })
        return opc
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
        GetGwOPCTypeData()
        fetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        opcvalue,
        GetGwOPCTypeData,
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
