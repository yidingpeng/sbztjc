<template>
  <div class="project-acceptance-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="项目编号：">
            <el-input
              v-model.trim="queryForm.projectCode"
              clearable
              placeholder="请输入项目编号"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="项目名称：">
            <el-input
              v-model.trim="queryForm.projectName"
              clearable
              placeholder="请输入项目名称"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="设备编号：">
            <el-input
              v-model.trim="queryForm.deviceNo"
              clearable
              placeholder="请输入设备编号"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="验收类别：">
            <el-select
              v-model="queryForm.acceptCategoryValue"
              clearable
              filterable
            >
              <el-option
                v-for="item in GetAcceptCategoryOption"
                :key="item.id"
                :label="item.dictionaryText"
                :value="item.id"
              />
            </el-select>
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
          v-permissions="{ permission: ['acceptance:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['acceptance:delete'] }"
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
      @row-click="tabRowClick"
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
        label="单位业主名称"
        prop="ownerUnitName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="设备编号"
        prop="deviceNo"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="设备名称"
        prop="deviceName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="验收类别"
        prop="acceptCategoryName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="验收日期"
        prop="acceptDate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="验收人员"
        prop="acceptancerName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="数量"
        prop="qty"
        show-overflow-tooltip
      >
        <template #default="scope">
          <span v-if="scope.row.qty == 0"></span>
          <span v-else>{{ scope.row.qty }}</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="竣工日期"
        prop="completedDate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="验收结论"
        prop="acceptResultTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="最终整改完成日期"
        prop="finalAbarbeitungDate"
        show-overflow-tooltip
      />
      <el-table-column
        v-permissions="{ permission: ['acceptance:annex'] }"
        align="center"
        label="签字确认附件"
        prop="signConfirmFile"
        show-overflow-tooltip
      >
        <template #default="scope">
          <el-link v-if="scope.row.signConfirmFile != 0">查看</el-link>
          <span v-else>无附件</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="70">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['acceptance:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['acceptance:delete'] }"
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
  import { getList, doDelete, GetAcceptCategory } from '@/api/projectAcceptance'
  import { getUrl, doDownload } from '@/api/system/uploadFile'
  import Edit from './components/ProjectAcceptanceEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'ProjectAcceptance',
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
        GetAcceptCategoryOption: [],
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          title: '',
        },
      })
      //验收类别
      const GetAcceptCategoryFetch = async () => {
        const GetOptions = await GetAcceptCategory()
        console.log(GetOptions)
        state.GetAcceptCategoryOption = GetOptions.data
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
      const tabRowClick = async (row, column) => {
        console.log(column)
        if (row.signConfirmFile) {
          if (column.no == 13) {
            fetchInsertFileData(row)
          }
        }
      }
      const fetchInsertFileData = async (row) => {
        const result = await getUrl()
        state.url = result
        // window.open(
        //   state.url + '?fileUrl=' + row.signConfirmFileUrl + '&id=' + row.id
        // )
        await doDownload(
          `${state.url}/File/GetFilePreview`,
          row.signConfirmFileUrl,
          row.signConfirmFile,
          row.fileName
        )
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
        state.queryForm.acceptCategory =
          state.queryForm.acceptCategoryValue == ''
            ? 0
            : state.queryForm.acceptCategoryValue
        state.queryForm.pageNo = 1
        fetchData()
      }
      const changeDate = (val) => {
        if (val == '0001-01-01 00:00:00' || val == null) {
          return null
        }
        const newDate = new Date(val)
        const y = newDate.getFullYear()
        const m =
          newDate.getMonth() + 1 < 10
            ? `0${newDate.getMonth() + 1}`
            : newDate.getMonth() + 1
        const d =
          newDate.getDate() < 10 ? `0${newDate.getDate()}` : newDate.getDate()
        return `${y}-${m}-${d}`
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        state.list = list
        state.list.forEach((item) => {
          item.acceptDate = changeDate(item.acceptDate)
          item.completedDate = changeDate(item.completedDate)
          item.finalAbarbeitungDate = changeDate(item.finalAbarbeitungDate)
        })
        state.total = total
        state.listLoading = false
      }
      onMounted(() => {
        fetchData()
        GetAcceptCategoryFetch()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        tabRowClick,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        changeDate,
        GetAcceptCategoryFetch,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
