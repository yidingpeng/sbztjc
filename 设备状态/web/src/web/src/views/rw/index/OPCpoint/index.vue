<template>
  <div class="role-management-container">
    <el-row :gutter="20">
      <!-- 左侧 字典父级 -->
      <el-col :lg="24" :md="24" :sm="24" :xl="24" :xs="24">
        <vab-card shadow="hover">
          <template #header>
            <div class="card-header">
              <div>点位管理</div>
            </div>
          </template>
          <vab-query-form>
            <vab-query-form-left-panel :span="10">
              <el-button v-permissions="{ permission: ['role:add'] }" :icon="Plus" type="primary"
                @click="handleEdit($event)">
                添加
              </el-button>
              <el-button v-permissions="{ permission: ['role:delete'] }" :icon="Delete" type="danger"
                @click="handleDelete">
                批量删除
              </el-button>
            </vab-query-form-left-panel>
            <vab-query-form-left-panel :span="14">
              <el-form :inline="true" :model="queryForm" @submit.prevent>
                <el-form-item>
                  <el-input v-model.trim="queryForm.address" clearable placeholder="请输入车轴型号" />
                </el-form-item>
                <el-form-item>
                  <el-button :icon="Search" type="primary" @click="queryData">
                    查询
                  </el-button>
                </el-form-item>
              </el-form>
            </vab-query-form-left-panel>
          </vab-query-form>

          <el-table v-loading="listLoading" border :data="list" highlight-current-row @selection-change="setSelectRows">
            <el-table-column align="center" show-overflow-tooltip type="selection" />
            <el-table-column align="center" label="标记名称" prop="tagName" show-overflow-tooltip />
            <el-table-column align="center" label="地址" prop="address" show-overflow-tooltip />
            <el-table-column align="center" label="说明" prop="explainInfo" show-overflow-tooltip />



          </el-table>
          <edit ref="editRef" @fetch-data="fetchData" />
          <el-pagination background :current-page="queryForm.pageNo" :layout="layout" :page-size="queryForm.pageSize"
            :total="total" @current-change="handleCurrentChange" @size-change="handleSizeChange" />
        </vab-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { Delete, Plus, Search } from '@element-plus/icons-vue'
import { GetList, doDelete } from '@/api/DeviceStatus/OPCpoint'
export default defineComponent({
  name: 'RoleManagement',
  components: {
    edit: defineAsyncComponent(() =>
      import('./OPCpointedit.vue')
    ),
  },
  setup() {
    const $baseConfirm = inject('$baseConfirm')
    const $baseMessage = inject('$baseMessage')

    const state = reactive({
      editRef: null,
      permissionRef: null,
      list: [],
      listLoading: true,
      saveLoading: false,
      layout: 'total, sizes, prev, pager, next, jumper',
      total: 0,
      selectRows: '',
      queryForm: {
        pageNo: 1,
        pageSize: 10,
        address: '',
      },
    })

    const setSelectRows = (val) => {
      state.selectRows = val
    }

    const handleEdit = (row) => {
      if (row.id) {
        console.log(row)
        state['editRef'].showEdit(row)
      } else {
        state['editRef'].showEdit()
      }
    }
    const handleDelete = (row) => {
      console.log(row.id)
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
      } = await GetList(state.queryForm)
      console.log(list)
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
<style lang="scss" scoped>
.card-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
</style>
