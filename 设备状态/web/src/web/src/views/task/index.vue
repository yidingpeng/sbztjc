<template>
  <div class="task-container">
    <vab-query-form>
      <vab-query-form-top-panel>
        <el-form
          :inline="true"
          label-width="100px"
          :model="queryForm"
          @submit.prevent
        >
          <el-form-item label="任务编码">
            <el-input
              v-model="queryForm.taskCode"
              placeholder="请输入任务编码"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item label="任务名称">
            <el-input
              v-model="queryForm.taskName"
              placeholder="请输入任务名称"
              style="width: 200px"
            />
          </el-form-item>
          <!-- <el-form-item v-show="!fold" label="项目类型" prop="projectClassID">
            <el-select
              v-model="form.projectClassID"
              clearable
              filterable
              placeholder="请选择项目类型"
            >
              <el-option
                v-for="(item, index) in ProjectClassOption"
                :key="index"
                :disabled="item.disabled"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="是否里程碑" prop="isMilestone">
            <el-select v-model="form.isMilestone">
              <el-option label="是" value="是" />
              <el-option label="否" value="否" />
            </el-select>
          </el-form-item>
          <el-form-item label="是否关键任务" prop="isKey">
            <el-select v-model="form.isKey">
              <el-option label="是" value="是" />
              <el-option label="否" value="否" />
            </el-select>
          </el-form-item>
          <el-form-item label="是否需审批" prop="isReview">
            <el-select v-model="form.isReview">
              <el-option label="是" value="是" />
              <el-option label="否" value="否" />
            </el-select>
          </el-form-item> -->
          <el-form-item>
            <el-button
              :icon="Search"
              native-type="submit"
              type="primary"
              @click="queryData"
            >
              查询
            </el-button>
            <!-- <el-button link type="primary" @click="handleFold">
              <span v-if="fold">展开</span>
              <span v-else>合并</span>
              <vab-icon
                class="vab-dropdown"
                :class="{ 'vab-dropdown-active': fold }"
                icon="arrow-up-s-line"
              />
            </el-button> -->
          </el-form-item>
        </el-form>
      </vab-query-form-top-panel>
      <vab-query-form-left-panel :span="24">
        <el-button
          v-permissions="{ permission: ['task:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <!-- <el-button :icon="Delete" type="danger" @click="handleDelete">
          批量删除
        </el-button> -->
      </vab-query-form-left-panel>
      <!-- <vab-query-form-right-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.taskCode"
              clearable
              placeholder="请输入任务编码"
            />
          </el-form-item>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.taskName"
              clearable
              placeholder="请输入任务名称"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-right-panel> -->
    </vab-query-form>

    <el-table
      ref="NodeRef"
      v-loading="listLoading"
      :data="list"
      :header-cell-style="{ background: '#c6e2ff' }"
      lazy
      :load="loadSearch"
      row-key="id"
      :row-style="TableRowStyle"
      :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
    >
      <el-table-column
        align="left"
        label="任务名称"
        prop="taskName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="任务编码"
        prop="taskCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目类型"
        prop="projectClassName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="是否里程碑"
        prop="isMilestone"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="是否关键任务"
        prop="isKey"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="是否需评审"
        prop="isReview"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="前置条件"
        prop="inputCondition"
        show-overflow-tooltip
      >
        <template #default="scope">
          <el-link
            v-if="scope.row.inputCondition != ''"
            target="_blank"
            @click="
              downloadFile(
                scope.row.pdfUrl,
                scope.row.filePdfName,
                scope.row.inputCondition
              )
            "
          >
            查看
          </el-link>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="输出文件"
        prop="outPutFile"
        show-overflow-tooltip
      >
        <template #default="scope">
          <el-link
            v-if="scope.row.outPutFile != ''"
            target="_blank"
            @click="
              downloadFile2(
                scope.row.pdfUrl,
                scope.row.fileWordName,
                scope.row.outPutFile
              )
            "
          >
            查看
          </el-link>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="工作内容"
        prop="workMemo"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="排序号"
        prop="seqNo"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="120">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['task:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['task:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['task:move'] }"
            title="上移"
            :underline="false"
            @click="moveUp(row)"
          >
            <vab-icon icon="arrow-up-circle-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['task:move'] }"
            title="下移"
            :underline="false"
            @click="moveDown(row)"
          >
            <vab-icon icon="arrow-down-circle-line" />
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
    <edit ref="editRef" @fetch-data="fetchData" @test="reshNodeTree" />
  </div>
</template>

<script>
  import {
    getList,
    GetByParentList,
    UpdateSeqNo,
    MaxSeqNo,
    MinSeqNo,
    doDelete,
  } from '@/api/task'
  import Edit from './components/TaskEdit'
  import { getUrl, doDownload, doDownload2 } from '@/api/system/uploadFile'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'Task',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        url: '',
        editRef: null,
        NodeRef: null,
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
        maps: new Map(),
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
        if (row.hasChildren) {
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
      const fetchInsertFileData = async () => {
        state.url = await getUrl()
      }
      const downloadFile = async (FileUrl, FileName, fileId) => {
        fetchInsertFileData()
        // window.open(
        //   state.url +
        //     '/File/UpdownFile?fileUrl=' +
        //     FileUrl +
        //     '&fileName=' +
        //     FileName
        // )
        // console.log(
        //   state.url +
        //     '/File/UpdownFile?fileUrl=' +
        //     FileUrl +
        //     '&fileName=' +
        //     FileName
        // )
        await doDownload(
          `${state.url}/File/GetFilePreview`,
          FileUrl,
          fileId,
          FileName
        )
      }
      const downloadFile2 = async (FileUrl, FileName, fileId) => {
        fetchInsertFileData()
        await doDownload2(
          `${state.url}/File/GetFilePreview`,
          FileUrl,
          fileId,
          FileName
        )
      }
      //懒加载
      const loadSearch = async (row, treeNode, resolve) => {
        state.maps.set(row.id, { row, treeNode, resolve }) //将当前选中节点数据存储到maps中
        //console.log(treeNode)
        const ByParentIdList = await GetByParentList({ Id: row.id })
        resolve(ByParentIdList.data)
      }
      //新增修改后重新加载子集数据
      const reshNodeTree = (data) => {
        const { row, treeNode, resolve } = state.maps.get(data.Id) //根据id取出对应的节点数据
        loadSearch(row, treeNode, resolve)
      }
      //上移
      const moveUp = async (row) => {
        const maxNo = await MaxSeqNo({ ParentId: row.parentId })
        if (maxNo.data == row.seqNo) {
          $baseMessage('已经到顶啦!', 'error', 'vab-hey-message-warning')
        } else {
          const { msg } = await UpdateSeqNo({
            Id: row.id,
            parentId: row.parentId,
            Type: 'Up',
          })
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          if (parseInt(row.parentId) > 0) {
            reshNodeTree({ Id: row.parentId })
          } else {
            fetchData()
          }
        }
      }
      //下移
      const moveDown = async (row) => {
        const minNo = await MinSeqNo({ ParentId: row.parentId })
        if (minNo.data == row.seqNo) {
          $baseMessage('已经到底啦!', 'error', 'vab-hey-message-warning')
        } else {
          const { msg } = await UpdateSeqNo({
            Id: row.id,
            parentId: row.parentId,
            Type: 'Down',
          })
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          if (parseInt(row.parentId) > 0) {
            reshNodeTree({ Id: row.parentId })
          } else {
            fetchData()
          }
        }
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
        downloadFile2,
        getUrl,
        loadSearch,
        reshNodeTree,
        moveUp,
        moveDown,
        Delete,
        Plus,
        Search,
      }
    },
    methods: {
      // 加上背景色
      TableRowStyle({ row }) {
        // 注意，这里返回的是一个对象
        const rowBackground = {}
        if (row.parentId == 0) {
          rowBackground.background = '#fdf6ec'
        } else {
          rowBackground.background = '#f0f9eb'
        }
        return rowBackground
      },
    },
  })
</script>
