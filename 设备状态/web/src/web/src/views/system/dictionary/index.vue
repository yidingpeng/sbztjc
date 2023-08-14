<template>
  <div class="dict-management-container">
    <el-row :gutter="20">
      <!-- 左侧 字典父级 -->
      <el-col :lg="8" :md="8" :sm="24" :xl="9" :xs="24">
        <vab-card shadow="hover">
          <vab-query-form>
            <vab-query-form-top-panel>
              <el-form :inline="true" :model="queryForm" @submit.prevent>
                <el-form-item>
                  <el-input
                    v-model.trim="queryForm.DictionaryText"
                    clearable
                    placeholder="请输入字典名称"
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
            :data="list"
            highlight-current-row
            @current-change="selectedRowChange"
            @selection-change="setSelectRows"
          >
            <el-table-column
              align="center"
              show-overflow-tooltip
              type="selection"
            />
            <el-table-column
              align="center"
              label="字典类型"
              prop="dictionaryText"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="字典值"
              prop="dictionaryValue"
              show-overflow-tooltip
            />
            <el-table-column align="center" label="操作">
              <template #default="{ row }">
                <el-button
                  v-permissions="{ permission: ['dictionary:edit'] }"
                  link
                  type="primary"
                  @click="handleEdit(row)"
                >
                  编辑
                </el-button>
                <el-button
                  v-permissions="{ permission: ['dictionary:delete'] }"
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

      <!-- 右侧 子级 -->
      <el-col :lg="16" :md="16" :sm="24" :xl="15" :xs="24">
        <vab-card shadow="hover">
          <vab-query-form>
            <vab-query-form-top-panel :span="12">
              <el-button
                v-permissions="{ permission: ['dictionary:add'] }"
                :disabled="disableAddChildren"
                :icon="Plus"
                type="primary"
                @click="handleEditChildren"
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
            v-loading="listLoadingChildren"
            border
            :data="listChildren"
            style="width: 100%"
            @selection-change="setSelectRows"
          >
            <el-table-column
              align="center"
              show-overflow-tooltip
              type="selection"
            />
            <!-- <el-table-column align="center" label="父级ID" prop="parentId" /> -->
            <el-table-column
              align="center"
              label="字典名称"
              prop="dictionaryText"
            />
            <el-table-column
              align="center"
              label="字典值"
              prop="dictionaryValue"
            />
            <el-table-column align="center" label="备注" prop="description" />
            <el-table-column align="center" label="操作">
              <template #default="{ row }">
                <el-button
                  v-permissions="{ permission: ['dictionary:edit'] }"
                  link
                  type="primary"
                  @click="handleEditChildren(row)"
                >
                  编辑
                </el-button>
                <el-button
                  v-permissions="{ permission: ['dictionary:delete'] }"
                  link
                  type="primary"
                  @click="handleDelete(row)"
                >
                  删除
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </vab-card>
      </el-col>
    </el-row>
    <edit ref="editRef" @fetch-data="fetchData" />
    <EditChildren ref="editRefChildren" @fetch-data="fetchDataChildren" />
  </div>
</template>

<script>
  import { getList, doDelete } from '@/api/system/dictionary'
  import Edit from './components/SystemDictionaryEdit.vue'
  import EditChildren from './components/SystemDictionaryEditChildren.vue'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'SystemDictionary',
    components: { Edit, EditChildren },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        editRefChildren: null,
        list: [],
        listChildren: [],
        listLoading: true,
        listLoadingChildren: false,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        totalChildren: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          DictionaryText: '',
          ParentId: 0,
        },
        queryFormChildren: {
          ParentId: -1,
        },
        disableAddChildren: true,
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
      const handleEditChildren = (row) => {
        if (row.id) {
          state.editRefChildren.showEdit(row, state.queryFormChildren.ParentId)
        } else {
          state.editRefChildren.showEdit(
            false,
            state.queryFormChildren.ParentId
          )
        }
      }
      const handleDelete = (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
            await fetchDataChildren()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await doDelete({ ids })
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              await fetchData()
              await fetchDataChildren()
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
      const selectedRowChange = (val) => {
        //console.log('page', val)
        if (val) state.queryFormChildren.ParentId = val.id
        state.disableAddChildren = false
        fetchDataChildren()
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
      const fetchDataChildren = async () => {
        state.listLoadingChildren = true
        const {
          data: { list, total },
        } = await getList(state.queryFormChildren)
        state.listChildren = list
        state.totalChildren = total
        state.listLoadingChildren = false
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleEdit,
        handleEditChildren,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        selectedRowChange,
        queryData,
        fetchData,
        fetchDataChildren,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
