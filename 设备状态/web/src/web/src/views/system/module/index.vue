<template>
  <div class="system-module-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['module:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      :data="list"
      default-expand-all
      row-key="id"
    >
      <el-table-column label="标题" prop="title" show-overflow-tooltip>
        <template #default="{ row }">
          <vab-icon v-if="row.icon" :icon="row.icon" />
          {{ row.title }}
        </template>
      </el-table-column>
      <el-table-column label="名称" prop="moduleName" show-overflow-tooltip />
      <el-table-column label="路径" prop="path" show-overflow-tooltip>
        <template #default="{ row }">
          <a :href="'/#' + row.path">{{ row.path }}</a>
        </template>
      </el-table-column>
      <el-table-column label="组件" prop="component" show-overflow-tooltip />
      <el-table-column label="状态" prop="hidden" show-overflow-tooltip>
        <template #default="{ row }">
          <el-tag v-if="row.hidden" effect="dark" type="danger">隐藏</el-tag>
          <el-tag v-else effect="dark" type="success">显示</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="排序号" prop="orderIndex" show-overflow-tooltip>
        <template #default="{ row }">
          <div @click="row.editSort = true">
            <label v-if="!row.editSort">
              {{ row.orderIndex }}
            </label>
            <el-input-number
              v-if="row.editSort"
              v-model="row.orderIndex"
              size="small"
              type="Number"
              @change="onChangeSort(row)"
            />
          </div>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['module:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['module:delete'] }"
            link
            type="primary"
            @click="handleDelete(row)"
          >
            删除
          </el-button>
          <el-button link type="primary" @click="handleDrawer(row)">
            功能
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <edit ref="editRef" :opt-parent="list" @fetch-data="fetchData" />
    <el-drawer v-model="opDrawer" title="功能权限">
      <Operation :module="currModule" />
    </el-drawer>
  </div>
</template>

<script>
  import { getList, doDelete, doSort } from '@/api/system/module'
  import Edit from './components/SystemModuleEdit'
  import Operation from './components/ModuleOperation'
  import { Plus } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'SystemModule',
    components: { Edit, Operation },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        list: [],
        listLoading: true,
        opDrawer: false,
        currModule: 2,
      })

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
      const onChangeSort = async (row) => {
        row.editSort = false
        const { msg } = await doSort({ id: row.id, index: row.orderIndex })
        $baseMessage(msg, 'success', 'vab-hey-message-success')
      }
      const fetchData = async () => {
        state.listLoading = true
        const { data } = await getList()
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
        Plus,
        onChangeSort,
      }
    },
  })
</script>
