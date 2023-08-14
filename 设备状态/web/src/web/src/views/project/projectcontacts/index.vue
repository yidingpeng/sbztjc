<template>
  <div class="project-projectcontacts-container">
    <vab-query-form>
      <vab-query-form-top-panel>
        <el-form
          :inline="true"
          label-width="80px"
          :model="queryForm"
          @submit.prevent
        >
          <el-form-item label="项目编号">
            <el-input
              v-model.trim="queryForm.projectCode"
              clearable
              placeholder="请输入项目编号"
            />
          </el-form-item>

          <el-form-item label="项目名称 ">
            <el-input
              v-model.trim="queryForm.pName"
              clearable
              placeholder="请输入项目名称"
            />
          </el-form-item>
          <el-form-item label="成员姓名 ">
            <el-input
              v-model.trim="queryForm.contactsName"
              clearable
              placeholder="请输入团队成员姓名"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-top-panel>
      <vab-query-form-left-panel>
        <el-button
          v-permissions="{ permission: ['projectcontacts:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit($event)"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['projectcontacts:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete($event)"
        >
          批量删除
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>
    <el-table
      v-loading="listLoading"
      border
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column
        v-if="isShow"
        align="center"
        label="id"
        prop="id"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目编号"
        prop="projectCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目名称"
        prop="pName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="成员姓名"
        prop="contactsName"
        show-overflow-tooltip
      />
      <!-- <el-table-column
        align="center"
        label="联系人现任职公司"
        prop="curCompany"
        show-overflow-tooltip
        width="290"
      /> -->
      <el-table-column
        align="center"
        label="部门"
        prop="deptName"
        show-overflow-tooltip
      />
      <!-- <el-table-column
        v-show="false"
        align="center"
        label="角色"
        prop="roles"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="性别"
        prop="sex"
        show-overflow-tooltip
      /> -->
      <el-table-column
        align="center"
        label="成员电话"
        prop="telephone1"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目角色"
        prop="fzbkName"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="70px">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['projectcontacts:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['projectcontacts:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link>
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
    <edit ref="editRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import { defineComponent, onMounted, reactive, toRefs } from 'vue'
  import { getList, doDelete } from '@/api/projectcontacts'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  export default defineComponent({
    name: 'Projectcontacts',
    components: {
      Edit: defineAsyncComponent(() =>
        import('./components/ProjectcontactsEdit')
      ),
    },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        editRef: null,
        isShow: false,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          title: '',
        },
      })

      const setSelectRows = (val) => {
        state.selectRows = val
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
      }
    },
  })
</script>
