<template>
  <div class="system-organization-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['organization:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['organization:delete'] }"
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
              v-model.trim="queryForm.key"
              clearable
              placeholder="请输入组织名称或组织编号"
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

    <el-table
      v-loading="listLoading"
      :data="list"
      default-expand-all
      row-key="id"
      @selection-change="setSelectRows"
    >
      <el-table-column show-overflow-tooltip type="selection" />
      <el-table-column label="组织编号" prop="code" show-overflow-tooltip />
      <el-table-column label="组织名称" prop="name" show-overflow-tooltip />
      <el-table-column
        label="组织负责人"
        prop="leaderName"
        show-overflow-tooltip
      />
      <el-table-column
        label="组织类型"
        prop="organizationTypeName"
        show-overflow-tooltip
      />
      <el-table-column label="组织描述" prop="desc" show-overflow-tooltip />
      <el-table-column label="操作" width="180">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['organization:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['organization:delete'] }"
            link
            type="primary"
            @click="handleDelete(row)"
          >
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <edit ref="editRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import { getList, doDelete } from '@/api/system/organization'
  import Edit from './components/SystemOrganizationEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'SystemOrganization',
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
        selectRows: '',
        queryForm: {
          key: '',
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
        let msgTxt = '你确定要删除当前项吗'
        if (row.children != null) {
          msgTxt = '当前选中父项还存有子项，你确定要删除当前项吗'
        }
        if (row.id) {
          $baseConfirm(msgTxt, null, async () => {
            const { msg } = await doDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm(msgTxt, null, async () => {
              const { msg } = await doDelete({ ids })
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              await fetchData()
            })
          } else {
            $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
          }
        }
      }
      const queryData = () => {
        fetchData()
      }
      const fetchData = async () => {
        state.listLoading = true
        const { data } = await getList(state.queryForm)
        state.list = data
        console.log(data)
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
        queryData,
        fetchData,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
