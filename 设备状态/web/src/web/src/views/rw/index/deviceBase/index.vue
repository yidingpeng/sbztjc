<template>
  <div class="role-management-container">
    <el-row :gutter="20">
      <!-- 左侧 字典父级 -->
      <el-col :lg="24" :md="24" :sm="24" :xl="24" :xs="24">
        <vab-card shadow="hover">
          <template #header>
            <div class="card-header">
              <div>试验间管理</div>
            </div>
          </template>
          <vab-query-form>
            <vab-query-form-left-panel :span="10">
              <el-button v-permissions="{ permission: ['role:add'] }" :icon="Plus" type="primary"
                @click="handleEdit($event)">
                添加
              </el-button>
              <!-- <el-button v-permissions="{ permission: ['role:delete'] }" :icon="Delete" type="danger"
                @click="handleDelete">
                批量删除
              </el-button> -->
            </vab-query-form-left-panel>
            <vab-query-form-left-panel :span="14">
              <el-form :inline="true" :model="queryForm" @submit.prevent>
                <el-form-item>
                  <el-input v-model.trim="queryForm.roomName" clearable placeholder="请输入试验间" />
                </el-form-item>
                <el-form-item>
                  <el-button :icon="Search" type="primary" @click="queryData">
                    查询
                  </el-button>
                </el-form-item>
              </el-form>
            </vab-query-form-left-panel>
          </vab-query-form>

          <el-table v-loading="listLoading" border :data="roomlist" highlight-current-row
            @selection-change="setSelectRows">
            <el-table-column align="center" show-overflow-tooltip type="selection" />
            <el-table-column align="center" label="id" prop="id" show-overflow-tooltip />
            <el-table-column align="center" label="试验间" prop="roomName" show-overflow-tooltip />
            <el-table-column align="center" label="操作" show-overflow-tooltip>
              <template #default="{ row }">
                <el-button v-permissions="{ permission: ['role:edit'] }" link type="primary" @click="handleEdit(row)">
                  编辑
                </el-button>
                <el-button v-permissions="{ permission: ['role:delete'] }" link type="primary" @click="handleDelete(row)">
                  删除
                </el-button>
              </template>
            </el-table-column>
          </el-table>
          <edit ref="roomeditRef" @dfetch-data="dfetchData" @fetch-data="fetchData" />
          <el-pagination background :current-page="queryForm.pageNo" :layout="layout" :page-size="queryForm.pageSize"
            :total="total" @current-change="handleCurrentChange" @size-change="handleSizeChange" />
        </vab-card>
      </el-col>
    </el-row>
    <el-row :gutter="20">
      <!-- 左侧 字典父级 -->
      <el-col :lg="24" :md="24" :sm="24" :xl="24" :xs="24">
        <vab-card shadow="hover">
          <template #header>
            <div class="card-header">
              <div>设备管理</div>
            </div>
          </template>
          <vab-query-form>
            <vab-query-form-left-panel :span="10">
              <el-button v-permissions="{ permission: ['role:add'] }" :icon="Plus" type="primary"
                @click="dhandleEdit($event)">
                添加
              </el-button>
              <!-- <el-button v-permissions="{ permission: ['role:delete'] }" :icon="Delete" type="danger"
                @click="dhandleDelete">
                批量删除
              </el-button> -->
            </vab-query-form-left-panel>
            <vab-query-form-left-panel :span="14">
              <el-form :inline="true" :model="dqueryForm" @submit.prevent>
                <el-form-item>
                  <el-input v-model.trim="dqueryForm.deviceName" clearable placeholder="请输入实验设备" />
                </el-form-item>
                <el-form-item>
                  <el-button :icon="Search" type="primary" @click="dqueryData">
                    查询
                  </el-button>
                </el-form-item>
              </el-form>
            </vab-query-form-left-panel>
          </vab-query-form>

          <el-table v-loading="listLoading" border :data="devicelist" highlight-current-row
            @selection-change="dsetSelectRows">
            <el-table-column align="center" show-overflow-tooltip type="selection" />
            <el-table-column align="center" label="试验间" prop="roomName" show-overflow-tooltip />
            <el-table-column align="center" label="试验设备" prop="deviceName" show-overflow-tooltip />
            <el-table-column align="center" label="操作" show-overflow-tooltip>
              <template #default="{ row }">
                <el-button v-permissions="{ permission: ['role:edit'] }" link type="primary" @click="dhandleEdit(row)">
                  编辑
                </el-button>
                <el-button v-permissions="{ permission: ['role:delete'] }" link type="primary"
                  @click="dhandleDelete(row)">
                  删除
                </el-button>
              </template>
            </el-table-column>
          </el-table>
          <deviceedit ref="deviceeditRef" @dfetch-data="dfetchData" @fetch-data="fetchData" />
          <el-pagination background :current-page="dqueryForm.pageNo" :layout="layout" :page-size="dqueryForm.pageSize"
            :total="dtotal" @current-change="dhandleCurrentChange" @size-change="dhandleSizeChange" />
        </vab-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { Delete, Plus, Search } from '@element-plus/icons-vue'

import { GetList, doDelete } from '@/api/DeviceStatus/DeviceBaseRoom'
import { dGetList, ddoDelete } from '@/api/DeviceStatus/DeviceBaseDevice'


export default defineComponent({
  name: 'RoleManagement',
  components: {
    edit: defineAsyncComponent(() =>
      import('./deviceBaseRoomEdit')
    ),
    deviceedit: defineAsyncComponent(() =>
      import('./deviceBaseDeviceEdit')
    ),
  },
  setup() {
    const $baseConfirm = inject('$baseConfirm')
    const $baseMessage = inject('$baseMessage')

    const state = reactive({
      roomeditRef: null,
      deviceeditRef: null,
      permissionRef: null,
      roomlist: [],
      devicelist: [],
      listLoading: true,
      saveLoading: false,
      layout: 'total, sizes, prev, pager, next, jumper',
      total: 0, dtotal: 0,
      selectRows: '',
      dselectRows: '',
      queryForm: {
        pageNo: 1,
        pageSize: 10,
        roomName: '',
      },
      dqueryForm: {
        pageNo: 1,
        pageSize: 10,
        deviceName: '',
      },
    })

    const setSelectRows = (val) => {
      state.selectRows = val
    }

    const handleEdit = (row) => {
      if (row.id) {
        state['roomeditRef'].showEdit(row)
      } else {
        state['roomeditRef'].showEdit()
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
        if (state.dselectRows.length > 0) {
          const ids = state.dselectRows.map((item) => item.id).join()
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
      state.roomlist = list
      state.total = total
      state.listLoading = false
    }


    const dsetSelectRows = (val) => {
      state.dselectRows = val
    }

    const dhandleEdit = (row) => {
      if (row.id) {
        console.log(row)
        state['deviceeditRef'].showEdit(row)
      } else {
        state['deviceeditRef'].showEdit()
      }
    }
    const dhandleDelete = (row) => {
      console.log(row.id)
      if (row.id) {
        $baseConfirm('你确定要删除当前项吗', null, async () => {
          const { msg } = await ddoDelete({ ids: row.id })
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          await dfetchData()
        })
      } else {
        if (state.selectRows.length > 0) {
          const ids = state.selectRows.map((item) => item.id).join()
          $baseConfirm('你确定要删除选中项吗', null, async () => {
            const { msg } = await ddoDelete({ ids })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await dfetchData()
          })
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }
    }
    const dhandleSizeChange = (val) => {
      state.dqueryForm.pageSize = val
      dfetchData()
    }
    const dhandleCurrentChange = (val) => {
      state.dqueryForm.pageNo = val
      dfetchData()
    }
    const dqueryData = () => {
      state.dqueryForm.pageNo = 1
      dfetchData()
    }
    const dfetchData = async () => {
      state.listLoading = true
      const {
        data: { list, total },
      } = await dGetList(state.dqueryForm)
      console.log(list)
      state.devicelist = list
      state.dtotal = total
      state.listLoading = false
    }
    onMounted(() => {
      fetchData()
      dfetchData()
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
      dsetSelectRows,
      dhandleEdit,
      dhandleDelete,
      dhandleSizeChange,
      dhandleCurrentChange,
      dqueryData,
      dfetchData,
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
