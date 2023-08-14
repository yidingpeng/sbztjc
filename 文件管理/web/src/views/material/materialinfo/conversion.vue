<template>
  <div class="material-material-info-container">
    <vab-query-form>
      <vab-query-form-top-panel>
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="物料编号">
            <el-input
              v-model.trim="queryForm.code"
              clearable
              placeholder="请输入物料编号"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="物料名称">
            <el-input
              v-model.trim="queryForm.name"
              clearable
              placeholder="请输入物料名称"
            />
          </el-form-item>
          <el-form-item>
            <el-button
              :icon="Search"
              native-type="submit"
              type="primary"
              @click="queryData"
            >
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-top-panel>
      <vab-query-form-left-panel :span="24">
        <el-button
          v-permissions="{ permission: ['conversion:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>
    <el-table v-loading="listLoading" border :data="list">
      <el-table-column
        align="center"
        label="物料编码"
        prop="code"
        show-overflow-tooltip
        width="200px"
      />
      <el-table-column
        align="center"
        label="物料名称"
        prop="name"
        show-overflow-tooltip
        width="300px"
      />
      <el-table-column
        align="center"
        label="规格型号"
        prop="model"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="基本单位"
        prop="basicUnit"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="换算单位"
        prop="hsUnit"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="数量(N:1)"
        prop="count"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="100">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['conversion:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            编辑
          </el-link>
          <el-link
            v-permissions="{ permission: ['conversion:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            删除
          </el-link>
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
  import { GetHsPageList, MatHsDoDelete } from '@/api/materialMaterialInfo'
  import Edit from './components/ConversionEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'MaterialMaterialInfo',
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
        queryForm: {
          pageNo: 1,
          pageSize: 10,
        },
      })
      //删除
      const handleDelete = (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await MatHsDoDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          $baseMessage('操作异常', 'error', 'vab-hey-message-error')
        }
      }
      //修改
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.showEdit()
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
      //加载分页
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await GetHsPageList(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
      }

      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        handleEdit,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        handleDelete,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
