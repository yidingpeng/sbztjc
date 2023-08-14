<template>
  <div class="supplier-container">
    <vab-query-form>
      <vab-query-form-top-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="供应商编码">
            <el-input
              v-model.trim="queryForm.supCode"
              clearable
              placeholder="请输入编码"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="供应商名称">
            <el-input
              v-model.trim="queryForm.supName"
              clearable
              placeholder="请输入名称"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="联系人">
            <el-input
              v-model.trim="queryForm.supPrincipal"
              clearable
              placeholder="请输入联系人"
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
          v-permissions="{ permission: ['supplier:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['supplier:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="物料属性"
        prop="matProperty"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="供应商编码"
        prop="supCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="供应商名称"
        prop="supName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="主要供货品牌"
        prop="supplyBrand"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="主要供货类别"
        prop="supplyType"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="公司性质"
        prop="companyType"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="联系人"
        prop="supPrincipal"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="联系方式"
        prop="contactDetails"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="地址"
        prop="address"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="120">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['supplier:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['supplier:delete'] }"
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
  import { supplierPageList, SupplierDel } from '@/api/purchase/purchase'
  import Edit from './components/SupplierEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'Supplier',
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
            const { msg } = await SupplierDel({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await SupplierDel({ ids })
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
        } = await supplierPageList(state.queryForm)
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
