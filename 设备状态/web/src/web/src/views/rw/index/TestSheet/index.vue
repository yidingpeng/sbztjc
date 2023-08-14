<!-- <template>
  <div class="system-configuration-container">
    <el-row :gutter="20">
      <edit ref="editRef" @fetch-data="fetchData" />
    </el-row>

  </div>
</template>

<script>
import { getList, doDelete } from '@/api/basics/production'
import Edit from './components/Testsheetedit'
import PModel from '@/views/Basics/productionmodel/index.vue'
import { Delete, Plus, Search } from '@element-plus/icons-vue'

export default defineComponent({
  name: 'BasicsProduction',
  components: { Edit },
  setup() {
    const $baseConfirm = inject('$baseConfirm')
    const $baseMessage = inject('$baseMessage')

    const state = reactive({
      editRef: null,

    })


    const handleEdit = () => {

      state.editRef.showEdit()



    }


    onMounted(() => {

      handleEdit()
    })

    return {
      ...toRefs(state),

      handleEdit,

      Delete,
      Plus,
      Search,
    }
  },
})
</script> -->
<template>
  <div class="role-management-container">
    <el-row :gutter="20">
      <!-- 左侧 字典父级 -->
      <el-col :lg="24" :md="24" :sm="24" :xl="24" :xs="24">
        <vab-card shadow="hover">
          <template #header>
            <div class="card-header">
              <div>试验单</div>
            </div>
          </template>
          <vab-query-form>
            <vab-query-form-left-panel :span="10">
              <el-button v-permissions="{ permission: ['role:add'] }" :icon="Plus" type="primary"
                @click="handleEdit($event)">
                试验单添加
              </el-button>
              <!-- <el-button v-permissions="{ permission: ['role:delete'] }" :icon="Delete" type="danger"
                @click="handleDelete">
                批量删除
              </el-button> -->
            </vab-query-form-left-panel>
            <vab-query-form-left-panel :span="14">
              <el-form :inline="true" :model="queryForm" @submit.prevent>
                <el-form-item>
                  <el-input v-model.trim="queryForm.pname" clearable placeholder="请输入试验名称" />
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
            <el-table-column align="center" label="试验间" prop="deviceRoom" show-overflow-tooltip />
            <el-table-column align="center" label="试验设备名称" prop="deviceName" show-overflow-tooltip />
            <el-table-column align="center" label="试验项目名称" prop="projectName" show-overflow-tooltip />
            <el-table-column align="center" label="实验员工号" prop="employeeId" show-overflow-tooltip />
            <el-table-column align="center" label="试验工程师" prop="testEngineer" show-overflow-tooltip />
            <el-table-column align="center" label="项目开始时间" prop="startTime" show-overflow-tooltip />
            <el-table-column align="center" label="项目结束时间" prop="endTime" show-overflow-tooltip
              :formatter="formatEndTime" />
            <el-table-column align="center" label="备注" prop="remark" show-overflow-tooltip />
            <!-- <el-table-column align="center" label="操作" show-overflow-tooltip>
              <template #default="{ row }">
                <el-button v-permissions="{ permission: ['role:edit'] }" link type="primary" @click="handleEdit(row)">
                  编辑
                </el-button>
                <el-button v-permissions="{ permission: ['role:delete'] }" link type="primary" @click="handleDelete(row)">
                  删除
                </el-button>
              </template>
            </el-table-column> -->
            <el-table-column align="center" label="操作" show-overflow-tooltip>
              <template #default="{ row }">
                <v-if v-if="true">
                  <el-button v-permissions="{ permission: ['role:edit'] }" type="primary"
                    @click="handleEdit(row, 'edit')">
                    输入结束时间/修改
                  </el-button>
                  <!-- <el-button type="info" disabled="true" style="margin-left:10px ;">
                    编辑
                  </el-button> -->
                </v-if>
                <v-if v-if="row.reportingStatus == 1">
                  <el-button disabled="true" type="info">
                    已填故障原因
                  </el-button>
                  <!-- <el-button v-permissions="{ permission: ['role:edit'] }" type="primary"
                    @click="handleEdit(row, 'edits')" style="margin-left:10px ;">
                    编辑
                  </el-button> -->
                </v-if>
              </template>
              <!-- <v-if v-if="row.currentState == 1">
                  <el-button type="primary" @click="goTo(row)">
                    查看履历
                  </el-button>
                  <el-button type="success">已完成</el-button>
                </v-if>
                <v-if v-if="row.currentState == 0">
                  <el-button disabled="true" type="primary" @click="goTo(row)">
                    查看履历
                  </el-button>
                  <el-button type="danger">未完成</el-button>
                </v-if> -->
            </el-table-column>
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
import { getAllList, doDelete } from '@/api/orders/sprayingAmount'
import { getTestSheetAllList } from '@/api/DeviceStatus/DeviceStatusTag'
export default defineComponent({
  name: 'RoleManagement',
  components: {
    edit: defineAsyncComponent(() =>
      import('./Testsheetedit')
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
        pname: '',
      },
    })

    const setSelectRows = (val) => {
      state.selectRows = val
    }
    const formatEndTime = (row) => {
      if (row.endTime === '0001-01-01 00:00:00') {
        return '';
      }
      return row.endTime;
    }
    const handleEdit = (row) => {
      if (row.id) {
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
      } = await getTestSheetAllList(state.queryForm)
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
      formatEndTime,
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
