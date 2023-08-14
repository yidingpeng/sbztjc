<template>
  <div class="system-module-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['processinfo:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['material:delete'] }"
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
              v-model.trim="queryForm.pcsName"
              clearable
              placeholder="请输入工序名称"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="fetchData">
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
      @row-dblclick="handleEdit"
      @selection-change="setSelectRows"
    >
      <el-table-column show-overflow-tooltip type="selection" />
      <el-table-column label="工序编号" prop="pcsNo" show-overflow-tooltip>
        <template #default="{ row }">
          {{ row.pcsNo }}
        </template>
      </el-table-column>
      <el-table-column label="工序名称" prop="pcsName" show-overflow-tooltip />
      <el-table-column
        label="是否是总装/总拆工序	"
        prop="isFinishGW"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <el-tag v-if="row.isFinishGW == 0" effect="dark" type="success">
            是
          </el-tag>
          <el-tag v-else effect="dark" type="danger">否</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="状态" prop="usingFlag" show-overflow-tooltip>
        <template #default="{ row }">
          <el-tag v-if="row.usingFlag == 0" effect="dark" type="warning">
            保存
          </el-tag>
          <el-tag v-else-if="row.usingFlag == 1" effect="dark" type="success">
            下发
          </el-tag>
          <el-tag v-else effect="dark" type="danger">禁用</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['processinfo:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['processinfo:delete'] }"
            link
            type="primary"
            @click="handleDelete(row)"
          >
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <edit ref="editRef" :opt-parent="list" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import { getList, doDelete } from '@/api/basics/processinfo'
  import Edit from './components/BasicsProcessInfoEdit'

  import { Plus } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'SystemModule',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        list: [],
        selectRows: '',
        listLoading: true,
        opDrawer: false,
        currModule: 2,
        queryForm: {
          pcsName: '',
        },
      })
      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          // eslint-disable-next-line
          const { children, ...formData } = row
          state.editRef.showEdit(formData)
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
          console.log(state.selectRows)
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
      const handleDrawer = (row) => {
        state.opDrawer = true
        state.currModule = row.id
      }

      const fetchData = async () => {
        state.listLoading = true
        const { data } = await getList(state.queryForm)
        state.list = data
        state.listLoading = false
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        handleEdit,
        handleDelete,
        handleDrawer,
        fetchData,
        setSelectRows,
        Plus,
      }
    },
  })
</script>
