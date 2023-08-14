<template>
  <div class="project-delivefile-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="项目编号：">
            <el-input
              v-model.trim="queryForm.projectCode"
              clearable
              placeholder="请输入项目编号"
              :style="{ width: '200px' }"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="项目名称：">
            <el-input
              v-model.trim="queryForm.projectName"
              clearable
              placeholder="请输入项目名称"
              :style="{ width: '200px' }"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="交付单据号：">
            <el-input
              v-model.trim="queryForm.deliveryNo"
              clearable
              placeholder="请输入交付单据号"
              :style="{ width: '200px' }"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="文件类别：">
            <el-select
              v-model="queryForm.deliveryTypeValue"
              clearable
              filterable
              :style="{ width: '200px' }"
            >
              <el-option
                v-for="item in DeliveryTypeOptions"
                :key="item.id"
                :label="item.dictionaryText"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item label="设备编号：">
            <el-input
              v-model.trim="queryForm.deviceCode"
              clearable
              placeholder="请输入设备编号题"
              :style="{ width: '200px' }"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['delivefile:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['delivefile:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="项目编号"
        prop="projectCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目名称"
        prop="projectName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目交付信息单据号"
        prop="deliveryNo"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="设备编号"
        prop="deviceCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="文件类别"
        prop="deliveryTypeText"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="数量"
        prop="qty"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="70">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['delivefile:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['delivefile:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link>
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
    <edit ref="editRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import {
    getList,
    doDelete,
    getDeliFileTypeCode,
  } from '@/api/projectDelivefile'
  import Edit from './components/ProjectDelivefileEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'ProjectDelivefile',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        list: [],
        DeliveryTypeOptions: [],
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

      //项目交付文件类型下拉框
      const DeliveryTypefetchData = async () => {
        const projectContacts = await getDeliFileTypeCode()
        state.DeliveryTypeOptions = projectContacts.data
      }

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
        state.queryForm.deliveryType =
          state.queryForm.deliveryTypeValue === ''
            ? 0
            : state.queryForm.deliveryTypeValue
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
        DeliveryTypefetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        DeliveryTypefetchData,
        queryData,
        fetchData,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
