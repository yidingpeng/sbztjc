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
              <div>故障填报</div>
            </div>
          </template>


          <el-table v-loading="listLoading" border :data="list" highlight-current-row @selection-change="setSelectRows">
            <el-table-column align="center" show-overflow-tooltip type="selection" />
            <el-table-column align="center" label="故障试验间" prop="testRoom" show-overflow-tooltip />
            <el-table-column align="center" label="故障时间" prop="faultDateTime" show-overflow-tooltip />
            <el-table-column align="center" label="故障设备名称" prop="deviceName" show-overflow-tooltip />
            <el-table-column align="center" label="故障原因" prop="faultReason" show-overflow-tooltip />
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
                <v-if v-if="row.reportingStatus == 0">
                  <el-button v-permissions="{ permission: ['role:add'] }" type="primary" @click="handleadd(row, 'adds')">
                    故障原因填报
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
          <adds ref="adds" @fetch-data="fetchData" />
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
import { GetList } from '@/api/DeviceStatus/FaultReporting'
import { color } from 'echarts'
export default defineComponent({
  name: 'RoleManagement',
  components: {
    edit: defineAsyncComponent(() =>
      import('./Faultedit')
    ),
    adds: defineAsyncComponent(() =>
      import('./Faulteadd')
    ),
  },
  setup() {
    const $baseConfirm = inject('$baseConfirm')
    const $baseMessage = inject('$baseMessage')

    const state = reactive({
      editRef: null,
      adds: null,
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
        deviceName: '',
      },
    })

    const setSelectRows = (val) => {
      state.selectRows = val
    }

    const handleEdit = (row, addoredit) => {

      state['editRef'].showEdit(row, 'edits')

    }
    const handleadd = (row, addoredit) => {

      state['adds'].showadd(row, 'edit')

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
      state.listLoading = false
      const {
        data: { list, total },
      } = await GetList(state.queryForm)
      state.list = list
      state.total = total
      //state.listLoading = false
    }

    onMounted(() => {
      fetchData()
      setInterval(() => {
        fetchData()
      }, 3000)
    })



    return {
      ...toRefs(state),
      setSelectRows,
      handleEdit, handleadd,
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
