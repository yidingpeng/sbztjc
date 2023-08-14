<template>
  <div class="role-management-container">
    <el-row :gutter="20">
      <!-- 左侧 字典父级 -->
      <el-col :lg="8" :md="8" :sm="24" :xl="9" :xs="24">
        <vab-card shadow="hover">
          <template #header>
            <div class="card-header">
              <div>角色</div>
            </div>
          </template>
          <vab-query-form>
            <vab-query-form-left-panel :span="10">
              <el-button
                v-permissions="{ permission: ['role:add'] }"
                :icon="Plus"
                type="primary"
                @click="handleEdit($event)"
              >
                添加
              </el-button>
              <el-button
                v-permissions="{ permission: ['role:delete'] }"
                :icon="Delete"
                type="danger"
                @click="handleDelete($event)"
              >
                批量删除
              </el-button>
            </vab-query-form-left-panel>
            <vab-query-form-left-panel :span="14">
              <el-form :inline="true" :model="queryForm" @submit.prevent>
                <el-form-item>
                  <el-input
                    v-model.trim="queryForm.role"
                    clearable
                    placeholder="请输入角色"
                  />
                </el-form-item>
                <el-form-item>
                  <el-button :icon="Search" type="primary" @click="queryData">
                    查询
                  </el-button>
                </el-form-item>
              </el-form>
            </vab-query-form-left-panel>
          </vab-query-form>

          <el-table
            v-loading="listLoading"
            border
            :data="list"
            highlight-current-row
            @row-click="setRowClick"
            @selection-change="setSelectRows"
          >
            <el-table-column
              align="center"
              show-overflow-tooltip
              type="selection"
            />
            <el-table-column
              align="center"
              label="角色名"
              prop="role"
              show-overflow-tooltip
            />
            <el-table-column align="center" label="操作" show-overflow-tooltip>
              <template #default="{ row }">
                <el-button
                  v-permissions="{ permission: ['role:edit'] }"
                  link
                  type="primary"
                  @click="handleEdit(row)"
                >
                  编辑
                </el-button>
                <el-button
                  v-permissions="{ permission: ['role:delete'] }"
                  link
                  type="primary"
                  @click="handleDelete(row)"
                >
                  删除
                </el-button>
              </template>
            </el-table-column>
            <template #empty>
              <el-empty class="vab-data-empty" description="暂无数据" />
            </template>
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
          <edit ref="editRef" @fetch-data="fetchData" @value="1" />
        </vab-card>
      </el-col>

      <!-- 右侧 子级 -->
      <el-col :lg="16" :md="16" :sm="24" :xl="15" :xs="24">
        <vab-card
          v-loading="saveLoading"
          body-style="{height:100%}"
          shadow="hover"
        >
          <template #header>
            <div class="card-header">
              <div>权限</div>
              <div>
                <el-link
                  type="primary"
                  :underline="false"
                  @click="savePermission"
                >
                  保存
                </el-link>
                <el-link type="primary" :underline="false">刷新</el-link>
              </div>
            </div>
          </template>
          <RwPermission ref="permissionRef" height="600px" mode="role" />
        </vab-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
  import { doDelete, getList, updatePermission } from '@/api/system/role'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'RoleManagement',
    components: {
      Edit: defineAsyncComponent(() => import('./components/SystemRoleEdit')),
      RwPermission: defineAsyncComponent(() =>
        import('@/plugins/RwPermission')
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
          role: '',
        },
        roleId: 0,
      })

      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const setRowClick = (val) => {
        state.roleId = val.id
        state.permissionRef.load(state.roleId)
      }
      const handleEdit = (row) => {
        if (row.id) {
          state['editRef'].showEdit(row)
        } else {
          state['editRef'].showEdit()
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

      const savePermission = async () => {
        state.saveLoading = true
        const permission = state.permissionRef.getCheckedAll()
        const data = { roleId: state.roleId, ...permission }
        try {
          const { msg } = await updatePermission(data)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
        } finally {
          state.saveLoading = false
        }
      }

      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        setRowClick,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        savePermission,
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
