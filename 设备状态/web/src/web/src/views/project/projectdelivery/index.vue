<template>
  <div class="project-projectdelivery-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="45">
        <el-form
          :inline="true"
          label-width="120px"
          :model="queryForm"
          @submit.prevent
        >
          <el-form-item label="项目名称：">
            <el-input
              v-model.trim="queryForm.projectName"
              clearable
              placeholder="请输入项目编号"
            />
          </el-form-item>
          <el-form-item label="交付单据号：">
            <el-input
              v-model.trim="queryForm.deliveryCode"
              clearable
              placeholder="请输入交付单据号"
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
          v-permissions="{ permission: ['projectdelivery:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['projectdelivery:delete'] }"
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
        label="交付单据号"
        prop="deliveryCode"
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
        prop="projectName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="计划交付日期"
        prop="jhjhDate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="实际交付日期"
        prop="sjjhDate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="计划验收日期"
        prop="jhysDate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="实际验收日期"
        prop="sjysDate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="验收阶段"
        prop="acceptancePhaseName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="是否终验收"
        prop="isZYS"
        show-overflow-tooltip
      >
        <template #default="scope">
          <span v-if="scope.row.isZYS == 1">是</span>
          <span v-if="scope.row.isZYS != 1">否</span>
        </template>
      </el-table-column>
      <el-table-column
        v-permissions="{ permission: ['projectdelivery:annex'] }"
        align="center"
        label="验收凭证"
        prop="acceptanceCertificate"
        show-overflow-tooltip
      >
        <template #default="scope">
          <el-link
            v-if="scope.row.acceptanceCertificate != 0"
            @click="
              downloadFile(
                scope.row.acceptanceCertificateUrl,
                scope.row.fileName,
                scope.row.acceptanceCertificate
              )
            "
          >
            查看
          </el-link>
          <span v-else></span>
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
            v-permissions="{ permission: ['projectdelivery:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['projectdelivery:delete'] }"
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
  import { defineComponent, onMounted, reactive, toRefs } from 'vue'
  import { getList, doDelete } from '@/api/projectdelivery'
  import { getUrl, doDownload } from '@/api/system/uploadFile'
  import Edit from './components/ProjectdeliveryEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'Projectdelivery',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        url: '',
        editRef: null,
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
          console.log(row)
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
      const fetchInsertFileData = async () => {
        state.url = await getUrl()
      }
      const downloadFile = async (FileUrl, FileName, FileId) => {
        fetchInsertFileData()
        // window.open(state.url + '?fileUrl=' + FileUrl + '&fileName=' + FileName)
        await doDownload(
          `${state.url}/File/GetFilePreview`,
          FileUrl,
          FileId,
          FileName
        )
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        list.forEach((item) => {
          item.jhjhDate = changeDate(item.jhjhDate)
          item.sjjhDate = changeDate(item.sjjhDate)
          item.jhysDate = changeDate(item.jhysDate)
          item.sjysDate = changeDate(item.sjysDate)
        })
        state.list = list
        state.total = total
        state.listLoading = false
        console.log(state.list)
      }
      const changeDate = (val) => {
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
      onMounted(() => {
        fetchData()
        fetchInsertFileData()
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
        downloadFile,
        fetchInsertFileData,
        changeDate,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
