<template>
  <div class="system-configuration-container">
    <vab-query-form>
      <!-- <vab-query-form-left-panel :span="12">
        <el-button :icon="Delete" type="danger" @click="handleDelete">
          批量删除
        </el-button>
      </vab-query-form-left-panel> -->
      <vab-query-form-right-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.name"
              clearable
              placeholder="请输入关键字"
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
      border
      :data="list"
      @selection-change="setSelectRows"
    >
      <!-- <el-table-column align="center" show-overflow-tooltip type="selection" /> -->
      <el-table-column
        align="center"
        label="id"
        prop="id"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="文件名"
        prop="filename"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="文件类型"
        prop="contentType"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="文件大小"
        prop="fileSize"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          {{ getSize(row.fileSize) }}
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="上传者"
        prop="uploader"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="上传时间"
        prop="uploadTime"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="110">
        <template #default="{ row }">
          <el-button link type="primary" @click="handleDownload(row)">
            下载
          </el-button>
          <el-button link type="primary" @click="handleDelete(row)">
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
    <edit ref="editRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import { getList, doDelete, doDownload } from '@/api/system/upload'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'FileManagement',
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
          pageNo: 1,
          pageSize: 20,
          name: '',
        },
      })

      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleDownload = (row) => {
        //const { uploadURL } = require('@/config')
        //window.open(uploadURL + '/download/' + row.id)
        doDownload(row.id, row.filename)
      }
      const handleDelete = (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete(row.id)
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
      const getSize = (size) => {
        const mb = Math.round((size / 1024 / 1024) * 100) / 100
        const kb = Math.round((size / 1024) * 100) / 100
        const bb = size % 1024
        if (mb > 1) return `${mb}MB`
        else if (kb > 1) return `${kb}KB`
        else return `${bb}byte`
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleDownload,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        getSize,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
