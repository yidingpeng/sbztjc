<template>
  <div class="system-configuration-container">
    <el-row :gutter="20">
      <!-- 左侧 产品信息 -->
      <el-col :lg="8" :md="8" :sm="24" :xl="9" :xs="24">
        <vab-card shadow="hover">
          <vab-query-form>
            <vab-query-form-top-panel>
              <el-form :inline="true" :model="queryForm" @submit.prevent>
                <el-form-item>
                  <el-input
                    v-model.trim="queryForm.pname"
                    clearable
                    placeholder="请输入产品名称"
                  />
                </el-form-item>
                <el-form-item>
                  <el-button :icon="Search" type="primary" @click="queryData">
                    查询
                  </el-button>
                </el-form-item>
              </el-form>
            </vab-query-form-top-panel>
            <vab-query-form-top-panel>
              <el-button
                v-permissions="{ permission: ['dictionary:add'] }"
                :icon="Plus"
                type="primary"
                @click="handleEdit"
              >
                添加
              </el-button>
              <el-button
                v-permissions="{ permission: ['dictionary:delete'] }"
                :icon="Delete"
                type="danger"
                @click="handleDelete"
              >
                批量删除
              </el-button>
            </vab-query-form-top-panel>
          </vab-query-form>

          <el-table
            v-loading="listLoading"
            border
            :data="list"
            highlight-current-row
            @current-change="selectedRowChange"
            @row-dblclick="handleEdit"
            @selection-change="setSelectRows"
          >
            <el-table-column show-overflow-tooltip type="selection" />
            <el-table-column
              align="center"
              label="产品名称"
              prop="pname"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="产品编码"
              prop="pCode"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="状态"
              prop="pstatus"
              show-overflow-tooltip
            >
              <template #default="{ row }">
                <el-tag v-if="row.pstatus == 0" effect="dark" type="success">
                  启用
                </el-tag>
                <el-tag v-else effect="dark" type="danger">禁用</el-tag>
              </template>
            </el-table-column>
            <el-table-column align="center" label="操作" width="180">
              <template #default="{ row }">
                <el-button
                  v-permissions="{ permission: ['production:edit'] }"
                  link
                  type="primary"
                  @click="handleEdit(row)"
                >
                  编辑
                </el-button>
                <el-button
                  v-permissions="{ permission: ['production:delete'] }"
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
        </vab-card>
      </el-col>

      <!-- 右侧 产品型号 -->
      <el-col :lg="16" :md="16" :sm="24" :xl="15" :xs="24">
        <vab-card shadow="hover">
          <pModel ref="editPModel" />
        </vab-card>
      </el-col>
    </el-row>
    <edit ref="editRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import { getList, doDelete } from '@/api/basics/production'
  import Edit from './components/BasicsProductionEdit'
  import PModel from '@/views/Basics/productionmodel/index.vue'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'BasicsProduction',
    components: { Edit, PModel },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        editPModel: null,
        list: [],
        modellist: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        Pid: 0,
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

      const selectedRowChange = (val) => {
        state.editPModel.inquireData(val.id)
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
        selectedRowChange,
        queryData,
        fetchData,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
