<template>
  <div class="system-user-container">
    <el-row :gutter="20">
      <el-col :lg="24">
        <vab-query-form>
          <vab-query-form-left-panel :span="12">
            <el-button
              v-permissions="{ permission: ['user:add'] }"
              :icon="Plus"
              type="primary"
              @click="handleEdit"
            >
              添加
            </el-button>
            <el-button
              v-permissions="{ permission: ['user:delete'] }"
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
                  v-model.trim="queryForm.Username"
                  clearable
                  placeholder="请输入姓名"
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
      </el-col>
      <el-col :lg="4" :md="8" :sm="24" :xl="4" :xs="24">
        <vab-card shadow="hover">
          <el-tree
            :data="data"
            default-expand-all
            :expand-on-click-node="false"
            node-key="id"
            :props="treeProps"
            @node-click="handleNodeClick"
          />
        </vab-card>
      </el-col>
      <el-col :lg="20" :md="16" :sm="24" :xl="20" :xs="24">
        <el-table
          v-loading="listLoading"
          border
          :data="list"
          @selection-change="setSelectRows"
        >
          <el-table-column show-overflow-tooltip type="selection" />
          <el-table-column label="id" prop="id" show-overflow-tooltip />
          <el-table-column label="头像" prop="avatar" show-overflow-tooltip>
            <template #default="{ row }">
              <el-avatar
                shape="square"
                :size="40"
                :src="baseURL + '/upload/image/' + row.avatar"
              />
            </template>
          </el-table-column>
          <el-table-column
            label="用户名"
            prop="account"
            show-overflow-tooltip
          />
          <el-table-column label="姓名" prop="fullname" show-overflow-tooltip />
          <el-table-column label="性别" prop="sex" show-overflow-tooltip />
          <el-table-column
            label="部门"
            prop="department"
            show-overflow-tooltip
          />
          <el-table-column label="角色" prop="roles" show-overflow-tooltip>
            <template #default="{ row }">
              <el-tag v-for="item in row.roles" :key="item">{{ item }}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="联系电话" prop="telnum" show-overflow-tooltip>
            <!-- <template #default="{ row }">
              <el-link type="primary">{{ row.telnum }}</el-link>
            </template> -->
          </el-table-column>

          <el-table-column label="操作" width="160">
            <template #default="{ row }">
              <el-button
                v-permissions="{ permission: ['user:reset'] }"
                type="warning"
                @click="handleResetPwd(row)"
              >
                重置密码
              </el-button>
              <br />
              <el-button
                v-permissions="{ permission: ['user:edit'] }"
                link
                type="primary"
                @click="handleEdit(row)"
              >
                编辑
              </el-button>
              <el-button
                v-permissions="{ permission: ['user:delete'] }"
                link
                type="primary"
                @click="handleDelete(row)"
              >
                删除
              </el-button>
              <el-button
                v-permissions="{ permission: ['user:purview'] }"
                link
                type="primary"
                @click="handleDrawer(row)"
              >
                权限
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
      </el-col>
    </el-row>
    <edit ref="editRef" @fetch-data="fetchData" />
    <el-drawer
      v-model="opDrawer"
      size="50%"
      title="用户权限"
      @closed="handleClosed"
      @opened="handleOpened"
    >
      <RwPermission ref="permissionRef" height="100%" mode="user" />
      <template #footer>
        <div style="flex: auto">
          <el-button type="primary">保存</el-button>
        </div>
      </template>
    </el-drawer>
  </div>
</template>

<script>
  import { getList, doDelete, doResetPwd, GetUserId } from '@/api/system/user'
  import { getTree } from '@/api/system/organization'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  import { getImage } from '~/src/api/system/upload'
  import { baseURL } from '~/src/config'

  export default defineComponent({
    name: 'User',
    components: {
      Edit: defineAsyncComponent(() => import('./components/SystemUserEdit')),
      RwPermission: defineAsyncComponent(() =>
        import('@/plugins/RwPermission')
      ),
    },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        opDrawer: false,
        currUser: 2,
        editRef: null,
        permissionRef: null,
        data: [],
        list: [],
        defaultProps: {
          children: 'children',
          label: 'label',
        },
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
        },
      })

      const treeProps = {}

      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleDrawer = (row) => {
        state.opDrawer = true
        state.currUser = row.id
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.showEdit()
        }
      }

      const handleOpened = () => {
        state.permissionRef.load(state.currUser)
      }

      const handleClosed = () => {
        state.permissionRef.clear()
      }

      getTree().then(({ data }) => {
        const list = [{ id: 0, label: '所有', children: data }]
        state.data = list
      })
      const handleNodeClick = ({ id }) => {
        state.queryForm.organization = id
        fetchData()
      }
      const handleResetPwd = (row) => {
        if (row.id) {
          $baseConfirm('确定要重置密码吗', null, async () => {
            const { msg } = await doResetPwd({ username: row.account })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
          })
        }
      }
      const handleDelete = async (row) => {
        const loginUserId = await GetUserId()
        if (row.id) {
          if (row.id == loginUserId.userId) {
            $baseMessage('不能删除自身账号', 'error', 'vab-hey-message-error')
            return false
          }
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            let isreturn = false
            state.selectRows.forEach((element) => {
              if (element.id == loginUserId.userId) {
                isreturn = true
              }
            })
            if (isreturn) {
              $baseMessage('不能删除自身账号', 'error', 'vab-hey-message-error')
              return
            }
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
        state.list.forEach((item) => {
          if (item.avatar)
            getImage(item.avatar).then((data) => {
              item.avatarData = data
            })
        })
        state.total = total
        state.listLoading = false
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleNodeClick,
        handleEdit,
        handleDelete,
        handleResetPwd,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        handleDrawer,
        handleOpened,
        handleClosed,
        Delete,
        Plus,
        Search,
        treeProps,
        baseURL,
      }
    },
  })
</script>
