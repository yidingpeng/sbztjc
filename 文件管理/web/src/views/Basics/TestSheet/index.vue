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
                  <el-input v-model.trim="queryForm.roomName" clearable placeholder="请输入试验台架" />
                </el-form-item>
                <el-form-item>
                  <el-button :icon="Search" type="primary" @click="queryData">
                    查询
                  </el-button>
                </el-form-item>
              </el-form>
            </vab-query-form-left-panel>
          </vab-query-form>

          <el-table v-loading="listLoading" border :data="list" highlight-current-row @selection-change="setSelectRows" style="width: 100%">
           
            <el-table-column align="center" fixed label="试验台架" prop="roomName" show-overflow-tooltip width="120"/>
            <el-table-column align="center" fixed label="试验编号" prop="testNumber" show-overflow-tooltip width="120"/>
            <el-table-column align="center" label="试验类型" prop="orderType" show-overflow-tooltip width="120" />
            <el-table-column align="center" label="试验类型开始时间" prop="orderTypeStartTime" show-overflow-tooltip :formatter="formatEndTime" width="220"  />
            <el-table-column align="center" label="试验类型结束时间" prop="orderTypeEndTime" show-overflow-tooltip :formatter="formatEndTime" width="220"/>
            <el-table-column align="center" label="运行试验开始时间" prop="operationTestStartTime" show-overflow-tooltip :formatter="formatEndTime" width="220" />
            <el-table-column align="center" label="运行试验结束时间" prop="operationTestEndTime" show-overflow-tooltip :formatter="formatEndTime" width="220"/>
            <el-table-column align="center" label="异常开始时间" prop="abnormalStartTime" show-overflow-tooltip :formatter="formatEndTime" width="220"/>
            <el-table-column align="center" label="异常结束时间" prop="abnormalEndTime" show-overflow-tooltip :formatter="formatEndTime" width="220"/>
            <el-table-column align="center" label="等待物料开始时间" prop="waitMaterialStartTime" show-overflow-tooltip
              :formatter="formatEndTime"  width="220"/>
              <el-table-column align="center" label="等待物料结束时间" prop="waitMaterialEndTime" show-overflow-tooltip
              :formatter="formatEndTime"  width="220"/>
              <el-table-column align="center" label="等待经理开始时间" prop="waitManageStartTime" show-overflow-tooltip
              :formatter="formatEndTime"  width="220"/>
              <el-table-column align="center" label="等待经理结束时间" prop="waitManageEndTime" show-overflow-tooltip
              :formatter="formatEndTime"  width="220"/>
             
            <el-table-column align="center" label="备注" prop="remark" show-overflow-tooltip  width="220"/>
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
            <el-table-column align="center" label="操作" show-overflow-tooltip  fixed="right" width="180">
              <template #default="{ row }">
                
                  <el-button v-permissions="{ permission: ['role:edit'] }" type="primary"
                    @click="handleEdit(row, 'edit')">
                    输入结束时间/修改
                  </el-button>
                  <!-- <el-button type="info" disabled="true" style="margin-left:10px ;">
                    编辑
                  </el-button> -->
                
             
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

import { getTestSheetAllList } from '@/api/devicefile/testsheet'
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
    const formatEndTime = (row,column) => {
      if (row[column.property] === '0001-01-01 00:00:00') {
        return '';
      } else {
        return row[column.property];
      }
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
